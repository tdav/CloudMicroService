using System.Runtime.CompilerServices;

namespace AsbtCore.Messages.Serialize
{
    public interface ISerializer
    {
        byte[] Serialize<T>(T message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0);
        T Deserialize<T>(byte[] bytes, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0);
    }
}
