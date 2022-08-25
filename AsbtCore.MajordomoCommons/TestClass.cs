namespace AsbtCore.MajordomoCommons
{
    public class TestRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class TestResponse
    {
        public Guid Id { get; set; }
        public DateTime CurDateTime { get; set; }
    }
}
