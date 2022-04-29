using MD.StdLib.Parser;

namespace MD.HTTP {
	class MethodState_GET : State {
		private class Rule_OnSP : Rule {
			protected override bool Test( State state, StateEngine engine ) {
				Token curr = engine.GetToken();
				if( curr is CharToken ) {
					return ((CharToken)curr).Value == ' ';
				} else {
					return false;
				}
			}

			protected override bool OnPass( State state, StateEngine engine ) {
				engine.ReplaceTokens( 4, 4, new MethodToken( Method.GET ) );
				engine.SetState( new URIState() );
				engine.PushState( new ReadTokenState() );
				return false;
			}
		}

		public MethodState_GET() : base() {
			rules.Add( new Rule_OnSP() );
			rules.Add( new Rule_Fail() );
		}
	}
}

