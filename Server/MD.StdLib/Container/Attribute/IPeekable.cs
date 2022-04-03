namespace MD.StdLib.Container.Attribute {
	interface IPeekable<T> {
		T Peek();
		T Current { get; }
	}
}

