using AsbtCore.CloudMicroService.Server;

namespace Worker
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            using var session = new CloudWorker();
            session.Start("tcp://192.168.0.234:5555");
        }
    }
}
