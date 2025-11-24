using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDFinder
{
	public sealed class SearchProgressEventArgs : EventArgs
	{
		public float Progress { get; init; }	// Percentage - make it more clear

		public SearchProgressEventArgs(float progress)
		{
			Progress = progress;
		}
	}
}
