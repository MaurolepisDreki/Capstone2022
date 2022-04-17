namespace MD.StdLib.Parser {
	// @breif Base-class for Rules
	public abstract class Rule {

		// @breif A check to determine whether to run OnPass or OnFail
		// @return true if passed; false if failed
		protected abstract bool Test( State state, StateEngine engine );

		// @breif What to do when a Test fails
		// @return true if another rule should be checked; false if checks should stop
		protected virtual bool OnPass( State state, StateEngine engine ) {
			return false;
		}

		// @breif What to do when a Test fails
		// @return true if another rule should be checked; false if checks should stop
		protected virtual bool OnFail( State state, StateEngine engine ) {
			return true;
		}

		// @breif Execute the rule
		// @details Runs Test followed by either OnPass or OnFail
		// @return true if another rule should be checked; false if checks should stop
		public bool Exec( State state, StateEngine engine ) {
			if( Test( state, engine ) )
				return OnPass( state, engine );
			else 
				return OnFail( state, engine );
		}
	}
}

