using BlazorPlus;
using MatBlazorDemo.Domain;

namespace MatBlazorDemo.Services
{
    public class VerifyCodeService
    {
        /// <summary>
        ///     数字验证码
        /// </summary>
        /// <returns></returns>
        public byte[] NumberVerifyCode()
        {
            var code = VerifyCodeHelper.GetSingleObj()
                .CreateVerifyCode(VerifyCodeHelper.VerifyCodeType.MixVerifyCode, 4);
            var codeImage = VerifyCodeHelper.GetSingleObj().CreateByteByImgVerifyCode(code, 100, 24);

            BlazorSession.Current["VerifyCode"] = code;

            return codeImage;
        }
    }
}
