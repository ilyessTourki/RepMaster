using System;
namespace TrainSheet.Model
{
	public class DayItem
	{
        public string DayName { get; set; }
        public int DayNumber { get; set; }
        public DateTime Date { get; set; }
        public bool IsSelected { get; set; }
        public bool HasSets { get; set; }
    }
}

