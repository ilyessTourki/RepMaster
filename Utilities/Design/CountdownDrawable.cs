using System;
namespace TrainSheet.Utilities.Design
{
	public class CountdownDrawable : IDrawable
    {
        private readonly Func<int> getRemaining;
        private readonly int totalSeconds;

        public CountdownDrawable(Func<int> getRemaining, int totalSeconds)
        {
            this.getRemaining = getRemaining;
            this.totalSeconds = totalSeconds;
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            float centerX = dirtyRect.Center.X;
            float centerY = dirtyRect.Center.Y;
            float radius = Math.Min(dirtyRect.Width, dirtyRect.Height) / 2 - 10;

            // Background track
            canvas.StrokeColor = Colors.DarkGray;
            canvas.StrokeSize = 15;
            canvas.DrawCircle(centerX, centerY, radius);

            // Progress arc
            float progress = (float)(totalSeconds - getRemaining()) / totalSeconds;
            canvas.StrokeColor = Color.FromArgb("#806a00"); // Orange/Yellow
            canvas.StrokeSize = 15;
            canvas.StrokeLineCap = LineCap.Round;

            if (progress > 0) // Only draw if there's progress
            {
                canvas.DrawArc(centerX - radius, centerY - radius,
                               radius * 2, radius * 2,
                               0, 360 * progress, false, false);
            }
        }

    }
}

