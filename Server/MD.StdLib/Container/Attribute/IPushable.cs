namespace MD.StdLib.Container.Attribute {
	// FIFO & FILO Insertaiton; a.k.a. `PushBack'
	interface IPushable<T> {
		void Push( T value );
	}
}

