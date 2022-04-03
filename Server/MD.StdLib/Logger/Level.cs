using System;

namespace MD.StdLib.Logger {
	[Flags]
	public enum Level {
		None        = 0b_0000_0000_0000,
		Fatal       = 0b_0000_0000_0001,
		Critical    = 0b_0000_0000_0010,
		Error       = 0b_0000_0000_0100,
		Warning     = 0b_0000_0000_1000,
		Deprication = 0b_0000_0001_0000,
		Notice      = 0b_0000_0010_0000,
		Status      = 0b_0000_0100_0000,
		Verbose     = 0b_0000_1000_0000,
		Debug       = 0b_0001_0000_0000,
		Trace       = 0b_0010_0000_0000
	}
}
		
