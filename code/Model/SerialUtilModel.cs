using DLT645.BasicMVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLT645.Model
{
    internal class SerialUtilModel : ObservableObject
    {
        public static byte[] HexStrToHexArr(string HexStr)
        {
            if (HexStr == null)
                return null;
            HexStr = HexStr.Replace(" ", "");
            HexStr = HexStr.Replace(".", "");
            HexStr = HexStr.Replace("\r", "");
            HexStr = HexStr.Replace("\n", "");

            if ((HexStr.Length % 2) != 0)
            {
                //if (HexStr.Length == 1)
                {
                    HexStr = "0" + HexStr;
                }
            }
            byte[] HexArr = new byte[1];
            try
            {
                return Convert.FromHexString(HexStr);
            }
            catch
            {
                return HexArr;
            }
        }

        public static string HexArrToHexStr(byte[] HexArr)
        {
            return Convert.ToHexString(HexArr);
        }

        public static byte[] ReverseHexArr(byte[] HexArr)
        {
            byte[] ReverseHexArr = new byte[HexArr.Length];
            for (int i = 0; i < HexArr.Length; i++)
            {
                ReverseHexArr[i] = HexArr[HexArr.Length - 1 - i];
            }
            return ReverseHexArr;
        }

        public static string HexArrToHexStr(byte[] HexArr, string insert)
        {
            string str = "";
            byte[] Hex = new byte[1];
            for (int i = 0; i < HexArr.Length; i++)
            {
                Hex[0] = HexArr[i];
                str += Convert.ToHexString(Hex) + insert;
            }
            return str;
        }

        public static string HexStrToHexStr(String HexStr, string insert)
        {
            if (HexStr == null)
                return null;
            HexStr = HexStr.Replace(" ", "");
            HexStr = HexStr.Replace(".", "");
            HexStr = HexStr.Replace("\r", "");
            HexStr = HexStr.Replace("\n", "");

            string str = "";
            byte[] Hex = new byte[1];
            try
            {
                byte[] HexArr = Convert.FromHexString(HexStr);
                for (int i = 0; i < HexArr.Length; i++)
                {
                    Hex[0] = HexArr[i];
                    str += Convert.ToHexString(Hex) + insert;
                }
            }
            catch
            {

            }
            return str;
        }

        public static string HexStrToHexStrReversed(String HexStr, string insert)
        {
            if (HexStr == null)
                return null;
            HexStr = HexStr.Replace(" ", "");
            HexStr = HexStr.Replace(".", "");
            HexStr = HexStr.Replace("\r", "");
            HexStr = HexStr.Replace("\n", "");

            string str = "";
            byte[] Hex = new byte[1];
            try
            {
                byte[] HexArr = Convert.FromHexString(HexStr);
                for (int i = 0; i < HexArr.Length; i++)
                {
                    Hex[0] = HexArr[HexArr.Length - 1 - i];
                    str += Convert.ToHexString(Hex) + insert;
                }
            }
            catch
            {

            }
            return str;
        }

        public static string StringFormat(string str)
        {
            if (str == null)
            {
                return null;
            }
            str = str.Replace(" ", "");
            str = str.Replace(".", "");
            str = str.Replace("\r", "");
            str = str.Replace("\n", "");
            return str;
        }

        public static byte CheckSum08(byte[] HexArr)
        {
            byte sum = 0;
            for (int i = 0; i < HexArr.Length; i++)
            {
                sum += HexArr[i];
            }
            return sum;
        }

        /*! Decimal to BCD */
        public static byte DEC2BCD(byte x)
        {
            return (byte)((((x) / 10) << 4) + ((x) % 10));
        }

        /*! BCD to decimal */
        public static byte BCD2DEC(byte x)
        {
            return (byte)((((x) >> 4) * 10u) + ((x) & 0x0F));
        }
    }
}
