using MD.StdLib.Parser;

namespace MD.HTTP {
	// @breif A \ref Rule for nicely changing to \ref BadState
	class Rule_Fail : Rule {
		protected override bool Test( State state, StateEngine engine ) {
			return false;
		}

		protected override bool OnFail( State state, StateEngine engine ) {
			engine.PushState( new BadState() );
			return false;
		}
	}
}

