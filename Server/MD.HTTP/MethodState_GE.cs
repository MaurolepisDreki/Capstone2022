using MD.StdLib.Parser;

namespace MD.HTTP {
	class MethodState_GE : State {
		private class Rule_OnT : Rule {
			protected override bool Test( State state, StateEngine engine ) {
				Token curr = engine.GetToken();
				if( curr is CharToken ) {
					return ((CharToken)curr).Value == 'T';
				} else {
					return false;
				}
			}

			protected override bool OnPass( State state, StateEngine engine ) {
				engine.SetState( new MethodState_GET() );
				engine.PushState( new ReadTokenState() );
				return false;
			}
		}	

		public MethodState_GE() : base() {
			rules.Add( new Rule_OnT() );
			rules.Add( new Rule_Fail() );
		}
	}
}

