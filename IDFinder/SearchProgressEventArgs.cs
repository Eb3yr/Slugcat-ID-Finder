using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDFinder
{
	public class SearchProgressEventArgs : EventArgs
	{
		public float Progress { get; init; }

		public SearchProgressEventArgs(float progress)
		{
			Progress = progress;
		}
	}
}
