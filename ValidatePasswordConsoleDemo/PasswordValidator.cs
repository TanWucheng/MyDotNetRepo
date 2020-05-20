using System;
using System.Collections.Generic;
using System.Linq;

namespace ValidatePasswordConsoleDemo
{
    /// <summary>
    /// 密码验证
    /// </summary>
    public static class PasswordValidator
    {
        /// <summary>
        /// 简单密码字典
        /// </summary>
        /// <value></value>
        private static readonly string[] SimplePasswordDic = { "password", "abc123", "iloveyou", "adobe123", "123123", "sunshine", "1314520", "a1b2c3", "123qwe", "aaa111", "qweasd", "admin", "passwd" };

        /// <summary>
        /// 检查字符类型
        /// </summary>
        /// <param name="c">字符</param>
        /// <returns></returns>
        private static CharacterType CheckCharacterType(char c)
        {
            if (char.IsNumber(c))
            {
                return CharacterType.Number;
            }
            else if (char.IsLower(c))
            {
                return CharacterType.LowercaseLetter;
            }
            else if (char.IsUpper(c))
            {
                return CharacterType.UppercaseLetter;
            }
            return CharacterType.OtherChar;
        }

        /// <summary>
        /// 判断两个字符是否正序连续
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool CheckIsPositiveContinuous(char a, char b)
        {
            return (short)b - (short)a == 1;
        }

        /// <summary>
        /// 判断两个字符是否倒序连续
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool CheckIsReverseContinuous(char a, char b)
        {
            return (short)a - (short)b == 1;
        }

        /// <summary>
        /// 检查字符串正序或倒序连续字符是否超过指定位数
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="digits">连续位数</param>
        /// <returns></returns>
        public static bool CheckIsCharContinuous(string str, int digits)
        {
            var positiveCount = 0;
            var reverseCount = 0;
            var count = digits - 1;

            var arr = str.ToCharArray();
            for (int i = 0; i < arr.Length - 1; i++)
            {
                positiveCount = CheckIsPositiveContinuous(arr[i], arr[i + 1]) ? positiveCount + 1 : 0;
                reverseCount = CheckIsReverseContinuous(arr[i], arr[i + 1]) ? reverseCount + 1 : 0;
                if (positiveCount >= count || reverseCount >= count)
                {
                    break;
                }
            }
            return positiveCount >= count || reverseCount >= count;
        }

