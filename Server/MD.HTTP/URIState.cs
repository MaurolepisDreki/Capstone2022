using MD.StdLib.Parser;

namespace MD.HTTP {
	// @breif Initial URI State
	class URIState : State {
		private class Rule_OnDIV : Rule {
			protected override bool Test( State state, StateEngine engine ) {
				Token curr = engine.GetToken();
				if( curr is CharToken ) {
					return ((CharToken)curr).Value == '/';
				} else {
					return false;
				}
			}

			protected override bool OnPass( State state, StateEngine engine ) {
				engine.SetState( new URIState_Path() );
				return false;
			}
		}

		// TODO: Add protocol and domain parser for URI
		//    This is non-essential for most URI requests, but will break others

		public URIState() : base() {
			rules.Add( new Rule_OnDIV() );
			rules.Add( new Rule_Fail() );
		}
	}
}

