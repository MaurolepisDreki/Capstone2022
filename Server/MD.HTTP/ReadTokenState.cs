using MD.StdLib.Parser;

namespace MD.HTTP {
	// @breif Dedicated State for requesting Tokens from source
	class ReadTokenState : State {
		private class FakeRule : Rule {
			protected override bool Test( State state, StateEngine engine ) {
				return true;
			}

			protected override bool OnPass( State state, StateEngine engine ) {
				engine.GetNextToken();
				engine.PopState();
				return false;
			}
		}

		public ReadTokenState() : base () {
			rules.Add( new FakeRule() );
		}
	}
}

