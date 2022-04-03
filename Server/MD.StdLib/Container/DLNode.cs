namespace MD.StdLib.Container {
	// Double Link Node
	public class DLNode<T> : Node<T> {
		public DLNode<T>? _next;
		public DLNode<T>? _prev;

		public DLNode<T>? Next { get => _next; set => _next = value; }
		public DLNode<T>? Prev { get => _prev; set => _prev = value; }

		public DLNode( T? init = default( T ), DLNode<T>? next = null, DLNode<T>? prev = null ) : base( init ) {
			_next = next;
			_prev = prev;
		}
	}
}

