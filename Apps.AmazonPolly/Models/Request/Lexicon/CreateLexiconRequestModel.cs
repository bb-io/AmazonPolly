namespace Apps.AmazonPolly.Models.Request.Lexicon;

public record CreateLexiconRequestModel
{
    public string Name { get; set; }
    public string Content { get; set; }
}