using Apps.AmazonPolly.DataSourceHandlers;
using Tests.AmazonPolly.Base;

namespace Tests.AmazonPolly
{
    [TestClass]
    public class DataHandlerTests : TestBase
    {
        [TestMethod]
        public async Task LexiconDataHandler_IsSuccess()
        {
           var handler = new LexiconDataHandler(InvocationContext);

            var result = await handler.GetDataAsync(new Blackbird.Applications.Sdk.Common.Dynamic.DataSourceContext
            {
                SearchString = null
            }, CancellationToken.None);

            foreach (var item in result)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task VoiceDataHandler_IsSuccess()
        {
            var handler = new VoiceDataHandler(InvocationContext);

            var result = await handler.GetDataAsync(new Blackbird.Applications.Sdk.Common.Dynamic.DataSourceContext
            {
                SearchString = null
            }, CancellationToken.None);

            foreach (var item in result)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }

            Assert.IsNotNull(result);
        }
    }
}
