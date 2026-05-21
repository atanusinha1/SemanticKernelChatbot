using SemanticKernelChatbot.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SemanticKernelChatbot.Services
{
    public interface IConversationService
    {
        Conversation GetOrCreateConversation(string conversationId);
        void AddMessage(string conversationId, Message message);
        Conversation GetConversation(string conversationId);
        List<Conversation> GetAllConversations();
        void DeleteConversation(string conversationId);
        List<Message> GetConversationHistory(string conversationId);
    }

    public class ConversationService : IConversationService
    {
        // Store conversations in memory (replace with database later)
        private readonly Dictionary<string, Conversation> _conversations = new();
        private readonly ILogger<ConversationService> _logger;

        public ConversationService(ILogger<ConversationService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get existing conversation or create new one
        /// </summary>
        public Conversation GetOrCreateConversation(string conversationId)
        {
            if (string.IsNullOrEmpty(conversationId))
            {
                conversationId = Guid.NewGuid().ToString();
            }

            if (!_conversations.ContainsKey(conversationId))
            {
                _logger.LogInformation($"📝 Creating new conversation: {conversationId}");
                
                _conversations[conversationId] = new Conversation
                {
                    Id = conversationId,
                    Messages = new List<Message>(),
                    CreatedAt = DateTime.UtcNow,
                    LastUpdatedAt = DateTime.UtcNow,
                    Title = "New Conversation"
                };
            }

            return _conversations[conversationId];
        }

        /// <summary>
        /// Get conversation by ID
        /// </summary>
        public Conversation GetConversation(string conversationId)
        {
            if (_conversations.TryGetValue(conversationId, out var conversation))
            {
                return conversation;
            }

            _logger.LogWarning($"⚠️ Conversation not found: {conversationId}");
            return null;
        }

        /// <summary>
        /// Add message to conversation
        /// </summary>
        public void AddMessage(string conversationId, Message message)
        {
            var conversation = GetOrCreateConversation(conversationId);
            conversation.Messages.Add(message);
            conversation.LastUpdatedAt = DateTime.UtcNow;

            // Update title if it's the first user message
            if (conversation.Messages.Count == 1 && message.Role == "user")
            {
                // Use first 50 characters of first user message as title
                conversation.Title = message.Content.Length > 50 
                    ? message.Content.Substring(0, 50) + "..." 
                    : message.Content;
                
                _logger.LogInformation($"💬 Conversation title set: {conversation.Title}");
            }

            _logger.LogInformation($"✉️ Message added to conversation {conversationId}");
        }

        /// <summary>
        /// Get conversation history (list of messages)
        /// </summary>
        /// <param name="conversationId"></param>
        /// <returns></returns>
        public List<Message> GetConversationHistory(string conversationId)
        {
            if (string.IsNullOrEmpty(conversationId))
            {
                return new List<Message>();
            }
            
            var conversation = GetConversation(conversationId);
            return conversation?.Messages ?? new List<Message>();
        }

        /// <summary>
        /// Get all conversations
        /// </summary>
        public List<Conversation> GetAllConversations()
        {
            return _conversations.Values.OrderByDescending(c => c.LastUpdatedAt).ToList();
        }

        /// <summary>
        /// Delete conversation
        /// </summary>
        public void DeleteConversation(string conversationId)
        {
            if (_conversations.Remove(conversationId))
            {
                _logger.LogInformation($"🗑️ Conversation deleted: {conversationId}");
            }
        }
    }
}