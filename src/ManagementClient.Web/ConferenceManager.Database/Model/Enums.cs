using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceManager.Database.Model
{
	[System.Flags]
	public enum SelectionCriteria
	{
		NoDeleted = 1 << 0,
		WithDeleted = 1 << 1,
	}
}
