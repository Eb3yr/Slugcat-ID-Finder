using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDFinder
{
	// The working idea is that a TimerCallback gets used to invoke both this and SearchProgressEventArgs periodically. That removes the if statements from the main loop that were previously used to update the console, and those timers can run as their own tasks.
	public class SearchWorkingValsEventArgs : EventArgs
	{
		public List<KeyValuePair<float, int>> WeightIDPairs { get; init; }

		/// <param name="weightIDPairs">A reference to the list of weight ID pairs. This is shallow copied within the constructor.</param>
		public SearchWorkingValsEventArgs(List<KeyValuePair<float, int>> weightIDPairs)
		{
			WeightIDPairs = weightIDPairs.ToList();
		}
	}
}
