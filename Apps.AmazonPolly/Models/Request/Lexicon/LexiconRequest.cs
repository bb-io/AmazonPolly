using Apps.AmazonPolly.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.AmazonPolly.Models.Request.Lexicon;

public class LexiconRequest
{
    [Display("Lexicon")]
    [DataSource(typeof(LexiconDataHandler))]
    public string LexiconName { get; set; }
}