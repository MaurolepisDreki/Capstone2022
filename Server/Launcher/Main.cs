using MD.StdLib.Logger;

namespace CS1410.Capstone {
	class Launcher {
		public static void Main() {
			// TODO: Configure Logger
			Sink consoleLog = new ConsoleSink( Level.Status );
			consoleLog.ReadFrom( "Launcher" );

			Source myLog = Source.Get( "Launcher" );

			myLog.Notice( "Logger is setup" );

			consoleLog.Close();
		}
	}
}

