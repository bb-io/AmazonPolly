using Blackbird.Applications.Sdk.Common;

namespace Apps.AmazonPolly.Models.Response;

public record TextToSpeechResponseModel([property: Display("File content")] byte[] FileContent);
