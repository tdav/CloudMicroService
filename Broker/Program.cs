﻿using AsbtCore.MajordomoProtocol;

namespace Broker
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();

            try
            {
                RunBroker(cts).Wait();
            }
            catch (AggregateException ex)
            {
                Console.WriteLine("ERROR:");
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine("{0}", e.Message);
                    Console.WriteLine("{0}", e.StackTrace);
                    Console.WriteLine("---------------");
                }
                Console.WriteLine("exit - any key");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("---------------");
                    Console.WriteLine(ex.InnerException.Message);
                    Console.WriteLine(ex.InnerException.StackTrace);
                    Console.WriteLine("---------------");
                }
                Console.WriteLine("exit - any key");
                Console.ReadKey();
            }
        }


        private static async Task RunBroker(CancellationTokenSource cts)
        {
            using var broker = new MDPBroker("tcp://*:5555");
            //  broker.LogInfoReady += (s, e) => Console.WriteLine(e.Info);
            //  broker.DebugInfoReady += (s, e) => Console.WriteLine(e.Info);

            await broker.Run(cts.Token);
        }
    }
}
