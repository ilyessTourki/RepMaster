using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using TrainSheet.Utilities.Design;
using System.Diagnostics;
using Plugin.LocalNotification;

namespace TrainSheet.ViewModel
{
    public class TimerBoxViewModel : BindableObject
    {
        public string StartStopButtonText { get; set; }
        public string Hours { get; set; }
        public string Minutes { get; set; }
        private bool _isRunning;
        private int hours = 0;
        private int minutes = 0;
        public int totalSeconds;
        public int remainingSeconds;

        private CountdownDrawable drawable;
        private GraphicsView ProgressCircle;
        private Label TimeLabel;
        private Label TotalLabel;
        public ICommand toggleTimerCommand { get; }
        public ICommand resetTimerCommand { get; }


        public TimerBoxViewModel()
        {
            StartStopButtonText = "play_arrow";
            toggleTimerCommand  = new Command(StartButton);
            resetTimerCommand   = new AsyncRelayCommand(ResetTimer);
        }
        public void SetComponents(GraphicsView progressCircle, Label timeLabel ,Label totalLabel)
        {
            ProgressCircle = progressCircle;
            TimeLabel = timeLabel;
            TotalLabel = totalLabel;

            drawable = new CountdownDrawable(() => remainingSeconds, totalSeconds);
            ProgressCircle.Drawable = drawable;
        }
        private void StartButton()
        {
            if (!_isRunning)
            {
                CheckTime();
                hours = int.Parse(Hours); 
                minutes = int.Parse(Minutes);

                StartCountdown();
                StartStopButtonText = "pause";
                _isRunning = true;
            }
            else
            {
                StartStopButtonText = "play_arrow";
                _isRunning = false;
            }
            OnPropertyChanged(nameof(StartStopButtonText));
        }
        private void CheckTime()
        {
            if (Hours is null)
            {
                Hours = "00";
            }
            else if (0 < int.Parse(Hours) && int.Parse(Hours) < 10 && !Hours.Contains("0"))
            {
                Hours = "0" + Hours;
            }
            if (Minutes is null)
            {
                Minutes = "00";
            }
            else if (0<int.Parse(Minutes) && int.Parse(Minutes) < 10 && !Minutes.Contains("0"))
            {
                Minutes = "0" + Minutes;
            }
            OnPropertyChanged(nameof(Hours));
            OnPropertyChanged(nameof(Minutes));
        }
        private async Task ResetTimer()
        {
            bool answer = await Application.Current.MainPage.DisplayAlert("Reset Timer", "Are you sure you want to reset the timer?", "Yes", "No");

            if (answer)
            {
                CheckTime();
                hours = int.Parse(Hours);
                minutes = int.Parse(Minutes);
                totalSeconds = (hours * 3600) + (minutes * 60);
                remainingSeconds = totalSeconds;

                drawable = new CountdownDrawable(() => remainingSeconds, totalSeconds);
                ProgressCircle.Drawable = drawable;
                UpdateLabels();
            }

            
        }
        private void StartCountdown()
        {

            // Only calculate totalSeconds if first start (not resume)
            if (totalSeconds == 0)
            {
                totalSeconds = (hours * 3600) + (minutes * 60);
                remainingSeconds = totalSeconds;
            }

            drawable = new CountdownDrawable(() => remainingSeconds, totalSeconds);
            ProgressCircle.Drawable = drawable;

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (!_isRunning) // If paused, stop ticking
                    return false;

                if (remainingSeconds > 0)
                {
                    remainingSeconds--;
                    UpdateLabels();
                    ProgressCircle.Invalidate();
                    return true;
                }
                _ = SendTimerNotification();
                return false;
            });
            UpdateLabels();
        }
        private void UpdateLabels()
        {
            var remaining = TimeSpan.FromSeconds(remainingSeconds);
            TimeLabel.Text = $"{(int)remaining.TotalHours:D2}:{remaining.Minutes:D2}:{remaining.Seconds:D2}";
            TotalLabel.Text = $"of {(int)TimeSpan.FromSeconds(totalSeconds).TotalHours:D2}:{TimeSpan.FromSeconds(totalSeconds).Minutes:D2} total";
        }
        private async Task SendTimerNotification()
        {
            var request = new NotificationRequest
            {
                NotificationId = 100,
                Title = "Session Ended",
                Description = "Time’s up! Wrap it up, avoid chit-chat, and finish strong for maximum results.",
            };

            await LocalNotificationCenter.Current.Show(request);
        }
    }
}

