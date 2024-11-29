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
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.ComponentModel;
using LiveCharts.Defaults;
using System;

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
        public SeriesCollection SeriesCollection { get; set; }
        public List<string> ElementCountLabel;
        public List<string> TimeCountLabel;
        private Random random;
        public MainWindow()
        {
            DataContext = this;

            InitializeComponent();
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Последовательно",
                    Values = new ChartValues<ObservablePoint>{ }
                },
                new LineSeries
                {
                    Title = "Параллельно",
                    Values = new ChartValues<ObservablePoint>{ }
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
            random = new Random();
            
            Chart.Series = SeriesCollection;
            Chart.Visibility = Visibility.Visible;

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
            int stepsCount = end / step;

            SeriesCollection[0].Values.Clear();
            SeriesCollection[1].Values.Clear();

            for (int i = 0; i <= stepsCount; i++)
            {
                end = step * i;

                stopWatch.Reset();
                stopWatch.Start();

                numberList.GetSumBasic(start, end);

                stopWatch.Stop();

                resultLog.Text += $"Последовательно подсчитано {end - start} элементов за {stopWatch.Elapsed.TotalMilliseconds} миллисекунд\n";
                SeriesCollection[0].Values.Add(new ObservablePoint(end, stopWatch.Elapsed.TotalMilliseconds));

                stopWatch.Reset();
                stopWatch.Start();

                numberList.GetSumPar(Convert.ToInt32(procCountComboBox.SelectedValue), start, end);

                stopWatch.Stop();

                resultLog.Text += $"Параллельно подсчитано {end - start} элементов за {stopWatch.Elapsed.TotalMilliseconds} миллисекунд\n";
                SeriesCollection[1].Values.Add(new ObservablePoint(end, stopWatch.Elapsed.TotalMilliseconds));
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