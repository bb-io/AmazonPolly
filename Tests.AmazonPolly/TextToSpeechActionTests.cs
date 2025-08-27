using Apps.AmazonPolly.Actions;
using Tests.AmazonPolly.Base;

namespace Tests.AmazonPolly
{
    [TestClass]
    public class TextToSpeechActionTests : TestBase
    {
        [TestMethod]
        public async Task TextToSpeechAction_IsSuccess()
        {
            var action = new TextToSpeechActions(InvocationContext,FileManager);

            var result = await action.SynthesizeSpeech(new Apps.AmazonPolly.Models.Request.Speech.SynthesizeSpeechRequestModel
            {
                Text = "Hello, this is a test.",
                VoiceName = "Gregory",
                //OutputFormat = Amazon.Polly.OutputFormat.Mp3,
                Engine = "long-form",
                OutputFormat = "mp3",
                TextType = "text",
            });

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            Console.WriteLine(json);
            Assert.IsNotNull(result);
        }
    }
}
