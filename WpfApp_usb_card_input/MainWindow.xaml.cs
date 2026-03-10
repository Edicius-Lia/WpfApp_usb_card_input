using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp_usb_card_input
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        BardCodeHook bardCodeHook = new ();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            bardCodeHook.Start();
            bardCodeHook.BarCodeEvent += new BardCodeHook.BardCodeDeletegate(BarCode_BarCodeEvent);
        }
        void BarCode_BarCodeEvent(BardCodeHook.BarCodes barCode)
        {
            ShowInfo(barCode);
        }
        private delegate void ShowInfoDelegate(BardCodeHook.BarCodes barCode);

        string IdString = "";
        private void ShowInfo(BardCodeHook.BarCodes barCode)
        {
            if (this.Dispatcher.HasShutdownStarted)
            {
                this.Dispatcher.BeginInvoke(new ShowInfoDelegate(ShowInfo), new object[] { barCode });
            }
            else
            {
                //判断首位信息，如果第一位是首位信息，则清除之前键盘输入的信息
                if (barCode.KeyName == "Enter")
                {
                    Tbox_ID.Text = IdString;
                    IdString = "";
                }
                else
                {
                    IdString += barCode.KeyName;
                }
                if (barCode.IsValid)
                {
                    Tbk_IsEnable.Text = "有效";
                }
                else
                {
                    Tbk_IsEnable.Text = "无效";
                }
            }
        }
    }
}