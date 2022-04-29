using MD.StdLib.Parser;

namespace MD.HTTP {
	// @breif Initial Method State
	class MethodState : State {
		private class Rule_OnG : Rule {
			protected override bool Test( State state, StateEngine engine ) {
				Token curr = engine.GetToken();
				if( curr is CharToken ) {
					return ((CharToken)curr).Value == 'G';
				} else {
					return false;
				}
			}

			protected override bool OnPass( State state, StateEngine engine ) {
				engine.SetState( new MethodState_G() );
				engine.PushState( new ReadTokenState() );
				return false;
			}
		}

		private class Rule_OnH : Rule {
			protected override bool Test( State state, StateEngine engine ) {
				Token curr = engine.GetToken();
				if( curr is CharToken ) {
					return ((CharToken)curr).Value == 'H';
				} else {
					return false;
				}
			}

			protected override bool OnPass( State state, StateEngine engine ) {
				engine.SetState( new MethodState_H() );
				engine.PushState( new ReadTokenState() );
				return false;
			}
		}

		public MethodState() : base() {
			rules.Add( new Rule_OnG() );
			rules.Add( new Rule_OnH() );
			rules.Add( new Rule_Fail() );
		}
	}
}

