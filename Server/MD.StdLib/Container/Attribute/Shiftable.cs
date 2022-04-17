namespace MD.StdLib.Container.Attribute {
	// FIFO Extraction; a.k.a. `PopFront'
	interface Shiftable<T> {
		T Shift();
	}
}

