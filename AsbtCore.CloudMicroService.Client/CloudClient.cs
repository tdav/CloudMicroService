using AsbtCore.MajordomoCommons;
using AsbtCore.MajordomoProtocol;
using AsbtCore.Messages.Serialize;
using AsbtCore.Messages.Types;
using NetMQ;
using System;
using System.Threading;

namespace AsbtCore.CloudMicroService.Client
{
    public class CloudClient : IDisposable
    {
        private readonly ISerializer serializer;

        private string ServiceName;
        private MDPReplyEventArgs Data;
        private EventWaitHandle handle;
        private MDPClientAsync client;

        public event EventHandler<MDPReplyEventArgs> OnReceiveMessage;

        public CloudClient()
        {
            serializer = new JsonSerializer();
            handle = new AutoResetEvent(false);
        }

        /// <summary>
        /// "tcp://192.168.0.234:5555"
        /// </summary>
        /// <param name="tcpUri"></param>
        public void Connect(string tcpUri, string serviceName)
        {
            var id = new[] { (byte)'C', (byte)'1' };

            ServiceName = serviceName;
            client = new MDPClientAsync(tcpUri, id);
            client.ReplyReady += OnMessage;
        }

        private void OnMessage(object? sender, MDPReplyEventArgs e)
        {
            Data = e;
            handle.Set();
        }

        public RpcResponse<R> SendAsync<T, R>(T param)
        {
            var request = new NetMQMessage();

            var ba = serializer.Serialize(param);
            request.Push(ba);
            client.Send(ServiceName, request);

            handle.WaitOne();

            if (!Data.Reply.IsEmpty)
            {
                var ss = Data.Reply.Pop();
                var ds = serializer.Deserialize<R>(ss.Buffer);
                var result = new RpcResponse<R>(Data.Exception, ds);
                return result;
            }

            return null;
        }

        public void SendOneWayAsync<T>(T param)
        {
            var request = new NetMQMessage();
            var ba = serializer.Serialize(param);
            request.Push(ba);
            client.Send(ServiceName, request);
        }

        public void Dispose()
        {
            client.ReplyReady -= OnReceiveMessage;
            client?.Dispose();
        }
    }
}