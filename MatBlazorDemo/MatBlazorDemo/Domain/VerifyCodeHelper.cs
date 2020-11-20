using System;
using System.DrawingCore;
using System.DrawingCore.Drawing2D;
using System.DrawingCore.Imaging;
using System.IO;

namespace MatBlazorDemo.Domain
{
    public sealed class VerifyCodeHelper
    {
        #region 单例模式

        //创建私有化静态obj锁 
        private static readonly object ObjLock = new object();

        //创建私有静态字段，接收类的实例化对象 
        private static VerifyCodeHelper _verifyCodeHelper;

        //构造函数私有化 
        private VerifyCodeHelper()
        {
        }

        /// <summary>
        /// 创建单利对象资源并返回
        /// </summary>
        /// <returns></returns>
        public static VerifyCodeHelper GetSingleObj()
        {
            if (_verifyCodeHelper != null) return _verifyCodeHelper;
            lock (ObjLock)
            {
                _verifyCodeHelper ??= new VerifyCodeHelper();
            }

            return _verifyCodeHelper;
        }

        #endregion

        #region 生产验证码

        public enum VerifyCodeType
        {
            NumberVerifyCode,
            AbcVerifyCode,
            MixVerifyCode
        }

        /// <summary>
        ///     1.数字验证码
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private static string CreateNumberVerifyCode(int length)
        {
            var randMembers = new int[length];
            var validateNumbs = new int[length];
            var validateNumberStr = "";
            //生成起始序列值 
            var seekSeek = unchecked((int)DateTime.Now.Ticks);
            var seekRand = new Random(seekSeek);
            var beginSeek = seekRand.Next(0, int.MaxValue - length * 10000);
            var seeks = new int[length];
            for (var i = 0; i < length; i++)
            {
                beginSeek += 10000;
                seeks[i] = beginSeek;
            }

            //生成随机数字 
            for (var i = 0; i < length; i++)
            {
                var rand = new Random(seeks[i]);
                var powNum = 1 * (int)Math.Pow(10, length);
                randMembers[i] = rand.Next(powNum, int.MaxValue);
            }

            //抽取随机数字 
            for (var i = 0; i < length; i++)
            {
                var numStr = randMembers[i].ToString();
                var numLength = numStr.Length;
                var rand = new Random();
                var numPosition = rand.Next(0, numLength - 1);
                validateNumbs[i] = int.Parse(numStr.Substring(numPosition, 1));
            }

            //生成验证码 
            for (var i = 0; i < length; i++) validateNumberStr += validateNumbs[i].ToString();
            return validateNumberStr;
        }

        /// <summary>
        ///     2.字母验证码
        /// </summary>
        /// <param name="length">字符长度</param>
        /// <returns>验证码字符</returns>
        private static string CreateAbcVerifyCode(int length)
        {
            var verification = new char[length];
            char[] dictionary =
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U',
                'V', 'W', 'X', 'Y', 'Z',
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u',
                'v', 'w', 'x', 'y', 'z'
            };
            var random = new Random();
            for (var i = 0; i < length; i++) verification[i] = dictionary[random.Next(dictionary.Length - 1)];
            return new string(verification);
        }

