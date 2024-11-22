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
using LiveCharts;
using LiveCharts.Wpf;

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
        public SeriesCollection SeriesCollection;
        public List<string> ElementCountLabel;
        public List<string> TimeCountLabel;
        public MainWindow()
        {
            InitializeComponent();
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Последовательно",
                    Values = new ChartValues<double>{ }
                }
            };
            ElementCountLabel = new List<string> { };
            TimeCountLabel = new List<string> { };
            numberList = new NumberList();
            initializeProcessors();
            stopWatch = new Stopwatch();
            resultLog.Text = "";
            listCreatedLog.Text = "";
            sumCountedLog.Text = "";
            
            Chart.Series = SeriesCollection;
            Chart.Visibility = Visibility.Visible;
            DataContext = this;
        }

        private void createListButton_Click(object sender, RoutedEventArgs e)
        {
            numberList.InitializeList(Convert.ToInt32(Convert.ToInt32(counterEndTextBlock.Text)));
            listCreatedLog.Text =  "Создан список на " + numberList.NumberListMain.Count().ToString() + " элементов";
        }

        private void countSumButton_Click(object sender, RoutedEventArgs e)
        {
            int start = Convert.ToInt32(startFromTextBlock.Text);
            int end = Convert.ToInt32(endTextBlock.Text);
            int step = Convert.ToInt32(stepTextBlock.Text);
            int stepsCount = end/step;
            List<string> TimeCountLabel = new List<string>();
            for (int i = 0; i < end/step; i++)
            {
                ElementCountLabel.Add((step * i).ToString());
            }
            ElementCountAxis.Labels = ElementCountLabel.ToArray();
            for (int i = 1; i <= stepsCount; i++)
            {
                stopWatch.Reset();
                stopWatch.Start();
                end = step * i;
                numberList.GetSumBasic(start, end);
                stopWatch.Stop();
                TimeCountLabel.Add(stopWatch.Elapsed.TotalMilliseconds.ToString());
                resultLog.Text += "Последовательно подсчитано " + (end - start) + " элементов за " + stopWatch.Elapsed.TotalMilliseconds + " милисекунд\n";
                SeriesCollection[0].Values.Add(stopWatch.Elapsed.TotalMilliseconds);
                TimeAxis.Labels = TimeCountLabel.ToArray();
            }
        }

        private void initializeProcessors()
        {
            procCountComboBox.Items.Clear();
            for (int i = 1; i < ProcCount+1; i++)
            {
                procCountComboBox.Items.Add(i);
            }
            procCountComboBox.SelectedItem = 1;
        }
    }
}