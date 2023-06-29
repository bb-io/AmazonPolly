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

    //TODO: Add lexicon ids to the request
    [Action("Synthesize speech", Description = "Synthesize speech from text")]
    public async Task<TextToSpeechResponseModel> SynthesizeSpeech(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] SynthesizeSpeechRequestModel inputData)
    {
        var client = AmazonPollyClientsFactory.CreateClientWithCreds(authenticationCredentialsProviders.ToArray());
        var voiceId = await GetAppropriateVoice(client, inputData);

        var request = new SynthesizeSpeechRequest
        {
            Text = inputData.Text,
            TextType = inputData.IsSsml ? TextType.Ssml : TextType.Text,
            OutputFormat = OutputFormat.Mp3,
            Engine = inputData.Engine,
            VoiceId = voiceId,
        };

        var speechResponse = await PollyRequestsHandler.ExecutePollyAction(client.SynthesizeSpeechAsync, request);

        return new(speechResponse.AudioStream.GetBytes());
    }

    #endregion

    #region Utils

    private async Task<VoiceId> GetAppropriateVoice(AmazonPollyClient client,
        SynthesizeSpeechRequestModel actionRequest)
    {
        var voiceName = actionRequest.VoiceName;
        var languageCode = actionRequest.LanguageCode;
        var engine = actionRequest.Engine;

        var request = new DescribeVoicesRequest
        {
            LanguageCode = languageCode,
            Engine = engine
        };

        var voices = new List<Voice>();
        do
        {
            var voicesResponse = await PollyRequestsHandler.ExecutePollyAction(client.DescribeVoicesAsync, request);
            request.NextToken = voicesResponse.NextToken;

            voices.AddRange(voicesResponse.Voices);
        } while (request.NextToken is not null);

        return voices.FirstOrDefault(x => x.Name.Equals(voiceName, StringComparison.OrdinalIgnoreCase))?.Id ??
               throw new Exception($"The voice {voiceName} is not supported for {languageCode} language and {engine} engine");
    }

    #endregion
}