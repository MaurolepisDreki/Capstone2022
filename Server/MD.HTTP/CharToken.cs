using MD.StdLib.Parser;

namespace MD.HTTP {
	class CharToken : Token {
		private char _value;

		public char Value { get => _value; }

		public CharToken( char data ) {
			_value = data;
		}
	}
}

