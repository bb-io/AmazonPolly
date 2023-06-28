namespace Apps.AmazonPolly.Models.Request;

public record CreateLexiconRequestModel
{
    public string Name { get; set; }
    public string Content { get; set; }
}