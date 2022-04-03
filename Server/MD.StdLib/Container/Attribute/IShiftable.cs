namespace MD.StdLib.Container.Attribute {
	// FIFO Extraction; a.k.a. `PopFront'
	interface IShiftable<T> {
		T Shift();
	}
}

