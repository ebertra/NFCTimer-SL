using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GalaSoft.MvvmLight;
using System.Windows.Threading;

namespace NFCTimer_SL.Model
{
    public class StopWatch : ModelBase
    {

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

        private TimeSpan _elapsedTimeBeforeLastStop;
        private DateTime _lastStartTime;

        public StopWatch()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 1);
            _elapsedTimeBeforeLastStop = TimeSpan.Zero;
            _timer.Tick += onTimerTick;
        }

        protected void onTimerTick(object sender, object e)
        {
            Time = DateTime.Now - _lastStartTime + _elapsedTimeBeforeLastStop; 
        }

        public void startStop()
        {
            if (!_timer.IsEnabled)
            {
                _lastStartTime = DateTime.Now;
                _timer.Start();
            }
            else
            {
                _elapsedTimeBeforeLastStop = Time;
                _timer.Stop();
            }
            IsRunning = !IsRunning;
        }

        public void reset()
        {
            _timer.Stop();
            IsRunning = false;
            Time = TimeSpan.Zero;
            _elapsedTimeBeforeLastStop = TimeSpan.Zero;
        }

    }
}
