using AsbtCore.MajordomoProtocol;
using NetMQ;
using System.Diagnostics;

namespace Client
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            const string service_name = "echo";
            const int max_runs = 1000;

            bool verbose = false;
            int runs = 10000;

            runs = runs == -1 ? 10 : runs > max_runs ? max_runs : runs;

            var id = new[] { (byte)'C', (byte)'1' };

            var watch = new Stopwatch();

            try
            {
                // create MDP client and set verboseness && use automatic disposal
                using var session = new MDPClientAsync("tcp://192.168.0.234:5555", id);

                if (verbose)
                    session.LogInfoReady += (s, e) => Console.WriteLine("{0}", e.Info);

                session.ReplyReady += (s, e) => Console.WriteLine("RRR--{0}", e.Reply);

                // just give everything time to settle in
                Thread.Sleep(500);

                watch.Start();

                for (int count = 0; count < runs; count++)
                {
                    var request = new NetMQMessage();
                    // set the request data
                    request.Push("Hello World!");


                    // send the request to the service
                    session.Send(service_name, request);

                    if (count % 1000 == 0)
                        Console.Write("|");
                    else
                        if (count % 100 == 0)
                        Console.Write(".");
                }

                
                watch.Stop();
              
                Console.Write("\nStop receiving with any key!");
              
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

                return;
            }

            var time = watch.Elapsed;

            Console.WriteLine("{0} request/replies in {1} ms processed! Took {2:N3} ms per REQ/REP", runs, time.TotalMilliseconds, time.TotalMilliseconds / runs);
            Console.Write("\nExit with any key!");
            Console.ReadKey();
        }

    }
}
