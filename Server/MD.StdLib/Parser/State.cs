namespace MD.StdLib.Parser {
	// @breif State Base-Object
	// @details Represents a generic State used by a \ref StateEngine
	public abstract class State {
		
		// @breif List of \ref Rule "Rules" the State will Exec when attempting to goto the Next State
		// @details Rules appearing in this list will be Exec'd in the order of appearance.
		protected System.Collections.Generic.List<Rule> rules;

		public State() {
			rules = new System.Collections.Generic.List<Rule>();
		}

		// @breif Performs a State change (in the StateEngine) by running all associated rules
		// @details Determinism is assumed by the State, meaning that all \ref Rule "Rules" 
		//    must stop the checks if it changes the State of the engine.
		// @return true if checks stoped by any rule; false if no stop occured after Exec-ing all Rules
		public bool Next( StateEngine engine ) {
			foreach( Rule rule in rules )
				if( rule.Exec( this, engine ) )
					return true;

			return false;
		}
	}
}		
		
