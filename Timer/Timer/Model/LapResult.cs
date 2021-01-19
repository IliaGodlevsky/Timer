using Timer.Resources;

namespace Timer.Model
{
    public class LapResult
    {
        public LapResult(int lapNumber, LapTimer timer)
        {
            LapNumber = lapNumber;
            HoursPassed = timer.HoursPassed;
            MinutesPassed = timer.MinutesPassed;
            SecondsPassed = timer.SecondsPassed;
            MillisecondsPassed = timer.MillisecondsPassed;
        }

        public int LapNumber { get; private set; }

        public int HoursPassed { get; private set; }

        public int MinutesPassed { get; private set; }

        public int SecondsPassed { get; private set; }

        public int MillisecondsPassed { get; private set; }

        public override string ToString()
        {
            var lapNote = $"Lap: {LapNumber}; Time: ";
            return string.Format(lapNote + TimerResources.DisplayFormat,
                    HoursPassed,
                    MinutesPassed,
                    SecondsPassed,
                    MillisecondsPassed);
        }
    }
}
