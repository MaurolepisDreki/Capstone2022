using MD.StdLib.Parser;

namespace MD.HTTP {
	class URIState_Query : State {
		private System.Text.StringBuilder _string;

		private class Rule_OnPERCENT : Rule {
			protected override bool Test( State state, StateEngine engine ) {
				Token curr = engine.GetToken();
				if( curr is CharToken ) {
					return ((CharToken)curr).Value == '%';
				} else {
					return false;
				}
			}

			protected override bool OnPass( State state, StateEngine engine ) {
				engine.DeleteToken();
				engine.PushState( new URIState_HexChar() );
				engine.PushState( new ReadTokenState() );
				return false;
			}
		}

		// Defined @RFC3986 S3.3
		private class Rule_OnPCHAR : Rule {
			protected override bool Test( State state, StateEngine engine ) {
				Token curr = engine.GetToken();
				if( curr is CharToken ) {
					char val = ((CharToken)curr).Value;
					return (val >= 'a' && val <= 'z')
						|| (val >= 'A' && val <= 'Z')
						|| (val >= '0' && val <= '9')
						|| val == '-'
						|| val == '.'
						|| val == '_'
						|| val == '~';
				} else {
					return false;
				}
			}

			protected override bool OnPass( State state, StateEngine engine ) {
				((URIState_Query)state)._string.Append( ((CharToken)engine.GetToken()).Value );
				engine.DeleteToken();
				engine.PushState( new ReadTokenState() );
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

		public URIState_Query() : base() {
			_string = new System.Text.StringBuilder();

			rules.Add( new Rule_OnFRAG() );
			rules.Add( new Rule_OnSP() );
			rules.Add( new Rule_Fail() );
		}
	}
}

