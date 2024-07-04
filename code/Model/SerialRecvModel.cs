using DLT645.BasicMVVM;
using DLT645.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLT645.Model
{
    internal class SerialRecvModel : ObservableObject
    {
        private string _RecvData;
        public string RecvData
        {
            get
            {
                return _RecvData;
            }
            set
            {
                if (_RecvData != value)
                {
                    _RecvData = value;
                    RaisePropertyChanged(nameof(RecvData));
                }
            }
        }

        public void InitializeComponent()
        {
        }
    }
}
