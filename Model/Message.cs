// File: Models/Message.cs
// Data model representing a single message in a conversation

using System;

namespace SemanticKernelChatbot.Models
{
    public class Message
    {
        /// <summary>
        /// Unique identifier for this message
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Role: "user" or "assistant"
        /// </summary>
        public string Role { get; set; } = string.Empty;

        /// <summary>
        /// The actual message content
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// When was this message created
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}