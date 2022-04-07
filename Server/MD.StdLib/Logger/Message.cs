using MD.StdLib.Locale; //< defines DateTime

namespace MD.StdLib.Logger {
	// Internal Representation of a Log Message
	public class Message {
		private DateTime dts;
		private Source src;
		private Level lvl;
		private string msg;

		public DateTime TimeStamp { get => dts; }
		public Source Source { get => src; }
		public Level Level { get => lvl; }
		public string Value { get => msg; }

		public Message( Source source, Level level, string message ) : 
			this( source, level, message, new DateTime() ) {}
		public Message( Source source, Level level, string message, DateTime timestamp ) {
			dts = timestamp;
			src = source;
			lvl = level;
			msg = message;
		}

		public static bool operator <( Message a, Message b ) {
			if( a.dts != b.dts ) {
				return a.dts < b.dts;
			} else if( a.lvl != b.lvl ) {
				return a.lvl < b.lvl;
			} else if( a.src.ID != b.src.ID ) {
				return System.String.Compare( a.src.ID, b.src.ID ) > 0;
			} else if( a.msg != b.msg ) {
				return System.String.Compare( a.msg, b.msg ) > 0;
			} else {
				return false;
			}
		}

		public static bool operator >( Message a, Message b ) {
			if( a.dts != b.dts ) {
				return a.dts > b.dts;
			} else if( a.lvl != b.lvl ) {
				return a.lvl > b.lvl;
			} else if( a.src.ID != b.src.ID ) {
				return System.String.Compare( a.src.ID, b.src.ID ) < 0;
			} else if( a.msg != b.msg ) {
				return System.String.Compare( a.msg, b.msg ) < 0;
			} else {
				return false;
			}
		}

		// TODO: overload other equality ops
	}
}

