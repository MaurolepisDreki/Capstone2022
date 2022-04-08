namespace MD.StdLib.Container.Attribute {
	// @brief Specifies the ability to Purge an item from the container
	public interface IPurgeable<T> {
		void Purge( T value );
	}
}

