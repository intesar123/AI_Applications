Packages: 

dotnet add package Microsoft.Extensions.VectorData.Abstractions
dotnet add package Azure.AI.OpenAI --version 2.1.0
dotnet add package OllamaSharp
dotnet add package Microsoft.Extensions.AI.OpenAI --prerelease
dotnet add package CommunityToolkit.VectorData.InMemory --version 1.0.1
dotnet add package Scalar.AspNetCore

Production Best Practices:

• Hybrid Search: Pure vector search struggles with exact acronyms, SKUs, or product codes. Combine vector similarity with keyword search (BM25 / full-text search) using Reciprocal Rank Fusion (RRF).
• Semantic Chunking: Avoid splitting mid-sentence. Use Microsoft.SemanticKernel.Text.TextChunker to split along paragraph and sentence boundaries.
• Guardrails & Citations: Always return the source document metadata alongside the answer so users can verify the claims, and enforce a strict fallback response when similarity scores fall below a minimum threshold.
• Multi-Tenancy: If serving multiple customers, filter vector queries by a TenantId metadata field before running similarity comparisons to prevent data leaks.
For deeper implementation guides, see the official Microsoft Learn .NET RAG documentation or Milan Jovanović's .NET + pgvector RAG guide.
To give you tailored code (such as EF Core pgvector setup, PDF parsing, or Semantic Kernel orchestration), let me know:
• LLM & Embedding provider (Azure OpenAI, OpenAI, or local Ollama)
• Database (PostgreSQL, SQL Server, Qdrant, Azure AI Search)
• Document types you need to ingest (PDFs, Markdown, SQL records, etc.)

### 1. Ingest sample chunks
POST {{baseUrl}}/ingest
Content-Type: application/json

{
  "fileName": "company_policy.txt",
  "chunks": [
    "Employees receive 24 days of paid annual leave per calendar year.",
    "Remote work is allowed up to 3 days per week with manager approval.",
    "Expense claims must be submitted within 15 days of travel."
  ]
}

### 2. Ask a question against the ingested data
GET {{baseUrl}}/ask?q=How%20many%20days%20of%20annual%20leave%20do%20employees%20get%3F

### 3. Open Scaller UI

https://localhost:7180/scalar/