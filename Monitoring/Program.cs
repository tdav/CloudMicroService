using NetMQ;
using NetMQ.Monitoring;
using NetMQ.Sockets;

namespace Monitoring
{
    internal class Program
    {
        static void Main(string[] args)
        {

            using var rep = new ResponseSocket();
            using var req = new RequestSocket();
            using var monitor = new NetMQMonitor(rep, "inproc://#monitor", SocketEvents.All);

            monitor.EventReceived += Monitor_EventReceived;
            monitor.Timeout = TimeSpan.FromMilliseconds(100);
            monitor.Start();



            Console.ReadKey();
            monitor.Stop();
            Thread.Sleep(200);
        }

        private static void Monitor_EventReceived(object? sender, NetMQMonitorEventArgs e)
        {
            Console.WriteLine(e);
        }
    }
}