using System;
using System.Timers;
using System.Windows;
using System.Windows.Media;

namespace MacroRecorderReplica
{
    public partial class MainWindow : Window
    {
        private Timer timer;
        private int seconds = 0;
        private int events = 0;
        private double size = 0;
        private bool isRecording = false;
        private bool isPaused = false;

        public MainWindow()
        {
            InitializeComponent();
            timer = new Timer(1000);
            timer.Elapsed += Timer_Elapsed;
        }

        private void StartRecording_Click(object sender, RoutedEventArgs e)
        {
            if (!isRecording)
            {
                isRecording = true;
                isPaused = false;
                seconds = 0;
                events = 0;
                size = 0;
                StatusText.Text = "Grabando...";
                StatusIndicator.Fill = Brushes.Red;
                timer.Start();
                RecordButton.Content = "Detener";
                PauseButton.IsEnabled = true;
                StopButton.IsEnabled = true;
            }
            else
            {
                StopRecording();
            }
        }

        private void PauseRecording_Click(object sender, RoutedEventArgs e)
        {
            isPaused = !isPaused;
            PauseButton.Content = isPaused ? "Reanudar" : "Pausar";
            StatusText.Text = isPaused ? "Pausado" : "Grabando...";
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            StopRecording();
        }

        private void StopRecording()
        {
            isRecording = false;
            isPaused = false;
            timer.Stop();
            RecordButton.Content = "Iniciar Grabación";
            PauseButton.Content = "Pausar";
            PauseButton.IsEnabled = false;
            StopButton.IsEnabled = false;
            StatusText.Text = "Grabación detenida.";
            StatusIndicator.Fill = Brushes.Green;

            MessageBox.Show("Macro guardada", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!isPaused)
            {
                seconds++;
                if (new Random().NextDouble() > 0.5)
                {
                    events += new Random().Next(1, 5);
                    size += new Random().NextDouble() * 0.5;
                }

                Dispatcher.Invoke(() =>
                {
                    TimeDisplay.Text = TimeSpan.FromSeconds(seconds).ToString(@"hh\:mm\:ss");
                    EventDisplay.Text = events.ToString();
                    SizeDisplay.Text = $"{size:F1} KB";
                });
            }
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
