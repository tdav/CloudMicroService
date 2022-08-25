namespace AsbtCore.Messages.Types
{
    public class RpcResponse<T>
    {
        public RpcResponse(Exception exception, T returnValue)
        {
            Exception = exception;
            ReturnValue = returnValue;
        }

        public Exception Exception { get; set; }

        public T ReturnValue { get; set; }
    }
}
