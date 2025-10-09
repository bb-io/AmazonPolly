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

[ActionList("Text to speech")]
public class TextToSpeechActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : BaseInvocable(invocationContext)
{
    private IEnumerable<AuthenticationCredentialsProvider> Creds =>
        InvocationContext.AuthenticationCredentialsProviders;

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

        var contentType = string.IsNullOrWhiteSpace(speechResponse.ContentType)
        ? GetMimeFallback(request.OutputFormat)
        : speechResponse.ContentType;

        var extension = contentType switch
        {
            "audio/mpeg" => ".mp3",
            "audio/ogg" => ".ogg",
            "audio/pcm" => ".pcm",
            _ => ""
        };

        var fileName = $"{inputData.VoiceName ?? "polly"}_{DateTime.UtcNow:yyyyMMddHHmmss}{extension}";

        await using var ms = new MemoryStream();
        await speechResponse.AudioStream.CopyToAsync(ms);
        ms.Position = 0;

        var file = await fileManagementClient.UploadAsync(
            ms,
            contentType,
            fileName);

        return new(file);
    }

    private static string GetMimeFallback(OutputFormat format)
    {
        var v = format?.Value;
        if (v == OutputFormat.Mp3.Value) return "audio/mpeg";
        if (v == OutputFormat.Ogg_vorbis.Value) return "audio/ogg";
        if (v == OutputFormat.Pcm.Value) return "audio/pcm";
        return "application/octet-stream";
    }

    #endregion
}