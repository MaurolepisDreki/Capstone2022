using MD.StdLib.Container.Attribute;
using MD.StdLib.Container.Exception;

namespace MD.StdLib.Container {
	public class RingQueue<T> : IContainer<T>, ICountable, IPeekable<T>, IPushable<T>, IShiftable<T>, ISkipable {
		private SLNode<T>? _tail;

		public RingQueue() {
			_tail = null;
		}

		public bool IsEmpty { get => _tail is null; }

		public uint Count { 
			get {
				SLNode<T>? buff = _tail;
				uint count = 0;
				if( ! IsEmpty ) {
					do {
						buff = buff!.Next;
						count++;
					} while( ! System.Object.ReferenceEquals( buff, _tail ) );
				}
				return count;
			}
		}

		public T Current { get => Peek(); }

		public T Peek() {
			if( IsEmpty ) throw new ContainerIsEmptyException();
			return _tail!.Next!.Value!;
		}

		public void Push( T value ) {
			if( IsEmpty ) {
				_tail = new SLNode<T>( value );
				_tail!.Next = _tail;
			} else {
				_tail!.Next = new SLNode<T>( value, _tail!.Next );
				_tail = _tail!.Next;
			}
		}

		public T Shift() {
			if( IsEmpty ) throw new ContainerIsEmptyException();

			SLNode<T>? deadnode = _tail!.Next;
			if( System.Object.ReferenceEquals( deadnode, _tail ) ) {
				_tail = _tail!.Next = null;
			} else {
				_tail!.Next = deadnode!.Next;
			}

			// deadnode is just waiting for the Garbage Man
			return deadnode!.Value!;
		}

		public void Skip() {
			if( ! IsEmpty ) {
				_tail = _tail!.Next;
			}
		}
	}
}
