using System.Runtime.CompilerServices;

namespace AsbtCore.Messages.Serialize
{
    public static class SerializeExtentions
    {
        public static string ToJson(this object value, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            try
            {
                return SpanJson.JsonSerializer.Generic.Utf16.Serialize(value);
            }
            catch (Exception ee)
            {
                Serilog.Log.Error($"AsbtCore.Messages.Serialize.ToJson Error:{ee.Message} CallerMemberName:{memberName} CallerFilePath:{sourceFilePath} CallerLineNumber:{sourceLineNumber}");
                throw;
            }
        }

        public static T FromJson<T>(string value, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            try
            {
                return SpanJson.JsonSerializer.Generic.Utf16.Deserialize<T>(value);
            }
            catch (Exception ee)
            {
                Serilog.Log.Error($"AsbtCore.Messages.Serialize.FromJson Error:{ee.Message} CallerMemberName:{memberName} CallerFilePath:{sourceFilePath} CallerLineNumber:{sourceLineNumber}");
                throw;
            }
        }
    }
}