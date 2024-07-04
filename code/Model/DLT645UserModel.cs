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
    internal class DLT645UserModel : ObservableObject
    {
        private string _DataId = String.Empty;
        public string DataId
        {
            get
            {
                return _DataId;
            }
            set
            {
                if (_DataId != value)
                {
                    _DataId = value;
                    RaisePropertyChanged(nameof(DataId));
                }
            }
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

        public string DLT645_Rspd_ReadAddr(string data)
        {
            CandidateAddr = SerialUtilModel.HexStrToHexStrReversed(data, "");
            return SerialUtilModel.HexStrToHexStrReversed(data, " ");
        }

        public string DLT645_Rspd_ReadData(string data)
        {
            return "";
        }

        public void InitializeComponent()
        {

        }
    }
}
