using System;
namespace TrainSheet.Service
{
	public class TimerService :BindableObject
	{
        private bool _isRunning;
        private TimeSpan _elapsedTime;
        private readonly Timer _timer;

        public bool IsRunning
        {
            get => _isRunning;
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    OnPropertyChanged(nameof(IsRunning));
                }
            }
        }

        public TimeSpan ElapsedTime
        {
            get => _elapsedTime;
            set
            {
                if (_elapsedTime != value)
                {
                    _elapsedTime = value;
                    OnPropertyChanged(nameof(ElapsedTime));
                }
            }
        }

        public TimerService()
        {
            _timer = new Timer(UpdateElapsedTime, null, Timeout.Infinite, 1000);
        }

        public void Start()
        {
            if (!IsRunning)
            {
                _timer.Change(0, 1000);
                IsRunning = true;
            }
        }

        public void Stop()
        {
            if (IsRunning)
            {
                _timer.Change(Timeout.Infinite, Timeout.Infinite);
                IsRunning = false;
            }
        }
        public void Reset()
        {
            Stop(); // Stop the timer
            ElapsedTime = TimeSpan.Zero;
        }

        private void UpdateElapsedTime(object state)
        {
            ElapsedTime += TimeSpan.FromSeconds(1);
        }
    }
}

