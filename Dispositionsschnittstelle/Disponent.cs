using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZMQ;

namespace Dispositionsschnittstelle
{
    public class Disponent
    {
        private static readonly Context Context;
        private static readonly Socket Socket;

        static Disponent()
        {
            Context = new Context();
            Socket = Context.Socket(SocketType.PUSH);
            Socket.Connect("tcp://127.0.0.1:16060");
        }


        public void Disponiere(string produkt, int menge, string lieferadresse)
        {
            Socket.SendMore(produkt, Encoding.UTF8);
            Socket.SendMore(menge.ToString(), Encoding.UTF8);
            Socket.Send(lieferadresse, Encoding.UTF8);
        }
    }
}
