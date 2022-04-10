using MD.StdLib.Logger;
using MD.HTTP;

namespace CS1410.Capstone {
	class Launcher {
		public static void Main() {
			// Configure Logger
			Sink consoleLog = new ConsoleSink( Level.Trace );
			consoleLog.ReadFrom( "Launcher" );

			Source myLog = Source.Get( "Launcher" );
			myLog.Trace( "Application Starting" );

			// Create HTTP Listener
			Listener ctrl = new Listener( "HTTP::Listener" );
			consoleLog.ReadFrom( "HTTP::Listener" );
			ctrl.Add( new System.Net.IPEndPoint( System.Net.IPAddress.Loopback, 80 ) );

			ctrl.Wait();

			// Close Logger
			consoleLog.Close();
		}
	}
}

