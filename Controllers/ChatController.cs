// File: Controllers/ChatController.cs
// This controller handles chat requests and uses Semantic Kernel to get AI responses

using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace SemanticKernelChatbot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly Kernel _kernel;
        private readonly ILogger<ChatController> _logger;

        public ChatController(Kernel kernel, ILogger<ChatController> logger)
        {
            _kernel = kernel;
            _logger = logger;
        }

        /// <summary>
        /// Request model for chat endpoint
        /// </summary>
        public class ChatRequest
        {
            public string Message { get; set; }
        }

        /// <summary>
        /// Response model for chat endpoint
        /// </summary>
        public class ChatResponse
        {
            public string Response { get; set; }
            public DateTime Timestamp { get; set; }
        }

        /// <summary>
        /// POST /api/chat
        /// Receives a user message and returns an AI-generated response
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ChatResponse>> Chat([FromBody] ChatRequest request)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(request?.Message))
                {
                    return BadRequest(new { error = "Message cannot be empty" });
                }

                _logger.LogInformation($"📨 Received message: {request.Message}");

                // ===== SYSTEM PROMPT =====
                // This defines how the AI should behave
                // You can customize this to change the chatbot's personality
                var systemPrompt = @"You are a friendly and helpful AI assistant named ChatBot.
- Keep responses concise and clear
- Be professional but conversational
- If you don't know something, admit it
- Always be helpful and respectful
- Respond in the same language as the user";

                // ===== GET CHAT COMPLETION SERVICE =====
                // This is the service that communicates with Azure OpenAI
                var chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();

                // ===== BUILD CONVERSATION HISTORY =====
                // In a real app, you'd store this in a database
                // For now, we create a fresh conversation for each request
                var chatHistory = new ChatHistory();
                chatHistory.AddSystemMessage(systemPrompt);
                chatHistory.AddUserMessage(request.Message);

                // ===== GET AI RESPONSE =====
                // Call Azure OpenAI through Semantic Kernel
                var response = await chatCompletionService.GetChatMessageContentAsync(
                    chatHistory,
                    executionSettings: null,
                    kernel: _kernel);

                var aiMessage = response.Content;

                _logger.LogInformation($"✅ Generated response: {aiMessage}");

                // Return the response
                return Ok(new ChatResponse
                {
                    Response = aiMessage,
                    Timestamp = DateTime.UtcNow
                });
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"❌ Azure OpenAI API Error: {ex.Message}");
                return StatusCode(503, new { error = "AI service is unavailable. Check your Azure OpenAI credentials." });
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ Unexpected error: {ex.Message}");
                return StatusCode(500, new { error = "An unexpected error occurred: " + ex.Message });
            }
        }

        /// <summary>
        /// GET /api/chat/health
        /// Simple health check endpoint to verify the API is running
        /// </summary>
        [HttpGet("health")]
        public ActionResult<object> Health()
        {
            return Ok(new { status = "healthy", timestamp = DateTime.UtcNow });
        }
    }
}