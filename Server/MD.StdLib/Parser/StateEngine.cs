using MD.StdLib.Container;
using MD.StdLib.Container.Attribute;

namespace MD.StdLib.Parser {
	// @breif That place where data streams are reacted to form a State Machine
	public class StateEngine {
		protected System.Collections.Generic.List<Token?> tokstrm;
		protected Stack<State> state;
		protected GetTokenCB gettokenCB;

		public StateEngine( GetTokenCB inputCB ) {
			gettokenCB = inputCB;
			tokstrm = new System.Collections.Generic.List<Token?>();
			state = new Stack<State>();
		}

		// @breif Gets next Token from external source and pushes it to the stream
		// @return true if token is returned from external source; false if null
		public bool GetNextToken() {
			Token? buff = gettokenCB();
			tokstrm.Add( buff );
			return buff is not null;
		}

		// @breif Start/Run the Engine
		// @param init The initial State of the engine
		// @return true if engine cleanly exited (no residual tokens)
		public bool Run( State init ) {
			state.Push( init );

			while( ! state.IsEmpty )
				state.Current.Next( this );

			return tokstrm.Count > 0;
		}

		// @breif Push State to Stack
		// @param newstate The new State of the engine
		public void PushState( State newstate ) {
			state.Push( newstate );
		}

		// @breif Pop State from Stack
		public void PopState() {
			state.Pop();
		}

		// @breif Change the State on top the stack
		// @details Equivelent to `PopState(); PushState( state );`
		// @param state The new State of the engine
		public void SetState( State newstate ) {
			PopState();
			PushState( newstate );
		}

		// @breif Retreive the current State of the engine
		// @return Current State of the engine; null if State stack is empty
		public State? GetState() {
			if( state.IsEmpty )
				return null;

			return state.Current;
		}

		// @breif Retreive the number of Tokens in stream
		// @return The number od Tokens in the stream
		public int CountTokens() {
			return tokstrm.Count;
		}

		// @breif Retreive a Token from the stream
		// @param index The index of the Token desired (FIFO Ordering)
		// @return The requested Token
		public Token? GetToken( int index = 0 ) {
			return tokstrm[ index ];
		}

		// @breif Replace Tokens in the stream with single token
		// @param index The index of the first Token to replace
		// @param count The number of Tokens to replace
		// @param tok The Token replacing those specified
		public void ReplaceTokens( int index, int count, Token tok ) {
			tokstrm.RemoveRange( index, count );
			tokstrm.Insert( index, tok );
		}

		// @breif Delete Tokens from the stream
		// @param index The index of the first Token to remove
		// @param count The number of Tokens to remove
		public void DeleteToken( int index = 0, int count = 1 ) {
			tokstrm.RemoveRange( index, count );
		}
	}
}

