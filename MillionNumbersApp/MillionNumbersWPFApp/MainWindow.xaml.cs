using MillionNumbersClassLib;
using System.Diagnostics;
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

namespace MillionNumbersWPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NumberList numberList;
        private int ProcCount = Environment.ProcessorCount;
        private Stopwatch stopWatch;
        public MainWindow()
        {
            InitializeComponent();
            numberList = new NumberList();
            procCountTextBlock.Text = ProcCount.ToString();
            stopWatch = new Stopwatch();
            resultLog.Text = "";
            listCreatedLog.Text = "";
            sumCountedLog.Text = "";
        }

        private void createListButton_Click(object sender, RoutedEventArgs e)
        {
            numberList.InitializeList(Convert.ToInt32(counterStartTextBlock.Text), Convert.ToInt32(counterEndTextBlock.Text));
            listCreatedLog.Text =  "Создан список на " + numberList.NumberListMain.Count().ToString() + " элементов";
        }

        private void countSumButton_Click(object sender, RoutedEventArgs e)
        {
            int start = Convert.ToInt32(startFromTextBlock.Text);
            int end = Convert.ToInt32(endTextBlock.Text);
            stopWatch.Reset();
            stopWatch.Start();
            numberList.GetSumBasic(start, end);
            stopWatch.Stop();
            resultLog.Text += "Последовательно подсчитано за " + stopWatch.Elapsed.TotalSeconds + " секунд";
            sumCountedLog.Text = "Подсчитано " + (end - start) + " элементов";
        }
    }
}