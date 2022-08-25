namespace Test
{
    public class JsonTestClass
    {
        public string MyProperty { get; set; }
        public int IgnoredProperty { get; set; }
    }


    internal class TestJsonSerializer
    {
        public static void Run()
        {
            var _json = new JsonTestClass() { MyProperty = "Some value" };
            var res = SpanJson.JsonSerializer.Generic.Utf16.Serialize<JsonTestClass>(_json);
        }
    }
}
