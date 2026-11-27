using Azure;
using Azure.Core;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.VectorData;
using OllamaSharp;
using OpenAI;
using OpenAI.Embeddings;
using RAGPipeline.Models;
using RAGPipeline.Services;
using Scalar.AspNetCore;
using System.ClientModel;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// register unified chat and embedding client
var groqApiKey = builder.Configuration["GroqOpenAI:ApiKey"];

var apiClient = new OpenAIClient(new ApiKeyCredential(groqApiKey), new OpenAIClientOptions
{
    Endpoint = new Uri(builder.Configuration["GroqOpenAI:Endpoint"])
});

//1. register chat client
builder.Services.AddChatClient(apiClient.GetChatClient("openai/gpt-oss-120b").AsIChatClient());

// 2. register embedding client
var apiKey = builder.Configuration["OpenAI:ApiKey"];
var _embeddingClient = new EmbeddingClient(
                        model: builder.Configuration["OpenAI:EmbeddingDeployment"],
                        credential: new ApiKeyCredential(apiKey),
                        options: new OpenAIClientOptions
                        {
                            Endpoint = new Uri(builder.Configuration["OpenAI:Endpoint"])
                        }
                    );
builder.Services.AddEmbeddingGenerator(_embeddingClient.AsIEmbeddingGenerator());

/* // Option B: Use OpenAI for embeddings instead of Ollama
var openAiClient = new OpenAIClient(builder.Configuration["OpenAI:ApiKey"]);
builder.Services.AddEmbeddingGenerator(
    openAiClient.GetEmbeddingClient("text-embedding-3-small").AsIEmbeddingGenerator()
);
*/

// 3. register vector store collection & RAG service

//builder.Services.AddInMemoryVectorStoreRecordCollection<string, DocumentChunk>("groq_knowledge_base");
builder.Services.AddInMemoryVectorStore();

builder.Services.AddSingleton<VectorStoreCollection<string, DocumentChunk>>(sp =>
{
    var store = sp.GetRequiredService<VectorStore>();
    return store.GetCollection<string, DocumentChunk>("knowledge_base");
});

builder.Services.AddScoped<RagService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(); // Serves UI at /scalar (or /scalar/v1)
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
