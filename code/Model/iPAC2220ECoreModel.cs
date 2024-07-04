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
    internal class iPAC2220ECoreModel : ObservableObject
    {
        public iPAC2220ECoreModel()
        {
        }

        private string _SendFrameType = String.Empty;
        public string SendFrameType
        {
            get
            {
                return _SendFrameType;
            }
            set
            {
                if (_SendFrameType != value)
                {
                    _SendFrameType = value;
                    RaisePropertyChanged(nameof(SendFrameType));
                }
            }
        }

        private string _SendCryptType = String.Empty;
        public string SendCryptType
        {
            get
            {
                return _SendCryptType;
            }
            set
            {
                if (_SendCryptType != value)
                {
                    _SendCryptType = value;
                    RaisePropertyChanged(nameof(SendCryptType));
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

        public string Asmb()
        {
            byte[] HexArr = SerialUtilModel.HexStrToHexArr(SendFrameData);
            string len = (HexArr.Length + 2).ToString("X4");
            string dat = SerialUtilModel.HexArrToHexStr(HexArr);

            string sta = SerialUtilModel.StringFormat("EB" + len + "EB" + SendCryptType + SendFrameType + dat);
            string sum = SerialUtilModel.CheckSum08(SerialUtilModel.HexStrToHexArr(sta)).ToString("X2");
            string str = SerialUtilModel.StringFormat(sta + sum);
            return str;
        }

        public string Asmb_Manual()
        {
            return Asmb();
        }

        public void InitializeComponent()
        {

        }
    }
}
