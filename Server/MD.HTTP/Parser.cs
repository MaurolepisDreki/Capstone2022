using MD.StdLib.Parser;

namespace MD.HTTP {
	class Parser : StateEngine {
		public Parser( GetTokenCB inputCB ) : base( inputCB ) {}
	}
}

