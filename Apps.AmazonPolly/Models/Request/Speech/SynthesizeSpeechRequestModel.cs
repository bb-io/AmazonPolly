using Apps.AmazonPolly.DataSourceHandlers;
using Apps.AmazonPolly.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.AmazonPolly.Models.Request.Speech;

public record SynthesizeSpeechRequestModel
{
    public string Text { get; set; }

    [Display("Voice")]
    [DataSource(typeof(VoiceDataHandler))]
    public string VoiceName { get; set; }

    [StaticDataSource(typeof(EngineDataHandler))]
    public string Engine { get; set; }

    [Display("Text type")]
    [StaticDataSource(typeof(TextTypeDataHandler))]
    public string? TextType { get; set; }

    [Display("Output format")]
    [StaticDataSource(typeof(OutputFormatDataHandler))]
    public string? OutputFormat { get; set; }

    [Display("Sample rate")]
    [StaticDataSource(typeof(SampleRateDataHandler))]
    public string? SampleRate { get; set; }

    public IEnumerable<string>? Lexicons { get; set; }
}