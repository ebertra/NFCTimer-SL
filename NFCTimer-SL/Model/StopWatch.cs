using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GalaSoft.MvvmLight;
using System.Windows.Threading;

namespace NFCTimer_SL.Model
{
    public class StopWatch : ViewModelBase //TODO: Should not it be a ModelBase??? Never mind the notificaiton protocol still works
    {

        //ISSUE WITH THIS CODE
        //The problem with this approach is that nothing guarantees that your timer1_Tick method will be called exactly every second. The only thing Windows guarantees is that at least one second will elapse between calls to the timer tick function. So, if you let this run for a day while you're still using your computer, it will probably read a bit slow. The correct way to calculate the elapsed time is to take the current time and subtract the start time, giving the amount of elapsed time between the start and now

        private DispatcherTimer _timer;

        private TimeSpan _time;
        public TimeSpan Time
        {
            get { return _time; }
            private set
            {
                if (_time != value)
                {
                    _time = value;
                    RaisePropertyChanged(() => Time);
                }
            }
        }

        private bool _isRunning;
        public bool IsRunning
        {
            get { return _isRunning; }
            private set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    RaisePropertyChanged(() => IsRunning);
                }
            }
        }

        public StopWatch()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Tick += onTimerTick;
        }

        protected void onTimerTick(object sender, object e)
        {
            Time += TimeSpan.FromSeconds(1);
        }

        public void startStop()
        {
            if (!_timer.IsEnabled)
            {
                _timer.Start();
            }
            else
            {
                _timer.Stop();
            }
            IsRunning = !IsRunning;
        }

        public void reset()
        {
            _timer.Stop();
            IsRunning = false;
            Time = TimeSpan.Zero;
        }

    }
}