        /// <summary>
        /// 检查密码是否连续三个字符重复
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool CheckIsPasswordRepetitive(string password)
        {
            var arr = password.ToCharArray();
            for (int i = 0; i < arr.Length - 2; i++)
            {
                if (arr[i] == arr[i + 1] && arr[i + 1] == arr[i + 2])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 获取密码包含的字符类型列表
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private static List<CharacterType> GetPasswordTypes(string password)
        {
            List<CharacterType> passwordTypes = new List<CharacterType>();
            foreach (var c in password)
            {
                passwordTypes.Add(CheckCharacterType(c));
            }
            var tempTypes = (from t in passwordTypes group t by t into g select g.Key);
            passwordTypes = tempTypes.ToList();
            return passwordTypes;
        }

        /// <summary>
        /// 获取密码里面包含的字符类型以及每种字符类型的字符数量
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private static Dictionary<CharacterType, int> GetCharTypeCount(string password)
        {
            var numCount = 0;
            var lowercaseCount = 0;
            var uppercaseCount = 0;
            var otherCount = 0;

            foreach (var c in password)
            {
                if (char.IsNumber(c))
                {
                    numCount++;
                }
                else if (char.IsLower(c))
                {
                    lowercaseCount++;
                }
                else if (char.IsUpper(c))
                {
                    uppercaseCount++;
                }
                else
                {
                    otherCount++;
                }
            }
            var dic = new Dictionary<CharacterType, int>();
            if (numCount > 0)
            {
                dic.Add(CharacterType.Number, numCount);
            }
            if (lowercaseCount > 0)
            {
                dic.Add(CharacterType.LowercaseLetter, lowercaseCount);
            }
            if (uppercaseCount > 0)
            {
                dic.Add(CharacterType.UppercaseLetter, uppercaseCount);
            }
            if (otherCount > 0)
            {
                dic.Add(CharacterType.OtherChar, otherCount);
            }
            return dic;
        }

        /// <summary>
        /// 检查密码里面包含每种字符类型的字符数量是否大于等于指定数量
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="overCount"></param>
        /// <returns></returns>
        private static bool CheckIsCharacterCountOverThan(Dictionary<CharacterType, int> dic, int overCount)
        {
            var keys = dic.Keys;
            foreach (var key in keys)
            {
                if (dic[key] < overCount)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 判断字符串是否由相同字符组成
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsStringHasSameChar(string str)
        {
            var count = (from c in str.ToCharArray() group c by c into g select g.Key).Count();
            return count == 1;
        }

        /// <summary>
        /// 获取字典项的值
        /// 含有指定key的项则返回项的值
        /// 未包含指定key的项则返回0
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int TryGetDictionaryValue(Dictionary<CharacterType, int> dic, CharacterType key)
        {
            if (dic.ContainsKey(key))
            {
                return dic[key];
            }
            return 0;
        }

        /// <summary>
        /// 密码等级上升
        /// </summary>
        /// <param name="password"></param>
        /// <param name="level"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static int InCreasePasswordLevel(string password, int level, Dictionary<CharacterType, int> dic)
        {
            var keysCount = dic.Keys.Count;
            var passwordLength = password.Length;
            level += keysCount;

            //长度大于4并且包含2种字符类型
            if (passwordLength > 4 && keysCount == 2)
            {
                level++;
            }
            //长度大于6并且包含3种字符类型
            else if (passwordLength > 6 && keysCount == 3)
            {
                level += 2;
            }
            //长度大于8并且包含4种字符类型
            else if (passwordLength > 8 && keysCount == 4)
            {
                level += 3;
            }

            var numLetterCount = TryGetDictionaryValue(dic, CharacterType.Number);
            var lowercaseCount = TryGetDictionaryValue(dic, CharacterType.LowercaseLetter);
            var uppercaseCount = TryGetDictionaryValue(dic, CharacterType.UppercaseLetter);
            var otherCharCount = TryGetDictionaryValue(dic, CharacterType.OtherChar);
            //密码长度大于6并且2种类型组合每种类型长度大于等于3或者2
            if (passwordLength > 6 && numLetterCount >= 3 && lowercaseCount >= 3
                || numLetterCount >= 3 && uppercaseCount >= 3
                || numLetterCount >= 3 && otherCharCount >= 2
                || lowercaseCount >= 3 && uppercaseCount >= 3
                || lowercaseCount >= 3 && otherCharCount >= 2
                || uppercaseCount >= 3 && otherCharCount >= 2)
            {
                level++;
            }
            //密码长度大于8并且3种类型组合每种类型长度大于等于2
            if (passwordLength > 8 && numLetterCount >= 2 && lowercaseCount >= 2 && uppercaseCount >= 2
            || numLetterCount >= 2 && lowercaseCount >= 2 && otherCharCount >= 2
            || numLetterCount >= 2 && uppercaseCount >= 2 && otherCharCount >= 2
            || lowercaseCount >= 2 && uppercaseCount >= 2 && otherCharCount >= 2)
            {
                level++;
            }
            //密码长度大于10并且4种类型组合每种类型长度大于等于2
            if (passwordLength > 10 && numLetterCount >= 2 && lowercaseCount >= 2 && uppercaseCount >= 2 && otherCharCount >= 2)
            {
                level++;
            }

            //6>特殊字符>=3 level+=1;
            if (otherCharCount >= 3 && otherCharCount < 6)
            {
                level++;
            }
            //6=<特殊字符<=12 level+=2;
            else if (otherCharCount >= 6 && otherCharCount <= 12)
            {
                level += 2;
            }
            //12<特殊字符<16 level+=3;
            else if (otherCharCount > 12 && otherCharCount < 16)
            {
                level += 3;
            }
            //16=<特殊字符 level+=4;
            else if (otherCharCount >= 16)
            {
                level += 4;
            }

            return level;
        }

        /// <summary>
        /// 密码等级下降
        /// </summary>
        /// <param name="password"></param>
        /// <param name="dic"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public static int DecreasePasswordLevel(string password, Dictionary<CharacterType, int> dic, int level)
        {
            var keysCount = dic.Keys.Count;
            var passwordLength = password.Length;
            //是顺序字母表的一部分
            if ("abcdefghijklmnopqrstuvwxyz".IndexOf(password) > 0 || "ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(password) > 0)
            {
                level--;
            }
            //是键盘字母布局的一部分
            if ("qwertyuiop".IndexOf(password) > 0 || "asdfghjkl".IndexOf(password) > 0 || "zxcvbnm".IndexOf(password) > 0)
            {
                level--;
            }
            //是顺序数字的一部分
            if (keysCount == 1 && dic.ContainsKey(CharacterType.Number) && ("01234567890".IndexOf(password) > 0 || "09876543210".IndexOf(password) > 0))
            {
                level--;
            }
            //全由小写或全由大写字母或全由数字组成
            if (keysCount == 1 && (dic.ContainsKey(CharacterType.LowercaseLetter) || dic.ContainsKey(CharacterType.UppercaseLetter) || dic.ContainsKey(CharacterType.Number)))
            {
                level--;
            }
            //密码长度为偶数，并且从中间分隔后两部分相等-1，或者两部分各由同一个字符组成再-1
            if (passwordLength % 2 == 0)
            {
                var partLength = passwordLength / 2;
                var part1 = password.Substring(0, partLength);
                var part2 = password.Substring(partLength);
                if (string.Equals(part1, part2))
                {
                    level--;
                }
                if (IsStringHasSameChar(part1) && IsStringHasSameChar(part2))
                {
                    level--;
                }
            }
            //如果密码能均分为三部分，而且三部分相等则-1
            if (passwordLength % 3 == 0)
            {
                var partLength = passwordLength / 3;
                var part1 = password.Substring(0, partLength);
                var part2 = password.Substring(partLength, partLength * 2);
                var part3 = password.Substring(partLength * 2);
                if (string.Equals(part1, part2) && string.Equals(part2, part3))
                {
                    level--;
                }
            }
            //如果是数字，并且符合生日格式则-1
            if (keysCount == 1 && dic.ContainsKey(CharacterType.Number) && passwordLength >= 6)
            {
                var year = 0;
                if (passwordLength == 8 || passwordLength == 6)
                {
                    int.TryParse(password.Substring(0, passwordLength - 4), out year);
                }
                var yearBits = passwordLength == 8 ? 4 : 2;
                var month = 0;
                int.TryParse(password.Substring(yearBits, 2), out month);
                var date = 0;
                int.TryParse(password.Substring(yearBits + 2), out date);
                if (year >= 1950 && year < 2050 && month >= 1 && month <= 12 && date >= 1 && date <= 31)
                {
                    level--;
                }
            }
            //如果是简单密码，则-1
            foreach (var item in SimplePasswordDic)
            {
                if (string.Equals(item, password) || item.IndexOf(password) >= 0)
                {
                    level--;
                    break;
                }
            }
            //随长度逐级减弱
            if (passwordLength <= 6)
            {
                level--;
                if (passwordLength <= 4)
                {
                    level--;
                    if (passwordLength <= 3)
                    {
                        level = 0;
                    }
                }
            }
            //如果全是重复字符，则为0
            if (IsStringHasSameChar(password))
            {
                level = 0;
            }
            //如果最后level<0则为0
            level = level < 0 ? 0 : level;

            return level;
        }

        /// <summary>
        /// 获取密码强度等级
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static int GetPasswordStrengthLevel(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("密码为空(password is empty)");
            }

            int level = 0;
            var dic = GetCharTypeCount(password);

            level = InCreasePasswordLevel(password, level, dic);
            level = DecreasePasswordLevel(password, dic, level);

            return level;
        }

        /// <summary>
        /// 检查密码是否符合校验规则
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static bool CheckIsPasswordValid(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("密码为空(password is empty)");
            }
            if (password.Length < 8)
            {
                throw new ArgumentException("At least 8 bits in length(长度至少8位)");
            }

            var passwordTypes = GetPasswordTypes(password);
            if ((from t in passwordTypes where t == CharacterType.Number select t).Count() < 1)
            {
                throw new Exception("Password must contain numbers(密码必须含有数字)");
            }
            if ((from t in passwordTypes where t == CharacterType.LowercaseLetter select t).Count() < 1)
            {
                throw new Exception("Password must contain lowercase letters(密码必须含有小写字母)");
            }
            if ((from t in passwordTypes where t == CharacterType.UppercaseLetter select t).Count() < 1)
            {
                throw new Exception("Password must contain uppercase letters(密码必须含有大写字母)");
            }
            if ((from t in passwordTypes where t == CharacterType.OtherChar select t).Count() < 1)
            {
                throw new Exception("Password must contain special characters(密码必须含有特殊字符)");
            }

            var isPwdContinuous = CheckIsCharContinuous(password, 3);
            if (isPwdContinuous)
            {
                throw new Exception("Cannot have three consecutive numbers or letters, for example 123 ABC(不能有三位连续的数字或者字母，例如123 abc)");
            }

            var isPwdRepetitive = CheckIsPasswordRepetitive(password);
            if (isPwdRepetitive)
            {
                throw new Exception("Cannot have more than three consecutive alphanumeric characters, such as AAA 111(不能有连续三次以上重复的数字字母字符，例如AAA 111)");
            }

            if (SimplePasswordDic.Contains(password))
            {
                throw new Exception("Can't have common words(不能有常用的单词)");
            }

            var userNameLength = userName.Length;
            var userNameList = new List<string>();
            for (int i = 0; i < userNameLength - 2; i++)
            {
                userNameList.Add(userName.Substring(i, 3));
            }
            foreach (var item in userNameList)
            {
                if (password.ToLower().IndexOf(item) != -1)
                {
                    throw new Exception("You cannot use content that contains bytes of your name as part of your password(不能使用包含你名字部分字节的内容作为密码的组成部分)");
                }
            }

            var newPassword = password.Replace("0", "o").Replace("1", "l").Replace("$", "s").Replace("@", "a");
            if (SimplePasswordDic.Contains(newPassword))
            {
                throw new Exception("Can't have common words(不能有常用的单词)");
            }

            return true;
        }

        /// <summary>
        /// 获取密码强度等级
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static PasswordStrengthLevel GetPasswordLevel(String password)
        {
            int level = GetPasswordStrengthLevel(password);
            switch (level)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    return PasswordStrengthLevel.Easy;
                case 4:
                case 5:
                case 6:
                    return PasswordStrengthLevel.Medium;
                case 7:
                case 8:
                case 9:
                    return PasswordStrengthLevel.Strong;
                case 10:
                case 11:
                case 12:
                    return PasswordStrengthLevel.VeryStrong;
                default:
                    return PasswordStrengthLevel.ExtremelyStrong;
            }
        }
    }
}