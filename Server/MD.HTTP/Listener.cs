using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using MD;

namespace MD.HTTP {
	public class Listener {
		private class Instance {
			private Listener ctrl;
			private EndPoint addr;
			private Socket sock; 
			private Thread actor;
			private bool sepku;

			public bool Running { get => actor.IsAlive; }

			public Instance( Listener controller, EndPoint address ) {
				ctrl = controller;
				addr = default( EndPoint );
				sock = default( Socket );
				actor = default( Thread );
				sepku = false;

				actor = new Thread( ThreadDo );
				SetAddr( address );
			}

			public void SetAddr( in EndPoint address ) {
				// DO NOT OPERATE ON AN ACTIVE THREAD!
				if( actor.IsAlive ) {
					Die();
				}

				// Change Addr
				addr = address;
				sock = new Socket( addr.AddressFamily, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp );
				sock.Bind( addr );

				actor.Start();
			}

			public void Die() {
				sepku = true;
				actor.Join();
			}

			private void ThreadDo() {
				sock.Listen();
				while( ! sepku ) {
					if( sock.Poll( 300, SelectMode.SelectRead ) ) {
						// Spawn Handler Thread
						Socket tunnel = sock.Accept();
						ctrl.myLog.Status( $"{addr} connected to {tunnel.RemoteEndPoint}" );
						(new Thread( ctrl.Processor )).Start( tunnel );
					}
				}
				sock.Shutdown( System.Net.Sockets.SocketShutdown.Both );
			}
		} // END STRUCT INSTANCE

		private List<Instance> listeners;
		private StdLib.Logger.Source myLog;

		// Processor Thread Routine
		private void Processor( object soc ) {
			Socket connection = (Socket)soc;
			byte[] bytes = new byte[1024];

			while( !connection.Poll( 1000, SelectMode.SelectRead ) || connection.Available != 0) {
				int bytesRec = connection.Receive( bytes );
				System.Console.WriteLine( System.Text.Encoding.ASCII.GetString( bytes, 0, bytesRec ) );
			}

			connection.Shutdown( System.Net.Sockets.SocketShutdown.Both );
			myLog.Status( $"Connection to {connection.RemoteEndPoint} closed." );
			connection.Close();
		}

		public Listener( string logsrc = "HTTP::Listener" ) {
			listeners = new List<Instance>();
			myLog = StdLib.Logger.Source.Get( logsrc );
		}

		public bool Add( in EndPoint addr ) {
			listeners.Add( new Instance( this, addr ) );
			myLog.Notice( $"Listening to {addr}" );
			return true; //< TODO: check for success
		}

		public void Wait() {
			myLog.Status( "Waiting for listeners to stop" );
			fancyloop_start:
				bool alldone = true;
				foreach( Instance inst in listeners )
					if( !( alldone = ! inst.Running ) ) 
						break;
				if( ! alldone ) {
					Thread.Sleep( 300 );
					goto fancyloop_start;
				}
			myLog.Notice( "All listeners have stopped" );
		}

		public void Stop() {
			myLog.Notice( "Signaling All-Stop" );
			foreach( Instance inst in listeners )
				inst.Die();
		}
	}
}

