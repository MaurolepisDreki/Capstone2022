using MD.StdLib.Parser;

namespace MD.HTTP {
	class MethodState_HEA : State {
		private class Rule_OnD : Rule {
			protected override bool Test( State state, StateEngine engine ) {
				Token curr = engine.GetToken();
				if( curr is CharToken ) {
					return ((CharToken)curr).Value == 'D';
				} else {
					return false;
				}
			}

			protected override bool OnPass( State state, StateEngine engine ) {
				engine.SetState( new MethodState_HEAD() );
				engine.PushState( new ReadTokenState() );
				return false;
			}
		}

		public MethodState_HEA() : base() {
			rules.Add( new Rule_OnD() );
			rules.Add( new Rule_Fail() );
		}
	}
}

