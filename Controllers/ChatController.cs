// File: Controllers/ChatController.cs (Fixed Version with Conversation Memory)
// API endpoints that support conversation history and memory

using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using SemanticKernelChatbot.Models;
using SemanticKernelChatbot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SemanticKernelChatbot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly Kernel _kernel;
        private readonly ILogger<ChatController> _logger;
        private readonly IConversationService _conversationService;

        public ChatController(
            Kernel kernel,
            ILogger<ChatController> logger,
            IConversationService conversationService)
        {
            _kernel = kernel;
            _logger = logger;
            _conversationService = conversationService;
        }

        // ===== REQUEST/RESPONSE MODELS =====

        public class ChatRequest
        {
            /// <summary>
            /// The user's message
            /// </summary>
            public string Message { get; set; } = string.Empty;

            /// <summary>
            /// Optional: Conversation ID to continue existing conversation
            /// If not provided, a new conversation is created
            /// </summary>
            public string? ConversationId { get; set; }
        }

        public class ChatResponse
        {
            /// <summary>
            /// The AI's response
            /// </summary>
            public string Response { get; set; } = string.Empty;

            /// <summary>
            /// The conversation ID (for resuming later)
            /// </summary>
            public string ConversationId { get; set; } = string.Empty;

            /// <summary>
            /// Total messages in this conversation
            /// </summary>
            public int MessageCount { get; set; }

            /// <summary>
            /// Response timestamp
            /// </summary>
            public DateTime Timestamp { get; set; }
        }

        // ===== ENDPOINTS =====

        /// <summary>
        /// POST /api/chat
        /// Send a message and get AI response with conversation memory
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ChatResponse>> Chat([FromBody] ChatRequest request)
        {
            try
            {
                // ===== VALIDATION =====
                if (request == null || string.IsNullOrWhiteSpace(request.Message))
                {
                    return BadRequest(new { error = "Message cannot be empty" });
                }

                // ===== CREATE/GET CONVERSATION =====
                var conversationId = request.ConversationId ?? Guid.NewGuid().ToString();
                var conversation = _conversationService.GetOrCreateConversation(conversationId);

                _logger.LogInformation(
                    $"📨 Received message in conversation {conversationId}: " +
                    $"'{request.Message.Substring(0, Math.Min(50, request.Message.Length))}'");

                // ===== SAVE USER MESSAGE =====
                var userMessage = new Message
                {
                    Role = "user",
                    Content = request.Message
                };
                _conversationService.AddMessage(conversationId, userMessage);

                // ===== SYSTEM PROMPT =====
                // This defines how the AI should behave
                var systemPrompt = @"You are a helpful and friendly AI assistant.
- Keep responses concise and clear
- Be professional but conversational
- Remember context from previous messages in this conversation
- Refer back to previous messages when relevant
- Always be helpful and respectful
- If the user asks about something they mentioned earlier, recall it";

                // ===== BUILD CHAT HISTORY WITH FULL CONVERSATION CONTEXT =====
                var chatHistory = new ChatHistory();
                chatHistory.AddSystemMessage(systemPrompt);

                // Get ALL previous messages from this conversation
                var conversationHistory = _conversationService.GetConversationHistory(conversationId);
                
                _logger.LogInformation($"📚 Loading conversation history: {conversationHistory.Count} messages");

                // Add all previous messages to context (newest messages get better context)
                foreach (var msg in conversationHistory)
                {
                    if (msg.Role == "user")
                    {
                        chatHistory.AddUserMessage(msg.Content);
                        _logger.LogDebug($"  ↪️ User: {msg.Content.Substring(0, Math.Min(40, msg.Content.Length))}...");
                    }
                    else if (msg.Role == "assistant")
                    {
                        chatHistory.AddAssistantMessage(msg.Content);
                        _logger.LogDebug($"  ↪️ Bot: {msg.Content.Substring(0, Math.Min(40, msg.Content.Length))}...");
                    }
                }

                _logger.LogInformation($"📖 Chat history prepared with {conversationHistory.Count} previous messages");

                // ===== GET AI RESPONSE =====
                var chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();
                
                _logger.LogInformation("🤖 Calling Azure OpenAI with full conversation context...");
                
                var response = await chatCompletionService.GetChatMessageContentAsync(
                    chatHistory,
                    kernel: _kernel);

                var aiMessage = response.Content;

                _logger.LogInformation($"✅ Received AI response: " +
                    $"'{aiMessage.Substring(0, Math.Min(50, aiMessage.Length))}'");

                // ===== SAVE AI RESPONSE =====
                var assistantMessage = new Message
                {
                    Role = "assistant",
                    Content = aiMessage
                };
                _conversationService.AddMessage(conversationId, assistantMessage);

                // ===== PREPARE RESPONSE =====
                var totalMessages = conversationHistory.Count + 2; // +2 for new user and assistant messages

                _logger.LogInformation(
                    $"💾 Conversation {conversationId} now has {totalMessages} total messages");

                return Ok(new ChatResponse
                {
                    Response = aiMessage,
                    ConversationId = conversationId,
                    MessageCount = totalMessages,
                    Timestamp = DateTime.UtcNow
                });
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"❌ Azure OpenAI API Error: {ex.Message}");
                return StatusCode(503, new
                {
                    error = "AI service is unavailable. Please check your Azure OpenAI configuration."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ Unexpected error: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, new { error = $"An unexpected error occurred: {ex.Message}" });
            }
        }

        /// <summary>
        /// GET /api/chat/conversations/{conversationId}
        /// Get full conversation history
        /// </summary>
        [HttpGet("conversations/{conversationId}")]
        public ActionResult<object> GetConversation(string conversationId)
        {
            var conversation = _conversationService.GetConversation(conversationId);

            if (conversation == null)
            {
                _logger.LogWarning($"⚠️ Requested conversation not found: {conversationId}");
                return NotFound(new { error = "Conversation not found" });
            }

            _logger.LogInformation($"📖 Retrieved conversation {conversationId} with {conversation.Messages.Count} messages");

            return Ok(new
            {
                id = conversation.Id,
                title = conversation.Title,
                messageCount = conversation.Messages.Count,
                userMessages = conversation.UserMessageCount,
                assistantMessages = conversation.AssistantMessageCount,
                createdAt = conversation.CreatedAt,
                lastUpdatedAt = conversation.LastUpdatedAt,
                messages = conversation.Messages.Select(m => new
                {
                    role = m.Role,
                    content = m.Content,
                    timestamp = m.Timestamp
                }).ToList()
            });
        }

        /// <summary>
        /// GET /api/chat/conversations
        /// Get list of all conversations
        /// </summary>
        [HttpGet("conversations")]
        public ActionResult<object> GetConversations()
        {
            var conversations = _conversationService.GetAllConversations();

            _logger.LogInformation($"📋 Retrieved {conversations.Count} conversations");

            return Ok(new
            {
                total = conversations.Count,
                conversations = conversations.Select(c => new
                {
                    id = c.Id,
                    title = c.Title,
                    messageCount = c.Messages.Count,
                    userMessages = c.UserMessageCount,
                    assistantMessages = c.AssistantMessageCount,
                    createdAt = c.CreatedAt,
                    lastUpdatedAt = c.LastUpdatedAt
                }).ToList()
            });
        }

        /// <summary>
        /// DELETE /api/chat/conversations/{conversationId}
        /// Delete a conversation
        /// </summary>
        [HttpDelete("conversations/{conversationId}")]
        public ActionResult DeleteConversation(string conversationId)
        {
            _conversationService.DeleteConversation(conversationId);
            _logger.LogInformation($"🗑️ Deleted conversation: {conversationId}");
            
            return Ok(new { message = "Conversation deleted successfully" });
        }

        /// <summary>
        /// GET /api/chat/health
        /// Health check endpoint
        /// </summary>
        [HttpGet("health")]
        public ActionResult<object> Health()
        {
            return Ok(new
            {
                status = "healthy",
                timestamp = DateTime.UtcNow,
                conversationMemoryEnabled = true
            });
        }
    }
}