using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;

namespace About
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AboutMe : Window
    {
        internal AboutWindowViewModel ObjectAboutWindowViewModel;

        public AboutMe()
        {
            InitializeComponent();
            ObjectAboutWindowViewModel = new AboutWindowViewModel();
            DataContext = ObjectAboutWindowViewModel;
            ObjectAboutWindowViewModel.InitializeComponent();
        }
        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", ((Hyperlink)sender).NavigateUri.ToString());
        }

        private void Hidden_Click(object sender, RoutedEventArgs e)
        {
            if (ObjectAboutWindowViewModel.PrintInfo == "")
            {
                ObjectAboutWindowViewModel.PrintInfo = "N.L.D is An AH.";
            }
            else
            {
                ObjectAboutWindowViewModel.PrintInfo = "";
            }
        }
    }
}
