using MD.StdLib.Parser;

namespace MD.HTTP {
	class MethodState_HE : State {
		private class Rule_OnA : Rule {
			protected override bool Test( State state, StateEngine engine ) {
				Token curr = engine.GetToken();
				if( curr is CharToken ) {
					return ((CharToken)curr).Value == 'A';
				} else {
					return false;
				}
			}

			protected override bool OnPass( State state, StateEngine engine ) {
				engine.SetState( new MethodState_HEA() );
				engine.PushState( new ReadTokenState() );
				return false;
			}
		}

		public MethodState_HE() : base() {
			rules.Add( new Rule_OnA() );
			rules.Add( new Rule_Fail() );
		}
	}
}

