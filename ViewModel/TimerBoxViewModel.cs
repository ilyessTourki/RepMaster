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
                StartStopButtonText = "pause";
            }
            else
            {
                StartStopButtonText = "play_arrow";
            }
            ToggleTimerCommand = new Command(() =>
            {
                if (_timerService.IsRunning)
                {
                    _timerService.Stop();
                    StartStopButtonText = "play_arrow";
                }
                else
                {
                    _timerService.Start();
                    StartStopButtonText = "pause";
                }
                OnPropertyChanged(nameof(StartStopButtonText));
            });
            ResetTimerCommand = new Command(() =>
            {
                _timerService.Reset();
                StartStopButtonText = "play_arrow";
                OnPropertyChanged(nameof(StartStopButtonText));
            });
        }
        public TimeSpan ElapsedTime => _timerService.ElapsedTime;


        public ICommand ToggleTimerCommand  { get; }
        public ICommand ResetTimerCommand   { get; }

    }
}
