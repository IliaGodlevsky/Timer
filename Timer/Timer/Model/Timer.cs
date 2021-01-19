using System;
using System.Windows.Threading;
using Timer.Resources;

namespace Timer.Model
{
    public sealed class LapTimer
    {
        public event EventHandler Tick
        {
            add => DispatcherTimer.Tick += value;            
            remove => DispatcherTimer.Tick -= value;            
        }

        public bool IsEnabled => DispatcherTimer.IsEnabled;

        public int HoursPassed => (DateTime.Now - startTime).Hours;
        public int MinutesPassed => (DateTime.Now - startTime).Minutes;
        public int SecondsPassed => (DateTime.Now - startTime).Seconds;
        public int MillisecondsPassed => DateTime.Now.Millisecond;

        public string GetPassedTime()
        {
            return string.Format(TimerResources.DisplayFormat,
                HoursPassed,
                MinutesPassed,
                SecondsPassed,
                MillisecondsPassed);
        }

        public LapTimer(int interval)
        {
            DispatcherTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(interval)
            };
        }

        public void Start()
        {
            startTime = DateTime.Now;
            DispatcherTimer.Start();           
        }

        public void Stop()
        {
            DispatcherTimer.Stop();
        }

        private DispatcherTimer DispatcherTimer { get; set; }

        private  DateTime startTime;
    }
}
