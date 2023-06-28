namespace Apps.AmazonPolly.Extensions;

public static class StreamExtensions
{
    public static byte[] GetBytes(this Stream stream)
    {
        using var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream);

        return memoryStream.ToArray();
    }
}