﻿using Blackbird.Applications.Sdk.Common;

namespace Apps.AmazonPolly.Models.Request;

public record SynthesizeSpeechRequestModel
{
    public string Text { get; set; }
    [Display("Language code")] public string LanguageCode { get; set; }
    [Display("Voice name")] public string VoiceName { get; set; }
    public string Engine { get; set; }
    [Display("Is text a ssml?")] public bool IsSsml { get; set; }
}