using System.Threading;
using MD.StdLib.Container.Attribute;
using MD.StdLib.Container.Exception;

namespace MD.StdLib.Container {
	public class PriorityQueue<T> : AContainer, Countable, Pushable<T>, Shiftable<T>, Sortable {
		private DSorter<T> _swapck;
		private DLNode<T>? _head;
		private Mutex _modacc;

		public PriorityQueue( DSorter<T> swapcheck ) {
			_swapck = swapcheck;
			_head = null;
			_modacc = new Mutex();
		}

		public bool IsEmpty { get => _head is null; }

		public uint Count {
			get {
				DLNode<T>? buff = _head;
				uint count = 0;
				if( ! IsEmpty ) {
					do {
						buff = buff!.Next;
						count++;
					} while( ! System.Object.ReferenceEquals( buff, _head ) );
				}
				return count;
			}
		}

		public void Push( T value ) {
			DLNode<T> newnode = new DLNode<T>( value );

			_modacc.WaitOne(); //< Lock Mutex
			if( IsEmpty ) {
				_head = newnode;
				newnode.Next = newnode.Prev = newnode;
			} else {
				// Attach newnode as tail
				newnode.Prev = _head!.Prev;
				newnode.Prev!.Next = newnode;
				newnode.Next = _head;
				_head!.Prev = newnode;
			}
			_modacc.ReleaseMutex();
		}

		public T Shift() {
			// Short Circuit
			if( IsEmpty ) throw new ContainerIsEmptyException();

			_modacc.WaitOne();  //< Lock Mutex
			DLNode<T>? thisnode = _head;
			if( System.Object.ReferenceEquals( _head, _head!.Next ) ) { 
				// Is last node:
				_head = _head!.Next = _head!.Prev = null;
			} else {
				// Is not last node:
				_head = _head!.Next;
				_head!.Prev = thisnode!.Prev;
				_head!.Prev!.Next = _head;
				
				// There should not be any more references to thisnode...
			}
			_modacc.ReleaseMutex();

			// ... so just return the contained value and wait for the Garbage Man.
			return thisnode!.Value!;
		}

		private void swap( DLNode<T> a, DLNode<T> b ) {
			T? tmp = a.Value;
			a.Value = b.Value;
			b.Value = tmp;
		}

		public void Sort() {
			// Short Ciruit
			if( IsEmpty ) return;

			_modacc.WaitOne(); //< Lock Mutex
			DLNode<T>? current = _head;
			while( ! System.Object.ReferenceEquals( current!.Next, _head ) ) {
				if( _swapck( current!.Value!, current!.Next!.Value! ) ) {
					swap( current, current.Next! );
					if( ! System.Object.ReferenceEquals( current, _head ) ) {
						current = current.Prev!;
					}
				} else {
					current = current.Next!;
				}
			}
			_modacc.ReleaseMutex();
		}
	}
}

