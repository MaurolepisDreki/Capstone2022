using System;
using System.Threading;

namespace MD.StdLib.Logger {
	// Logger Output Object/Controller
	public abstract class Sink {
		// Section: Controller Interface
		private static MD.StdLib.Container.RingQueue<MD.StdLib.Logger.Sink> registery = new MD.StdLib.Container.RingQueue<MD.StdLib.Logger.Sink>();
		private static System.Threading.Thread writter = new System.Threading.Thread( WritterLoop );

		private static void WritterLoop() {
			while( ! registery.IsEmpty ) {
				registery.Current.Flush();
				registery.Skip();
			}
		}

		public static void Register( Sink sink ) {
			registery.Push( sink );

			if( ! writter.IsAlive )
				writter.Start();
		}

		public static void Unregister( Sink sink ) {
			registery.Purge( sink );
		}

		// Section: Instance
		protected System.Collections.Generic.List<Source> sources;
		protected MD.StdLib.Container.PriorityQueue<Message> messages;
		protected Level acceptedLevels;
		
		protected static bool __compfn( Message a, Message b ) {
			return a > b;
		}

		// @param[ lvl ] flags indicating the levels this Sink accepts
		// @param[ implicitLevels ] when true, automatically selects all levels below the lowest explicit level
		// @param[ swapon ] the sorting function to be applied to the message queue
		public Sink( Level lvl, bool implicitLevels = true ) : this( Sink.__compfn, lvl, implicitLevels ) {}
		public Sink( MD.StdLib.Container.DSorter<Message> swapon, Level lvl, bool implicitLevels = true ) {
			messages = new MD.StdLib.Container.PriorityQueue<Message>( swapon );
			sources = new System.Collections.Generic.List<Source>();
			SetLevels( lvl, implicitLevels );
		}

		public void SetLevels( Level lvl, bool implicitLevels = true ) {
			acceptedLevels = lvl;
			if( implicitLevels ) {
				foreach( Level l in Enum.GetValues( typeof( Level ) ) ) {
					if ( (acceptedLevels & l) != l ) {
						acceptedLevels = acceptedLevels | l;
					} else {
						break;
					}
				}
			}
		}
		
		public abstract bool Flush();
		
		public void Write( Message msg ) {
			messages.Push( msg );
		}

		// @breif Add a source to the sink
		public void ReadFrom( string sourceID ) {
			Source source = Source.Get( sourceID );
			if( ! sources.Contains( source ) ) {
				source.AddSink( this );
				sources.Add( source );
			}
		}

		public virtual void Close() {
			foreach( Source src in sources ) {
				src.RemoveSink( this );
			}

			Sink.Unregister( this );
			while( ! messages.IsEmpty )
				Flush();
		}
	}

// TODO: Impliment MD.StdLib.Logger.FileSink
//	public class FileSink : Sink {
//		
//	}
  
	// A Log Sink that writes to STDOUT
	public class ConsoleSink : Sink {
		public ConsoleSink( Level lvl = Level.Notice, bool implicitLevels = true ) : base( lvl, implicitLevels ) {}

		public override bool Flush() {
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

