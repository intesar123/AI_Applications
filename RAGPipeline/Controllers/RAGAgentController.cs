using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RAGPipeline.Models;
using RAGPipeline.Services;

namespace RAGPipeline.Controllers
{
    [Route("api/RAGAgent/[action]")]
    [ApiController]
    public class RAGAgentController : ControllerBase
    {
        private readonly RagService _ragService;
        public RAGAgentController(RagService ragService)
        {
            _ragService = ragService;
        }

        [HttpPost]
        [ActionName("ingest")]
        public async Task<IActionResult> Ingest([FromBody] IngestRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.FileName) || request.Chunks == null || !request.Chunks.Any())
            {
                return BadRequest("Invalid request. Please provide a valid file name and text chunks.");
            }
            await _ragService.IngestAsync(request.FileName, request.Chunks);
            return Ok(new { Message = $"Ingested {request.Chunks.Count} chunks." });
        }

        [HttpPost]
        [ActionName("ask")]
        public async Task<IActionResult> Ask([FromBody] string question)
        {
            if (string.IsNullOrEmpty(question))
            {
                return BadRequest("Question cannot be empty.");
            }
            var answer = await _ragService.AskAsync(question);
            return Ok(new { Answer = answer });
        }
    }
}
