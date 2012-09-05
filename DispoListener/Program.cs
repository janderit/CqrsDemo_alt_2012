using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZMQ;

namespace DispoListener
{
    class Program
    {
        static void Main(string[] args)
        {
            const string interfaceBinding = "tcp://127.0.0.1:16060";
            const int timeoutMicroseconds = 100000;

            var context = new Context();
            var socket = context.Socket(SocketType.PULL);
            socket.PollInHandler += (s,r) =>
                                        {
                                            var data = s.RecvAll(Encoding.UTF8).ToArray();
                                            Console.WriteLine("DISPO: {0} x {1} -> {2}", data[1], data[0], data[2]);
                                        };

            socket.Bind(interfaceBinding);

            Console.WriteLine("listening... Press <ESC> for shutdown.");

            while (true)
            {
                while (!Console.KeyAvailable)
                {
                    Context.Poller(timeoutMicroseconds, socket);
                }
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("shutting down...");
                    socket.Dispose();
                    Console.WriteLine("done.");
                    return;
                }
            }

        }

        
    }
}
