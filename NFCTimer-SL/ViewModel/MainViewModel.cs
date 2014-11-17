using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NFCTimer_SL.Model;
using System;
using System.ComponentModel;

namespace NFCTimer_SL.ViewModel
{
   public class MainViewModel : ViewModelBase
    {
        private string _time;
        public string Time
        {
            get {return _time;}
            set
            {
                if (_time != value)
                {
                    _time = value;
                    RaisePropertyChanged(() => Time);
                }
            }
        }

        string _startStopButtonLabel;
        public string StartStopButtonLabel
        {
            get {return _startStopButtonLabel;}
            private set
            {
                if (_startStopButtonLabel != value)
                {
                    _startStopButtonLabel = value;
                    RaisePropertyChanged(() => StartStopButtonLabel);
                }
            }
        }

        private StopWatch stopWatch;

        public RelayCommand StartStopTimerCommand { get; private set; }
        public RelayCommand ResetTimerCommand { get; private set; }

        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                stopWatch = new StopWatch();
                Time = displayTime(TimeSpan.Zero);
                StartStopButtonLabel = "Start";
                this.StartStopTimerCommand = new RelayCommand(this.startStopTimer);
                this.ResetTimerCommand = new RelayCommand(this.resetTimer);
                //Subscribing to my models' notifications
                stopWatch.PropertyChanged += onStopWatchModelPropertyChanged;
            }
        }

        private void onStopWatchModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Time")
                this.Time = displayTime(stopWatch.Time);
            else if (e.PropertyName == "IsRunning")
                StartStopButtonLabel = getStartStopButtonLabelValue();
        }

        private string displayTime(TimeSpan time)
        {
            return time.Hours.ToString() + ":" + time.Minutes.ToString() + ":" + time.Seconds.ToString();
        }

        private string getStartStopButtonLabelValue()
        {
            return stopWatch.IsRunning ? "Stop" : "Start";
        }

        public void startStopTimer()
        {
            stopWatch.startStop();
            StartStopButtonLabel = getStartStopButtonLabelValue();
        }

        public void resetTimer()
        {
            stopWatch.reset();
        }
    }
}