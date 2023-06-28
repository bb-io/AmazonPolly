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

        var speechResponse = await PollyRequestsHandler.ExecutePollyAction(() => client.SynthesizeSpeechAsync(request));

        return new(speechResponse.AudioStream.GetBytes());
    }

    [Action("List lexicons", Description = "List pronunciation lexicons")]
    public async Task<List<LexiconModel>> ListLexicons(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
    {
        var client = AmazonPollyClientsFactory.CreateClientWithCreds(authenticationCredentialsProviders.ToArray());

        var lexiconsResponse = await PollyRequestsHandler.ExecutePollyAction(() => client.ListLexiconsAsync(new()));

        return lexiconsResponse.Lexicons.Select(x => new LexiconModel(x.Name, string.Empty)).ToList();
    }

    [Action("Get lexicon", Description = "Get pronunciation lexicon by name")]
    public async Task<LexiconModel> GetLexicon(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] [Display("Lexicon name")]
        string lexiconName)
    {
        var client = AmazonPollyClientsFactory.CreateClientWithCreds(authenticationCredentialsProviders.ToArray());

        var request = new GetLexiconRequest
        {
            Name = lexiconName
        };

        var lexiconResponse = await PollyRequestsHandler.ExecutePollyAction(() => client.GetLexiconAsync(request));

        return new(lexiconResponse.Lexicon.Name, lexiconResponse.Lexicon.Content);
    }

    [Action("Delete lexicon", Description = "Delete pronunciation lexicon")]
    public async Task DeleteLexicon(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] [Display("Lexicon name")]
        string lexiconName)
    {
        var client = AmazonPollyClientsFactory.CreateClientWithCreds(authenticationCredentialsProviders.ToArray());

        var request = new DeleteLexiconRequest
        {
            Name = lexiconName
        };

        await PollyRequestsHandler.ExecutePollyAction(() => client.DeleteLexiconAsync(request));
    }

    public async Task<LexiconModel> CreateLexicon(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] CreateLexiconRequestModel inputData)
    {
        var client = AmazonPollyClientsFactory.CreateClientWithCreds(authenticationCredentialsProviders.ToArray());

        var request = new PutLexiconRequest
        {
            Name = inputData.Name,
            Content = inputData.Content,
        };

        await PollyRequestsHandler.ExecutePollyAction(() => client.PutLexiconAsync(request));

        return new(inputData.Name, inputData.Content);
    }

    #endregion

    #region Utils

    private async Task<VoiceId> GetAppropriateVoice(AmazonPollyClient client, string languageCode)
    {
        var voicesResponse = await client.DescribeVoicesAsync(new()
        {
            LanguageCode = LanguageCode.FindValue(languageCode)
        });

        return voicesResponse.Voices.First().Id;
    }

    #endregion
}