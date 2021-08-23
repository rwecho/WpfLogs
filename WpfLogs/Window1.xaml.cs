using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfLogs
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private string TestData = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum";

        private Random Random { get; } = new Random();
        public Window1()
        {
            InitializeComponent();
            Words = TestData.Split(' ').ToList();
            Maxword =  Words.Count - 1;
            this.Logs = new ObservableCollection<string>();

            this.Number = 10000;
            this.Timer = new Timer(this.Timer_Elapsed, null, 1000,10);
            this.DataContext = this;
        }

        private void Timer_Elapsed(object? state)
        {
            Dispatcher.BeginInvoke((Action)(() => Logs.Add(GetRandomEntry())));
        }

        private string GetRandomEntry()
        {
            var log = string.Join(" ", Enumerable.Range(5, Random.Next(10, 50))
                                                      .Select(x => Words[Random.Next(0, Maxword)]));

            return log;
        }

        public List<string> Words { get; }

        public int Number { get; set; }

        private readonly int Maxword;

        public ObservableCollection<string> Logs { get; }
        public Timer Timer { get; }
    }
}
