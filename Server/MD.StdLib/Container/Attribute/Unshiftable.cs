namespace MD.StdLib.Container.Attribute {
	// LIFO Insertation; a.k.a. `PushFront'
	interface Unshiftable<T> {
		void Unshift( T value );
	}
}

