using System;
using System.ComponentModel;
using System.Windows.Input;
using TrainSheet.Service;

namespace TrainSheet.ViewModel
{
    public class TimerBoxViewModel : BindableObject
    {
        private readonly TimerService _timerService;
        public string StartStopButtonText { get; set; }

        public TimerBoxViewModel(TimerService timerService)
        {
            _timerService = timerService;
            _timerService.PropertyChanged += (s, e) => OnPropertyChanged(e.PropertyName);
            if (_timerService.IsRunning)
            {
                StartStopButtonText = "pause.png";
            }
            else
            {
                StartStopButtonText = "start.png";
            }
            ToggleTimerCommand = new Command(() =>
            {
                if (_timerService.IsRunning)
                {
                    _timerService.Stop();
                    StartStopButtonText = "start.png";
                }
                else
                {
                    _timerService.Start();
                    StartStopButtonText = "pause.png";
                }
                OnPropertyChanged(nameof(StartStopButtonText));
            });
            ResetTimerCommand = new Command(() =>
            {
                _timerService.Reset();
                StartStopButtonText = "start.png";
                OnPropertyChanged(nameof(StartStopButtonText));
            });
        }
        public TimeSpan ElapsedTime => _timerService.ElapsedTime;


        public ICommand ToggleTimerCommand  { get; }
        public ICommand ResetTimerCommand   { get; }

    }
}
