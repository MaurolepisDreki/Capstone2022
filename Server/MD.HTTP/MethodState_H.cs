using MD.StdLib.Parser;

namespace MD.HTTP {
	class MethodState_H : State {
		private class Rule_OnE : Rule {
			protected override bool Test( State state, StateEngine engine ) {
				Token curr = engine.GetToken();
				if( curr is CharToken ) {
					return ((CharToken)curr).Value == 'E';
				} else {
					return false;
				}
			}

			protected override bool OnPass( State state, StateEngine engine ) {
				engine.SetState( new MethodState_HE() );
				engine.PushState( new ReadTokenState() );
				return false;
			}
		}

		public MethodState_H() : base() {
			rules.Add( new Rule_OnE() );
			rules.Add( new Rule_Fail() );
		}
	}
}

