using DLT645.BasicMVVM;
using DLT645.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace DLT645.ViewModel
{
    internal class MainWindowViewModel : ObservableObject
    {
        public SerialCommModel SerialCommModel { get; set; }
        public SerialPortModel SerialPortModel { get; set; }
        public SerialRecvModel SerialRecvModel { get; set; }
        public SerialSendModel SerialSendModel { get; set; }
        public DLT645CoreModel DLT645CoreModel { get; set; }
        public DLT645UserModel DLT645UserModel { get; set; }
        public iPAC2220ECoreModel iPAC2220ECoreModel { get; set; }

        public MainWindowViewModel()
        {
            SerialCommModel = new SerialCommModel();
            SerialCommModel.InitializeComponent();
            SerialPortModel = new SerialPortModel();
            SerialPortModel.InitializeComponent();
            SerialRecvModel = new SerialRecvModel();
            SerialRecvModel.InitializeComponent();
            SerialSendModel = new SerialSendModel();
            SerialSendModel.InitializeComponent();
            DLT645CoreModel = new DLT645CoreModel();
            DLT645CoreModel.InitializeComponent();
            DLT645UserModel = new DLT645UserModel();
            DLT645UserModel.InitializeComponent();
            iPAC2220ECoreModel = new iPAC2220ECoreModel();
            iPAC2220ECoreModel.InitializeComponent();
        }

        #region 菜单栏

        #region 文件
        internal void TaskExit()
        {
            if (SerialPort.IsOpen)
            {
                PortCloz(0);
            }
        }
        #endregion

        #region 编辑

        #endregion

        #region 视图

        #endregion

        #region 工具

        #endregion

        #region 选项

        #endregion

        #region 信息

        #endregion

        #endregion

        #region 状态栏

        private string _ParsInfo;
        public string ParsInfo
        {
            get
            {
                return _ParsInfo;
            }
            set
            {
                if (_ParsInfo != value)
                {
                    _ParsInfo = value;
                    RaisePropertyChanged(nameof(ParsInfo));
                }
            }
        }

        private string _StatInfo;
        public string StatInfo
        {
            get
            {
                return _StatInfo;
            }
            set
            {
                if (_StatInfo != value)
                {
                    _StatInfo = value;
                    RaisePropertyChanged(nameof(StatInfo));
                }
            }
        }

        private DateTime _DateTime;
        public DateTime DateTime
        {
            get { return _DateTime; }
            set
            {
                if (_DateTime != value)
                {
                    _DateTime = value;
                    RaisePropertyChanged(nameof(DateTime));
                }
            }
        }

        #endregion

        private string _ClockTimerUTC_8 = String.Empty;
        public string ClockTimerUTC_8
        {
            get
            {
                return _ClockTimerUTC_8;
            }
            set
            {
                if (_ClockTimerUTC_8 != value)
                {
                    _ClockTimerUTC_8 = value;
                    RaisePropertyChanged(nameof(ClockTimerUTC_8));
                }
            }
        }

        private string _ClockTimerUTC_0 = String.Empty;
        public string ClockTimerUTC_0
        {
            get
            {
                return _ClockTimerUTC_0;
            }
            set
            {
                if (_ClockTimerUTC_0 != value)
                {
                    _ClockTimerUTC_0 = value;
                    RaisePropertyChanged(nameof(ClockTimerUTC_0));
                }
            }
        }

        private string _ClockTimerStamp = String.Empty;
        public string ClockTimerStamp
        {
            get
            {
                return _ClockTimerStamp;
            }
            set
            {
                if (_ClockTimerStamp != value)
                {
                    _ClockTimerStamp = value;
                    RaisePropertyChanged(nameof(ClockTimerStamp));
                }
            }
        }

        private string _EpochConverterTimeLocal_Max2038 = String.Empty;
        public string EpochConverterTimeLocal_Max2038
        {
            get
            {
                return _EpochConverterTimeLocal_Max2038;
            }
            set
            {
                if (_EpochConverterTimeLocal_Max2038 != value)
                {
                    _EpochConverterTimeLocal_Max2038 = value;
                    RaisePropertyChanged(nameof(EpochConverterTimeLocal_Max2038));
                }
            }
        }

        private string _EpochConverterTimeUtc_Max2038 = String.Empty;
        public string EpochConverterTimeUtc_Max2038
        {
            get
            {
                return _EpochConverterTimeUtc_Max2038;
            }
            set
            {
                if (_EpochConverterTimeUtc_Max2038 != value)
                {
                    _EpochConverterTimeUtc_Max2038 = value;
                    RaisePropertyChanged(nameof(EpochConverterTimeUtc_Max2038));
                }
            }
        }

        private string _EpochConverterTimeLocal_Max2106 = String.Empty;
        public string EpochConverterTimeLocal_Max2106
        {
            get
            {
                return _EpochConverterTimeLocal_Max2106;
            }
            set
            {
                if (_EpochConverterTimeLocal_Max2106 != value)
                {
                    _EpochConverterTimeLocal_Max2106 = value;
                    RaisePropertyChanged(nameof(EpochConverterTimeLocal_Max2106));
                }
            }
        }

        private string _EpochConverterTimeUtc_Max2106 = String.Empty;
        public string EpochConverterTimeUtc_Max2106
        {
            get
            {
                return _EpochConverterTimeUtc_Max2106;
            }
            set
            {
                if (_EpochConverterTimeUtc_Max2106 != value)
                {
                    _EpochConverterTimeUtc_Max2106 = value;
                    RaisePropertyChanged(nameof(EpochConverterTimeUtc_Max2106));
                }
            }
        }

        private string _EpochConverterTimeStamp_INT32 = String.Empty;
        public string EpochConverterTimeStamp_INT32
        {
            get
            {
                return _EpochConverterTimeStamp_INT32;
            }
            set
            {
                if (_EpochConverterTimeStamp_INT32 != value)
                {
                    _EpochConverterTimeStamp_INT32 = value;
                    byte[] data = SerialUtilModel.HexStrToHexArr(value);
                    if (data.Length < 4)
                    {
                        return;
                    }
                    long time = data[0] << 0x18 | data[1] << 0x10 | data[2] << 0x08 | data[3];
                    DateTime result = DateTimeOffset.FromUnixTimeSeconds(time).DateTime;
                    _EpochConverterTimeUtc_Max2038 = result.ToString("yyyy-MM-dd hh:mm:ss") + " UTC";
                    _EpochConverterTimeLocal_Max2038 = result.ToLocalTime().ToString("yyyy-MM-dd hh:mm:ss") + " UTC+8";
                    RaisePropertyChanged(nameof(EpochConverterTimeStamp_INT32));
                    RaisePropertyChanged(nameof(EpochConverterTimeUtc_Max2038));
                    RaisePropertyChanged(nameof(EpochConverterTimeLocal_Max2038));
                }
            }
        }

        private string _EpochConverterTimeStamp_UINT32 = String.Empty;
        public string EpochConverterTimeStamp_UINT32
        {
            get
            {
                return _EpochConverterTimeStamp_UINT32;
            }
            set
            {
                if (_EpochConverterTimeStamp_UINT32 != value)
                {
                    _EpochConverterTimeStamp_UINT32 = value;
                    byte[] data = SerialUtilModel.HexStrToHexArr(value);
                    if (data.Length < 4)
                    {
                        return;
                    }
                    UInt32 time = (UInt32)(data[0]<<0x18 | data[1]<<0x10 | data[2]<<0x08 | data[3]);
                    DateTime result = DateTimeOffset.FromUnixTimeSeconds(time).DateTime;
                    _EpochConverterTimeUtc_Max2106 = result.ToString("yyyy-MM-dd hh:mm:ss") + " UTC";
                    _EpochConverterTimeLocal_Max2106 = result.ToLocalTime().ToString("yyyy-MM-dd hh:mm:ss") + " UTC+8";
                    RaisePropertyChanged(nameof(EpochConverterTimeStamp_UINT32));
                    RaisePropertyChanged(nameof(EpochConverterTimeUtc_Max2106));
                    RaisePropertyChanged(nameof(EpochConverterTimeLocal_Max2106));
                }
            }
        }

        private string _Global_CandidateAddrAA = String.Empty;
        public string Global_CandidateAddrAA
        {
            get
            {
                return _Global_CandidateAddrAA;
            }
            set
            {
                if (_Global_CandidateAddrAA != value)
                {
                    _Global_CandidateAddrAA = value;
                    RaisePropertyChanged(nameof(Global_CandidateAddrAA));
                    if (dlt645JsonConfigRoot != null)
                    {
                        if (dlt645JsonConfigRoot?["ViewModel.LinkLayer.AddressA"] != null)
                        {
                            dlt645JsonConfigRoot?["ViewModel.LinkLayer.AddressA"].ReplaceWith<string>(value);
                            File.WriteAllText(dlt645JsonConfigFile, dlt645JsonConfigRoot.ToString());
                        }
                    }
                }
            }
        }

        private string _Global_CandidateAddr99 = String.Empty;
        public string Global_CandidateAddr99
        {
            get
            {
                return _Global_CandidateAddr99;
            }
            set
            {
                if (_Global_CandidateAddr99 != value)
                {
                    _Global_CandidateAddr99 = value;
                    RaisePropertyChanged(nameof(Global_CandidateAddr99));
                    if (dlt645JsonConfigRoot != null)
                    {
                        if (dlt645JsonConfigRoot?["ViewModel.LinkLayer.Address9"] != null)
                        {
                            dlt645JsonConfigRoot?["ViewModel.LinkLayer.Address9"].ReplaceWith<string>(value);
                            File.WriteAllText(dlt645JsonConfigFile, dlt645JsonConfigRoot.ToString());
                        }
                    }
                }
            }
        }

        private string _Global_CandidateAddr01 = String.Empty;
        public string Global_CandidateAddr01
        {
            get
            {
                return _Global_CandidateAddr01;
            }
            set
            {
                if (_Global_CandidateAddr01 != value)
                {
                    _Global_CandidateAddr01 = value;
                    RaisePropertyChanged(nameof(Global_CandidateAddr01));
                    if (dlt645JsonConfigRoot != null)
                    {
                        if (dlt645JsonConfigRoot?["ViewModel.LinkLayer.Address1"] != null)
                        {
                            dlt645JsonConfigRoot?["ViewModel.LinkLayer.Address1"].ReplaceWith<string>(value);
                            File.WriteAllText(dlt645JsonConfigFile, dlt645JsonConfigRoot.ToString());
                        }
                    }
                }
            }
        }

        private string _Global_CandidateAddr02 = String.Empty;
        public string Global_CandidateAddr02
        {
            get
            {
                return _Global_CandidateAddr02;
            }
            set
            {
                if (_Global_CandidateAddr02 != value)
                {
                    _Global_CandidateAddr02 = value;
                    RaisePropertyChanged(nameof(Global_CandidateAddr02));
                    if (dlt645JsonConfigRoot != null)
                    {
                        if (dlt645JsonConfigRoot?["ViewModel.LinkLayer.Address2"] != null)
                        {
                            dlt645JsonConfigRoot?["ViewModel.LinkLayer.Address2"].ReplaceWith<string>(value);
                            File.WriteAllText(dlt645JsonConfigFile, dlt645JsonConfigRoot.ToString());
                        }
                    }
                }
            }
        }

        private string _Global_CandidateAddr03 = String.Empty;
        public string Global_CandidateAddr03
        {
            get
            {
                return _Global_CandidateAddr03;
            }
            set
            {
                if (_Global_CandidateAddr03 != value)
                {
                    _Global_CandidateAddr03 = value;
                    RaisePropertyChanged(nameof(Global_CandidateAddr03));
                    if (dlt645JsonConfigRoot != null)
                    {
                        if (dlt645JsonConfigRoot?["ViewModel.LinkLayer.Address3"] != null)
                        {
                            dlt645JsonConfigRoot?["ViewModel.LinkLayer.Address3"].ReplaceWith<string>(value);
                            File.WriteAllText(dlt645JsonConfigFile, dlt645JsonConfigRoot.ToString());
                        }
                    }
                }
            }
        }

        private string _Global_CandidateAddr04 = String.Empty;
        public string Global_CandidateAddr04
        {
            get
            {
                return _Global_CandidateAddr04;
            }
            set
            {
                if (_Global_CandidateAddr04 != value)
                {
                    _Global_CandidateAddr04 = value;
                    RaisePropertyChanged(nameof(Global_CandidateAddr04));
                    if (dlt645JsonConfigRoot != null)
                    {
                        if (dlt645JsonConfigRoot?["ViewModel.LinkLayer.Address4"] != null)
                        {
                            dlt645JsonConfigRoot?["ViewModel.LinkLayer.Address4"].ReplaceWith<string>(value);
                            File.WriteAllText(dlt645JsonConfigFile, dlt645JsonConfigRoot.ToString());
                        }
                    }
                }
            }
        }

        private string _Global_CandidateAddr05 = String.Empty;
        public string Global_CandidateAddr05
        {
            get
            {
                return _Global_CandidateAddr05;
            }
            set
            {
                if (_Global_CandidateAddr05 != value)
                {
                    _Global_CandidateAddr05 = value;
                    RaisePropertyChanged(nameof(Global_CandidateAddr05));
                    if (dlt645JsonConfigRoot != null)
                    {
                        if (dlt645JsonConfigRoot?["ViewModel.LinkLayer.Address5"] != null)
                        {
                            dlt645JsonConfigRoot?["ViewModel.LinkLayer.Address5"].ReplaceWith<string>(value);
                            File.WriteAllText(dlt645JsonConfigFile, dlt645JsonConfigRoot.ToString());
                        }
                    }
                }
            }
        }

        private string _Global_CandidateAddr06 = String.Empty;
        public string Global_CandidateAddr06
        {
            get
            {
                return _Global_CandidateAddr06;
            }
            set
            {
                if (_Global_CandidateAddr06 != value)
                {
                    _Global_CandidateAddr06 = value;
                    RaisePropertyChanged(nameof(Global_CandidateAddr06));
                    if (dlt645JsonConfigRoot != null)
                    {
                        if (dlt645JsonConfigRoot?["ViewModel.LinkLayer.Address6"] != null)
                        {
                            dlt645JsonConfigRoot?["ViewModel.LinkLayer.Address6"].ReplaceWith<string>(value);
                            File.WriteAllText(dlt645JsonConfigFile, dlt645JsonConfigRoot.ToString());
                        }
                    }
                }
            }
        }

        private string _Global_CandidateAddr07 = String.Empty;
        public string Global_CandidateAddr07
        {
            get
            {
                return _Global_CandidateAddr07;
            }
            set
            {
                if (_Global_CandidateAddr07 != value)
                {
                    _Global_CandidateAddr07 = value;
                    RaisePropertyChanged(nameof(Global_CandidateAddr07));
                    if (dlt645JsonConfigRoot != null)
                    {
                        if (dlt645JsonConfigRoot?["ViewModel.LinkLayer.Address7"] != null)
                        {
                            dlt645JsonConfigRoot?["ViewModel.LinkLayer.Address7"].ReplaceWith<string>(value);
                            File.WriteAllText(dlt645JsonConfigFile, dlt645JsonConfigRoot.ToString());
                        }
                    }
                }
            }
        }

        private string _Global_CandidateAddr08 = String.Empty;
        public string Global_CandidateAddr08
        {
            get
            {
                return _Global_CandidateAddr08;
            }
            set
            {
                if (_Global_CandidateAddr08 != value)
                {
                    _Global_CandidateAddr08 = value;
                    RaisePropertyChanged(nameof(Global_CandidateAddr08));
                    if (dlt645JsonConfigRoot != null)
                    {
                        if (dlt645JsonConfigRoot?["ViewModel.LinkLayer.Address8"] != null)
                        {
                            dlt645JsonConfigRoot?["ViewModel.LinkLayer.Address8"].ReplaceWith<string>(value);
                            File.WriteAllText(dlt645JsonConfigFile, dlt645JsonConfigRoot.ToString());
                        }
                    }
                }
            }
        }

        private string _Global_Password = String.Empty;
        public string Global_Password
        {
            get
            {
                return _Global_Password;
            }
            set
            {
                if (_Global_Password != value)
                {
                    _Global_Password = value;
                    RaisePropertyChanged(nameof(Global_Password));
                    if (dlt645JsonConfigRoot != null)
                    {
                        if (dlt645JsonConfigRoot?["ViewModel.LinkLayer.Password"] != null)
                        {
                            dlt645JsonConfigRoot?["ViewModel.LinkLayer.Password"].ReplaceWith<string>(value);
                            File.WriteAllText(dlt645JsonConfigFile, dlt645JsonConfigRoot.ToString());
                        }
                    }
                }
            }
        }

        private string _Global_Operator = String.Empty;
        public string Global_Operator
        {
            get
            {
                return _Global_Operator;
            }
            set
            {
                if (_Global_Operator != value)
                {
                    _Global_Operator = value;
                    RaisePropertyChanged(nameof(Global_Operator));
                    if (dlt645JsonConfigRoot != null)
                    {
                        if (dlt645JsonConfigRoot?["ViewModel.LinkLayer.Operator"] != null)
                        {
                            dlt645JsonConfigRoot?["ViewModel.LinkLayer.Operator"].ReplaceWith<string>(value);
                            File.WriteAllText(dlt645JsonConfigFile, dlt645JsonConfigRoot.ToString());
                        }
                    }
                }
            }
        }

        private string _Global_NewPassword = String.Empty;
        public string Global_NewPassword
        {
            get
            {
                return _Global_NewPassword;
            }
            set
            {
                if (_Global_NewPassword != value)
                {
                    _Global_NewPassword = value;
                    RaisePropertyChanged(nameof(Global_NewPassword));
                    if (dlt645JsonConfigRoot != null)
                    {
                        if (dlt645JsonConfigRoot?["ViewModel.LinkLayer.NewPassword"] != null)
                        {
                            dlt645JsonConfigRoot?["ViewModel.LinkLayer.NewPassword"].ReplaceWith<string>(value);
                            File.WriteAllText(dlt645JsonConfigFile, dlt645JsonConfigRoot.ToString());
                        }
                    }
                }
            }
        }

        public Collection<string> Global_NewPasswordLevelItemsSource { get; set; }

        private string _Global_NewPasswordLevelDataId = String.Empty;
        public string Global_NewPasswordLevelDataId
        {
            get
            {
                return _Global_NewPasswordLevelDataId;
            }
            set
            {
                if (_Global_NewPasswordLevelDataId != value)
                {
                    _Global_NewPasswordLevelDataId = value;
                    RaisePropertyChanged(nameof(Global_NewPasswordLevelDataId));
                }
            }
        }

        private string _Global_NewPasswordLevel = String.Empty;
        public string Global_NewPasswordLevel
        {
            get
            {
                return _Global_NewPasswordLevel;
            }
            set
            {
                if (_Global_NewPasswordLevel != value)
                {
                    _Global_NewPasswordLevel = value;
                    RaisePropertyChanged(nameof(Global_NewPasswordLevel));
                    for (int i = 0; i < Global_NewPasswordLevelItemsSource.Count; i++)
                    {
                        if (_Global_NewPasswordLevel == Global_NewPasswordLevelItemsSource[i])
                        {
                            Global_NewPasswordLevelDataId = "04000C" + (i + 1).ToString("X2");
                        }
                    }
                }
            }
        }

        public Collection<int> Global_NewBaudRateItemsSource { get; set; }

        private int _Global_NewBaudRate;
        public int Global_NewBaudRate
        {
            get
            {
                return _Global_NewBaudRate;
            }
            set
            {
                if (_Global_NewBaudRate != value)
                {
                    _Global_NewBaudRate = value;
                    RaisePropertyChanged(nameof(Global_NewBaudRate));
                }
            }
        }

        private string _Global_DataIdx4 = String.Empty;
        public string Global_DataIdx4
        {
            get
            {
                return _Global_DataIdx4;
            }
            set
            {
                if (_Global_DataIdx4 != value)
                {
                    _Global_DataIdx4 = value;
                    RaisePropertyChanged(nameof(Global_DataIdx4));
                }
            }
        }

        private string _Global_DataArea = String.Empty;
        public string Global_DataArea
        {
            get
            {
                return _Global_DataArea;
            }
            set
            {
                if (_Global_DataArea != value)
                {
                    _Global_DataArea = value;
                    RaisePropertyChanged(nameof(Global_DataArea));
                }
            }
        }

        private string _ManualAsmb_AddrArea = String.Empty;
        public string ManualAsmb_AddrArea
        {
            get
            {
                return _ManualAsmb_AddrArea;
            }
            set
            {
                if (_ManualAsmb_AddrArea != value)
                {
                    _ManualAsmb_AddrArea = value;
                    RaisePropertyChanged(nameof(ManualAsmb_AddrArea));
                }
            }
        }

        private string _ManualAsmb_CtrlArea = String.Empty;
        public string ManualAsmb_CtrlArea
        {
            get
            {
                return _ManualAsmb_CtrlArea;
            }
            set
            {
                if (_ManualAsmb_CtrlArea != value)
                {
                    _ManualAsmb_CtrlArea = value;
                    RaisePropertyChanged(nameof(ManualAsmb_CtrlArea));
                }
            }
        }

        private string _ManualAsmb_Password = String.Empty;
        public string ManualAsmb_Password
        {
            get
            {
                return _ManualAsmb_Password;
            }
            set
            {
                if (_ManualAsmb_Password != value)
                {
                    _ManualAsmb_Password = value;
                    RaisePropertyChanged(nameof(ManualAsmb_Password));
                }
            }
        }

        private string _ManualAsmb_Operator = String.Empty;
        public string ManualAsmb_Operator
        {
            get
            {
                return _ManualAsmb_Operator;
            }
            set
            {
                if (_ManualAsmb_Operator != value)
                {
                    _ManualAsmb_Operator = value;
                    RaisePropertyChanged(nameof(ManualAsmb_Operator));
                }
            }
        }

        private string _ManualAsmb_DataIdx4 = String.Empty;
        public string ManualAsmb_DataIdx4
        {
            get
            {
                return _ManualAsmb_DataIdx4;
            }
            set
            {
                if (_ManualAsmb_DataIdx4 != value)
                {
                    _ManualAsmb_DataIdx4 = value;
                    RaisePropertyChanged(nameof(ManualAsmb_DataIdx4));
                }
            }
        }

        private string _ManualAsmb_DataPart = String.Empty;
        public string ManualAsmb_DataPart
        {
            get
            {
                return _ManualAsmb_DataPart;
            }
            set
            {
                if (_ManualAsmb_DataPart != value)
                {
                    _ManualAsmb_DataPart = value;
                    RaisePropertyChanged(nameof(ManualAsmb_DataPart));
                }
            }
        }

        private string _ManualAsmb_ToBeSend;
        public string ManualAsmb_ToBeSend
        {
            get
            {
                return _ManualAsmb_ToBeSend;
            }
            set
            {
                if (_ManualAsmb_ToBeSend != value)
                {
                    _ManualAsmb_ToBeSend = value;
                    RaisePropertyChanged(nameof(ManualAsmb_ToBeSend));

                    string str = value;
                    str = str.Replace(" ", "");
                    str = str.Replace(".", "");
                    str = str.Replace("\r", "");
                    str = str.Replace("\n", "");
                    if (str.Length % 2 != 0)
                    {
                        return;
                    }
                    string value_S33H = DLT645CoreModel.Sub33H(value);
                    if (value_S33H != null)
                    {
                        ManualAsmb_ToBeSend_S33H = value_S33H + "    \"-33H\"";
                    }
                    string value_S33H_all = DLT645CoreModel.Sub33H_all(value);
                    if (value_S33H_all != null)
                    {
                        ManualAsmb_ToBeSend_S33H_all = value_S33H_all + "    \"-33H\"";
                    }
                    string value_reverse = SerialUtilModel.HexStrToHexStrReversed(value, " ");
                    if (value_reverse != null)
                    {
                        ManualAsmb_ToBeSend_Reverse = value_reverse + "    \"<--H\"";
                    }
                }
            }
        }

        private string _ManualAsmb_ToBeSend_S33H;
        public string ManualAsmb_ToBeSend_S33H
        {
            get
            {
                return _ManualAsmb_ToBeSend_S33H;
            }
            set
            {
                if (_ManualAsmb_ToBeSend_S33H != value)
                {
                    _ManualAsmb_ToBeSend_S33H = value;
                    RaisePropertyChanged(nameof(ManualAsmb_ToBeSend_S33H));
                }
            }
        }

        private string _ManualAsmb_ToBeSend_S33H_all;
        public string ManualAsmb_ToBeSend_S33H_all
        {
            get
            {
                return _ManualAsmb_ToBeSend_S33H_all;
            }
            set
            {
                if (_ManualAsmb_ToBeSend_S33H_all != value)
                {
                    _ManualAsmb_ToBeSend_S33H_all = value;
                    RaisePropertyChanged(nameof(ManualAsmb_ToBeSend_S33H_all));
                }
            }
        }

        private string _ManualAsmb_ToBeSend_Reverse;
        public string ManualAsmb_ToBeSend_Reverse
        {
            get
            {
                return _ManualAsmb_ToBeSend_Reverse;
            }
            set
            {
                if (_ManualAsmb_ToBeSend_Reverse != value)
                {
                    _ManualAsmb_ToBeSend_Reverse = value;
                    RaisePropertyChanged(nameof(ManualAsmb_ToBeSend_Reverse));
                }
            }
        }

        private string _Convert_HexToString_Hex;
        public string Convert_HexToString_Hex
        {
            get
            {
                return _Convert_HexToString_Hex;
            }
            set
            {
                if (_Convert_HexToString_Hex != value)
                {
                    _Convert_HexToString_Hex = value;
                    byte[] HexArr = SerialUtilModel.HexStrToHexArr(value);
                    if (HexArr != null)
                    {
                        _Convert_HexToString_String = Encoding.ASCII.GetString(HexArr);
                    }
                    RaisePropertyChanged(nameof(Convert_HexToString_Hex));
                    RaisePropertyChanged(nameof(Convert_HexToString_String));
                }
            }
        }

        private string _Convert_HexToString_String;
        public string Convert_HexToString_String
        {
            get
            {
                return _Convert_HexToString_String;
            }
            set
            {
                if (_Convert_HexToString_String != value)
                {
                    _Convert_HexToString_String = value;
                    byte[] byteArr = value.ToCharArray().Select(c => (byte)c).ToArray();
                    _Convert_HexToString_Hex = Convert.ToHexString(byteArr);
                    RaisePropertyChanged(nameof(Convert_HexToString_String));
                    RaisePropertyChanged(nameof(Convert_HexToString_Hex));
                }
            }
        }

        private string _ManualAsmbiPAC_FrameType = String.Empty;
        public string ManualAsmbiPAC_FrameType
        {
            get
            {
                return _ManualAsmbiPAC_FrameType;
            }
            set
            {
                if (_ManualAsmbiPAC_FrameType != value)
                {
                    _ManualAsmbiPAC_FrameType = value;
                    RaisePropertyChanged(nameof(ManualAsmbiPAC_FrameType));
                }
            }
        }

        private string _ManualAsmbiPAC_CryptType = String.Empty;
        public string ManualAsmbiPAC_CryptType
        {
            get
            {
                return _ManualAsmbiPAC_CryptType;
            }
            set
            {
                if (_ManualAsmbiPAC_CryptType != value)
                {
                    _ManualAsmbiPAC_CryptType = value;
                    RaisePropertyChanged(nameof(ManualAsmbiPAC_CryptType));
                }
            }
        }

        private string _ManualAsmbiPAC_CryptData = String.Empty;
        public string ManualAsmbiPAC_CryptData
        {
            get
            {
                return _ManualAsmbiPAC_CryptData;
            }
            set
            {
                if (_ManualAsmbiPAC_CryptData != value)
                {
                    _ManualAsmbiPAC_CryptData = value;
                    RaisePropertyChanged(nameof(ManualAsmbiPAC_CryptData));
                }
            }
        }

        private string _ManualAsmbiPAC_ToBeSend;
        public string ManualAsmbiPAC_ToBeSend
        {
            get
            {
                return _ManualAsmbiPAC_ToBeSend;
            }
            set
            {
                if (_ManualAsmbiPAC_ToBeSend != value)
                {
                    _ManualAsmbiPAC_ToBeSend = value;
                    RaisePropertyChanged(nameof(ManualAsmbiPAC_ToBeSend));
                }
            }
        }

        private string _CaliMilliSecondType;
        public string CaliMilliSecondType
        {
            get
            {
                return _CaliMilliSecondType;
            }
            set
            {
                if (_CaliMilliSecondType != value)
                {
                    _CaliMilliSecondType = value;
                    RaisePropertyChanged(nameof(CaliMilliSecondType));
                }
            }
        }

        private string _CaliAddrType;
        public string CaliAddrType
        {
            get
            {
                return _CaliAddrType;
            }
            set
            {
                if (_CaliAddrType != value)
                {
                    _CaliAddrType = value;
                    RaisePropertyChanged(nameof(CaliAddrType));
                }
            }
        }

        private string _CaliTimeType;
        public string CaliTimeType
        {
            get
            {
                return _CaliTimeType;
            }
            set
            {
                if (_CaliTimeType != value)
                {
                    _CaliTimeType = value;
                    RaisePropertyChanged(nameof(CaliTimeType));
                }
            }
        }

        private string _FreezeCmdData;
        public string FreezeCmdData
        {
            get
            {
                return _FreezeCmdData;
            }
            set
            {
                if (_FreezeCmdData != value)
                {
                    _FreezeCmdData = value;
                    RaisePropertyChanged(nameof(FreezeCmdData));
                }
            }
        }

        private string _DataReverseType;
        public string DataReverseType
        {
            get
            {
                return _DataReverseType;
            }
            set
            {
                if (_DataReverseType != value)
                {
                    _DataReverseType = value;
                    RaisePropertyChanged(nameof(DataReverseType));
                }
            }
        }

        private string _EventsClearDataId = String.Empty;
        public string EventsClearDataId
        {
            get
            {
                return _EventsClearDataId;
            }
            set
            {
                if (_EventsClearDataId != value)
                {
                    _EventsClearDataId = value;
                    RaisePropertyChanged(nameof(EventsClearDataId));
                }
            }
        }

        #region 右侧栏

        System.Timers.Timer t;
        System.Timers.Timer OneFrameTimer;
        bool isFirstPart = true;

        public void Execute(object source, System.Timers.ElapsedEventArgs e)
        {
            t.Stop(); //先关闭定时器
            PortCloz(1);
        }

        internal void PortCommTimerStart()
        {
            t = new System.Timers.Timer(3000); //设置间隔时间为3000毫秒；
            t.Elapsed += new System.Timers.ElapsedEventHandler(Execute);//到达时间的时候执行事件；
            t.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
            t.Start(); //启动定时器
        }

        public static async Task WriteString(string str)
        {
            using StreamWriter file = new("DLT645_LOG.txt", append: true);
            await file.WriteLineAsync(str);
        }

        public async void PortOneFrameFinishExecute(object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                OneFrameTimer.Stop(); //先关闭定时器
                isFirstPart = true;
                SerialCommModel.RecordList.Add(new SerialCommModelRecord(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff"), "Rx", SerialRecvModel.RecvData));
                //await WriteString(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " Rx" + SerialRecvModel.RecvData);
                SerialRecvModel.RecvData += "\r\n";
                if (SerialCommModel.RecordMemberSize < SerialRecvModel.RecvData.Length * 8)
                {
                    SerialCommModel.RecordMemberSize = SerialRecvModel.RecvData.Length * 8;
                }
                SerialCommModel.UpdateRecordList();

                string FrameStr = SerialRecvModel.RecvData;
                bool ret = DLT645CoreModel.Parse(FrameStr);
                ParsInfo += "应答帧：" + FrameStr;
                ParsInfo += "地址域：" + SerialUtilModel.HexStrToHexStr(DLT645CoreModel.RecvFrameAddr, " ") + "\n";
                ParsInfo += "控制码：" + SerialUtilModel.HexStrToHexStr(DLT645CoreModel.RecvFrameCtrl, " ") + "\n";
                //ParsInfo += "校验码：" + SerialUtilModel.HexStrToHexStr(DLT645CoreModel.RecvFrameSum8, " ") + "\n";
                int len = Convert.ToInt32("0x" + DLT645CoreModel.RecvFrameSize, 16);
                ParsInfo += "长度域：" + SerialUtilModel.HexStrToHexStr(DLT645CoreModel.RecvFrameSize, " ");
                int ctrl = Convert.ToInt32("0x" + DLT645CoreModel.RecvFrameCtrl, 16);
                if (ctrl == 0x11 || ctrl == 0x91 || ctrl == 0x12 || ctrl == 0x92)
                {
                    ParsInfo += "(" + len + "=" + "4+" + (len - 4) + ")";
                }
                else
                if (ctrl == 0x14)
                {
                    ParsInfo += "(" + len + "=" + "4+4+4+" + (len - 12) + ")";
                }
                else
                {
                    ParsInfo += "(" + len + ")";
                }
                ParsInfo += "\n";
                //ParsInfo += "数据域：" + SerialUtilModel.HexStrToHexStr(DLT645CoreModel.RecvFrameData, " ") + "\n";
                if (ret)
                {
                    DLT645CoreModel.ParseData();
                }
                ParsInfo += DLT645CoreModel.RecvFrameInfo;
            }
            catch { }
        }

        internal void PortOneFrameTimerStart()
        {
            isFirstPart = false;
            OneFrameTimer = new System.Timers.Timer(20);
            OneFrameTimer.Elapsed += new System.Timers.ElapsedEventHandler(PortOneFrameFinishExecute);//到达时间的时候执行事件；
            OneFrameTimer.AutoReset = false;//设置是执行一次（false）还是一直执行(true)；
            OneFrameTimer.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
            OneFrameTimer.Start(); //启动定时器
        }

        internal SerialPort SerialPort = new SerialPort();

        internal void RefreshPortName()
        {
            SerialPortModel.SerialPort_RefreshPortNameItemsSource();
        }

        internal void PortOpen(int OpenMode)
        {
            try
            {
                SerialPort.PortName = SerialPortModel.PortName;
                SerialPort.BaudRate = SerialPortModel.BaudRate;
                SerialPort.DataBits = SerialPortModel.DataBits;
                SerialPort.StopBits = SerialPortModel.StopBits;
                SerialPort.Parity = SerialPortModel.PariType;

                SerialPort.ReadBufferSize = 2097152;    /* 输入缓冲区的大小为2097152字节 = 2MB */
                SerialPort.WriteBufferSize = 1048576;   /* 输出缓冲区的大小为1048576字节 = 1MB */

                /* 数据接收事件 */
                SerialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPortDataReceived);

                SerialPort.Open();

                if (SerialPort.IsOpen)
                {
                    if (OpenMode == 0)
                    {
                        SerialPortModel.PortStat = "关闭";

                        SerialPortModel.PortNameEnable = false;
                        SerialPortModel.BaudRateEnable = false;
                        SerialPortModel.DataBitsEnable = false;
                        SerialPortModel.StopBitsEnable = false;
                        SerialPortModel.PariTypeEnable = false;
                        StatInfo = string.Format(CultureInfos, "成功打开串行端口{0}、波特率{1}、数据位{2}、停止位{3}、校验位{4}",
                                                 SerialPort.PortName,
                                                 SerialPort.BaudRate.ToString(CultureInfos),
                                                 SerialPort.DataBits.ToString(CultureInfos),
                                                 SerialPort.StopBits.ToString(),
                                                 SerialPort.Parity.ToString());
                    }

                    if (OpenMode == 1)
                    {
                        PortCommTimerStart();
                    }

                    OneFrameTimer = new System.Timers.Timer(1);
                }
                else
                {
                    StatInfo = string.Format(CultureInfos, "串行端口打开失败");
                }
            }
            catch (UnauthorizedAccessException e)
            {
                StatInfo = e.Message.Replace("\r\n", "");
            }
            catch (ArgumentOutOfRangeException e)
            {
                StatInfo = e.Message.Replace("\r\n", "");
            }
            catch (ArgumentException e)
            {
                StatInfo = string.Format(CultureInfos, "串行端口属性{0}为非法参数，请重新输入", e.ParamName);
            }
            catch (IOException e)
            {
                StatInfo = e.Message.Replace("\r\n", "");
            }
            catch (InvalidOperationException e)
            {
                StatInfo = e.Message.Replace("\r\n", "");
            }
        }

        internal void PortCloz(int ClozMode)
        {
            try
            {
                SerialPort.Close();

                if (ClozMode == 0)
                {
                    StatInfo = string.Format(CultureInfos, "串行端口关闭成功");

                    SerialPortModel.PortStat = "打开";
                    SerialPortModel.PortNameEnable = true;
                    SerialPortModel.BaudRateEnable = true;
                    SerialPortModel.DataBitsEnable = true;
                    SerialPortModel.StopBitsEnable = true;
                    SerialPortModel.PariTypeEnable = true;
                }
            }
            catch (IOException e)
            {
                StatInfo = e.Message.Replace("\r\n", "");
            }
        }

        internal void PortAble()
        {
            if (SerialPort.IsOpen)
            {
                PortCloz(0);
            }
            else
            {
                PortOpen(0);
            }
        }

        internal async Task PortSend(string str)
        {
            if (!SerialPort.IsOpen)
            {
                PortOpen(1);
            }

            if (!SerialPort.IsOpen)
            {
                StatInfo = string.Format(CultureInfos, "请先打开串行端口");
                return;
            }

            try
            {
                int SendCount = 0;
                bool IsHex = true;
                ParsInfo = "";

                if (IsHex)
                {
                    if (str == "")
                    {
                        return;
                    }
                    str = string.Join(" ", System.Text.RegularExpressions.Regex.Split(str, "(?<=\\G.{2})(?!$)"));

                    ParsInfo = "请求帧：" + str + "\n";
                    SerialCommModel.RecordList.Add(new SerialCommModelRecord(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff"), "Tx", str));
                    if (SerialCommModel.RecordMemberSize < str.Length * 8)
                    {
                        SerialCommModel.RecordMemberSize = str.Length * 8;
                    }
                    SerialCommModel.UpdateRecordList();

                    string[] _sendData = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    byte[] sendData = new byte[_sendData.Length];

                    foreach (var tmp in _sendData)
                    {
                        sendData[SendCount++] = byte.Parse(tmp, NumberStyles.AllowHexSpecifier, CultureInfos);
                    }

                    await SerialPort.BaseStream.WriteAsync(sendData, 0, SendCount).ConfigureAwait(false);
                }
                else
                {
                    SendCount = SerialPort.Encoding.GetByteCount(str);
                    await SerialPort.BaseStream.WriteAsync(SerialPort.Encoding.GetBytes(str), 0, SendCount).ConfigureAwait(false);
                }

                if (OneFrameTimer.Enabled)
                {
                    OneFrameTimer.Stop(); //先关闭定时器
                    isFirstPart = true;
                    SerialRecvModel.RecvData += "\r\n";
                }

                //if (TimerModel.TimeStampEnable)
                {
                    DateTime _DateTime = DateTime.Now;
                    SerialRecvModel.RecvData += "[" + _DateTime.ToString("yyyy/MM/dd HH:mm:ss:fff", CultureInfos) + "]";
                    SerialRecvModel.RecvData += " ";
                }

                if (true)//(SerialRecvModel.HexRecv)
                {
                    SerialRecvModel.RecvData += str;
                }
                else
                {

                }

                SerialRecvModel.RecvData += "\r\n";
            }
            catch (ArgumentException e)
            {
                StatInfo = e.Message.Replace("\r\n", "");
            }
            catch (IOException e)
            {
                StatInfo = e.Message.Replace("\r\n", "");
            }
            catch (OutOfMemoryException e)
            {
                StatInfo = e.Message.Replace("\r\n", "");
            }
            catch (FormatException e)
            {
                StatInfo = e.Message.Replace("\r\n", "");
            }
            catch (OverflowException)
            {
                StatInfo = string.Format(CultureInfos, "请输入合法十六进制数据，且用空格隔开，比如A0 B1 C2 D3 E4 F5");
            }
            catch (IndexOutOfRangeException)
            {
                StatInfo = string.Format(CultureInfos, "正在试图执行越界访问，请通过菜单栏<帮助>报告问题！");
            }
            catch (ObjectDisposedException)
            {
                StatInfo = string.Format(CultureInfos, "正在对已释放的对象执行操作，请通过菜单栏<帮助>报告问题！");
            }
            catch (NotFiniteNumberException e)
            {
                StatInfo = e.Message.Replace("\r\n", "");
            }
        }

        internal string AsmbToBeSend()
        {
            if (ManualAsmb_AddrArea.Replace(" ", "").Length % 2 != 0 || ManualAsmb_CtrlArea.Replace(" ", "").Length % 2 != 0 || ManualAsmb_DataIdx4.Replace(" ", "").Length % 2 != 0
             || ManualAsmb_Password.Replace(" ", "").Length % 2 != 0 || ManualAsmb_Operator.Replace(" ", "").Length % 2 != 0 || ManualAsmb_DataPart.Replace(" ", "").Length % 2 != 0)
            {
                return null;
            }

            DLT645CoreModel.SendFrameAddr = SerialUtilModel.HexStrToHexStrReversed(ManualAsmb_AddrArea, "");
            DLT645CoreModel.SendFrameCtrl = ManualAsmb_CtrlArea;
            string data_area_str;
            if (DataReverseType == "逆序")
            {
                data_area_str = SerialUtilModel.HexStrToHexStrReversed(ManualAsmb_DataPart, "");
            }
            else
            {
                data_area_str = SerialUtilModel.HexStrToHexStr(ManualAsmb_DataPart, "");
            }

            if (ManualAsmb_CtrlArea == "14" || ManualAsmb_CtrlArea == "04")
            {

                DLT645CoreModel.SendFrameData =
                    SerialUtilModel.HexStrToHexStrReversed(ManualAsmb_DataIdx4, "") +
                    SerialUtilModel.HexStrToHexStrReversed(ManualAsmb_Password, "") +
                    SerialUtilModel.HexStrToHexStrReversed(ManualAsmb_Operator, "") +
                    data_area_str;
            }
            else
            if (ManualAsmb_CtrlArea == "11" || ManualAsmb_CtrlArea == "91")
            {
                DLT645CoreModel.SendFrameData =
                    SerialUtilModel.HexStrToHexStrReversed(ManualAsmb_DataIdx4, "") +
                    data_area_str;
            }
            else
            if (ManualAsmb_CtrlArea == "01")
            {
                DLT645CoreModel.SendFrameData =
                    SerialUtilModel.HexStrToHexStrReversed(ManualAsmb_DataIdx4, "") +
                    data_area_str;
            }
            else
            {
                DLT645CoreModel.SendFrameData =
                    data_area_str;
            }
            return DLT645CoreModel.Asmb_Manual();
        }

        internal string AsmbToBeSendiPAC()
        {
            if (ManualAsmbiPAC_FrameType.Replace(" ", "").Length % 2 != 0 || ManualAsmbiPAC_CryptType.Replace(" ", "").Length % 2 != 0 || ManualAsmbiPAC_CryptData.Replace(" ", "").Length % 2 != 0)
            {
                return null;
            }

            iPAC2220ECoreModel.SendFrameType = ManualAsmbiPAC_FrameType;
            iPAC2220ECoreModel.SendCryptType = ManualAsmbiPAC_CryptType;
            iPAC2220ECoreModel.SendFrameData = ManualAsmbiPAC_CryptData;

            return iPAC2220ECoreModel.Asmb_Manual();
        }

        internal string DataRead(string DataIdStr, string Data)
        {
            return DLT645CoreModel.Asmb_ReadData(DataIdStr, Data);
        }

        internal string DataWrit(string DataIdStr, string Data)
        {
            return DLT645CoreModel.Asmb_WritData(DataIdStr, Global_Password, Global_Operator, Data);
        }

        internal string Asmb_ReadAddr()
        {
            return DLT645CoreModel.Asmb_ReadAddr();
        }

        internal string Asmb_WritAddr()
        {
            return DLT645CoreModel.Asmb_WritAddr();
        }

        internal string TimeCali_SysDateTime(DateTime DateTimeVal)
        {
            return DLT645CoreModel.Asmb_TimeCali(DateTimeVal, CaliTimeType == "给定时间", CaliAddrType == "给定地址", CaliMilliSecondType == "毫秒开启");
        }

        internal string TimeCali_UsrDateTime(DateTime DateTimeVal)
        {
            return DLT645CoreModel.Asmb_TimeCali_UsrDateTime(DateTimeVal, CaliAddrType == "广播校时", CaliMilliSecondType == "毫秒开启");
        }

        internal string DataCtrl()
        {
            return DLT645CoreModel.Asmb_DataCtrl();
        }

        internal string DataForz()
        {
            return DLT645CoreModel.Asmb_DataForz(FreezeCmdData);
        }

        internal string DataForz_Broad()
        {
            return DLT645CoreModel.Asmb_DataForz_Broadcast(FreezeCmdData);
        }

        internal string BaudRate_Alter()
        {
            return DLT645CoreModel.Asmb_BaudRate_Alter(Global_NewBaudRate);
        }

        internal string PassWord_Alter()
        {
            return DLT645CoreModel.Asmb_PassWord_Alter(Global_NewPasswordLevelDataId, Global_Password, Global_Operator, Global_NewPassword);
        }

        internal string DmndData_Clear()
        {
            return DLT645CoreModel.Asmb_DmndData_Clear(Global_Password, Global_Operator);
        }

        internal string MetrData_Clear()
        {
            return DLT645CoreModel.Asmb_MetrData_Clear(Global_Password, Global_Operator);
        }

        internal string EvntData_Clear()
        {
            return DLT645CoreModel.Asmb_EvntData_Clear(Global_Password, Global_Operator, EventsClearDataId);
        }

        internal string Calibrate_Measure_Pwr()
        {
            return ""; //DLT645CoreModel.Asmb_Calibrate_Measure_Pwr(0x01);
        }

        internal string Calibrate_Measure_Pha()
        {
            return ""; //DLT645CoreModel.Asmb_Calibrate_Measure_Pwr(0x02);
        }

        #endregion

        #region 工作区

        #region IDisposable Support
        private bool disposedValue = false;   /* 冗余检测 */

        /// <summary>
        /// 释放组件所使用的非托管资源，并且有选择的释放托管资源（可以看作是Dispose()的安全实现）
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            /* 检查是否已调用dispose */
            if (!disposedValue)
            {
                if (disposing)
                {
                    /* 释放托管资源（如果需要的话） */

                    /* SerialPort属于托管资源，但其本身却拥有非托管资源，所以需要实现IDisposable */
                    SerialPort.DataReceived -= SerialPortDataReceived;
                    SerialPort.Dispose();
                }

                /* 释放非托管资源（如果有的话） */

                disposedValue = true;   /* 处理完毕 */
            }
        }

        /// <summary>
        /// 实现IDisposable，释放组件所使用的所有资源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);   /* this: OSDA.ViewModels.MainWindowViewmodel */
        }
        #endregion

        private void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
#if true
            if ((SerialPort)sender == null)
            {
                return;
            }

            SerialPort _SerialPort = (SerialPort)sender;

            if (SerialPort.IsOpen)
            {
                int _BytesToRead = _SerialPort.BytesToRead;
                byte[] _RecvData = new byte[_BytesToRead];

                int length = _SerialPort.Read(_RecvData, 0, _BytesToRead);


                if (length <= 0)
                {
                    return;
                }

                //if (TimerModel.TimeStampEnable)
                {
                    if (isFirstPart)
                    {
                        DateTime _DateTime = DateTime.Now;
                        SerialRecvModel.RecvData = "";
                        //SerialRecvModel.RecvData.Append("[" + _DateTime.ToString("yyyy/MM/dd HH:mm:ss:fff", CultureInfos) + "]");
                    }
                }

                if (true)//(SerialRecvModel.HexRecv)
                {
                    foreach (var tmp in _RecvData)
                    {
                        SerialRecvModel.RecvData += string.Format(CultureInfos, "{0:X2} ", tmp);
                    }
                }
                else
                {
                    //SerialRecvModel.RecvData.Append(_SerialPort.Encoding.GetString(_RecvData));
                }

                if (isFirstPart)
                {
                    PortOneFrameTimerStart();
                }
                else
                {
                    OneFrameTimer.Stop();
                    OneFrameTimer.Start();
                }

                //if (TimerModel.TimeStampEnable)
                //{
                //    SerialRecvModel.RecvData.Append("\r\n");
                //}
            }

            //if (SerialPort.IsOpen && PortStat == "打开")
            //{
            //    PortCloz(1);
            //}
#endif
        }

        #endregion

        #region 中上栏

        #region 扩展

        #endregion

        #endregion

        string dlt645JsonConfigFile;
        string dlt645JsonConfigText;
        JsonNode dlt645JsonConfigRoot;

        public void InitializeComponent()
        {
            dlt645JsonConfigFile = "DLT645.config.json";
            dlt645JsonConfigText = File.ReadAllText(dlt645JsonConfigFile);
            dlt645JsonConfigRoot = JsonNode.Parse(dlt645JsonConfigText)!;

            JsonNode Json_AddressX;
            Json_AddressX = dlt645JsonConfigRoot?["ViewModel.LinkLayer.Address1"];
            if (Json_AddressX != null)
            {
                Global_CandidateAddr01 = Json_AddressX.ToString();
            }
            else
            {
                Global_CandidateAddr01 = "001500100001";
            }
            Json_AddressX = dlt645JsonConfigRoot?["ViewModel.LinkLayer.Address2"];
            if (Json_AddressX != null)
            {
                Global_CandidateAddr02 = Json_AddressX.ToString();
            }
            else
            {
                Global_CandidateAddr02 = "001500100002";
            }
            Json_AddressX = dlt645JsonConfigRoot?["ViewModel.LinkLayer.Address3"];
            if (Json_AddressX != null)
            {
                Global_CandidateAddr03 = Json_AddressX.ToString();
            }
            else
            {
                Global_CandidateAddr03 = "001500100003";
            }
            Json_AddressX = dlt645JsonConfigRoot?["ViewModel.LinkLayer.Address4"];
            if (Json_AddressX != null)
            {
                Global_CandidateAddr04 = Json_AddressX.ToString();
            }
            else
            {
                Global_CandidateAddr04 = "001500100004";
            }
            Json_AddressX = dlt645JsonConfigRoot?["ViewModel.LinkLayer.Address5"];
            if (Json_AddressX != null)
            {
                Global_CandidateAddr05 = Json_AddressX.ToString();
            }
            else
            {
                Global_CandidateAddr05 = "001500100005";
            }
            Json_AddressX = dlt645JsonConfigRoot?["ViewModel.LinkLayer.Address6"];
            if (Json_AddressX != null)
            {
                Global_CandidateAddr06 = Json_AddressX.ToString();
            }
            else
            {
                Global_CandidateAddr06 = "001500100006";
            }
            Json_AddressX = dlt645JsonConfigRoot?["ViewModel.LinkLayer.Address7"];
            if (Json_AddressX != null)
            {
                Global_CandidateAddr07 = Json_AddressX.ToString();
            }
            else
            {
                Global_CandidateAddr07 = "001500100007";
            }
            Json_AddressX = dlt645JsonConfigRoot?["ViewModel.LinkLayer.Address8"];
            if (Json_AddressX != null)
            {
                Global_CandidateAddr08 = Json_AddressX.ToString();
            }
            else
            {
                Global_CandidateAddr08 = "001500100008";
            }
            Json_AddressX = dlt645JsonConfigRoot?["ViewModel.LinkLayer.Address9"];
            if (Json_AddressX != null)
            {
                Global_CandidateAddr99 = Json_AddressX.ToString();
            }
            else
            {
                Global_CandidateAddr99 = "999999999999";
            }
            Json_AddressX = dlt645JsonConfigRoot?["ViewModel.LinkLayer.AddressA"];
            if (Json_AddressX != null)
            {
                Global_CandidateAddrAA = Json_AddressX.ToString();
            }
            else
            {
                Global_CandidateAddrAA = "AAAAAAAAAAAA";
            }

            JsonNode Json_Password = dlt645JsonConfigRoot?["ViewModel.LinkLayer.Password"];
            if (Json_Password != null)
            {
                Global_Password = Json_Password.ToString();
            }
            else
            {
                Global_Password = "00000000";
            }

            JsonNode Json_Operator = dlt645JsonConfigRoot?["ViewModel.LinkLayer.Operator"];
            if (Json_Operator != null)
            {
                Global_Operator = Json_Operator.ToString();
            }
            else
            {
                Global_Operator = "00000000";
            }

            Global_NewPassword = "78523900";
            Global_NewPasswordLevelItemsSource = new Collection<string>
            {
                "零级密码", "一级密码", "二级密码", "三级密码", "四级密码", "五级密码", "六级密码", "七级密码", "八级密码", "九级密码"
            };
            Global_NewPasswordLevel = "零级密码";
            Global_NewPasswordLevelDataId = "04000C01";

            Global_NewBaudRateItemsSource = new Collection<int>
            {
                1200, 2400, 4800, 7200, 9600, 14400, 19200, 28800, 38400, 57600, 115200, 128000, 153600, 230400, 256000
            };
            Global_NewBaudRate = 9600;

            Global_DataIdx4 = "0400010C";
            Global_DataArea = "";

            ManualAsmb_ToBeSend = "";
            ManualAsmb_AddrArea = "AAAAAAAAAAAA";
            ManualAsmb_CtrlArea = "11";
            ManualAsmb_DataIdx4 = "04000401";
            ManualAsmb_Password = "00000000";
            ManualAsmb_Operator = "00000000";
            ManualAsmb_DataPart = "";

            ManualAsmbiPAC_CryptType = "82";
            ManualAsmbiPAC_FrameType = "01";

            CaliMilliSecondType = "毫秒关闭";
            CaliAddrType = "广播地址";
            CaliTimeType = "系统时间";
            DataReverseType = "逆序";
            FreezeCmdData = "99999999";
            EventsClearDataId = "FFFFFFFF";
        }
    }
}