        /// <summary>
        ///     3.混合验证码
        /// </summary>
        /// <param name="length">字符长度</param>
        /// <returns>验证码字符</returns>
        private static string CreateMixVerifyCode(int length)
        {
            var verification = new char[length];
            char[] dictionary =
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U',
                'V', 'W', 'X', 'Y', 'Z',
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u',
                'v', 'w', 'x', 'y', 'z'
            };
            var random = new Random();
            for (var i = 0; i < length; i++) verification[i] = dictionary[random.Next(dictionary.Length - 1)];
            return new string(verification);
        }

        /// <summary>
        ///     产生验证码（随机产生4-6位）
        /// </summary>
        /// <param name="type">验证码类型：数字，字符，符合</param>
        /// <returns></returns>
        public string CreateVerifyCode(VerifyCodeType type)
        {
            var verifyCode = string.Empty;
            var random = new Random();
            var length = random.Next(4, 6);
            verifyCode = type switch
            {
                VerifyCodeType.NumberVerifyCode => CreateNumberVerifyCode(length),
                VerifyCodeType.AbcVerifyCode => CreateAbcVerifyCode(length),
                VerifyCodeType.MixVerifyCode => CreateMixVerifyCode(length),
                _ => verifyCode
            };

            return verifyCode;
        }

        /// <summary>
        ///     产生验证码（指定长度）
        /// </summary>
        /// <param name="type">验证码类型：数字，字符，符合</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public string CreateVerifyCode(VerifyCodeType type, int length)
        {
            var verifyCode = string.Empty;
            // var random = new Random();
            verifyCode = type switch
            {
                VerifyCodeType.NumberVerifyCode => CreateNumberVerifyCode(length),
                VerifyCodeType.AbcVerifyCode => CreateAbcVerifyCode(length),
                VerifyCodeType.MixVerifyCode => CreateMixVerifyCode(length),
                _ => verifyCode
            };

            return verifyCode;
        }

        #endregion

        #region 验证码图片

        /// <summary>
        ///     验证码图片 => Bitmap
        /// </summary>
        /// <param name="verifyCode">验证码</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <returns>Bitmap</returns>
        public Bitmap CreateBitmapByImgVerifyCode(string verifyCode, int width, int height)
        {
            var font = new Font("Arial", 14, FontStyle.Bold | FontStyle.Italic);
            var bitmap = new Bitmap(width, height);
            var g = Graphics.FromImage(bitmap);
            var totalSizeF = g.MeasureString(verifyCode, font);
            var startPointF = new PointF(0, (height - totalSizeF.Height) / 2);
            var random = new Random(); //随机数产生器
            g.Clear(Color.White); //清空图片背景色 
            foreach (var code in verifyCode)
            {
                Brush brush = new LinearGradientBrush(new Point(0, 0), new Point(1, 1),
                    Color.FromArgb(random.Next(255), random.Next(255), random.Next(255)),
                    Color.FromArgb(random.Next(255), random.Next(255), random.Next(255)));
                g.DrawString(code.ToString(), font, brush, startPointF);
                var curCharSizeF = g.MeasureString(code.ToString(), font);
                startPointF.X += curCharSizeF.Width;
            }

            //画图片的干扰线 
            for (var i = 0; i < 10; i++)
            {
                var x1 = random.Next(bitmap.Width);
                var x2 = random.Next(bitmap.Width);
                var y1 = random.Next(bitmap.Height);
                var y2 = random.Next(bitmap.Height);
                g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
            }

            //画图片的前景干扰点 
            for (var i = 0; i < 100; i++)
            {
                var x = random.Next(bitmap.Width);
                var y = random.Next(bitmap.Height);
                bitmap.SetPixel(x, y, Color.FromArgb(random.Next()));
            }

            g.DrawRectangle(new Pen(Color.Silver), 0, 0, bitmap.Width - 1, bitmap.Height - 1); //画图片的边框线 
            g.Dispose();
            return bitmap;
        }

        /// <summary>
        ///     验证码图片 => byte[]
        /// </summary>
        /// <param name="verifyCode">验证码</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <returns>byte[]</returns>
        public byte[] CreateByteByImgVerifyCode(string verifyCode, int width, int height)
        {
            var font = new Font("Arial", 14, FontStyle.Bold | FontStyle.Italic);
            var bitmap = new Bitmap(width, height);
            var g = Graphics.FromImage(bitmap);
            var totalSizeF = g.MeasureString(verifyCode, font);
            var startPointF = new PointF(0, (height - totalSizeF.Height) / 2);
            var random = new Random(); //随机数产生器
            g.Clear(Color.White); //清空图片背景色 
            foreach (var code in verifyCode)
            {
                Brush brush = new LinearGradientBrush(new Point(0, 0), new Point(1, 1),
                    Color.FromArgb(random.Next(255), random.Next(255), random.Next(255)),
                    Color.FromArgb(random.Next(255), random.Next(255), random.Next(255)));
                g.DrawString(code.ToString(), font, brush, startPointF);
                var curCharSizeF = g.MeasureString(code.ToString(), font);
                startPointF.X += curCharSizeF.Width;
            }

            //画图片的干扰线 
            for (var i = 0; i < 10; i++)
            {
                var x1 = random.Next(bitmap.Width);
                var x2 = random.Next(bitmap.Width);
                var y1 = random.Next(bitmap.Height);
                var y2 = random.Next(bitmap.Height);
                g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
            }

            //画图片的前景干扰点 
            for (var i = 0; i < 100; i++)
            {
                var x = random.Next(bitmap.Width);
                var y = random.Next(bitmap.Height);
                bitmap.SetPixel(x, y, Color.FromArgb(random.Next()));
            }

            g.DrawRectangle(new Pen(Color.Silver), 0, 0, bitmap.Width - 1, bitmap.Height - 1); //画图片的边框线 
            g.Dispose();

            //保存图片数据 
            var stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Jpeg);
            //输出图片流 
            return stream.ToArray();
        }

        #endregion
    }
}
