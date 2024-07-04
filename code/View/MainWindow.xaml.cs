using DLT645.Interface;
using DLT645.Model;
using DLT645.ViewModel;
using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Text.Json.Nodes;
using DLT645.BasicMVVM;
using About;

namespace DLT645.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal MainWindowViewModel ObjectMainWindowViewModel;

        private MenuItem[] menuItems = new MenuItem[8];

        public MainWindow()
        {
            InitializeComponent();

            ObjectMainWindowViewModel = new MainWindowViewModel();
            DataContext = ObjectMainWindowViewModel;
            ObjectMainWindowViewModel.InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            Timer_Tick(new object(), new EventArgs());


            string dlt645JsonConfigFile;
            string dlt645JsonConfigText;
            JsonNode dlt645JsonConfigRoot;
            dlt645JsonConfigFile = "DLT645.config.json";
            dlt645JsonConfigText = File.ReadAllText(dlt645JsonConfigFile);
            dlt645JsonConfigRoot = JsonNode.Parse(dlt645JsonConfigText)!;

            try
            {
                string dlt645JsonDataIdConfigFile;
                string dlt645JsonDataIdConfigText;
                JsonNode dlt645JsonDataIdConfigRoot;
                JsonArray dlt645JsonDataIdConfigArray;
                MenuItem dlt645JsonDataIdConfigMenu;
                MenuItem dlt645JsonDataIdConfigMenuChild;
                JsonArray nodeProtocolArray;

                // 参数
                dlt645JsonDataIdConfigFile = "dlt645_table_parameter.json";
                dlt645JsonDataIdConfigText = File.ReadAllText(dlt645JsonDataIdConfigFile);
                dlt645JsonDataIdConfigRoot = JsonNode.Parse(dlt645JsonDataIdConfigText)!;
                dlt645JsonDataIdConfigMenu = new MenuItem() { Id = "XXXXXXXX", Name = "你可以在对应的JSON文件中配置该列表                              " };
                nodeProtocolArray = dlt645JsonConfigRoot["Protocol"]!.AsArray();
                foreach (JsonNode? nodeProtocol in nodeProtocolArray)
                {
                    string array = nodeProtocol.GetValue<string>() ?? string.Empty;
                    if (dlt645JsonDataIdConfigRoot?[array] is not JsonNode nodeArray)
                    {
                        continue;
                    }
                    dlt645JsonDataIdConfigArray = dlt645JsonDataIdConfigRoot[array]!.AsArray();
                    dlt645JsonDataIdConfigMenuChild = new MenuItem() { Id = array, Name = "                                                             " };
                    foreach (JsonNode? item in dlt645JsonDataIdConfigArray)
                    {
                        if (item?["id"] is JsonNode idNode)
                        {
                            if (item?["name"] is JsonNode nameNode)
                            {
                                dlt645JsonDataIdConfigMenuChild.Items.Add(new MenuItem() { Id = (string)idNode, Name = (string)nameNode });
                            }
                        }
                    }
                    dlt645JsonDataIdConfigMenu.Items.Add(dlt645JsonDataIdConfigMenuChild);
                }
                treeParameter.Items.Add(dlt645JsonDataIdConfigMenu);

                // 变量
                dlt645JsonDataIdConfigFile = "dlt645_table_electric.json";
                dlt645JsonDataIdConfigText = File.ReadAllText(dlt645JsonDataIdConfigFile);
                dlt645JsonDataIdConfigRoot = JsonNode.Parse(dlt645JsonDataIdConfigText)!;
                dlt645JsonDataIdConfigMenu = new MenuItem() { Id = "XXXXXXXX", Name = "你可以在对应的JSON文件中配置该列表                              " };
                nodeProtocolArray = dlt645JsonConfigRoot["Protocol"]!.AsArray();
                foreach (JsonNode? nodeProtocol in nodeProtocolArray)
                {
                    string array = nodeProtocol.GetValue<string>() ?? string.Empty;
                    if (dlt645JsonDataIdConfigRoot?[array] is not JsonNode nodeArray)
                    {
                        continue;
                    }
                    dlt645JsonDataIdConfigArray = dlt645JsonDataIdConfigRoot[array]!.AsArray();
                    dlt645JsonDataIdConfigMenuChild = new MenuItem() { Id = array, Name = "                                                             " };
                    foreach (JsonNode? item in dlt645JsonDataIdConfigArray)
                    {
                        if (item?["id"] is JsonNode idNode)
                        {
                            if (item?["name"] is JsonNode nameNode)
                            {
                                dlt645JsonDataIdConfigMenuChild.Items.Add(new MenuItem() { Id = (string)idNode, Name = (string)nameNode });
                            }
                        }
                    }
                    dlt645JsonDataIdConfigMenu.Items.Add(dlt645JsonDataIdConfigMenuChild);
                }
                treeElectric.Items.Add(dlt645JsonDataIdConfigMenu);

                // 电能
                dlt645JsonDataIdConfigFile = "dlt645_table_energy.json";
                dlt645JsonDataIdConfigText = File.ReadAllText(dlt645JsonDataIdConfigFile);
                dlt645JsonDataIdConfigRoot = JsonNode.Parse(dlt645JsonDataIdConfigText)!;
                dlt645JsonDataIdConfigMenu = new MenuItem() { Id = "XXXXXXXX", Name = "你可以在对应的JSON文件中配置该列表                              " };
                nodeProtocolArray = dlt645JsonConfigRoot["Protocol"]!.AsArray();
                foreach (JsonNode? nodeProtocol in nodeProtocolArray)
                {
                    string array = nodeProtocol.GetValue<string>() ?? string.Empty;
                    if (dlt645JsonDataIdConfigRoot?[array] is not JsonNode nodeArray)
                    {
                        continue;
                    }
                    dlt645JsonDataIdConfigArray = dlt645JsonDataIdConfigRoot[array]!.AsArray();
                    dlt645JsonDataIdConfigMenuChild = new MenuItem() { Id = array, Name = "                                                             " };
                    foreach (JsonNode? item in dlt645JsonDataIdConfigArray)
                    {
                        if (item?["id"] is JsonNode idNode)
                        {
                            if (item?["name"] is JsonNode nameNode)
                            {
                                dlt645JsonDataIdConfigMenuChild.Items.Add(new MenuItem() { Id = (string)idNode, Name = (string)nameNode });
                            }
                        }
                    }
                    dlt645JsonDataIdConfigMenu.Items.Add(dlt645JsonDataIdConfigMenuChild);
                }
                treeEnergy.Items.Add(dlt645JsonDataIdConfigMenu);

                // 冻结
                dlt645JsonDataIdConfigFile = "dlt645_table_record.json";
                dlt645JsonDataIdConfigText = File.ReadAllText(dlt645JsonDataIdConfigFile);
                dlt645JsonDataIdConfigRoot = JsonNode.Parse(dlt645JsonDataIdConfigText)!;
                dlt645JsonDataIdConfigMenu = new MenuItem() { Id = "XXXXXXXX", Name = "你可以在对应的JSON文件中配置该列表                              " };
                nodeProtocolArray = dlt645JsonConfigRoot["Protocol"]!.AsArray();
                foreach (JsonNode? nodeProtocol in nodeProtocolArray)
                {
                    string array = nodeProtocol.GetValue<string>() ?? string.Empty;
                    if (dlt645JsonDataIdConfigRoot?[array] is not JsonNode nodeArray)
                    {
                        continue;
                    }
                    dlt645JsonDataIdConfigArray = dlt645JsonDataIdConfigRoot[array]!.AsArray();
                    dlt645JsonDataIdConfigMenuChild = new MenuItem() { Id = array, Name = "                                                             " };
                    foreach (JsonNode? item in dlt645JsonDataIdConfigArray)
                    {
                        if (item?["id"] is JsonNode idNode)
                        {
                            if (item?["name"] is JsonNode nameNode)
                            {
                                dlt645JsonDataIdConfigMenuChild.Items.Add(new MenuItem() { Id = (string)idNode, Name = (string)nameNode });
                            }
                        }
                    }
                    dlt645JsonDataIdConfigMenu.Items.Add(dlt645JsonDataIdConfigMenuChild);
                }
                treeRecord.Items.Add(dlt645JsonDataIdConfigMenu);

                // 事件
                dlt645JsonDataIdConfigFile = "dlt645_table_events.json";
                dlt645JsonDataIdConfigText = File.ReadAllText(dlt645JsonDataIdConfigFile);
                dlt645JsonDataIdConfigRoot = JsonNode.Parse(dlt645JsonDataIdConfigText)!;
                dlt645JsonDataIdConfigMenu = new MenuItem() { Id = "XXXXXXXX", Name = "你可以在对应的JSON文件中配置该列表                              " };
                nodeProtocolArray = dlt645JsonConfigRoot["Protocol"]!.AsArray();
                foreach (JsonNode? nodeProtocol in nodeProtocolArray)
                {
                    string array = nodeProtocol.GetValue<string>() ?? string.Empty;
                    if (dlt645JsonDataIdConfigRoot?[array] is not JsonNode nodeArray)
                    {
                        continue;
                    }
                    dlt645JsonDataIdConfigArray = dlt645JsonDataIdConfigRoot[array]!.AsArray();
                    dlt645JsonDataIdConfigMenuChild = new MenuItem() { Id = array, Name = "                                                             " };
                    foreach (JsonNode? item in dlt645JsonDataIdConfigArray)
                    {
                        if (item?["id"] is JsonNode idNode)
                        {
                            if (item?["name"] is JsonNode nameNode)
                            {
                                dlt645JsonDataIdConfigMenuChild.Items.Add(new MenuItem() { Id = (string)idNode, Name = (string)nameNode });
                            }
                        }
                    }
                    dlt645JsonDataIdConfigMenu.Items.Add(dlt645JsonDataIdConfigMenuChild);
                }
                treeEvents.Items.Add(dlt645JsonDataIdConfigMenu);

                // 其他
                dlt645JsonDataIdConfigFile = "dlt645_table_others.json";
                dlt645JsonDataIdConfigText = File.ReadAllText(dlt645JsonDataIdConfigFile);
                dlt645JsonDataIdConfigRoot = JsonNode.Parse(dlt645JsonDataIdConfigText)!;
                dlt645JsonDataIdConfigMenu = new MenuItem() { Id = "XXXXXXXX", Name = "你可以在对应的JSON文件中配置该列表                              " };
                nodeProtocolArray = dlt645JsonConfigRoot["Protocol"]!.AsArray();
                foreach (JsonNode? nodeProtocol in nodeProtocolArray)
                {
                    string array = nodeProtocol.GetValue<string>() ?? string.Empty;
                    if (dlt645JsonDataIdConfigRoot?[array] is not JsonNode nodeArray)
                    {
                        continue;
                    }
                    dlt645JsonDataIdConfigArray = dlt645JsonDataIdConfigRoot[array]!.AsArray();
                    dlt645JsonDataIdConfigMenuChild = new MenuItem() { Id = array, Name = "                                                             " };
                    foreach (JsonNode? item in dlt645JsonDataIdConfigArray)
                    {
                        if (item?["id"] is JsonNode idNode)
                        {
                            if (item?["name"] is JsonNode nameNode)
                            {
                                dlt645JsonDataIdConfigMenuChild.Items.Add(new MenuItem() { Id = (string)idNode, Name = (string)nameNode });
                            }
                        }
                    }
                    dlt645JsonDataIdConfigMenu.Items.Add(dlt645JsonDataIdConfigMenuChild);
                }
                treeOthers.Items.Add(dlt645JsonDataIdConfigMenu);

                // 扩展
                dlt645JsonDataIdConfigFile = "dlt645_table_extend.json";
                dlt645JsonDataIdConfigText = File.ReadAllText(dlt645JsonDataIdConfigFile);
                dlt645JsonDataIdConfigRoot = JsonNode.Parse(dlt645JsonDataIdConfigText)!;
                dlt645JsonDataIdConfigMenu = new MenuItem() { Id = "XXXXXXXX", Name = "你可以在对应的JSON文件中配置该列表                              " };
                nodeProtocolArray = dlt645JsonConfigRoot["Protocol"]!.AsArray();
                foreach (JsonNode? nodeProtocol in nodeProtocolArray)
                {
                    string array = nodeProtocol.GetValue<string>() ?? string.Empty;
                    if (dlt645JsonDataIdConfigRoot?[array] is not JsonNode nodeArray)
                    {
                        continue;
                    }
                    dlt645JsonDataIdConfigArray = dlt645JsonDataIdConfigRoot[array]!.AsArray();
                    dlt645JsonDataIdConfigMenuChild = new MenuItem() { Id = array, Name = "                                                             " };
                    foreach (JsonNode? item in dlt645JsonDataIdConfigArray)
                    {
                        if (item?["id"] is JsonNode idNode)
                        {
                            if (item?["name"] is JsonNode nameNode)
                            {
                                dlt645JsonDataIdConfigMenuChild.Items.Add(new MenuItem() { Id = (string)idNode, Name = (string)nameNode });
                            }
                        }
                    }
                    dlt645JsonDataIdConfigMenu.Items.Add(dlt645JsonDataIdConfigMenuChild);
                }
                treeExtend.Items.Add(dlt645JsonDataIdConfigMenu);
            }
            catch
            {
            }

            EpochDateTimePickerUTC.Value = DateTime.UtcNow;
            EpochDateTimePickerLocal.Value = DateTime.Now;
        }

        public class MenuItem : ObservableObject
        {
            public MenuItem()
            {
                this.Items = new ObservableCollection<MenuItem>();
            }

            public string Id { get; set; }
            private string _index;
            public string Index 
            {
                get
                {
                    return _index;
                }
                set
                {
                    if (_index != value)
                    {
                        _index = value;
                        RaisePropertyChanged(nameof(Index));

                        try 
                        {
                            if (Index == null)
                            {
                                Id = Id.Substring(0, 6) + "00";
                            }
                            else
                            if (value.Length < 2)
                            {
                                Id = Id.Substring(0, 6) + "0" + Index.Substring(0, 1);
                            }
                            else
                            {
                                Id = Id.Substring(0, 6) + Index.Substring(0, 2);
                            }
                            RaisePropertyChanged(nameof(Id));
                        }
                        catch { }
                    }
                }
            }
            public string Name { get; set; }
            private string _DataGet;
            public string DataGet
            {
                get
                {
                    return _DataGet;
                }
                set
                {
                    if (_DataGet != value)
                    {
                        _DataGet = value;
                        RaisePropertyChanged(nameof(DataGet));
                    }
                }
            }

            private string _DataSet;
            public string DataSet
            {
                get
                {
                    return _DataSet;
                }
                set
                {
                    if (_DataSet != value)
                    {
                        _DataSet = value;
                        RaisePropertyChanged(nameof(DataSet));
                    }
                }
            }

            public ObservableCollection<MenuItem> Items { get; set; }
        }

        private async void MainWindow_TreeView(object sender, RoutedEventArgs e)
        {
            TextBlock text = sender as TextBlock;
            string str = ObjectMainWindowViewModel.DataRead(text.Text, null);

            //Button btn = sender as Button;
            //string str = ObjectMainWindowViewModel.DataRead(btn.Content.ToString(), null);

            await ObjectMainWindowViewModel.PortSend(str).ConfigureAwait(false);
        }

        private async void MainWindow_TreeViewSet(object sender, RoutedEventArgs e)
        {
            TextBlock text = sender as TextBlock;
            string dataid = text.Text.Substring(0, 8);
            if (text.Text.Length < 9)
            {
                return;
            }
            string data = text.Text.Substring(9);
            string str = ObjectMainWindowViewModel.DataWrit(dataid, data);

            await ObjectMainWindowViewModel.PortSend(str).ConfigureAwait(false);
        }

        private async void MainWindow_TreeViewGet(object sender, RoutedEventArgs e)
        {
            TextBlock text = sender as TextBlock;
            string dataid = text.Text.Substring(0, 8);
            if (text.Text.Length < 9)
            {
                return;
            }
            string data = text.Text.Substring(9);
            string str = ObjectMainWindowViewModel.DataRead(dataid, data);

            await ObjectMainWindowViewModel.PortSend(str).ConfigureAwait(false);
        }

        private void MainWindow_TreeViewGetParse(object sender, DataTransferEventArgs e)
        {
            try
            {
                TextBlock text = sender as TextBlock;
                string dataid = text.Text.Substring(0, 8);
                if (text.Text.Length < 9)
                {
                    return;
                }
                string data = text.Text.Substring(9);
                if (data.Length % 2 != 0)
                {
                    return;
                }
                string str = ObjectMainWindowViewModel.DataRead(dataid, data);
                str = SerialUtilModel.HexStrToHexStr(str, " ") + "\n";

                ObjectMainWindowViewModel.ParsInfo = "";
                bool ret = ObjectMainWindowViewModel.DLT645CoreModel.Parse(str);
                ObjectMainWindowViewModel.ParsInfo += "当前帧：" + str;
                ObjectMainWindowViewModel.ParsInfo += "地址域：" + SerialUtilModel.HexStrToHexStr(ObjectMainWindowViewModel.DLT645CoreModel.RecvFrameAddr, " ") + "\n";
                ObjectMainWindowViewModel.ParsInfo += "控制码：" + SerialUtilModel.HexStrToHexStr(ObjectMainWindowViewModel.DLT645CoreModel.RecvFrameCtrl, " ") + "\n";
                //ParsInfo += "校验码：" + SerialUtilModel.HexStrToHexStr(DLT645CoreModel.RecvFrameSum8, " ") + "\n";
                int len = Convert.ToInt32("0x" + ObjectMainWindowViewModel.DLT645CoreModel.RecvFrameSize, 16);
                ObjectMainWindowViewModel.ParsInfo += "长度域：" + SerialUtilModel.HexStrToHexStr(ObjectMainWindowViewModel.DLT645CoreModel.RecvFrameSize, " ");
                int ctrl = Convert.ToInt32("0x" + ObjectMainWindowViewModel.DLT645CoreModel.RecvFrameCtrl, 16);
                if (ctrl == 0x11 || ctrl == 0x91 || ctrl == 0x12 || ctrl == 0x92)
                {
                    ObjectMainWindowViewModel.ParsInfo += "(" + len + "=" + "4+" + (len - 4) + ")";
                }
                else
                if (ctrl == 0x14)
                {
                    ObjectMainWindowViewModel.ParsInfo += "(" + len + "=" + "4+4+4+" + (len - 12) + ")";
                }
                else
                {
                    ObjectMainWindowViewModel.ParsInfo += "(" + len + ")";
                }
                ObjectMainWindowViewModel.ParsInfo += "\n";
                //ParsInfo += "数据域：" + SerialUtilModel.HexStrToHexStr(DLT645CoreModel.RecvFrameData, " ") + "\n";
                if (ret)
                {
                    ObjectMainWindowViewModel.DLT645CoreModel.ParseData();
                }
                ObjectMainWindowViewModel.ParsInfo += ObjectMainWindowViewModel.DLT645CoreModel.RecvFrameInfo;
            }
            catch { }
        }

        private void MainWindow_TreeViewSetParse(object sender, DataTransferEventArgs e)
        {
            try
            {
                TextBlock text = sender as TextBlock;
                string dataid = text.Text.Substring(0, 8);
                if (text.Text.Length < 9)
                {
                    return;
                }
                string data = text.Text.Substring(9);
                if (data.Length % 2 != 0)
                {
                    return;
                }
                string str = ObjectMainWindowViewModel.DataWrit(dataid, data);
                str = SerialUtilModel.HexStrToHexStr(str, " ") + "\n";

                ObjectMainWindowViewModel.ParsInfo = "";
                bool ret = ObjectMainWindowViewModel.DLT645CoreModel.Parse(str);
                ObjectMainWindowViewModel.ParsInfo += "当前帧：" + str;
                ObjectMainWindowViewModel.ParsInfo += "地址域：" + SerialUtilModel.HexStrToHexStr(ObjectMainWindowViewModel.DLT645CoreModel.RecvFrameAddr, " ") + "\n";
                ObjectMainWindowViewModel.ParsInfo += "控制码：" + SerialUtilModel.HexStrToHexStr(ObjectMainWindowViewModel.DLT645CoreModel.RecvFrameCtrl, " ") + "\n";
                //ParsInfo += "校验码：" + SerialUtilModel.HexStrToHexStr(DLT645CoreModel.RecvFrameSum8, " ") + "\n";
                int len = Convert.ToInt32("0x" + ObjectMainWindowViewModel.DLT645CoreModel.RecvFrameSize, 16);
                ObjectMainWindowViewModel.ParsInfo += "长度域：" + SerialUtilModel.HexStrToHexStr(ObjectMainWindowViewModel.DLT645CoreModel.RecvFrameSize, " ");
                int ctrl = Convert.ToInt32("0x" + ObjectMainWindowViewModel.DLT645CoreModel.RecvFrameCtrl, 16);
                if (ctrl == 0x11 || ctrl == 0x91 || ctrl == 0x12 || ctrl == 0x92)
                {
                    ObjectMainWindowViewModel.ParsInfo += "(" + len + "=" + "4+" + (len - 4) + ")";
                }
                else
                if (ctrl == 0x14)
                {
                    ObjectMainWindowViewModel.ParsInfo += "(" + len + "=" + "4+4+4+" + (len - 12) + ")";
                }
                else
                {
                    ObjectMainWindowViewModel.ParsInfo += "(" + len + ")";
                }
                ObjectMainWindowViewModel.ParsInfo += "\n";
                //ParsInfo += "数据域：" + SerialUtilModel.HexStrToHexStr(DLT645CoreModel.RecvFrameData, " ") + "\n";
                if (ret)
                {
                    ObjectMainWindowViewModel.DLT645CoreModel.ParseData();
                }
                ObjectMainWindowViewModel.ParsInfo += ObjectMainWindowViewModel.DLT645CoreModel.RecvFrameInfo;
            }
            catch { }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            ObjectMainWindowViewModel.ClockTimerUTC_0 = DateTime.Now.ToUniversalTime().ToString() + " UTC";
            ObjectMainWindowViewModel.ClockTimerUTC_8 = DateTime.Now.ToLocalTime().ToString() + " UTC+8";
            ObjectMainWindowViewModel.ClockTimerStamp = "0x" + new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString("X8") + "  " + new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();
        }

        private void MainWindow_AppRestart(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            System.Windows.Forms.Application.Restart();
        }

        private void MainWindow_AppExit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MainWindow_PortName(object sender, MouseButtonEventArgs e)
        {
            ObjectMainWindowViewModel.RefreshPortName();
        }

        private void MainWindow_PortAble(object sender, RoutedEventArgs e)
        {
            ObjectMainWindowViewModel.PortAble();
        }

        private void MainWindow_CandidateAddrAA(object sender, RoutedEventArgs e)
        {
            ObjectMainWindowViewModel.DLT645CoreModel.CandidateAddr = ObjectMainWindowViewModel.Global_CandidateAddrAA;
        }

        private void MainWindow_CandidateAddr99(object sender, RoutedEventArgs e)
        {
            ObjectMainWindowViewModel.DLT645CoreModel.CandidateAddr = ObjectMainWindowViewModel.Global_CandidateAddr99;
        }

        private void MainWindow_CandidateAddr01(object sender, RoutedEventArgs e)
        {
            ObjectMainWindowViewModel.DLT645CoreModel.CandidateAddr = ObjectMainWindowViewModel.Global_CandidateAddr01;
        }

        private void MainWindow_CandidateAddr02(object sender, RoutedEventArgs e)
        {
            ObjectMainWindowViewModel.DLT645CoreModel.CandidateAddr = ObjectMainWindowViewModel.Global_CandidateAddr02;
        }

        private void MainWindow_CandidateAddr03(object sender, RoutedEventArgs e)
        {
            ObjectMainWindowViewModel.DLT645CoreModel.CandidateAddr = ObjectMainWindowViewModel.Global_CandidateAddr03;
        }

        private void MainWindow_CandidateAddr04(object sender, RoutedEventArgs e)
        {
            ObjectMainWindowViewModel.DLT645CoreModel.CandidateAddr = ObjectMainWindowViewModel.Global_CandidateAddr04;
        }

        private void MainWindow_CandidateAddr05(object sender, RoutedEventArgs e)
        {
            ObjectMainWindowViewModel.DLT645CoreModel.CandidateAddr = ObjectMainWindowViewModel.Global_CandidateAddr05;
        }

        private void MainWindow_CandidateAddr06(object sender, RoutedEventArgs e)
        {
            ObjectMainWindowViewModel.DLT645CoreModel.CandidateAddr = ObjectMainWindowViewModel.Global_CandidateAddr06;
        }

        private void MainWindow_CandidateAddr07(object sender, RoutedEventArgs e)
        {
            ObjectMainWindowViewModel.DLT645CoreModel.CandidateAddr = ObjectMainWindowViewModel.Global_CandidateAddr07;
        }

        private void MainWindow_CandidateAddr08(object sender, RoutedEventArgs e)
        {
            ObjectMainWindowViewModel.DLT645CoreModel.CandidateAddr = ObjectMainWindowViewModel.Global_CandidateAddr08;
        }

        private async void MainWindow_ReadAddr(object sender, RoutedEventArgs e)
        {
            string str = ObjectMainWindowViewModel.Asmb_ReadAddr();
            await ObjectMainWindowViewModel.PortSend(str).ConfigureAwait(false);
        }

        private async void MainWindow_WritAddr(object sender, RoutedEventArgs e)
        {
            string str = ObjectMainWindowViewModel.Asmb_WritAddr();
            await ObjectMainWindowViewModel.PortSend(str).ConfigureAwait(false);
        }

        private async void MainWindow_PassWord_Alter(object sender, RoutedEventArgs e)
        {
            string str = ObjectMainWindowViewModel.PassWord_Alter();
            await ObjectMainWindowViewModel.PortSend(str).ConfigureAwait(false);
        }

        private async void MainWindow_BaudRate_Alter(object sender, RoutedEventArgs e)
        {
            string str = ObjectMainWindowViewModel.BaudRate_Alter();
            await ObjectMainWindowViewModel.PortSend(str).ConfigureAwait(false);
        }

        private void MainWindow_CaliTimeType(object sender, RoutedEventArgs e)
        {
            if (ObjectMainWindowViewModel.CaliTimeType == "系统时间")
            {
                ObjectMainWindowViewModel.CaliTimeType = "给定时间";
            }
            else
            {
                ObjectMainWindowViewModel.CaliTimeType = "系统时间";
            }
        }

        private void MainWindow_DataReverseType(object sender, RoutedEventArgs e)
        {
            if (ObjectMainWindowViewModel.DataReverseType == "逆序")
            {
                ObjectMainWindowViewModel.DataReverseType = "正序";
            }
            else
            {
                ObjectMainWindowViewModel.DataReverseType = "逆序";
            }
            MainWindow_UpdtManualAsmb(null, null);
        }

        private void MainWindow_CaliAddrType(object sender, RoutedEventArgs e)
        {
            if (ObjectMainWindowViewModel.CaliAddrType == "广播地址")
            {
                ObjectMainWindowViewModel.CaliAddrType = "给定地址";
            }
            else
            {
                ObjectMainWindowViewModel.CaliAddrType = "广播地址";
            }
        }

        private void MainWindow_CaliMilliSecondType(object sender, RoutedEventArgs e)
        {
            if (ObjectMainWindowViewModel.CaliMilliSecondType == "毫秒开启")
            {
                ObjectMainWindowViewModel.CaliMilliSecondType = "毫秒关闭";
            }
            else
            {
                ObjectMainWindowViewModel.CaliMilliSecondType = "毫秒开启";
            }
        }

        private void MainWindow_UpdtManualAsmb(object sender, RoutedEventArgs e)
        {
            if (ObjectMainWindowViewModel == null)
            {
                return;
            }
            string str = ObjectMainWindowViewModel.AsmbToBeSend();
            if (str != null)
            {
                ObjectMainWindowViewModel.ManualAsmb_ToBeSend = SerialUtilModel.HexStrToHexStr(str, " ");
            }
        }

        private void MainWindow_UpdtManualAsmbiPAC(object sender, RoutedEventArgs e)
        {
            string str = ObjectMainWindowViewModel.AsmbToBeSendiPAC();
            if (str != null)
            {
                ObjectMainWindowViewModel.ManualAsmbiPAC_ToBeSend = SerialUtilModel.HexStrToHexStr(str, " ");
            }
        }

        private async void MainWindow_SendManualAsmb(object sender, RoutedEventArgs e)
        {
            string str = SerialUtilModel.StringFormat(ObjectMainWindowViewModel.ManualAsmb_ToBeSend);
            await ObjectMainWindowViewModel.PortSend(str).ConfigureAwait(false);
        }

        private async void MainWindow_ReadData(object sender, RoutedEventArgs e)
        {
            string str = ObjectMainWindowViewModel.DataRead(ObjectMainWindowViewModel.Global_DataIdx4, null);
            await ObjectMainWindowViewModel.PortSend(str).ConfigureAwait(false);
        }

        private async void MainWindow_WritData(object sender, RoutedEventArgs e)
        {
            string str = ObjectMainWindowViewModel.DataWrit(ObjectMainWindowViewModel.Global_DataIdx4, "");
            await ObjectMainWindowViewModel.PortSend(str).ConfigureAwait(false);
        }

        private async void MainWindow_ParaSet_Reboot(object sender, RoutedEventArgs e)
        {
            string str = ObjectMainWindowViewModel.DataWrit("FAFA0000", "");
            await ObjectMainWindowViewModel.PortSend(str).ConfigureAwait(false);
        }

        private async void MainWindow_ParaSet_ResetFactory(object sender, RoutedEventArgs e)
        {
            string str = ObjectMainWindowViewModel.DataWrit("FAFA0100", "");
            await ObjectMainWindowViewModel.PortSend(str).ConfigureAwait(false);
        }

        private async void MainWindow_ParaSet_ResetPara_ALL(object sender, RoutedEventArgs e)
        {
            string str = ObjectMainWindowViewModel.DataWrit("FAFA0101", "");
            await ObjectMainWindowViewModel.PortSend(str).ConfigureAwait(false);
        }

        private async void MainWindow_ParaSet_ClearData_ALL(object sender, RoutedEventArgs e)
        {
            string str = ObjectMainWindowViewModel.DataWrit("FAFA0102", "");
            await ObjectMainWindowViewModel.PortSend(str).ConfigureAwait(false);
        }

        private async void MainWindow_ParaSet_ClearData_LOG(object sender, RoutedEventArgs e)
        {
            string str = ObjectMainWindowViewModel.DataWrit("0F0F0F05", "000000FF");
            await ObjectMainWindowViewModel.PortSend(str).ConfigureAwait(false);
        }

        private async void MainWindow_ParaGet_SoftwareVersion(object sender, RoutedEventArgs e)
        {
            string str = ObjectMainWindowViewModel.DataRead("04800001", null);
            await ObjectMainWindowViewModel.PortSend(str).ConfigureAwait(false);
        }

        private async void MainWindow_ParaGet_HardwareVersion(object sender, RoutedEventArgs e)
        {
            string str = ObjectMainWindowViewModel.DataRead("04800002", null);
            await ObjectMainWindowViewModel.PortSend(str).ConfigureAwait(false);
        }

        private async void MainWindow_ParaGet_DateNow(object sender, RoutedEventArgs e)
        {
            string str = ObjectMainWindowViewModel.DataRead("04000101", null);
            await ObjectMainWindowViewModel.PortSend(str).ConfigureAwait(false);
        }

        private async void MainWindow_ParaGet_TimeNow(object sender, RoutedEventArgs e)
        {
            string str = ObjectMainWindowViewModel.DataRead("04000102", null);
            await ObjectMainWindowViewModel.PortSend(str).ConfigureAwait(false);
        }

        private async void MainWindow_DataRead_Date(object sender, RoutedEventArgs e)
        {
            string str = ObjectMainWindowViewModel.DataRead("04000101", null);
            await ObjectMainWindowViewModel.PortSend(str).ConfigureAwait(false);
        }

        private async void MainWindow_DataRead_Date_PDIOT(object sender, RoutedEventArgs e)
        {
            string str = ObjectMainWindowViewModel.DataRead("04020101", null);
            await ObjectMainWindowViewModel.PortSend(str).ConfigureAwait(false);
        }

        private async void MainWindow_DataRead_Time(object sender, RoutedEventArgs e)
        {
            string str = ObjectMainWindowViewModel.DataRead("04000102", null);
            await ObjectMainWindowViewModel.PortSend(str).ConfigureAwait(false);
        }

        private async void MainWindow_DataRead_Time_PDIOT(object sender, RoutedEventArgs e)
        {
            string str = ObjectMainWindowViewModel.DataRead("04020102", null);
            await ObjectMainWindowViewModel.PortSend(str).ConfigureAwait(false);
        }

        private async void MainWindow_DataRead_DateTime(object sender, RoutedEventArgs e)
        {
            string str = ObjectMainWindowViewModel.DataRead("0400010C", null);
            await ObjectMainWindowViewModel.PortSend(str).ConfigureAwait(false);
        }

        private async void MainWindow_DataRead_DateTime_PDIOT(object sender, RoutedEventArgs e)
        {
            string str = ObjectMainWindowViewModel.DataRead("0400010C", null);
            await ObjectMainWindowViewModel.PortSend(str).ConfigureAwait(false);
        }

        private async void MainWindow_TimeCali_SysDateTime(object sender, RoutedEventArgs e)
        {
            string str = ObjectMainWindowViewModel.TimeCali_SysDateTime(InputDateTimePicker.Value);
            await ObjectMainWindowViewModel.PortSend(str).ConfigureAwait(false);
        }

        private async void MainWindow_TimeCali_UsrDateTime(object sender, RoutedEventArgs e)
        {
            string str = ObjectMainWindowViewModel.TimeCali_UsrDateTime(InputDateTimePicker.Value);
            await ObjectMainWindowViewModel.PortSend(str).ConfigureAwait(false);
        }

        private async void MainWindow_DataCtrl(object sender, RoutedEventArgs e)
        {
            string str = ObjectMainWindowViewModel.DataCtrl();
            await ObjectMainWindowViewModel.PortSend(str).ConfigureAwait(false);
        }

        private async void MainWindow_DataForz(object sender, RoutedEventArgs e)
        {
            string str = ObjectMainWindowViewModel.DataForz();
            await ObjectMainWindowViewModel.PortSend(str).ConfigureAwait(false);
        }

        private async void MainWindow_DataForz_Broad(object sender, RoutedEventArgs e)
        {
            string str = ObjectMainWindowViewModel.DataForz_Broad();
            await ObjectMainWindowViewModel.PortSend(str).ConfigureAwait(false);
        }

        private async void MainWindow_DmndData_Clear(object sender, RoutedEventArgs e)
        {
            string str = ObjectMainWindowViewModel.DmndData_Clear();
            await ObjectMainWindowViewModel.PortSend(str).ConfigureAwait(false);
        }

        private async void MainWindow_MetrData_Clear(object sender, RoutedEventArgs e)
        {
            string str = ObjectMainWindowViewModel.MetrData_Clear();
            await ObjectMainWindowViewModel.PortSend(str).ConfigureAwait(false);
        }

        private async void MainWindow_EvntData_Clear(object sender, RoutedEventArgs e)
        {
            string str = ObjectMainWindowViewModel.EvntData_Clear();
            await ObjectMainWindowViewModel.PortSend(str).ConfigureAwait(false);
        }

        private async void MainWindow_Calibrate_Measure_Pwr(object sender, RoutedEventArgs e)
        {
            string str = ObjectMainWindowViewModel.Calibrate_Measure_Pwr();
            await ObjectMainWindowViewModel.PortSend(str).ConfigureAwait(false);
        }

        private async void MainWindow_Calibrate_Measure_Pha(object sender, RoutedEventArgs e)
        {
            string str = ObjectMainWindowViewModel.Calibrate_Measure_Pha();
            await ObjectMainWindowViewModel.PortSend(str).ConfigureAwait(false);
        }

        private void TextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                string FrameStr = ObjectMainWindowViewModel.ManualAsmb_ToBeSend + "\n";
                ObjectMainWindowViewModel.ParsInfo = "";
                bool ret = ObjectMainWindowViewModel.DLT645CoreModel.Parse(FrameStr);
                ObjectMainWindowViewModel.ParsInfo += "当前帧：" + FrameStr;
                ObjectMainWindowViewModel.ParsInfo += "地址域：" + SerialUtilModel.HexStrToHexStr(ObjectMainWindowViewModel.DLT645CoreModel.RecvFrameAddr, " ") + "\n";
                ObjectMainWindowViewModel.ParsInfo += "控制码：" + SerialUtilModel.HexStrToHexStr(ObjectMainWindowViewModel.DLT645CoreModel.RecvFrameCtrl, " ") + "\n";
                //ParsInfo += "校验码：" + SerialUtilModel.HexStrToHexStr(DLT645CoreModel.RecvFrameSum8, " ") + "\n";
                int len = Convert.ToInt32("0x" + ObjectMainWindowViewModel.DLT645CoreModel.RecvFrameSize, 16);
                ObjectMainWindowViewModel.ParsInfo += "长度域：" + SerialUtilModel.HexStrToHexStr(ObjectMainWindowViewModel.DLT645CoreModel.RecvFrameSize, " ");
                int ctrl = Convert.ToInt32("0x" + ObjectMainWindowViewModel.DLT645CoreModel.RecvFrameCtrl, 16);
                if (ctrl == 0x11 || ctrl == 0x91 || ctrl == 0x12 || ctrl == 0x92)
                {
                    ObjectMainWindowViewModel.ParsInfo += "(" + len + "=" + "4+" + (len - 4) + ")";
                }
                else
                if (ctrl == 0x14)
                {
                    ObjectMainWindowViewModel.ParsInfo += "(" + len + "=" + "4+4+4+" + (len - 12) + ")";
                }
                else
                {
                    ObjectMainWindowViewModel.ParsInfo += "(" + len + ")";
                }
                ObjectMainWindowViewModel.ParsInfo += "\n";
                //ParsInfo += "数据域：" + SerialUtilModel.HexStrToHexStr(DLT645CoreModel.RecvFrameData, " ") + "\n";
                if (ret)
                {
                    ObjectMainWindowViewModel.DLT645CoreModel.ParseData();
                }
                ObjectMainWindowViewModel.ParsInfo += ObjectMainWindowViewModel.DLT645CoreModel.RecvFrameInfo;
            }
            catch { }
        }

        private void MainWindow_EpochConverter_UtcToTimeStamp(object sender, RoutedEventArgs e)
        {
            DateTime datetime = EpochDateTimePickerUTC.Value;
            string str = new DateTimeOffset(datetime.ToLocalTime()).ToUnixTimeSeconds().ToString("X8");
            ObjectMainWindowViewModel.EpochConverterTimeStamp_INT32 = str;
            ObjectMainWindowViewModel.EpochConverterTimeStamp_UINT32 = str;
        }

        private void MainWindow_EpochConverter_LocalToTimeStamp(object sender, RoutedEventArgs e)
        {
            DateTime datetime = EpochDateTimePickerLocal.Value;
            int ts = (int)new DateTimeOffset(datetime).ToUnixTimeSeconds();
            ObjectMainWindowViewModel.EpochConverterTimeStamp_INT32 = ts.ToString("X8");
            ObjectMainWindowViewModel.EpochConverterTimeStamp_UINT32 = ts.ToString("X8");
        }

        private void MainWindow_EpochConverter_UtcSetToNow(object sender, RoutedEventArgs e)
        {
            EpochDateTimePickerUTC.Value = DateTime.UtcNow;
            EpochDateTimePickerLocal.Value = DateTime.Now;
            MainWindow_EpochConverter_UtcToTimeStamp(sender, e);
        }

        private void MainWindow_EpochConverter_LocalSetToNow(object sender, RoutedEventArgs e)
        {
            EpochDateTimePickerUTC.Value = DateTime.UtcNow;
            EpochDateTimePickerLocal.Value = DateTime.Now;
            MainWindow_EpochConverter_LocalToTimeStamp(sender, e);
        }

        private void MainWindow_EpochConverter_UtcSetTo1970(object sender, RoutedEventArgs e)
        {
            DateTime datetime = new DateTime(1970, 1, 1, 0, 0, 0);
            EpochDateTimePickerUTC.Value = datetime;
            EpochDateTimePickerLocal.Value = datetime.ToLocalTime();
            MainWindow_EpochConverter_UtcToTimeStamp(sender, e);
        }

        private void MainWindow_EpochConverter_LocalSetTo1970(object sender, RoutedEventArgs e)
        {
            DateTime datetime = new DateTime(1970, 1, 1, 0, 0, 0);
            EpochDateTimePickerUTC.Value = datetime.ToUniversalTime();
            EpochDateTimePickerLocal.Value = datetime;
            MainWindow_EpochConverter_LocalToTimeStamp(sender, e);
        }

        private void MainWindow_EpochConverter_UtcSetTo2038(object sender, RoutedEventArgs e)
        {
            DateTime datetime = new DateTime(2038, 1, 19, 3, 14, 7);
            EpochDateTimePickerUTC.Value = datetime;
            EpochDateTimePickerLocal.Value = datetime.ToLocalTime();
            MainWindow_EpochConverter_UtcToTimeStamp(sender, e);
        }

        private void MainWindow_EpochConverter_LocalSetTo2038(object sender, RoutedEventArgs e)
        {
            DateTime datetime = new DateTime(2038, 1, 19, 3, 14, 7);
            EpochDateTimePickerUTC.Value = datetime.ToUniversalTime();
            EpochDateTimePickerLocal.Value = datetime;
            MainWindow_EpochConverter_LocalToTimeStamp(sender, e);
        }

        private void MainWindow_EpochConverter_UtcSetTo2106(object sender, RoutedEventArgs e)
        {
            DateTime datetime = new DateTime(2106, 2, 7, 6, 28, 15);
            EpochDateTimePickerUTC.Value = datetime;
            EpochDateTimePickerLocal.Value = datetime.ToLocalTime();
            MainWindow_EpochConverter_UtcToTimeStamp(sender, e);
        }

        private void MainWindow_EpochConverter_LocalSetTo2106(object sender, RoutedEventArgs e)
        {
            DateTime datetime = new DateTime(2106, 2, 7, 6, 28, 15);
            EpochDateTimePickerUTC.Value = datetime.ToUniversalTime();
            EpochDateTimePickerLocal.Value = datetime;
            MainWindow_EpochConverter_LocalToTimeStamp(sender, e);
        }

        private void MainWindow_CandidateAddr99(object sender, TextChangedEventArgs e)
        {

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ListView list = sender as ListView;
                string FrameStr = list.SelectedItems[0].ToString();
                FrameStr = FrameStr.Substring(88);
                FrameStr = FrameStr.Replace(" ", "");
                FrameStr = FrameStr.Replace("}", "");
                FrameStr = SerialUtilModel.HexStrToHexStr(FrameStr, " ") + "\n";
                ObjectMainWindowViewModel.ParsInfo = "";
                bool ret = ObjectMainWindowViewModel.DLT645CoreModel.Parse(FrameStr);
                ObjectMainWindowViewModel.ParsInfo += "当前帧：" + FrameStr;
                ObjectMainWindowViewModel.ParsInfo += "地址域：" + SerialUtilModel.HexStrToHexStr(ObjectMainWindowViewModel.DLT645CoreModel.RecvFrameAddr, " ") + "\n";
                ObjectMainWindowViewModel.ParsInfo += "控制码：" + SerialUtilModel.HexStrToHexStr(ObjectMainWindowViewModel.DLT645CoreModel.RecvFrameCtrl, " ") + "\n";
                //ParsInfo += "校验码：" + SerialUtilModel.HexStrToHexStr(DLT645CoreModel.RecvFrameSum8, " ") + "\n";
                int len = Convert.ToInt32("0x" + ObjectMainWindowViewModel.DLT645CoreModel.RecvFrameSize, 16);
                ObjectMainWindowViewModel.ParsInfo += "长度域：" + SerialUtilModel.HexStrToHexStr(ObjectMainWindowViewModel.DLT645CoreModel.RecvFrameSize, " ");
                int ctrl = Convert.ToInt32("0x" + ObjectMainWindowViewModel.DLT645CoreModel.RecvFrameCtrl, 16);
                if (ctrl == 0x11 || ctrl == 0x91 || ctrl == 0x12 || ctrl == 0x92)
                {
                    ObjectMainWindowViewModel.ParsInfo += "(" + len + "=" + "4+" + (len - 4) + ")";
                }
                else
                if (ctrl == 0x14)
                {
                    ObjectMainWindowViewModel.ParsInfo += "(" + len + "=" + "4+4+4+" + (len - 12) + ")";
                }
                else
                {
                    ObjectMainWindowViewModel.ParsInfo += "(" + len + ")";
                }
                ObjectMainWindowViewModel.ParsInfo += "\n";
                //ParsInfo += "数据域：" + SerialUtilModel.HexStrToHexStr(DLT645CoreModel.RecvFrameData, " ") + "\n";
                if (ret)
                {
                    ObjectMainWindowViewModel.DLT645CoreModel.ParseData();
                }
                ObjectMainWindowViewModel.ParsInfo += ObjectMainWindowViewModel.DLT645CoreModel.RecvFrameInfo;
            }
            catch { }
        }

        private void MainWindow_AboutClick(object sender, RoutedEventArgs e)
        {
            AboutMe about = new AboutMe();
            about.ShowDialog();
        }

        private void MainWindow_DocClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://sulfurandcu.github.io/sulfurandcu.io/cly6m4gfj002qscrq77a5a7cn.html");
        }
    }

    public class AutoScrollBehavior : Behavior<ListBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.SelectionChanged += new SelectionChangedEventHandler(AssociatedObject_SelectionChanged);
        }
        void AssociatedObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListBox listBox)
            {
                if (listBox.SelectedItem != null)
                {
                    listBox.Dispatcher.BeginInvoke((Action)delegate
                    {
                        listBox.UpdateLayout();
                        listBox.ScrollIntoView(listBox.SelectedItem);//在这里使用一的方法
                    });
                }
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.SelectionChanged -= new SelectionChangedEventHandler(AssociatedObject_SelectionChanged);
        }
    }

    class MyScrollViewer : ScrollViewer
    {
        protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
        {
            base.OnPreviewMouseWheel(e);
            if (!e.Handled)
            {
                e.Handled = true;
                this.RaiseEvent(new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta)
                {
                    RoutedEvent = UIElement.MouseWheelEvent,
                    Source = this
                });
            }
        }
    }

    public class ScrollViewerExtensions
    {
        public static readonly DependencyProperty AlwaysScrollToEndProperty = DependencyProperty.RegisterAttached("AlwaysScrollToEnd", typeof(bool), typeof(ScrollViewerExtensions), new PropertyMetadata(false, AlwaysScrollToEndChanged));
        private static bool _autoScroll;


        private static void AlwaysScrollToEndChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ScrollViewer scroll = sender as ScrollViewer;
            if (scroll != null)
            {
                bool alwaysScrollToEnd = (e.NewValue != null) && (bool)e.NewValue;
                if (alwaysScrollToEnd)
                {
                    scroll.ScrollToEnd();
                    scroll.ScrollChanged += ScrollChanged;
                    // scroll.SizeChanged += Scroll_SizeChanged;
                }
                else { scroll.ScrollChanged -= ScrollChanged; /*scroll.ScrollChanged -= ScrollChanged; */}
            }
            else { throw new InvalidOperationException("The attached AlwaysScrollToEnd property can only be applied to ScrollViewer instances."); }
        }


        //private static void Scroll_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    ScrollViewer scroll = sender as ScrollViewer;
        //    if (scroll == null) { throw new InvalidOperationException("The attached AlwaysScrollToEnd property can only be applied to ScrollViewer instances."); }
        //    double d = scroll.ActualHeight + scroll.ViewportHeight + scroll.ExtentHeight;
        //    scroll.ScrollToVerticalOffset(d);
        //}


        public static bool GetAlwaysScrollToEnd(ScrollViewer scroll)
        {
            if (scroll == null) { throw new ArgumentNullException("scroll"); }
            return (bool)scroll.GetValue(AlwaysScrollToEndProperty);
        }


        public static void SetAlwaysScrollToEnd(ScrollViewer scroll, bool alwaysScrollToEnd)
        {
            if (scroll == null) { throw new ArgumentNullException("scroll"); }
            scroll.SetValue(AlwaysScrollToEndProperty, alwaysScrollToEnd);
        }


        private static void ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            ScrollViewer scroll = sender as ScrollViewer;
            if (scroll == null) { throw new InvalidOperationException("The attached AlwaysScrollToEnd property can only be applied to ScrollViewer instances."); }


            if (e.ExtentHeightChange == 0) { _autoScroll = scroll.VerticalOffset == scroll.ScrollableHeight; }
            if (_autoScroll && e.ExtentHeightChange != 0) { scroll.ScrollToVerticalOffset(scroll.ExtentHeight); }
        }
    }
}
