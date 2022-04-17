using MD.StdLib.Container.Attribute;
using MD.StdLib.Container.Exception;

namespace MD.StdLib.Container {
	public class Stack<T> : AContainer, Countable, Pushable<T>, Popable<T>, Peekable<T> {
		private SLNode<T>? _head;

		public Stack() {
			_head = null;
		}

		public bool IsEmpty { get => _head is null; }

		public uint Count {
			get {
				uint count = 0;
				SLNode<T>? cur = _head;

				while( cur is not null ) {
					count++;
					cur = cur.Next;
				}

				return count;
			}
		}

		public T Current { get => Peek(); }

		public T Peek() {
			if( _head is null )
				throw new ContainerIsEmptyException();
			return _head!.Value;
		}

		public T Pop() {
			if( _head is null )
				throw new ContainerIsEmptyException();
			
			SLNode<T> old = _head;
			_head = _head!.Next;
			return old!.Value;
		}

		public void Push( T value ) {
			_head = new SLNode<T>( value, _head );
		}
	}
}

