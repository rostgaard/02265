/// <author>Piotr Milczarek, Anna Walach, Kim Rostgaard Christensen, Pawel Drozdowski </author>

using System;

namespace Surveys
{
	public class Scheduler
	{
		public Schedule Frequency { get; set; }
		public DateTime	Available { get; set; }
		public Scheduler (Schedule frequency)
		{
			Frequency = frequency;
			Available = DateTime.Today;
		}
	}

	public enum Schedule {ONCE, DAILY, WEEKLY, MONTHLY}
}

