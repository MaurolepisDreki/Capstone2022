namespace MD.StdLib.Container.Attribute {
	interface Peekable<T> {
		T Peek();
		T Current { get; }
	}
}

