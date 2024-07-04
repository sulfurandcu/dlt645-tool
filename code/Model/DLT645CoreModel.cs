using DLT645.BasicMVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlTypes;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DLT645.Model
{
    internal class DLT645CoreModel : ObservableObject
    {
        public DLT645UserModel DLT645UserModel { get; set; }

        public DLT645CoreModel()
        {
            DLT645UserModel = new DLT645UserModel();
            DLT645UserModel.InitializeComponent();
        }

        private string _CandidateAddr = String.Empty;
        public string CandidateAddr
        {
            get
            {
                return _CandidateAddr;
            }
            set
            {
                if (_CandidateAddr != value)
                {
                    _CandidateAddr = value;
                    RaisePropertyChanged(nameof(CandidateAddr));
                }
            }
        }

        private string _SendFramePrmb = String.Empty;
        public string SendFramePrmb
        {
            get
            {
                return _SendFramePrmb;
            }
            set
            {
                if (_SendFramePrmb != value)
                {
                    _SendFramePrmb = value;
                    RaisePropertyChanged(nameof(SendFramePrmb));
                    if (dlt645JsonConfigRoot != null)
                    {
                        if (dlt645JsonConfigRoot?["ViewModel.LinkLayer.Preamble"] != null)
                        {
                            dlt645JsonConfigRoot?["ViewModel.LinkLayer.Preamble"].ReplaceWith<string>(value);
                            File.WriteAllText(dlt645JsonConfigFile, dlt645JsonConfigRoot.ToString());
                        }
                    }
                }
            }
        }

        private string _SendFrameAddr = String.Empty;
        public string SendFrameAddr
        {
            get
            {
                return _SendFrameAddr;
            }
            set
            {
                if (_SendFrameAddr != value)
                {
                    _SendFrameAddr = value;
                    RaisePropertyChanged(nameof(SendFrameAddr));
                }
            }
        }

        private string _SendFrameCtrl = String.Empty;
        public string SendFrameCtrl
        {
            get
            {
                return _SendFrameCtrl;
            }
            set
            {
                if (_SendFrameCtrl != value)
                {
                    _SendFrameCtrl = value;
                    RaisePropertyChanged(nameof(SendFrameCtrl));
                }
            }
        }

        private string _SendFrameSize = String.Empty;
        public string SendFrameSize
        {
            get
            {
                return _SendFrameSize;
            }
            set
            {
                if (_SendFrameSize != value)
                {
                    _SendFrameSize = value;
                    RaisePropertyChanged(nameof(SendFrameSize));
                }
            }
        }

        private string _SendFrameData = String.Empty;
        public string SendFrameData
        {
            get
            {
                return _SendFrameData;
            }
            set
            {
                if (_SendFrameData != value)
                {
                    _SendFrameData = value;
                    RaisePropertyChanged(nameof(SendFrameData));
                }
            }
        }

        private string _SendFrameSum8 = String.Empty;
        public string SendFrameSum8
        {
            get
            {
                return _SendFrameSum8;
            }
            set
            {
                if (_SendFrameSum8 != value)
                {
                    _SendFrameSum8 = value;
                    RaisePropertyChanged(nameof(SendFrameSum8));
                }
            }
        }

        private string _SendFrameInfo = String.Empty;
        public string SendFrameInfo
        {
            get
            {
                return _SendFrameInfo;
            }
            set
            {
                if (_SendFrameInfo != value)
                {
                    _SendFrameInfo = value;
                    RaisePropertyChanged(nameof(SendFrameInfo));
                }
            }
        }

        public string Sub33H(string str)
        {
            byte[] HexArr = SerialUtilModel.HexStrToHexArr(str);

            if (HexArr.Length > 10)
            {
                for (int i = 10; i < HexArr.Length - 2; ++i)
                {
                    HexArr[i] -= 0x33;
                }
                str = SerialUtilModel.HexArrToHexStr(HexArr);
                str = SerialUtilModel.HexStrToHexStr(str, " ");
                return str;
            }

            return null;
        }

        public string Sub33H_all(string str)
        {
            byte[] HexArr = SerialUtilModel.HexStrToHexArr(str);
            if (HexArr.Length < 1)
            {
                return null;
            }

            for (int i = 0; i < HexArr.Length; ++i)
            {
                HexArr[i] -= 0x33;
            }
            str = SerialUtilModel.HexArrToHexStr(HexArr);
            str = SerialUtilModel.HexStrToHexStr(str, " ");
            return str;
        }

        public string Asmb()
        {
            try
            {
                byte[] HexArr = SerialUtilModel.HexStrToHexArr(SendFrameData);
                for (int i = 0; i < HexArr.Length; i++)
                {
                    HexArr[i] += 0x33;
                }
                string len = HexArr.Length.ToString("X2");
                string dat = SerialUtilModel.HexArrToHexStr(HexArr);

                string sta = SerialUtilModel.StringFormat("68" + SendFrameAddr + "68" + SendFrameCtrl + len + dat);
                string sum = SerialUtilModel.CheckSum08(SerialUtilModel.HexStrToHexArr(sta)).ToString("X2");
                string preamble = SendFramePrmb;
                if (preamble.Replace(" ", "").Length % 2 != 0)
                {
                    preamble = preamble.Substring(0, preamble.Length-1);
                }
                string str = SerialUtilModel.StringFormat(preamble + sta + sum + "16");
                return str;
            }
            catch
            {
                return null;
            }
        }

        public string Asmb_Manual()
        {
            return Asmb();
        }

        public string Asmb_ReadAddr()
        {
            SendFrameAddr = "AA AA AA AA AA AA";
            SendFrameCtrl = "13";
            SendFrameData = "";
            return Asmb();
        }

        public string Asmb_WritAddr()
        {
            SendFrameAddr = "AA AA AA AA AA AA";
            SendFrameCtrl = "15";
            SendFrameData = SerialUtilModel.HexStrToHexStrReversed(CandidateAddr, "");
            return Asmb();
        }

        public string Asmb_ReadData(string strDataId, string strData)
        {
            SendFrameAddr = SerialUtilModel.HexStrToHexStrReversed(CandidateAddr, "");
            SendFrameCtrl = "11";
            SendFrameData = SerialUtilModel.HexStrToHexStrReversed(strDataId, "") + SerialUtilModel.HexStrToHexStrReversed(strData, "");
            return Asmb();
        }

        public string Asmb_WritData(string strDataId, string strPassword, string strOperator, string strData)
        {
            SendFrameAddr = SerialUtilModel.HexStrToHexStrReversed(CandidateAddr, "");
            SendFrameCtrl = "14";
            SendFrameData = SerialUtilModel.HexStrToHexStrReversed(strDataId, "") + SerialUtilModel.HexStrToHexStrReversed(strPassword, "") + SerialUtilModel.HexStrToHexStrReversed(strOperator, "") + SerialUtilModel.HexStrToHexStrReversed(strData, "");
            return Asmb();
        }

        public string Asmb_TimeCali(DateTime DateTimeVal, bool isGivenTime, bool isGivenAddr, bool isMilliSecond)
        {
            byte[] HexArr = new byte[6];
            int i = 0;
            DateTime timenow;
            SendFrameCtrl = "08";
            if (isGivenTime)
            {
                timenow = DateTimeVal;
            }
            else
            {
                timenow = DateTime.Now;
            }
            if (isMilliSecond)
            {
                SendFrameCtrl = "1E";
                HexArr = new byte[8];
                if (isGivenTime)
                {
                    HexArr[i++] = 0x00;
                    HexArr[i++] = 0x00;
                }
                else
                {
                    HexArr[i++] = SerialUtilModel.DEC2BCD((byte)(timenow.Millisecond % 100));
                    HexArr[i++] = SerialUtilModel.DEC2BCD((byte)(timenow.Millisecond / 100));
                }
            }
            HexArr[i++] = SerialUtilModel.DEC2BCD((byte)(timenow.Second));
            HexArr[i++] = SerialUtilModel.DEC2BCD((byte)(timenow.Minute));
            HexArr[i++] = SerialUtilModel.DEC2BCD((byte)(timenow.Hour));
            HexArr[i++] = SerialUtilModel.DEC2BCD((byte)(timenow.Day));
            HexArr[i++] = SerialUtilModel.DEC2BCD((byte)(timenow.Month));
            HexArr[i++] = SerialUtilModel.DEC2BCD((byte)(timenow.Year % 100));
            string str = SerialUtilModel.HexArrToHexStr(HexArr);
            if (isGivenAddr)
            {
                SendFrameAddr = SerialUtilModel.HexStrToHexStrReversed(CandidateAddr, "");
            }
            else
            {
                SendFrameAddr = "99 99 99 99 99 99";
            }
            SendFrameData = str;
            return Asmb();
        }

        public string Asmb_TimeCali_UsrDateTime(DateTime DateTimeVal, bool isBroadCast, bool isMilliSecond)
        {
            byte[] HexArr = new byte[6];
            int i = 0;
            SendFrameCtrl = "08";
            if (isMilliSecond)
            {
                SendFrameCtrl = "1E";
                HexArr = new byte[8];
                HexArr[i++] = 0x00;
                HexArr[i++] = 0x00;
            }
            HexArr[i++] = SerialUtilModel.DEC2BCD((byte)(DateTimeVal.Second));
            HexArr[i++] = SerialUtilModel.DEC2BCD((byte)(DateTimeVal.Minute));
            HexArr[i++] = SerialUtilModel.DEC2BCD((byte)(DateTimeVal.Hour));
            HexArr[i++] = SerialUtilModel.DEC2BCD((byte)(DateTimeVal.Day));
            HexArr[i++] = SerialUtilModel.DEC2BCD((byte)(DateTimeVal.Month));
            HexArr[i++] = SerialUtilModel.DEC2BCD((byte)(DateTimeVal.Year % 100));
            string str = SerialUtilModel.HexArrToHexStr(HexArr);
            if (isBroadCast)
            {
                SendFrameAddr = "99 99 99 99 99 99";
            }
            else
            {
                SendFrameAddr = SerialUtilModel.HexStrToHexStrReversed(CandidateAddr, "");
            }
            SendFrameData = str;
            return Asmb();
        }

        public string Asmb_DataCtrl()
        {
            SendFrameAddr = SerialUtilModel.HexStrToHexStrReversed(CandidateAddr, "");
            SendFrameCtrl = "1C";
            SendFrameData = "";
            return Asmb();
        }

        public string Asmb_DataForz(string time4byte)
        {
            SendFrameAddr = SerialUtilModel.HexStrToHexStrReversed(CandidateAddr, "");
            SendFrameCtrl = "16";
            SendFrameData = SerialUtilModel.HexStrToHexStrReversed(time4byte, "");
            return Asmb();
        }

        public string Asmb_DataForz_Broadcast(string time4byte)
        {
            SendFrameAddr = "99 99 99 99 99 99";
            SendFrameCtrl = "16";
            SendFrameData = SerialUtilModel.HexStrToHexStrReversed(time4byte, "");
            return Asmb();
        }

        public string Asmb_BaudRate_Alter(int strBaudRate)
        {
            byte BaudRateWord = 0;

            if (strBaudRate == 1200)
            {
                BaudRateWord = 0x06;
            }
            else
            if (strBaudRate == 2400)
            {
                BaudRateWord = 0x07;
            }
            else
            if (strBaudRate == 4800)
            {
                BaudRateWord = 0x08;
            }
            else
            if (strBaudRate == 9600)
            {
                BaudRateWord = 0x01;
            }
            else
            if (strBaudRate == 19200)
            {
                BaudRateWord = 0x09;
            }
            else
            if (strBaudRate == 38400)
            {
                BaudRateWord = 0x0A;
            }
            else
            if (strBaudRate == 57600)
            {
                BaudRateWord = 0x0B;
            }
            else
            if (strBaudRate == 115200)
            {
                BaudRateWord = 0x02;
            }
            else
            if (strBaudRate == 250000)
            {
                BaudRateWord = 0x03;
            }
            else
            if (strBaudRate == 500000)
            {
                BaudRateWord = 0x04;
            }
            else
            if (strBaudRate == 1000000)
            {
                BaudRateWord = 0x05;
            }

            SendFrameAddr = SerialUtilModel.HexStrToHexStrReversed(CandidateAddr, "");
            SendFrameCtrl = "17";
            SendFrameData = BaudRateWord.ToString();
            return Asmb();
        }

        public string Asmb_PassWord_Alter(string strDataId, string strPassword, string strOperator, string strNewPassword)
        {
            SendFrameAddr = SerialUtilModel.HexStrToHexStrReversed(CandidateAddr, "");
            SendFrameCtrl = "18";
            SendFrameData = SerialUtilModel.HexStrToHexStrReversed(strDataId, "") + SerialUtilModel.HexStrToHexStrReversed(strPassword, "") + SerialUtilModel.HexStrToHexStrReversed(strNewPassword, "");
            return Asmb();
        }

        public string Asmb_DmndData_Clear(string strPassword, string strOperator)
        {
            SendFrameAddr = SerialUtilModel.HexStrToHexStrReversed(CandidateAddr, "");
            SendFrameCtrl = "19";
            SendFrameData = SerialUtilModel.HexStrToHexStrReversed(strPassword, "") + SerialUtilModel.HexStrToHexStrReversed(strOperator, "");
            return Asmb();
        }

        public string Asmb_MetrData_Clear(string strPassword, string strOperator)
        {
            SendFrameAddr = SerialUtilModel.HexStrToHexStrReversed(CandidateAddr, "");
            SendFrameCtrl = "1A";
            SendFrameData = SerialUtilModel.HexStrToHexStrReversed(strPassword, "") + SerialUtilModel.HexStrToHexStrReversed(strOperator, "");
            return Asmb();
        }

        public string Asmb_EvntData_Clear(string strPassword, string strOperator, string strEventsClearDataId)
        {
            SendFrameAddr = SerialUtilModel.HexStrToHexStrReversed(CandidateAddr, "");
            SendFrameCtrl = "1B";
            SendFrameData = SerialUtilModel.HexStrToHexStrReversed(strPassword, "") + SerialUtilModel.HexStrToHexStrReversed(strOperator, "") + SerialUtilModel.HexStrToHexStrReversed(strEventsClearDataId, "");
            return Asmb();
        }

        private string[] dlt645_user_extend_calibrate_measure_pwr_vol = new string[3];
        private string[] dlt645_user_extend_calibrate_measure_pwr_cur = new string[3];
        private string[] dlt645_user_extend_calibrate_measure_pwr_pwr = new string[3];
        private string[] dlt645_user_extend_calibrate_measure_pwr_pwrfactor = new string[3];
        private string[] dlt645_user_extend_calibrate_measure_pha = new string[3];
        private string dlt645_user_extend_calibrate_protect_cur;
        private string dlt645_user_extend_calibrate_protect_cur_residual;
        private string dlt645_user_extend_calibrate_hsdc;

        public string[] DLT645_User_Extend_Calibrate_Measure_Pwr_Vol
        {
            get
            {
                return dlt645_user_extend_calibrate_measure_pwr_vol;
            }
            set
            {
                if (dlt645_user_extend_calibrate_measure_pwr_vol != value)
                {
                    dlt645_user_extend_calibrate_measure_pwr_vol = value;
                    RaisePropertyChanged(nameof(DLT645_User_Extend_Calibrate_Measure_Pwr_Vol));
                }
            }
        }

        public string[] DLT645_User_Extend_Calibrate_Measure_Pwr_Cur
        {
            get
            {
                return dlt645_user_extend_calibrate_measure_pwr_cur;
            }
            set
            {
                if (dlt645_user_extend_calibrate_measure_pwr_cur != value)
                {
                    dlt645_user_extend_calibrate_measure_pwr_cur = value;
                    RaisePropertyChanged(nameof(DLT645_User_Extend_Calibrate_Measure_Pwr_Cur));
                }
            }
        }

        public string[] DLT645_User_Extend_Calibrate_Measure_Pwr_Pwr
        {
            get
            {
                return dlt645_user_extend_calibrate_measure_pwr_pwr;
            }
            set
            {
                if (dlt645_user_extend_calibrate_measure_pwr_pwr != value)
                {
                    dlt645_user_extend_calibrate_measure_pwr_pwr = value;
                    RaisePropertyChanged(nameof(DLT645_User_Extend_Calibrate_Measure_Pwr_Pwr));
                }
            }
        }

        public string[] DLT645_User_Extend_Calibrate_Measure_Pwr_PwrFactor
        {
            get
            {
                return dlt645_user_extend_calibrate_measure_pwr_pwrfactor;
            }
            set
            {
                if (dlt645_user_extend_calibrate_measure_pwr_pwrfactor != value)
                {
                    dlt645_user_extend_calibrate_measure_pwr_pwrfactor = value;
                    RaisePropertyChanged(nameof(DLT645_User_Extend_Calibrate_Measure_Pwr_PwrFactor));
                }
            }
        }

        public string[] DLT645_User_Extend_Calibrate_Measure_Pha
        {
            get
            {
                return dlt645_user_extend_calibrate_measure_pha;
            }
            set
            {
                if (dlt645_user_extend_calibrate_measure_pha != value)
                {
                    dlt645_user_extend_calibrate_measure_pha = value;
                    RaisePropertyChanged(nameof(DLT645_User_Extend_Calibrate_Measure_Pha));
                }
            }
        }

        public string DLT645_User_Extend_Calibrate_Protect_Cur
        {
            get
            {
                return dlt645_user_extend_calibrate_protect_cur;
            }
            set
            {
                if (dlt645_user_extend_calibrate_protect_cur != value)
                {
                    dlt645_user_extend_calibrate_protect_cur = value;
                    RaisePropertyChanged(nameof(DLT645_User_Extend_Calibrate_Protect_Cur));
                }
            }
        }

        public string DLT645_User_Extend_Calibrate_Protect_Cur_Residual
        {
            get
            {
                return dlt645_user_extend_calibrate_protect_cur_residual;
            }
            set
            {
                if (dlt645_user_extend_calibrate_protect_cur_residual != value)
                {
                    dlt645_user_extend_calibrate_protect_cur_residual = value;
                    RaisePropertyChanged(nameof(DLT645_User_Extend_Calibrate_Protect_Cur_Residual));
                }
            }
        }

        public string DLT645_User_Extend_Calibrate_HSDC
        {
            get
            {
                return dlt645_user_extend_calibrate_hsdc;
            }
            set
            {
                if (dlt645_user_extend_calibrate_hsdc != value)
                {
                    dlt645_user_extend_calibrate_hsdc = value;
                    RaisePropertyChanged(nameof(DLT645_User_Extend_Calibrate_HSDC));
                }
            }
        }

        public void Asmb_Calibrate_Measure_Pwr(byte step)
        {
#if false
            // 计量校准：功率校准
            // 68 AA AA AA AA AA AA 68 14 2E 33 35 D5 37 33 6C 85 89 33 33 33 33 34 33 55 33 55 33 55 33 33 58 33 33 58 33 33 58 33 33 33 88 33 33 33 88 33 33 33 88 33 43 33 43 33 43 C6 16

            // 计量校准：相位校准
            // 68 AA AA AA AA AA AA 68 14 2E 33 35 D5 37 33 6C 85 89 33 33 33 33 35 33 55 33 55 33 55 33 33 58 33 33 58 33 33 58 33 33 33 88 33 33 33 88 33 33 33 88 33 38 33 38 33 38 A6 16

            byte[] HexArr;
            byte[] HexArrAll = new byte[0x2E];
            byte[] HexArrReversed;
            int index = 0;

            Global_DataIdx4 = "04A20200";
            Global_Password = "56523900";
            Global_Operator = "00000000";

            HexArr = SerialUtilModel.HexStrToHexArr(Global_DataIdx4);
            HexArrReversed = new byte[HexArr.Length];

            for (int j = 0; j < HexArr.Length; ++j)
            {
                HexArrReversed[j] = HexArr[HexArr.Length - 1 - j];
            }

            foreach (byte b in HexArrReversed)
            {
                HexArrAll[index] = b;
                index++;
            }

            HexArr = SerialUtilModel.HexStrToHexArr(Global_Password);
            HexArrReversed = new byte[HexArr.Length];

            for (int j = 0; j < HexArr.Length; ++j)
            {
                HexArrReversed[j] = HexArr[HexArr.Length - 1 - j];
            }

            foreach (byte b in HexArrReversed)
            {
                HexArrAll[index] = b;
                index++;
            }

            HexArr = SerialUtilModel.HexStrToHexArr(Global_Operator);
            HexArrReversed = new byte[HexArr.Length];

            for (int j = 0; j < HexArr.Length; ++j)
            {
                HexArrReversed[j] = HexArr[HexArr.Length - 1 - j];
            }

            foreach (byte b in HexArrReversed)
            {
                HexArrAll[index] = b;
                index++;
            }

            HexArrAll[index] = step;
            index++;

            for (int i = 0; i < DLT645_User_Extend_Calibrate_Measure_Pwr_Vol.Length; i++)
            {
                HexArr = SerialUtilModel.HexStrToHexArr(DLT645_User_Extend_Calibrate_Measure_Pwr_Vol[i]);
                HexArrReversed = new byte[HexArr.Length];

                for (int j = 0; j < HexArr.Length; ++j)
                {
                    HexArrReversed[j] = HexArr[HexArr.Length - 1 - j];
                }

                foreach (byte b in HexArrReversed)
                {
                    HexArrAll[index] = b;
                    index++;
                }
            }

            for (int i = 0; i < DLT645_User_Extend_Calibrate_Measure_Pwr_Cur.Length; i++)
            {
                HexArr = SerialUtilModel.HexStrToHexArr(DLT645_User_Extend_Calibrate_Measure_Pwr_Cur[i]);
                HexArrReversed = new byte[HexArr.Length];

                for (int j = 0; j < HexArr.Length; ++j)
                {
                    HexArrReversed[j] = HexArr[HexArr.Length - 1 - j];
                }

                foreach (byte b in HexArrReversed)
                {
                    HexArrAll[index] = b;
                    index++;
                }
            }

            for (int i = 0; i < DLT645_User_Extend_Calibrate_Measure_Pwr_Pwr.Length; i++)
            {
                HexArr = SerialUtilModel.HexStrToHexArr(DLT645_User_Extend_Calibrate_Measure_Pwr_Pwr[i]);
                HexArrReversed = new byte[HexArr.Length];

                for (int j = 0; j < HexArr.Length; ++j)
                {
                    HexArrReversed[j] = HexArr[HexArr.Length - 1 - j];
                }

                foreach (byte b in HexArrReversed)
                {
                    HexArrAll[index] = b;
                    index++;
                }
            }

            if (step == 0x01)
            {
                for (int i = 0; i < DLT645_User_Extend_Calibrate_Measure_Pwr_PwrFactor.Length; i++)
                {
                    HexArr = SerialUtilModel.HexStrToHexArr(DLT645_User_Extend_Calibrate_Measure_Pwr_PwrFactor[i]);
                    HexArrReversed = new byte[HexArr.Length];

                    for (int j = 0; j < HexArr.Length; ++j)
                    {
                        HexArrReversed[j] = HexArr[HexArr.Length - 1 - j];
                    }

                    foreach (byte b in HexArrReversed)
                    {
                        HexArrAll[index] = b;
                        index++;
                    }
                }
            }
            else if (step == 0x02)
            {
                for (int i = 0; i < DLT645_User_Extend_Calibrate_Measure_Pha.Length; i++)
                {
                    HexArr = SerialUtilModel.HexStrToHexArr(DLT645_User_Extend_Calibrate_Measure_Pha[i]);
                    HexArrReversed = new byte[HexArr.Length];

                    for (int j = 0; j < HexArr.Length; ++j)
                    {
                        HexArrReversed[j] = HexArr[HexArr.Length - 1 - j];
                    }

                    foreach (byte b in HexArrReversed)
                    {
                        HexArrAll[index] = b;
                        index++;
                    }
                }
            }

            Global_DataArea = SerialUtilModel.HexArrToHexStr(HexArrAll);
            Asmb_WritData();
#endif
        }

        private string _RecvFrameAddr = String.Empty;
        public string RecvFrameAddr
        {
            get
            {
                return _RecvFrameAddr;
            }
            set
            {
                if (_RecvFrameAddr != value)
                {
                    _RecvFrameAddr = value;
                    RaisePropertyChanged(nameof(RecvFrameAddr));
                }
            }
        }

        private string _RecvFrameCtrl = String.Empty;
        public string RecvFrameCtrl
        {
            get
            {
                return _RecvFrameCtrl;
            }
            set
            {
                if (_RecvFrameCtrl != value)
                {
                    _RecvFrameCtrl = value;
                    RaisePropertyChanged(nameof(RecvFrameCtrl));
                }
            }
        }

        private string _RecvFrameSize = String.Empty;
        public string RecvFrameSize
        {
            get
            {
                return _RecvFrameSize;
            }
            set
            {
                if (_RecvFrameSize != value)
                {
                    _RecvFrameSize = value;
                    RaisePropertyChanged(nameof(RecvFrameSize));
                }
            }
        }

        private string _RecvFrameData = String.Empty;
        public string RecvFrameData
        {
            get
            {
                return _RecvFrameData;
            }
            set
            {
                if (_RecvFrameData != value)
                {
                    _RecvFrameData = value;
                    RaisePropertyChanged(nameof(RecvFrameData));
                }
            }
        }

        private string _RecvFrameSum8 = String.Empty;
        public string RecvFrameSum8
        {
            get
            {
                return _RecvFrameSum8;
            }
            set
            {
                if (_RecvFrameSum8 != value)
                {
                    _RecvFrameSum8 = value;
                    RaisePropertyChanged(nameof(RecvFrameSum8));
                }
            }
        }

        private string _RecvFrameInfo = String.Empty;
        public string RecvFrameInfo
        {
            get
            {
                return _RecvFrameInfo;
            }
            set
            {
                if (_RecvFrameInfo != value)
                {
                    _RecvFrameInfo = value;
                    RaisePropertyChanged(nameof(RecvFrameInfo));
                }
            }
        }

        public bool IsCorrect(string str)
        {
            bool ret = false;

            str = str.Replace(" ", "");
            str = str.Replace("\r", "");
            str = str.Replace("\n", "");

            int idx = 0;
            byte[] frame = SerialUtilModel.HexStrToHexArr(str);
            for (int i = 0; i < frame.Length; i++)
            {
                if (frame[i] == 0x68)
                {
                    idx = i;
                    break;
                }
            }

            if (idx < frame.Length && frame.Length >= (12 + idx))
            {
                byte soh = frame[idx + 7];
                byte len = frame[idx + 9];
                if (idx+9+1+len+2 > frame.Length)
                {
                    return false;
                }
                byte chk = frame[idx + 9 + 1 + len];
                byte eot = frame[idx + 9 + 1 + len + 1];
                byte sum = 0;
                RecvFrameSize = len.ToString("X2");
                RecvFrameSum8 = chk.ToString("X2");

                if (soh == 0x68 && eot == 0x16 && len <= 200)
                {
                    for (int i = 0; i < 10 + len; i++)
                    {
                        sum += frame[idx + i];
                    }
                    if (sum == chk)
                    {
                        ret = true;
                    }
                }
            }

            return ret;
        }

        public bool Parse(string str)
        {
            try
            {
                if (IsCorrect(str))
                {

                }
                str = str.Replace(" ", "");
                str = str.Replace("\r", "");
                str = str.Replace("\n", "");

                int idx = 0;
                byte[] frame = SerialUtilModel.HexStrToHexArr(str);
                if (frame.Length < 10)
                {
                    return false;
                }
                for (int i = 0; i < frame.Length; i++)
                {
                    if (frame[i] == 0x68)
                    {
                        idx = i;
                        break;
                    }
                }

                RecvFrameCtrl = Convert.ToHexString(frame, idx + 8, 1);
                RecvFrameAddr = Convert.ToHexString(frame, idx + 1, 6);
                for (int i = 0; i < frame[idx + 9]; i++)
                {
                    frame[idx + 10 + i] -= 0x33;
                }
                RecvFrameData = Convert.ToHexString(frame, idx + 10, frame[idx + 9]);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public string ParseJsonNormalType(JsonNode item, string dataid, string data, out int offset)
        {
            string result = null;
            offset = 0;

            try
            {
                if (item?["name"] is not JsonNode nodeName)
                {
                    return result;
                }
                result = "\n数据项：" + nodeName.GetValue<string>();
                if (item?["type"] is not JsonNode nodeType)
                {
                    return result;
                }

                string type = nodeType.GetValue<string>() ?? string.Empty;
                string dataFormat;

                if (type == "bcd")
                {
                    if (item?["data"] is not JsonNode nodeData)
                    {
                        return result;
                    }

                    JsonValueKind kind = nodeData.GetValueKind();
                    if (kind.Equals(JsonValueKind.Array))
                    {
                        JsonArray nodeDataArray = nodeData.AsArray();
                        int index = 0;
                        foreach (JsonNode? nodeDataItem in nodeDataArray)
                        {
                            result += "\n";
                            dataFormat = nodeDataItem.GetValue<string>() ?? string.Empty;
                            int len = dataFormat.Replace(".", "").Replace(" ", "").Length;
                            offset += len;
                            if (len > data.Length)
                            {
                                continue;
                            }
                            string part = SerialUtilModel.HexStrToHexStrReversed(data.Substring(index, len), "");
                            if (dataFormat.Contains('.'))
                            {
                                int idx = dataFormat.IndexOf('.');
                                result += part.Substring(0, idx) + "." + part.Substring(idx, len - idx);
                            }
                            else
                            {
                                dataFormat = dataFormat.Replace(" ", "");
                                result += part.Substring(0, len);
                            }
                            index += len;

                            if (item?["unit"] is JsonNode nodeUnit)
                            {
                                result += nodeUnit.GetValue<string>();
                            }

                            //if (nodeDataItem.GetElementIndex() < nodeDataArray.Count - 1)
                            //{
                            //    result += "\n";
                            //}
                        }
                    }
                    else if (kind.Equals(JsonValueKind.String))
                    {
                        result += "\n";
                        dataFormat = nodeData.GetValue<string>() ?? string.Empty;
                        int len = dataFormat.Replace(".", "").Replace(" ", "").Length;
                        offset += len;
                        if (len > data.Length)
                        {
                            len = data.Length;
                        }
                        string part = SerialUtilModel.HexStrToHexStrReversed(data.Substring(0, len), "");
                        if (dataFormat.Contains('.'))
                        {
                            int idx = dataFormat.IndexOf('.');
                            result += part.Substring(0, idx) + "." + part.Substring(idx, len - idx);
                        }
                        else
                        {
                            result += part.Substring(0, len);
                        }
                        if (item?["unit"] is JsonNode nodeUnit)
                        {
                            result += nodeUnit.GetValue<string>();
                        }
                        //result += "\n";
                    }
                    else
                    {
                        return result;
                    }
                }
                else if (type == "hex")
                {
                    if (item?["data"] is not JsonNode nodeData)
                    {
                        return result;
                    }
                    result += "\n";
                    dataFormat = nodeData.GetValue<string>() ?? string.Empty;
                    int len = dataFormat.Replace(".", "").Replace(" ", "").Length;
                    offset += len;
                    if (len > data.Length)
                    {
                        len = data.Length;
                    }
                    string part = SerialUtilModel.HexStrToHexStrReversed(data.Substring(0, len), "");
                    if (dataFormat.Contains('.'))
                    {
                        int idx = dataFormat.IndexOf('.');
                        result += part.Substring(0, idx) + "." + part.Substring(idx, len - idx);
                    }
                    else
                    {
                        result += part.Substring(0, len);
                    }
                }
                else if (type == "bit")
                {
                    if (item?["data"] is not JsonNode nodeData)
                    {
                        return result;
                    }
                    result += "\n";
                    dataFormat = nodeData.GetValue<string>() ?? string.Empty;
                    int len = dataFormat.Replace(".", "").Replace(" ", "").Length;
                    offset += len;
                    if (len > data.Length)
                    {
                        len = data.Length;
                    }
                    string part = SerialUtilModel.HexStrToHexStrReversed(data.Substring(0, len), "");
                    if (part.Length > 2)
                    {
                        // 逆序
                        result += "0x" + part + "（逆序）设置时拷贝这个到上面\n";
                        // 原帧
                        part = SerialUtilModel.HexStrToHexStrReversed(part.Substring(0, len), "");
                        result += "0x" + part + "（原帧）";
                    }
                    else
                    {
                        result += "0x" + part.Substring(0, len);
                    }

                    byte[] array = SerialUtilModel.HexStrToHexArr(part);

                    if (item?["bits"] is not JsonNode nodeBits)
                    {
                        return result;
                    }
                    JsonArray nodeBitsArray = nodeBits.AsArray();
                    foreach (JsonNode? nodeBitsItem in nodeBitsArray)
                    {
                        if (nodeBitsItem?["msb"] is not JsonNode nodeMSB)
                        {
                            return result;
                        }
                        if (nodeBitsItem?["lsb"] is not JsonNode nodeLSB)
                        {
                            return result;
                        }
                        if (nodeBitsItem?["name"] is not JsonNode nodeBitName)
                        {
                            return result;
                        }
                        int msb = nodeMSB.GetValue<int>();
                        int lsb = nodeLSB.GetValue<int>();
                        byte[] val = Convert.FromHexString(part);

                        int bitlen = msb - lsb + 1;
                        int mask = 1;
                        for (int i = 0; i < bitlen; ++i)
                        {
                            mask *= 2;
                        }
                        mask -= 1;
                        // 暂时不支持跨字节解析
                        int value = (val[lsb / 8] >> (lsb%8)) & mask;
                        try
                        {
                            string strVal = value.ToString("X2");
                            if (nodeBitsItem?[strVal] is JsonNode nodeVal)
                            {
                                result += "\n　　　　" + nodeBitName.GetValue<string>() + ": " + nodeVal.GetValue<string>();
                            }
                        }
                        catch
                        {
                            return result;
                        }
                    }
                }
                else if (type == "string")
                {
                    if (item?["len"] is not JsonNode nodeLen)
                    {
                        return result;
                    }
                    if (data.Length == 0)
                    {
                        return result;
                    }
                    int len = nodeLen.GetValue<int>()*2;
                    offset += len;
                    // 原帧
                    if (len != data.Length)
                    {
                        result += "\t💀：帧中ASCII的长度与定义的不一致!（定义的是" + len/2 + "字节）（实际上是" + data.Length/2 + "字节）";
                        len = data.Length;
                    }
                    result += "\n";
                    string part = SerialUtilModel.HexStrToHexStr(data.Substring(0, len), "");
                    result += part.Substring(0, len) + "（原帧）";
                    result += "\n";
                    byte[] HexArr = SerialUtilModel.HexStrToHexArr(part);
                    if (HexArr != null)
                    {
                        result += Encoding.ASCII.GetString(HexArr);
                    }

                    result += "\n";

                    // 逆序
                    part = SerialUtilModel.HexStrToHexStrReversed(data.Substring(0, len), "");
                    result += part.Substring(0, len) + "（逆序）";
                    result += "\n";
                    HexArr = SerialUtilModel.HexStrToHexArr(part);
                    if (HexArr != null)
                    {
                        result += Encoding.ASCII.GetString(HexArr);
                    }
                }
                else if (type == "time")
                {
                    if (item?["data"] is not JsonNode nodeData)
                    {
                        return result;
                    }
                    result += "\n";
                    dataFormat = nodeData.GetValue<string>() ?? string.Empty;
                    if (data.Length == 0)
                    {
                        return result;
                    }
                    int len = dataFormat.Replace(".", "").Replace(" ", "").Length;
                    offset += len;
                    if (len > data.Length)
                    {
                        len = data.Length;
                    }
                    string part = SerialUtilModel.HexStrToHexStrReversed(data.Substring(0, len), "");
                    for (int i = 0; i < dataFormat.Length / 2; i++)
                    {
                        string strTime = dataFormat.Substring(2 * i, 2);
                        if (strTime == "WW")
                        {
                            result += "星期";
                            string week = part.Substring(2 * i, 2);
                            if (week == "01")
                            {
                                result += "一";
                            }
                            else
                            if (week == "02")
                            {
                                result += "二";
                            }
                            else
                            if (week == "03")
                            {
                                result += "三";
                            }
                            else
                            if (week == "04")
                            {
                                result += "四";
                            }
                            else
                            if (week == "05")
                            {
                                result += "五";
                            }
                            else
                            if (week == "06")
                            {
                                result += "六";
                            }
                            else
                            if (week == "00")
                            {
                                result += "日";
                            }
                            result += " ";
                        }
                        else
                        if (strTime == "YY")
                        {
                            result += "20";
                            result += part.Substring(2 * i, 2);
                        }
                        else
                        if (strTime == "MM")
                        {
                            result += "-";
                            result += part.Substring(2 * i, 2);
                        }
                        else if (strTime == "DD")
                        {
                            result += "-";
                            result += part.Substring(2 * i, 2);
                            result += " ";
                        }
                        else if (strTime == "hh")
                        {
                            result += part.Substring(2 * i, 2);
                        }
                        else if (strTime == "mm")
                        {
                            result += ":";
                            result += part.Substring(2 * i, 2);
                        }
                        else if (strTime == "ss")
                        {
                            result += ":";
                            result += part.Substring(2 * i, 2);
                        }
                    }
                }
            }
            catch { return result; }

            return result;
        }

        public string ParseJsonData(JsonNode node, string dataid, string data, out int offset)
        {
            string result = null;
            offset = 0;

            try
            {
                if (node?["type"] is not JsonNode nodeType)
                {
                    return result;
                }

                string type = nodeType.GetValue<string>() ?? string.Empty;

                if (type == "block")
                {
                    if (node?["data"] is not JsonNode blockNodeData)
                    {
                        return result;
                    }

                    if (node?["name"] is not JsonNode blockNodeName)
                    {
                        return result;
                    }
                    result += "\n数据项：" + blockNodeName.GetValue<string>();
                    if (IsLastN(dataid))
                    {
                        result += "（上" + dataid.Substring(6, 2) + "次）";
                    }
                    result += "\n";

                    JsonArray BlockArray = blockNodeData.AsArray();
                    //data = SerialUtilModel.HexStrToHexStrReversed(data, "");
                    foreach (JsonNode? item in BlockArray)
                    {
                        result += ParseJsonData(item, dataid, data, out offset) + "\n";
                        if (offset > data.Length)
                        {
                            continue;
                        }
                        data = data.Substring(offset);
                    }
                }
                else
                {
                    result = ParseJsonNormalType(node, dataid, data, out offset);
                }
            }
            catch
            {
                return result;
            }

            return result;
        }

        public string[] dlt645JsonDataIdConfigFiles =
        {
            "dlt645_table_parameter.json",
            "dlt645_table_electric.json",
            "dlt645_table_energy.json",
            "dlt645_table_record.json",
            "dlt645_table_events.json",
            "dlt645_table_others.json",
            "dlt645_table_extend.json",
        };

        public bool IsLastN(string dataid)
        {
            JsonArray nodeLastNArray = dlt645JsonConfigRoot["LastN"]!.AsArray();
            foreach (JsonNode? node in nodeLastNArray)
            {
                string item = node.GetValue<string>() ?? string.Empty;
                if (item == dataid.Substring(0, item.Length) && dataid.Substring(6, 2) != "00")
                    return true;
            }

            return false;
        }

        public bool IsLastN(string dataid, string nodeid)
        {
            JsonArray nodeNotLastNArray = dlt645JsonConfigRoot["NotLastN"]!.AsArray();
            foreach (JsonNode? node in nodeNotLastNArray)
            {
                string item = node.GetValue<string>() ?? string.Empty;
                if (item == nodeid.Substring(0, item.Length))
                    return false;
            }

            JsonArray nodeLastNArray = dlt645JsonConfigRoot["LastN"]!.AsArray();
            foreach (JsonNode? node in nodeLastNArray)
            {
                string item = node.GetValue<string>() ?? string.Empty;
                if (item == dataid.Substring(0, item.Length) && dataid.Substring(0, 6) == nodeid.Substring(0, 6))
                    return true;
            }

            return false;
        }

        public string ParseJsonFile(string dataid, string data)
        {
            string result = null;
            int offset = 0;

            string dlt645JsonDataIdConfigFile;
            string dlt645JsonDataIdConfigText;
            JsonNode dlt645JsonDataIdConfigRoot;
            JsonArray dlt645JsonDataIdConfigArray;

            // 遍历所有Json配置文件
            foreach (string item in dlt645JsonDataIdConfigFiles)
            {
                dlt645JsonDataIdConfigFile = item;
                dlt645JsonDataIdConfigText = File.ReadAllText(dlt645JsonDataIdConfigFile);
                dlt645JsonDataIdConfigRoot = JsonNode.Parse(dlt645JsonDataIdConfigText)!;

                JsonArray nodeProtocolArray = dlt645JsonConfigRoot["Protocol"]!.AsArray();
                foreach (JsonNode? nodeProtocol in nodeProtocolArray)
                {
                    string array = nodeProtocol.GetValue<string>() ?? string.Empty;
                    if (dlt645JsonDataIdConfigRoot?[array] is not JsonNode nodeArray)
                    {
                        continue;
                    }
                    dlt645JsonDataIdConfigArray = dlt645JsonDataIdConfigRoot[array]!.AsArray();
                    // 遍历Json配置文件中的所有数据项（dataid）
                    foreach (JsonNode? node in dlt645JsonDataIdConfigArray)
                    {
                        if (node?["id"] is JsonNode nodeId)
                        {
                            if ((nodeId.GetValue<string>() == dataid) || IsLastN(dataid, nodeId.GetValue<string>()))
                            {
                                result = ParseJsonData(node, dataid, data, out offset);
                                break;
                            }
                        }
                    }
                    if (result != null)
                    {
                        return result;
                    }
                }
            }

            return result;
        }

        public void ParseData()
        {
            byte[] ctrl = SerialUtilModel.HexStrToHexArr(RecvFrameCtrl);
            bool Master = (ctrl[0] & 0x80) == 0;
            bool Normal = (ctrl[0] & 0x40) == 0;
            byte FuncId = (byte)(ctrl[0] & 0x1F);
            RecvFrameInfo = "";
            //if (Master)
            //{
            //    RecvFrameInfo += "帧方向：来自主站的请求帧\n";
            //}
            //else
            //{
            //    RecvFrameInfo += "帧方向：来自从站的应答帧";
            //    if (Normal)
            //    {
            //        RecvFrameInfo += "（正常应答）\n";
            //    }
            //    else
            //    {
            //        RecvFrameInfo += "（异常应答）\n";
            //    }
            //}

            RecvFrameInfo += "功能码：";

            switch (FuncId)
            {
                case 0b00000:
                    RecvFrameInfo += "保留字段\n";
                    break;
                case 0b01000:
                    RecvFrameInfo += "广播校时\n";
                    if (Master)
                    {
                        RecvFrameInfo += "数据域：\n";
                    }
                    else
                    {
                        // 广播校时无应答
                    }
                    break;
                case 0b10011:
                    RecvFrameInfo += "读取地址\n";
                    if (Master)
                    {

                    }
                    else
                    {
                        if (Normal)
                        {
                            RecvFrameInfo += "数据域：" + DLT645UserModel.DLT645_Rspd_ReadAddr(RecvFrameData) + "\n";
                            CandidateAddr = DLT645UserModel.CandidateAddr;
                        }
                        else
                        {
                            // 从站异常不应答
                        }
                    }
                    break;
                case 0b10101:
                    RecvFrameInfo += "写入地址\n";
                    if (Master)
                    {
                        RecvFrameInfo += "新地址：\n";
                    }
                    else
                    {
                        if (Normal)
                        {
                            RecvFrameInfo += "数据域：写入成功\n";
                        }
                        else
                        {
                            // 从站异常不应答
                        }
                    }
                    break;
                case 0b10001:
                    RecvFrameInfo += "读取数据\n";
                    if (Master)
                    {
                        try
                        {
                            RecvFrameInfo += "标识符：" + SerialUtilModel.HexStrToHexStrReversed(RecvFrameData.Substring(0, 8), "") + "\n";
                            string dataid = SerialUtilModel.HexStrToHexStrReversed(RecvFrameData.Substring(0, 8), "");
                            string data = RecvFrameData.Substring(8, RecvFrameData.Length - 8);
                            RecvFrameInfo += ParseJsonFile(dataid, data);
                        }
                        catch { }
                    }
                    else
                    {
                        if (Normal)
                        {
                            try
                            {
                                RecvFrameInfo += "标识符：" + SerialUtilModel.HexStrToHexStrReversed(RecvFrameData.Substring(0, 8), "") + "\n";
                                //RecvFrameInfo += "数据域：" + SerialUtilModel.HexStrToHexStrReversed(RecvFrameData.Substring(8, RecvFrameData.Length - 8), " ") + "\n";
                                //RecvFrameInfo += "数据域：" + DLT645UserModel.DLT645_Rspd_ReadAddr(RecvFrameData.Substring(8, RecvFrameData.Length - 8), "") + "\n";
                                string dataid = SerialUtilModel.HexStrToHexStrReversed(RecvFrameData.Substring(0, 8), "");
                                string data = RecvFrameData.Substring(8, RecvFrameData.Length - 8);
                                RecvFrameInfo += ParseJsonFile(dataid, data);
                            }
                            catch { }
                        }
                        else
                        {
                            RecvFrameInfo += "错误码：\n";
                        }
                    }
                    break;
                case 0b10010:
                    RecvFrameInfo += "读取后续\n";
                    if (Master)
                    {
                        RecvFrameInfo += "标识符：\n";
                        RecvFrameInfo += "帧序号：\n";
                    }
                    else
                    {
                        if (Normal)
                        {
                        }
                        else
                        {
                            RecvFrameInfo += "错误码：\n";
                        }
                    }
                    break;
                case 0b10100:
                    RecvFrameInfo += "写入数据\n";
                    if (Master)
                    {
                        try
                        {
                            RecvFrameInfo += "标识符：" + SerialUtilModel.HexStrToHexStrReversed(RecvFrameData.Substring(0, 8), "") + "\n";
                            RecvFrameInfo += "密码域：\n";
                            RecvFrameInfo += "代码域：\n";
                            string dataid = SerialUtilModel.HexStrToHexStrReversed(RecvFrameData.Substring(0, 8), "");
                            string data = RecvFrameData.Substring(24, RecvFrameData.Length - 24);
                            RecvFrameInfo += ParseJsonFile(dataid, data);
                        }
                        catch { }
                    }
                    else
                    {
                        if (Normal)
                        {
                            RecvFrameInfo += "数据域：写入成功\n";
                        }
                        else
                        {
                            RecvFrameInfo += "错误码：\n";
                        }
                    }
                    break;
                case 0b10110:
                    RecvFrameInfo += "冻结命令\n";
                    if (Master)
                    {
                        RecvFrameInfo += "周期值：\n";
                    }
                    else
                    {
                        if (Normal)
                        {
                        }
                        else
                        {
                            RecvFrameInfo += "错误码：\n";
                        }
                    }
                    break;
                case 0b10111:
                    RecvFrameInfo += "更改速率\n";
                    if (Master)
                    {
                        RecvFrameInfo += "特征字：\n";
                    }
                    else
                    {
                        if (Normal)
                        {
                        }
                        else
                        {
                            RecvFrameInfo += "错误码：\n";
                        }
                    }
                    break;
                case 0b11000:
                    RecvFrameInfo += "更改密码\n";
                    if (Master)
                    {
                        RecvFrameInfo += "标识符：\n";
                        RecvFrameInfo += "旧密码：\n";
                        RecvFrameInfo += "新密码：\n";
                    }
                    else
                    {
                        if (Normal)
                        {
                        }
                        else
                        {
                            RecvFrameInfo += "错误码：\n";
                        }
                    }
                    break;
                case 0b11001:
                    RecvFrameInfo += "需量清零\n";
                    if (Master)
                    {

                    }
                    else
                    {
                        if (Normal)
                        {
                        }
                        else
                        {
                            RecvFrameInfo += "错误码：\n";
                        }
                    }
                    break;
                case 0b11010:
                    RecvFrameInfo += "电表清零\n";
                    if (Master)
                    {

                    }
                    else
                    {
                        if (Normal)
                        {
                        }
                        else
                        {
                            RecvFrameInfo += "错误码：\n";
                        }
                    }
                    break;
                case 0b11011:
                    RecvFrameInfo += "事件清零\n";
                    if (Master)
                    {

                    }
                    else
                    {
                        if (Normal)
                        {
                        }
                        else
                        {
                            RecvFrameInfo += "错误码：\n";
                        }
                    }
                    break;
                default: break;
            }
        }

        string dlt645JsonConfigFile;
        string dlt645JsonConfigText;
        public JsonNode dlt645JsonConfigRoot;

        public void InitializeComponent()
        {
            dlt645JsonConfigFile = "DLT645.config.json";
            dlt645JsonConfigText = File.ReadAllText(dlt645JsonConfigFile);
            dlt645JsonConfigRoot = JsonNode.Parse(dlt645JsonConfigText)!;

            CandidateAddr = "AAAAAAAAAAAA"; // 正序显示｜倒序发送

            JsonNode Json_Preamble = dlt645JsonConfigRoot?["ViewModel.LinkLayer.Preamble"];
            if (Json_Preamble != null)
            {
                SendFramePrmb = Json_Preamble.ToString();
            }
            else
            {
                SendFramePrmb = "";
            }

            DLT645_User_Extend_Calibrate_Measure_Pwr_Vol = [ "220.0","","" ];
            DLT645_User_Extend_Calibrate_Measure_Pwr_Vol[0] = "220.0";
            DLT645_User_Extend_Calibrate_Measure_Pwr_Vol[1] = "220.0";
            DLT645_User_Extend_Calibrate_Measure_Pwr_Vol[2] = "220.0";
            DLT645_User_Extend_Calibrate_Measure_Pwr_Cur[0] = "250.000";
            DLT645_User_Extend_Calibrate_Measure_Pwr_Cur[1] = "250.000";
            DLT645_User_Extend_Calibrate_Measure_Pwr_Cur[2] = "250.000";
            DLT645_User_Extend_Calibrate_Measure_Pwr_Pwr[0] = "55000.000";
            DLT645_User_Extend_Calibrate_Measure_Pwr_Pwr[1] = "55000.000";
            DLT645_User_Extend_Calibrate_Measure_Pwr_Pwr[2] = "55000.000";
            DLT645_User_Extend_Calibrate_Measure_Pwr_PwrFactor[0] = "1.000";
            DLT645_User_Extend_Calibrate_Measure_Pwr_PwrFactor[1] = "1.000";
            DLT645_User_Extend_Calibrate_Measure_Pwr_PwrFactor[2] = "1.000";
            DLT645_User_Extend_Calibrate_Measure_Pha[0] = "0.500";
            DLT645_User_Extend_Calibrate_Measure_Pha[1] = "0.500";
            DLT645_User_Extend_Calibrate_Measure_Pha[2] = "0.500";
        }
    }
}
