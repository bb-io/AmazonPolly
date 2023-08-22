using Blackbird.Applications.Sdk.Common;

namespace Apps.AmazonPolly.Models.Response.Speech;

public record TextToSpeechResponseModel([property: Display("File content")] byte[] FileContent);
