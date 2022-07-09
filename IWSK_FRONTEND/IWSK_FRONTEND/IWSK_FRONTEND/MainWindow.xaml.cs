using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using IWSK_RS232;

namespace IWSK_FRONTEND
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {

        public string Message { get; set; } = "hm";
        public string TestMsg { get; set; } = "SDf";

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewModel();
        }


        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    string message = "fdfsd";
        //    MessageBox.Show(model.MessageInput);
        //    TestMsg = "fgd";
        //    model.Tekscik = "LOL!";
        //}
    }
}
