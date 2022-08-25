using System.Runtime.CompilerServices;

namespace AsbtCore.Messages.Serialize
{
    public class JsonSerializer : ISerializer
    {
        public byte[] Serialize<T>(T message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            try
            {
                return SpanJson.JsonSerializer.Generic.Utf8.Serialize(message);
            }
            catch (Exception ee)
            {
                Serilog.Log.Error($"AsbtCore.Messages.Serialize.ToJson Error:{ee.Message} CallerMemberName:{memberName} CallerFilePath:{sourceFilePath} CallerLineNumber:{sourceLineNumber}");
                throw;
            }
        }

        public T Deserialize<T>(byte[] bytes, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            try
            {
                return SpanJson.JsonSerializer.Generic.Utf8.Deserialize<T>(bytes);
            }
            catch (Exception ee)
            {
                Serilog.Log.Error($"AsbtCore.Messages.Serialize.FromJson Error:{ee.Message} CallerMemberName:{memberName} CallerFilePath:{sourceFilePath} CallerLineNumber:{sourceLineNumber}");
                throw;
            }
        }
    }
}