using Microsoft.Extensions.VectorData;
namespace RAGPipeline.Models
{
    public class DocumentChunk
    {
        [VectorStoreKey]
        public string Id { get; set; }
        [VectorStoreData]
        public string SourceFile { get; set; }
        [VectorStoreData]
        public string Text { get; set; }
        // why is this 1536? Because the OpenAI embedding model "text-embedding-ada-002" returns embeddings of size 1536.
        [VectorStoreVector(1536)]
        public ReadOnlyMemory<float> Embedding { get; set; }

    }
}