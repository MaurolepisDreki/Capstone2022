namespace MD.StdLib.Container {
	// Single Link Node
	public class SLNode<T> : Node<T> {
		private SLNode<T>? _next;

		public SLNode<T>? Next { get => _next; set => _next = value; }

		public SLNode( T? init = default( T ), SLNode<T>? next = null ) : base( init ) {
			_next = next;
		}
	}
}

