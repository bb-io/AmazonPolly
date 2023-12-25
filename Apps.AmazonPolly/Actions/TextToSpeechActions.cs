using System.Net.Mime;
using Amazon.Polly;
using Amazon.Polly.Model;
using Apps.AmazonPolly.Factories;
using Apps.AmazonPolly.Models.Request.Speech;
using Apps.AmazonPolly.Models.Response.Speech;
using Apps.AmazonPolly.Utils;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;

namespace Apps.AmazonPolly.Actions;

[ActionList]
public class TextToSpeechActions : BaseInvocable
{
    private IEnumerable<AuthenticationCredentialsProvider> Creds =>
        InvocationContext.AuthenticationCredentialsProviders;

    private readonly IFileManagementClient _fileManagementClient;

    public TextToSpeechActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : base(
        invocationContext)
    {
        _fileManagementClient = fileManagementClient;
    }

    #region Actions

    [Action("Synthesize speech", Description = "Synthesize speech from text")]
    public async Task<TextToSpeechResponseModel> SynthesizeSpeech(
        [ActionParameter] SynthesizeSpeechRequestModel inputData)
    {
        var client = AmazonPollyClientsFactory.CreateClientWithCreds(Creds.ToArray());

        var request = new SynthesizeSpeechRequest
        {
            Text = inputData.Text,
            TextType = inputData.TextType ?? TextType.Text,
            OutputFormat = inputData.OutputFormat ?? OutputFormat.Mp3,
            Engine = inputData.Engine,
            VoiceId = inputData.VoiceName,
            LexiconNames = inputData.Lexicons?.ToList(),
            SampleRate = inputData.SampleRate
        };

        var speechResponse = await PollyRequestsHandler.ExecutePollyAction(client.SynthesizeSpeechAsync, request);

        var file = await _fileManagementClient.UploadAsync(speechResponse.AudioStream, MediaTypeNames.Application.Octet,
            $"{inputData.VoiceName}");

        return new(file);
    }

    #endregion
}