using System.Collections.Generic;

namespace MD.StdLib.Logger {
	public class Source {
		private static Dictionary<string, Source> registery;

		private static void Register( Source source ) {
			registery.Add( source.ID, source );
		}

		public static Source Get( string id ) {
			if( registery.ContainsKey( id ) ) {
				return registery[ id ];
			} else {
				return new Source( id );
			}
		}

		private List<Sink> sinks; // Write to
		private string myID;

		public string ID { get => myID; }

		private Source( string id ) {
			myID = id;
			sinks = new List<Sink>();
			Source.Register( this );
		}

		public void Write( Level level, string msg ) {
			if( level == Level.None ) {
				Source src = Source.Get( "Logger" );
				src.Write( Level.Trace, "Bad state detected [MD.StdLib.Logger.Source.Write]" );
				src.Write( Level.Error, "Received Message with level `None'; #invalid" );
				src.Write( Level.Verbose, $"Content: {msg}" );
				src.Write( Level.Trace, "Bad state handled" );
				return;
			}

			Message msgobj = new Message( this, level, msg );
			foreach( Sink s in sinks ) {
				s.Write( msgobj );
			}
		}

		public void Fatal( string msg ) {
			Write( Level.Fatal, msg );
		}

		public void Critical( string msg ) {
			Write( Level.Critical, msg );
		}

		public void Error( string msg ) {
			Write( Level.Error, msg );
		}

		public void Warning( string msg ) {
			Write( Level.Warning, msg );
		}

		public void Deprication( string msg ) {
			Write( Level.Deprication, msg );
		}

		public void Notice( string msg ) {
			Write( Level.Notice, msg );
		}

		public void Status( string msg ) {
			Write( Level.Status, msg );
		}

		public void Verbose( string msg ) {
			Write( Level.Verbose, msg );
		}

		public void Debug( string msg ) {
			Write( Level.Debug, msg );
		}

		public void Trace( string msg ) {
			Write( Level.Trace, msg );
		}
	}
}

