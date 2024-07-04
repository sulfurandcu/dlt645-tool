using DLT645.BasicMVVM;
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

namespace About
{
    internal class AboutWindowViewModel : ObservableObject
    {

        public AboutWindowViewModel()
        {

        }

        private string _PrintInfo;
        public string PrintInfo
        {
            get
            {
                return _PrintInfo;
            }
            set
            {
                if (_PrintInfo != value)
                {
                    _PrintInfo = value;
                    RaisePropertyChanged(nameof(PrintInfo));
                }
            }
        }

        public void InitializeComponent()
        {
            PrintInfo = "";
        }
    }
}
