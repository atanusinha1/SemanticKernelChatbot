// File: Models/Conversation.cs
// Data model representing an entire conversation with message history

using System;
using System.Collections.Generic;
using System.Linq;

namespace SemanticKernelChatbot.Models
{
    public class Conversation
    {
        /// <summary>
        /// Unique identifier for this conversation
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// List of all messages in this conversation (ordered chronologically)
        /// </summary>
        public List<Message> Messages { get; set; } = new List<Message>();

        /// <summary>
        /// When was this conversation created
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// When was this conversation last updated
        /// </summary>
        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Human-readable title for this conversation
        /// Usually set from the first user message
        /// </summary>
        public string Title { get; set; } = "New Conversation";

        /// <summary>
        /// Get total number of messages in this conversation
        /// </summary>
        public int MessageCount => Messages.Count;

        /// <summary>
        /// Get number of user messages
        /// </summary>
        public int UserMessageCount => Messages.Count(m => m.Role == "user");

        /// <summary>
        /// Get number of assistant messages
        /// </summary>
        public int AssistantMessageCount => Messages.Count(m => m.Role == "assistant");
    }
}