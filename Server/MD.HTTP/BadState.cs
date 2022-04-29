using MD.StdLib.Parser;

namespace MD.HTTP {
	// @breif State for indicating that the Parser is dead and cannot continue
	// @details To be pushed on top of the failing State for further debugging
	class BadState : State {
		public BadState() : base() {}
	}
}

