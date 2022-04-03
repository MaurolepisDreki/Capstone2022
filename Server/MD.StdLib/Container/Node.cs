namespace MD.StdLib.Container {
	// Basic Data Node
	public class Node<T> : INode<T> {
		private T? _data;

		public T? Value{ get => _data; set=> _data = value; }

		public Node( T? init = default( T ) ) {
			_data = init;
		}
	}
}

