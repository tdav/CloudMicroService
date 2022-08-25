namespace Test
{
    internal class TestEventWaitHandle
    {
        static EventWaitHandle handle = new AutoResetEvent(false);
        public static void Run()
        {
            new Thread(SayHello).Start("1");
            new Thread(SayHello).Start("2");
            new Thread(SayHello).Start("3");

            Thread.Sleep(2000);
            handle.Set();

            Thread.Sleep(2000);
            handle.Set();

            Thread.Sleep(2000);
            handle.Set();
        }

        static void SayHello(object data)
        {
            Console.WriteLine($"say data: {data}");
            handle.WaitOne();
            Console.WriteLine($"{data}");
        }
    }
}
