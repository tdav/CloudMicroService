using AsbtCore.MajordomoProtocol;
using NetMQ;

namespace AsbtCore.CloudMicroService.Server
{
    public class CloudWorker : IDisposable
    {
        private string ServiceName = "echo";
        private MDPWorker session;

        /// <summary>
        /// "tcp://192.168.0.234:5555"
        /// </summary>
        /// <param name="tcpUri"></param>
        public void Start(string tcpUri)
        {
            var id = new[] { (byte)'W', (byte)'1' };

            try
            {
                var exit = false;

                session = new MDPWorker(tcpUri, ServiceName, id);
                session.HeartbeatDelay = TimeSpan.FromMilliseconds(10000);

                //if (verbose)
                //    session.LogInfoReady += (s, e) => Console.WriteLine("{0}", e.Info);

                NetMQMessage reply = null;

                while (!exit)
                {
                    var request = session.Receive(reply);


                    Console.WriteLine("Received: {0}", request);

                    if (ReferenceEquals(request, null)) break;

                    reply = request;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR:");
                Console.WriteLine("{0}", ex.Message);
                Console.WriteLine("{0}", ex.StackTrace);
            }
        }

        public void Dispose()
        {
            session?.Dispose();
        }
    }
}