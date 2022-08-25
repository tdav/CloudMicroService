using AsbtCore.CloudMicroService.Client;
using AsbtCore.MajordomoCommons;

namespace Client
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            using var client = new CloudClient();
            client.Connect("tcp://192.168.0.234:5555", "echo");

            var p = new TestRequest() { Id = Guid.NewGuid(), Name = "Hello Worker" };
            var res = client.SendAsync<TestRequest, TestResponse>(p);

            Console.WriteLine($"Res Id:{res.ReturnValue.Id}  Date:{res.ReturnValue.CurDateTime}");
        }
    }
}
