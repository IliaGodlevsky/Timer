using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Timer.Infrastructure;
using Timer.Model;
using Timer.Resources;

namespace Timer.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string timerDisplay;
        public string TimerDisplay
        {
            get => timerDisplay;
            set { timerDisplay = value; OnPropertyChanged(); }
        }

        private int lapNumber;
        public int LapNumber
        {
            get => lapNumber;
            set { lapNumber = value; OnPropertyChanged(); }
        }

        public ObservableCollection<LapResult> LapResults { get; set; }

        public ICommand StartTimerCommand { get; }
        public ICommand StopTimerCommand { get; }
        public ICommand FixLapTimeCommand { get; }

        public MainWindowViewModel()
        {
            TimerDisplay = TimerResources.DisplayDefaultView;
            LapResults = new ObservableCollection<LapResult>();

            iconHandler = new NotifyIconHandler();
            lapTimer = new LapTimer(interval: 5);
            lapTimer.DispatcherTimer.Tick += TimerEvent;

            StartTimerCommand = new RelayCommand(ExecuteStartTimerCommand, CanExecuteStartTimerCommand);
            StopTimerCommand = new RelayCommand(ExecuteStopTimerCommand, CanExecuteStopTimerCommand);
            FixLapTimeCommand = new RelayCommand(ExecuteFixLapTimeCommand, CanExecuteFixLapTimeCommand);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ExecuteStartTimerCommand(object param)
        {
            lapTimer.Start();
        }

        private bool CanExecuteStartTimerCommand(object param)
        {
            return !lapTimer.IsEnabled;
        }

        private void ExecuteStopTimerCommand(object param)
        {
            TimerDisplay = TimerResources.DisplayDefaultView;
            lapTimer.Stop();
            LapResults.Clear();
            LapNumber = 0;
        }

        private bool CanExecuteStopTimerCommand(object param)
        {
            return lapTimer.IsEnabled;
        }

        private void ExecuteFixLapTimeCommand(object param)
        {
            LapNumber++;
            LapResults.Add(new LapResult(lapNumber, lapTimer));
        }

        private bool CanExecuteFixLapTimeCommand(object param)
        {
            return lapTimer.IsEnabled;
        }

        private void TimerEvent(object sender, EventArgs e)
        {
            TimerDisplay = lapTimer.GetPassedTime();
        }

        private readonly LapTimer lapTimer;
        private readonly NotifyIconHandler iconHandler;
    }
}
