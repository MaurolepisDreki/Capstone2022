using System;
using System.Threading;

namespace MD.StdLib.Logger {
	// Logger Output Object/Controller
	public abstract class Sink {
		// Section: Controller Interface
		private static MD.StdLib.Container.RingQueue<MD.StdLib.Logger.Sink> registery = new MD.StdLib.Container.RingQueue<MD.StdLib.Logger.Sink>();
		private static System.Threading.Thread writter = new System.Threading.Thread( WritterLoop );

		private static void WritterLoop() {
			while( !registery.IsEmpty ) {
				registery.Current.Flush();
				registery.Skip();
			}
		}

		// Section: Instance
		protected MD.StdLib.Container.PriorityQueue<Message> messages;
		
		protected static bool __compfn( Message a, Message b ) {
			return a > b;
		}

		public Sink() : this( Sink.__compfn ) {}
		public Sink( MD.StdLib.Container.DSorter<Message> swapon ) {
			messages = new MD.StdLib.Container.PriorityQueue<Message>( swapon );
		}
		
		protected abstract bool Flush();
		
		public void Write( Message msg ) {
			messages.Push( msg );
		}
	}

// TODO: Impliment MD.StdLib.Logger.FileSink
//	public class FileSink : Sink {
//		
//	}
  
	// A Log Sink that writes to STDOUT
	public class ConsoleSink : Sink {
		protected override bool Flush() {
			// TODO: Colorize output (MD.StdLib.Logger.ConsoleSink.Flush)
			Console.WriteLine( messages.Shift() );
			return true; //< There is no known way to check if STDOUT is good, so we assume it is always good.  (Shame on us!)
		}
	}

// TODO: Impliment MD.StdLib.Logger.SocketSink
//	public class SocketSink : Sink {
//		
//	}
}

