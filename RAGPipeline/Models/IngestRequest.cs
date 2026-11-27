namespace RAGPipeline.Models
{
    public record IngestRequest(string FileName, List<string> Chunks);
}
