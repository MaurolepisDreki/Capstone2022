using System.Net;
using System.Net.Sockets;
using System.Threading;

using MD;

namespace MD.HTTP {
	class Listener {
		private struct Instance {
			private Listener ctrl;
			private EndPoint addr;
			private Socket sock; 
			private Thread actor;
			private bool sepku;

			public Instance( Listener controller, EndPoint address ) {
				ctrl = controller;
				addr = default( EndPoint );
				sock = default( Socket );
				actor = new Thread( ThreadDo );
				sepku = false;

				SetAddr( address );
			}

			public void SetAddr( in EndPoint address ) {
				// DO NOT OPERATE ON AN ACTIVE THREAD!
				if( actor.IsAlive ) {
					sepku = true;
					actor.Join();
				}

				// TODO: Change Addr
				addr = address;
				sock = Socket( addr.AddressFamily, System.Net.Sockets.SocketType, System.Net.Sockets.ProtocolType.Tcp );
				sock.Bind( addr );

				actor.Start();
			}

			private void ThreadDo() {
				sock.Listen();
				while( ! sepku ) {
					if( sock.Poll( 300, SelectMode.SelectRead ) ) {
						// Spawn Handler Thread
						sock.Accept();
					}
				}
				sock.Shutdown();
			}
		}
	}
}

