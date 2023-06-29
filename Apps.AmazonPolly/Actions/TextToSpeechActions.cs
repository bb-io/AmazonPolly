using Amazon.Polly;
using Amazon.Polly.Model;
using Apps.AmazonPolly.Extensions;
using Apps.AmazonPolly.Factories;
using Apps.AmazonPolly.Models.Request;
using Apps.AmazonPolly.Models.Response;
using Apps.AmazonPolly.Utils;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;

namespace Apps.AmazonPolly.Actions;

[ActionList]
public class TextToSpeechActions
{
    #region Actions

    [Action("Synthesize speech", Description = "Synthesize speech from text")]
    public async Task<TextToSpeechResponseModel> SynthesizeSpeech(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] SynthesizeSpeechRequestModel inputData)
    {
        var client = AmazonPollyClientsFactory.CreateClientWithCreds(authenticationCredentialsProviders.ToArray());
        var voiceId = await GetAppropriateVoice(client, inputData.LanguageCode);

        var request = new SynthesizeSpeechRequest
        {
            Text = inputData.Text,
            TextType = inputData.IsSsml ? TextType.Ssml : TextType.Text,
            OutputFormat = OutputFormat.Mp3,
            VoiceId = voiceId,
        };

        var speechResponse = await PollyRequestsHandler.ExecutePollyAction(client.SynthesizeSpeechAsync, request);

        return new(speechResponse.AudioStream.GetBytes());
    }

    #endregion

    #region Utils

    private async Task<VoiceId> GetAppropriateVoice(AmazonPollyClient client, string languageCode)
    {
        var request = new DescribeVoicesRequest
        {
            LanguageCode = languageCode
        };
        
        var voicesResponse = await PollyRequestsHandler.ExecutePollyAction(client.DescribeVoicesAsync, request);
        return voicesResponse.Voices.First().Id;
    }

    #endregion
}