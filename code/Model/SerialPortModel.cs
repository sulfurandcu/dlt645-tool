using DLT645.BasicMVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace DLT645.Model
{
    internal class SerialPortModel : ObservableObject
    {
        public Collection<int> BaudRateItemsSource { get; set; }
        public Collection<int> DataBitsItemsSource { get; set; }
        public Collection<StopBits> StopBitsItemsSource { get; set; }
        public Collection<Parity> PariTypeItemsSource { get; set; }

        private string[] _PortNameItemsSource;

        public string[] PortNameItemsSource
        {
            get
            {
                return _PortNameItemsSource;
            }
            set
            {
                if (_PortNameItemsSource != value)
                {
                    _PortNameItemsSource = value;
                    RaisePropertyChanged(nameof(PortNameItemsSource));
                }
            }
        }

        private string _PortName;

        public string PortName
        {
            get
            {
                return _PortName;
            }
            set
            {
                if (_PortName != value)
                {
                    _PortName = value;
                    RaisePropertyChanged(nameof(PortName));
                    if (dlt645JsonConfigRoot != null)
                    {
                        if (dlt645JsonConfigRoot?["ViewModel.SerialPort.PortName"] != null)
                        {
                            dlt645JsonConfigRoot?["ViewModel.SerialPort.PortName"].ReplaceWith<string>(value);
                            File.WriteAllText(dlt645JsonConfigFile, dlt645JsonConfigRoot.ToString());
                        }
                    }
                }
            }
        }

        private int _BaudRate;

        public int BaudRate
        {
            get
            {
                return _BaudRate;
            }
            set
            {
                if (_BaudRate != value)
                {
                    _BaudRate = value;
                    RaisePropertyChanged(nameof(BaudRate));
                    if (dlt645JsonConfigRoot != null)
                    {
                        if (dlt645JsonConfigRoot?["ViewModel.SerialPort.BaudRate"] != null)
                        {
                            dlt645JsonConfigRoot?["ViewModel.SerialPort.BaudRate"].ReplaceWith<int>(value);
                            File.WriteAllText(dlt645JsonConfigFile, dlt645JsonConfigRoot.ToString());
                        }
                    }
                }
            }
        }

        private int _DataBits;

        public int DataBits
        {
            get
            {
                return _DataBits;
            }
            set
            {
                if (_DataBits != value)
                {
                    _DataBits = value;
                    RaisePropertyChanged(nameof(DataBits));
                    if (dlt645JsonConfigRoot != null)
                    {
                        if (dlt645JsonConfigRoot?["ViewModel.SerialPort.DataBits"] != null)
                        {
                            dlt645JsonConfigRoot?["ViewModel.SerialPort.DataBits"].ReplaceWith<int>(value);
                            File.WriteAllText(dlt645JsonConfigFile, dlt645JsonConfigRoot.ToString());
                        }
                    }
                }
            }
        }

        private StopBits _StopBits;

        public StopBits StopBits
        {
            get
            {
                return _StopBits;
            }
            set
            {
                if (_StopBits != value)
                {
                    _StopBits = value;
                    RaisePropertyChanged(nameof(StopBits));
                    if (dlt645JsonConfigRoot != null)
                    {
                        if (dlt645JsonConfigRoot?["ViewModel.SerialPort.StopBits"] != null)
                        {
                            dlt645JsonConfigRoot?["ViewModel.SerialPort.StopBits"].ReplaceWith<int>((int)value);
                            File.WriteAllText(dlt645JsonConfigFile, dlt645JsonConfigRoot.ToString());
                        }
                    }
                }
            }
        }

        private Parity _PariType;

        public Parity PariType
        {
            get
            {
                return _PariType;
            }
            set
            {
                if (_PariType != value)
                {
                    _PariType = value;
                    RaisePropertyChanged(nameof(PariType));
                    if (dlt645JsonConfigRoot != null)
                    {
                        if (dlt645JsonConfigRoot?["ViewModel.SerialPort.Parity"] != null)
                        {
                            dlt645JsonConfigRoot?["ViewModel.SerialPort.Parity"].ReplaceWith<int>((int)value);
                            File.WriteAllText(dlt645JsonConfigFile, dlt645JsonConfigRoot.ToString());
                        }
                    }
                }
            }
        }

        private bool _PortNameEnable;

        public bool PortNameEnable
        {
            get
            {
                return _PortNameEnable;
            }
            set
            {
                if (_PortNameEnable != value)
                {
                    _PortNameEnable = value;
                    RaisePropertyChanged(nameof(PortNameEnable));
                }
            }
        }

        private bool _BaudRateEnable;

        public bool BaudRateEnable
        {
            get
            {
                return _BaudRateEnable;
            }
            set
            {
                if (_BaudRateEnable != value)
                {
                    _BaudRateEnable = value;
                    RaisePropertyChanged(nameof(BaudRateEnable));
                }
            }
        }

        private bool _DataBitsEnable;

        public bool DataBitsEnable
        {
            get
            {
                return _DataBitsEnable;
            }
            set
            {
                if (_DataBitsEnable != value)
                {
                    _DataBitsEnable = value;
                    RaisePropertyChanged(nameof(DataBitsEnable));
                }
            }
        }

        private bool _StopBitsEnable;

        public bool StopBitsEnable
        {
            get
            {
                return _StopBitsEnable;
            }
            set
            {
                if (_StopBitsEnable != value)
                {
                    _StopBitsEnable = value;
                    RaisePropertyChanged(nameof(StopBitsEnable));
                }
            }
        }

        private bool _PariTypeEnable;

        public bool PariTypeEnable
        {
            get
            {
                return _PariTypeEnable;
            }
            set
            {
                if (_PariTypeEnable != value)
                {
                    _PariTypeEnable = value;
                    RaisePropertyChanged(nameof(PariTypeEnable));
                }
            }
        }

        private string _PortStat;

        public string PortStat
        {
            get
            {
                return _PortStat;
            }
            set
            {
                if (_PortStat != value)
                {
                    _PortStat = value;
                    RaisePropertyChanged(nameof(PortStat));
                }
            }
        }

        string dlt645JsonConfigFile;
        string dlt645JsonConfigText;
        JsonNode dlt645JsonConfigRoot;

        public void InitializeComponent()
        {
            PortNameItemsSource = SerialPort.GetPortNames();

            BaudRateItemsSource = new Collection<int>
            {
                1200, 2400, 4800, 7200, 9600, 14400, 19200, 28800, 38400, 57600, 115200, 128000, 153600, 230400, 256000
            };

            DataBitsItemsSource = new Collection<int>
            {
                5, 6, 7, 8
            };

            StopBitsItemsSource = new Collection<StopBits>
            {
                StopBits.One, StopBits.Two, StopBits.OnePointFive
            };

            PariTypeItemsSource = new Collection<Parity>
            {
                Parity.None, Parity.Odd, Parity.Even, Parity.Mark, Parity.Space
            };

            dlt645JsonConfigFile = "DLT645.config.json";
            dlt645JsonConfigText = File.ReadAllText(dlt645JsonConfigFile);
            dlt645JsonConfigRoot = JsonNode.Parse(dlt645JsonConfigText)!;

            PortStat = "打开";
            PortNameEnable = true;
            BaudRateEnable = true;
            DataBitsEnable = true;
            StopBitsEnable = true;
            PariTypeEnable = true;

            JsonNode Json_SerialPortName = dlt645JsonConfigRoot?["ViewModel.SerialPort.PortName"];
            if (Json_SerialPortName != null)
            {
                PortName = Json_SerialPortName.ToString();
            }
            else
            {
                if (PortNameItemsSource.Length > 0)
                {
                    PortName = PortNameItemsSource[0];
                }
            }

            JsonNode Json_SerialBaudRate = dlt645JsonConfigRoot?["ViewModel.SerialPort.BaudRate"];
            if (Json_SerialBaudRate != null)
            {
                BaudRate = Json_SerialBaudRate.GetValue<int>();
            }
            else
            {
                BaudRate = 115200;
            }

            JsonNode Json_SerialDataBits = dlt645JsonConfigRoot?["ViewModel.SerialPort.DataBits"];
            if (Json_SerialDataBits != null)
            {
                DataBits = Json_SerialDataBits.GetValue<int>();
            }
            else
            {
                DataBits = 8;
            }

            JsonNode Json_SerialStopBits = dlt645JsonConfigRoot?["ViewModel.SerialPort.StopBits"];
            if (Json_SerialStopBits != null)
            {
                StopBits = (StopBits)Json_SerialStopBits.GetValue<int>();
            }
            else
            {
                StopBits = StopBits.One;
            }

            JsonNode Json_SerialParity = dlt645JsonConfigRoot?["ViewModel.SerialPort.Parity"];
            if (Json_SerialParity != null)
            {
                PariType = (Parity)Json_SerialParity.GetValue<int>();
            }
            else
            {
                PariType = Parity.Even;
            }
        }

        public void SerialPort_RefreshPortNameItemsSource()
        {
            PortNameItemsSource = SerialPort.GetPortNames();
        }
    }
}
