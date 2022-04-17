namespace MD.StdLib.Container.Attribute {
	interface Setable<T> {
		T Current { set; }
		void Set( T value );
	}
}

