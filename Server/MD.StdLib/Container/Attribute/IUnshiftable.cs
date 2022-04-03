namespace MD.StdLib.Container.Attribute {
	// LIFO Insertation; a.k.a. `PushFront'
	interface IUnshiftable<T> {
		void Unshift( T value );
	}
}

