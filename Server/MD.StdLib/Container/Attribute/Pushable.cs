namespace MD.StdLib.Container.Attribute {
	// FIFO & FILO Insertaiton; a.k.a. `PushBack'
	interface Pushable<T> {
		void Push( T value );
	}
}

