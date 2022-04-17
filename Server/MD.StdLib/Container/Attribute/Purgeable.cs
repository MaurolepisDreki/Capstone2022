namespace MD.StdLib.Container.Attribute {
	// @brief Specifies the ability to Purge an item from the container
	public interface Purgeable<T> {
		void Purge( T value );
	}
}

