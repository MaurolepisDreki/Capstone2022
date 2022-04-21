using MD.StdLib.Parser;

namespace MD.HTTP {
	class MethodToken : Token {
		private Method _value;

		public Method Value { get => _value; }

		public MethodToken( Method data = Method.NONE ) {
			_value = data;
		}
	}
}

