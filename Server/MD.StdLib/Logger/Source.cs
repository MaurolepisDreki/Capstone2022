using System.Collections.Generic;

namespace MD.StdLib.Logger {
	public class Source {
		private static Dictionary<string, Source> registery;

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

