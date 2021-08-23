using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace WpfLogs
{
    /// <summary>
    /// Interaction logic for LogsViewer.xaml
    /// reference https://stackoverflow.com/questions/16743804/implementing-a-log-viewer-with-wpf
    /// </summary>
    public partial class LogsViewer : UserControl
    {
        public ObservableCollection<string> LogEntries
        {
            get { return (ObservableCollection<string>)GetValue(LogEntriesProperty); }
            set { SetValue(LogEntriesProperty, value); }
        }

        public Timer Timer { get; }

        public static readonly DependencyProperty LogEntriesProperty =
            DependencyProperty.Register("LogEntries", typeof(ObservableCollection<string>), typeof(LogsViewer), new PropertyMetadata(new ObservableCollection<string>()));

        public uint Buffer
        {
            get { return (uint)GetValue(BufferProperty); }
            set { SetValue(BufferProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyNumber.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BufferProperty =
            DependencyProperty.Register("Buffer", typeof(uint), typeof(LogsViewer), new PropertyMetadata(10000U));


        public LogsViewer()
        {
            InitializeComponent();

            this.Timer = new Timer(this.Timer_Elapsed, null, 1000, 10);
        }

        private void Timer_Elapsed(object? state)
        {
            Dispatcher.BeginInvoke(() =>
            {
                if (this.LogEntries.Count > this.Buffer)
                {
                    for (int i = 0; i < 1.0 / this.Buffer; i++)
                    {
                        this.LogEntries.RemoveAt(i);
                    }
                }
            });
        }

        private bool AutoScroll = true;

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.Source is not ScrollViewer scrollViewer)
            {
                return;
            }

            // User scroll event : set or unset autoscroll mode
            if (e.ExtentHeightChange == 0)
            {   // Content unchanged : user scroll event
                if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
                {   // Scroll bar is in bottom
                    // Set autoscroll mode
                    AutoScroll = true;
                }
                else
                {   // Scroll bar isn't in bottom
                    // Unset autoscroll mode
                    AutoScroll = false;
                }
            }

            // Content scroll event : autoscroll eventually
            if (AutoScroll && e.ExtentHeightChange != 0)
            {   // Content changed and autoscroll mode set
                // Autoscroll
                scrollViewer.ScrollToVerticalOffset(scrollViewer.ExtentHeight);
            }
        }
    }
}
