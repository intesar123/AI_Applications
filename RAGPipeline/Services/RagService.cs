using Microsoft.Extensions.AI;
using Microsoft.Extensions.VectorData;
using RAGPipeline.Models;
using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.VectorData;

namespace RAGPipeline.Services
{
    /// <summary>
    /// Summary description for RagService
    /// </summary>
    public class RagService(
        IChatClient chatClient,
        IEmbeddingGenerator<string, Embedding<float>> embeddingGenerator,
          VectorStoreCollection<string, DocumentChunk> collection
        )
    {
        public async Task IngestAsync(string sourceFile, IEnumerable<string> textChunks)
        {
            await collection.EnsureCollectionExistsAsync();

            foreach (var text in textChunks)
            {
                // 1. Generate embedding for the text
                var embedding = await embeddingGenerator.GenerateVectorAsync(text);

                // 2. Create a DocumentChunk object

                await collection.UpsertAsync(new DocumentChunk
                {
                    Id = Guid.NewGuid().ToString(),
                    SourceFile = sourceFile,
                    Text = text,
                    Embedding = embedding
                });
            }
        }

        public async Task<string> AskAsync(string question)
        {
            // ✅ Ensure the collection exists even if /ingest hasn't been called yet
            await collection.EnsureCollectionExistsAsync();

            // 1. Embed user question
            var questionEmbedding = await embeddingGenerator.GenerateVectorAsync(question);

            // 2. Retrieve top 3 matching chunks
            var contextBlocks = new List<string>();
            await foreach(var result in collection.SearchAsync(questionEmbedding, top: 3))
            {
                contextBlocks.Add($"[Source: {result.Record.SourceFile}]\n{result.Record.Text}");
            }

            if(contextBlocks.Count == 0)
            {
                return "No relevant documents found in the knowledge base.";
            }

            // 3. Construct grounded prompt
            var context = string.Join("\n\n", contextBlocks);
            var message = new List<ChatMessage> { 
                new(ChatRole.System, """
                You are a precise assistant. Answer the user's question using ONLY the provided context.
                If the answer is not in the context, say "I don't have enough information to answer that."
                Always cite the [Source] file name.
                """),
                new(ChatRole.User, $"Context:\n{context}\n\nQuestion: {question}")
            };

            // 4. Get answer from chat model
            var response = await chatClient.GetResponseAsync(message, new ChatOptions { Temperature = 0.0F });
            return response.Text??"No response generated.";
        }
    }
}
