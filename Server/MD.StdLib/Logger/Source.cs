using System.Collections.Generic;

namespace MD.StdLib.Logger {
	public class Source {
		private static Dictionary<string, Source> registery;

		private static void Register( Source source ) {
			registery.Add( source.ID, source );
		}

		public static Source Aquire( string id ) {
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
	}
}

