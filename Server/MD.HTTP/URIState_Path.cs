using MD.StdLib.Parser;

namespace MD.HTTP {
	class URIState_Path : State {
		private class Rule_OnDIV : Rule {
			protected override bool Test( State state, StateEngine engine ) {
				Token curr = engine.GetToken();
				if( curr is CharToken ) {
					return ((CharToken)curr).Value == '/';
				} else {
					return false;
				}
			}

			protected override bool OnPass( State state, StateEngine engine ) {
				engine.DeleteToken();
				engine.PushState( new URIState_Atom() );
				engine.PushState( new ReadTokenState() );
				return false;
			}
		}

		private class Rule_OnQUERY : Rule {
			protected override bool Test( State state, StateEngine engine ) {
				Token curr = engine.GetToken();
				if( curr is CharToken ) {
					return ((CharToken)curr).Value == '?';
				} else {
					return false;
				}
			}

			protected override bool OnPass( State state, StateEngine engine ) {
				engine.DeleteToken();
				engine.SetState( new URIState_Query() );
				return false;
			}
		}

		private class Rule_OnFRAG : Rule {
			protected override bool Test( State state, StateEngine engine ) {
				Token curr = engine.GetToken();
				if( curr is CharToken ) {
					return ((CharToken)curr).Value == '#';
				} else {
					return false;
				}
			}

			protected override bool OnPass( State state, StateEngine engine ) {
				engine.DeleteToken();
				engine.SetState( new URIState_Fragment() );
				return false;
			}
		}

		private class Rule_OnSP : Rule {
			protected override bool Test( State state, StateEngine engine ) {
				Token curr = engine.GetToken();
				if( curr is CharToken ) {
					return ((CharToken)curr).Value == ' ';
				} else {
					return false;
				}
			}

			protected override bool OnPass( State state, StateEngine engine ) {
				engine.DeleteToken();
				engine.SetState( new VersionState() );
				return false;
			}
		}

		public URIState_Path() : base() {
			rules.Add( new Rule_OnDIV() );
			rules.Add( new Rule_OnQUERY() );
			rules.Add( new Rule_OnFRAG() );
			rules.Add( new Rule_OnSP() );
			rules.Add( new Rule_Fail() );
		}
	}
}

