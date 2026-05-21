# 🧠 Semantic Kernel ChatBot API - Backend

A production-ready **C# / ASP.NET Core** REST API powered by **Semantic Kernel** and **Azure OpenAI**. This backend handles AI processing, chat management, and API orchestration.

> **Part of**: [Semantic Kernel ChatBot](https://github.com/yourusername/semantic-kernel-chatbot) - Full-stack AI chatbot project
> 
> **Frontend Repository**: [chatbot-frontend](https://github.com/yourusername/chatbot-frontend)

---

## 📋 Table of Contents

- [Features](#features)
- [Tech Stack](#tech-stack)
- [Prerequisites](#prerequisites)
- [Quick Start](#quick-start)
- [Project Structure](#project-structure)
- [API Endpoints](#api-endpoints)
- [Configuration](#configuration)
- [Development](#development)
- [Deployment](#deployment)
- [Troubleshooting](#troubleshooting)
- [Contributing](#contributing)

---

## ✨ Features

### Current Version
- ✅ **REST API** - Clean, well-documented endpoints
- ✅ **Semantic Kernel Integration** - Modern AI orchestration
- ✅ **Azure OpenAI** - GPT-3.5 and GPT-4 support
- ✅ **Error Handling** - Comprehensive exception management
- ✅ **Logging** - Built-in request/response logging
- ✅ **CORS Support** - Ready for React frontend
- ✅ **Health Checks** - API availability monitoring
- ✅ **Input Validation** - Safe message handling

### Planned Features
- 🔄 Conversation memory management
- 🔧 Function calling & custom tools
- 💾 Database integration
- 🔐 Authentication & authorization
- 📊 Usage analytics & cost tracking
- 🚀 Performance optimization
- 📈 Rate limiting

---

## 🛠️ Tech Stack

| Component | Technology | Version | Purpose |
|-----------|-----------|---------|---------|
| **Framework** | ASP.NET Core | 8.0 | Web API |
| **Language** | C# | 12 | Backend code |
| **AI Framework** | Semantic Kernel | 1.8.0+ | AI orchestration |
| **LLM Service** | Azure OpenAI | - | Language models |
| **Config** | DotNetEnv | 2.1.1 | Environment variables |
| **Logging** | ILogger | Built-in | Request tracking |
| **API Format** | REST/JSON | - | Communication |

---

## 📋 Prerequisites

### System Requirements
- **[.NET 8 SDK](https://dotnet.microsoft.com/download)** - .NET runtime
- **[Git](https://git-scm.com/)** - Version control
- **[Visual Studio Code](https://code.visualstudio.com/)** - Code editor (optional but recommended)
  - Extension: C# (powered by OmniSharp)

### Azure Requirements
- **[Azure Account](https://azure.microsoft.com/free/)** - With $200 free credit
- **Azure OpenAI Resource** - Deployed with a model
- **API Key & Endpoint** - From Azure Portal

### Development Tools (Optional)
- **[Postman](https://www.postman.com/)** - API testing
- **[Thunder Client](https://www.thunderclient.com/)** - VS Code API client

---

## 🚀 Quick Start

### 1️⃣ Clone Repository

```bash
git clone https://github.com/yourusername/semantic-kernel-chatbot-api.git
cd semantic-kernel-chatbot-api
```

### 2️⃣ Install Dependencies

```bash
dotnet restore
```

### 3️⃣ Configure Azure Credentials

```bash
# Copy template
cp .env.template .env

# Edit .env with your Azure credentials
# AZURE_OPENAI_ENDPOINT=https://your-resource.openai.azure.com/
# AZURE_OPENAI_API_KEY=your-api-key
# AZURE_OPENAI_DEPLOYMENT_NAME=chatbot-gpt35
```

**⚠️ IMPORTANT**: Never commit `.env` to version control!

### 4️⃣ Build Project

```bash
dotnet build
```

Expected output:
```
Build succeeded.
```

### 5️⃣ Run API

```bash
dotnet run
```

Expected output:
```
🤖 API running on: http://localhost:5000
📡 Connected to Azure OpenAI: https://your-resource.openai.azure.com/
🧠 Using deployment: chatbot-gpt35
```

### 6️⃣ Test API

```bash
# Health check
curl http://localhost:5000/api/chat/health

# Send a message
curl -X POST http://localhost:5000/api/chat \
  -H "Content-Type: application/json" \
  -d '{"message":"Hello, how are you?"}'
```

---

## 📁 Project Structure

```
SemanticKernelChatbot/
│
├── Controllers/
│   └── ChatController.cs              # Main API controller
│       ├── POST /api/chat             # Process message
│       └── GET /api/chat/health       # Health check
│
├── Models/
│   ├── ChatRequest.cs                 # Request DTO
│   ├── ChatResponse.cs                # Response DTO
│   └── Conversation.cs                # (Future) Chat data
│
├── Services/
│   ├── ConversationService.cs         # (Future) Chat management
│   └── RAGService.cs                  # (Future) Knowledge search
│
├── Middleware/
│   └── ErrorHandlingMiddleware.cs     # (Future) Error handling
│
├── Program.cs                         # Application startup
├── appsettings.json                   # App configuration
├── appsettings.Development.json       # Dev-specific config
├── SemanticKernelChatbot.csproj       # Project file
├── .env.template                      # Environment template
├── .gitignore                         # Git ignore rules
├── README.md                          # This file
└── LICENSE                            # MIT License
```

---

## 🔌 API Endpoints

### 1. Chat Endpoint

**POST** `/api/chat`

Send a message and get an AI response.

**Request:**
```json
{
  "message": "What is machine learning?"
}
```

**Response (200 OK):**
```json
{
  "response": "Machine learning is a subset of artificial intelligence...",
  "timestamp": "2024-05-21T10:30:00Z"
}
```

**Error Responses:**
- `400 Bad Request` - Empty or invalid message
- `503 Service Unavailable` - Azure OpenAI unreachable
- `500 Internal Server Error` - Unexpected error

**cURL Example:**
```bash
curl -X POST http://localhost:5000/api/chat \
  -H "Content-Type: application/json" \
  -d '{
    "message": "Tell me about Semantic Kernel"
  }'
```

**JavaScript/Fetch Example:**
```javascript
const response = await fetch('http://localhost:5000/api/chat', {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify({ message: 'Hello!' })
});

const data = await response.json();
console.log(data.response);
```

### 2. Health Check Endpoint

**GET** `/api/chat/health`

Check if API is running and healthy.

**Response (200 OK):**
```json
{
  "status": "healthy",
  "timestamp": "2024-05-21T10:30:00Z"
}
```

**cURL Example:**
```bash
curl http://localhost:5000/api/chat/health
```

---

## ⚙️ Configuration

### Environment Variables (.env)

```env
# ===== REQUIRED =====
# Azure OpenAI Configuration
AZURE_OPENAI_ENDPOINT=https://your-resource.openai.azure.com/
AZURE_OPENAI_API_KEY=your-api-key-here
AZURE_OPENAI_DEPLOYMENT_NAME=chatbot-gpt35

# ===== OPTIONAL =====
# Server Configuration
ASPNETCORE_URLS=http://localhost:5000
ASPNETCORE_ENVIRONMENT=Development

# Logging
LOG_LEVEL=Information
```

### appsettings.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning"
    }
  }
}
```

### Security Best Practices

⚠️ **Critical for Production:**

```csharp
// In Program.cs
// 1. Use Azure Key Vault instead of .env
var keyVaultUrl = new Uri(Environment.GetEnvironmentVariable("KEY_VAULT_URL"));
var credential = new DefaultAzureCredential();
builder.Configuration.AddAzureKeyVault(keyVaultUrl, credential);

// 2. Add HTTPS requirement
builder.WebHost.UseUrls("https://localhost:5001");

// 3. Add authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.Authority = "https://your-auth-server";
        options.Audience = "your-api";
    });
```

---

## 💻 Development

### Build Commands

```bash
# Clean build
dotnet clean
dotnet build

# Build with release configuration
dotnet build -c Release

# Build and run
dotnet run

# Build and run with specific port
dotnet run --urls "http://localhost:5001"
```

### Testing

```bash
# Run all tests (when you add test projects)
dotnet test

# Run tests with coverage
dotnet test /p:CollectCoverage=true

# Run specific test
dotnet test --filter "ClassName"
```

### Debugging

**VS Code Launch Configuration** (`.vscode/launch.json`):

```json
{
    "version": "0.2.0",
    "configurations": [
        {
            "name": ".NET Core Launch (web)",
            "type": "coreclr",
            "request": "launch",
            "preLaunchTask": "build",
            "program": "${workspaceFolder}/bin/Debug/net8.0/SemanticKernelChatbot.dll",
            "args": [],
            "cwd": "${workspaceFolder}",
            "stopAtEntry": false,
            "serverReadyAction": {
                "pattern": "\\bNow listening on:\\s+(https?://\\S+)",
                "uriFormat": "{0}",
                "action": "openExternally"
            }
        }
    ]
}
```

### Adding Dependencies

```bash
# Add NuGet package
dotnet add package PackageName --version 1.0.0

# Update package
dotnet add package PackageName --version 2.0.0

# Remove package
dotnet remove package PackageName

# List installed packages
dotnet list package
```

### Common Development Issues

**Port Already in Use:**
```bash
# macOS/Linux
lsof -ti:5000 | xargs kill -9

# Windows
netstat -ano | findstr :5000
taskkill /PID <PID> /F
```

**IKernel Not Found:**
```csharp
// Add missing using statements
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

// Use Kernel instead of IKernel
private readonly Kernel _kernel;
```

---

## 🚢 Deployment

### Azure App Service

```bash
# Login to Azure
az login

# Create resource group
az group create --name chatbot-rg --location eastus

# Create App Service plan
az appservice plan create \
  --name chatbot-plan \
  --resource-group chatbot-rg \
  --sku B1

# Create web app
az webapp create \
  --resource-group chatbot-rg \
  --plan chatbot-plan \
  --name my-chatbot-api \
  --runtime "DOTNETCORE|8.0"

# Publish application
dotnet publish -c Release -o ./publish

# Deploy
az webapp deployment source config-zip \
  --resource-group chatbot-rg \
  --name my-chatbot-api \
  --src publish.zip
```

### Docker Deployment

```dockerfile
# Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "SemanticKernelChatbot.dll"]
```

```bash
# Build image
docker build -t chatbot-api:latest .

# Run container
docker run -p 5000:5000 --env-file .env chatbot-api:latest
```

### Environment Variables in Azure

Set in Azure App Service > Configuration > Application settings:

```
AZURE_OPENAI_ENDPOINT = https://your-resource.openai.azure.com/
AZURE_OPENAI_API_KEY = your-api-key
AZURE_OPENAI_DEPLOYMENT_NAME = chatbot-gpt35
ASPNETCORE_ENVIRONMENT = Production
```

---

## 🧪 Testing the API

### Using Postman

1. Create new POST request to `http://localhost:5000/api/chat`
2. Set header: `Content-Type: application/json`
3. Body (raw JSON):
   ```json
   {
     "message": "Hello, what can you help me with?"
   }
   ```
4. Send and see response

### Using Thunder Client (VS Code)

1. Install Thunder Client extension
2. Create new request
3. Set method to POST
4. URL: `http://localhost:5000/api/chat`
5. Add JSON body:
   ```json
   {
     "message": "Tell me about C#"
   }
   ```

### Using PowerShell

```powershell
$body = @{
    message = "Hello!"
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5000/api/chat" `
    -Method Post `
    -ContentType "application/json" `
    -Body $body
```

---

## 🐛 Troubleshooting

### Build Errors

| Error | Solution |
|-------|----------|
| `error CS0246: IKernel not found` | Add `using Microsoft.SemanticKernel;` |
| `Package not found` | Run `dotnet restore` |
| `Duplicate symbol` | Check for duplicate using statements |
| `Build failed` | Run `dotnet clean` then `dotnet build` |

### Runtime Errors

| Error | Solution |
|-------|----------|
| `401 Unauthorized` | Verify API key in .env |
| `Endpoint not found` | Check endpoint URL ends with `/` |
| `Connection timeout` | Verify Azure OpenAI resource exists |
| `Port already in use` | Kill process on port 5000 |

### Azure OpenAI Issues

**Issue**: "401 Unauthorized"
```
Solution:
1. Copy API key again from Azure Portal
2. Check for extra spaces in .env
3. Verify endpoint includes region: https://xxx.openai.azure.com/
4. Check deployment name matches exactly
```

**Issue**: "Model deployment not found"
```
Solution:
1. Verify deployment exists in Azure Portal
2. Check spelling of AZURE_OPENAI_DEPLOYMENT_NAME
3. Ensure you're in correct Azure subscription
```

**Issue**: "Rate limit exceeded"
```
Solution:
1. Implement caching
2. Add retry logic with exponential backoff
3. Consider upgrading Azure pricing tier
```

---

## 🤝 Contributing

### Setup Development Environment

```bash
# 1. Fork repository
# 2. Clone your fork
git clone https://github.com/YOUR_USERNAME/semantic-kernel-chatbot-api.git
cd semantic-kernel-chatbot-api

# 3. Create feature branch
git checkout -b feature/amazing-feature

# 4. Make changes and commit
git commit -m "Add amazing feature"

# 5. Push to your fork
git push origin feature/amazing-feature

# 6. Create Pull Request on GitHub
```

### Code Style Guidelines

```csharp
// Use meaningful names
private readonly IChatCompletionService _chatService; // ✓ Good
private readonly ICS _cs; // ✗ Bad

// Add comments for complex logic
// Call Azure OpenAI to generate response
var response = await _service.GetChatMessageContentAsync(chatHistory);

// Use async/await
public async Task<ActionResult> ProcessMessage(string message)
{
    var result = await _service.ProcessAsync(message);
    return Ok(result);
}

// Handle exceptions
try
{
    // code
}
catch (HttpRequestException ex)
{
    _logger.LogError($"API error: {ex.Message}");
    return StatusCode(503, new { error = "Service unavailable" });
}
```

### Commit Message Format

```
feat: Add conversation memory
fix: Resolve API timeout issue
docs: Update README with deployment steps
test: Add unit tests for ChatController
refactor: Simplify error handling
```

---

## 📚 Documentation

- **[Setup Guide](../docs/SETUP_GUIDE.md)** - Detailed setup instructions
- **[API Documentation](./API.md)** - Complete API reference
- **[Architecture](../docs/ARCHITECTURE.md)** - System design
- **[Semantic Kernel Docs](https://learn.microsoft.com/semantic-kernel/)** - Official SK docs
- **[Azure OpenAI Docs](https://learn.microsoft.com/azure/ai-services/openai/)** - Official Azure docs

---

## 📊 Performance & Costs

### API Response Times

| Operation | Expected Time | Notes |
|-----------|---------------|-------|
| Health check | <10ms | No external calls |
| Chat request | 1-3 seconds | Depends on AI model |
| Error response | <100ms | Validation only |

### Azure Costs

```
GPT-3.5:
- Input: $0.50 per 1M tokens
- Output: $1.50 per 1M tokens
- Average: $0.005 per 1K tokens

GPT-4:
- Input: $30 per 1M tokens
- Output: $60 per 1M tokens
- Average: $0.03 per 1K tokens

Free tier: First 1M tokens free per month
```

### Cost Optimization

```csharp
// 1. Implement response caching
services.AddStackExchangeRedisCache(options => {
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

// 2. Batch requests
// Send multiple messages at once instead of one-by-one

// 3. Use streaming
// Stream responses to avoid holding entire response in memory

// 4. Monitor usage
_logger.LogInformation($"Tokens used: {tokenCount}, Cost: ${cost}");
```

---

## 🔗 Related Repositories

- **[Frontend (React)](https://github.com/yourusername/chatbot-frontend)** - Chat UI
- **[Main Project](https://github.com/yourusername/semantic-kernel-chatbot)** - Full-stack repo
- **[Documentation](https://github.com/yourusername/semantic-kernel-chatbot/tree/main/docs)** - Detailed docs

---

## 📝 License

This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details.

---

## 🙏 Acknowledgments

- [Microsoft Semantic Kernel](https://github.com/microsoft/semantic-kernel) - Amazing framework
- [Azure OpenAI Service](https://azure.microsoft.com/services/cognitive-services/openai-service/) - AI models
- [ASP.NET Core](https://dotnet.microsoft.com/) - Web framework
- Contributors and community

---

## 📞 Support

### Get Help

- 📖 Check [Documentation](../docs/)
- 🐛 Search [GitHub Issues](https://github.com/yourusername/semantic-kernel-chatbot-api/issues)
- 💬 [GitHub Discussions](https://github.com/yourusername/semantic-kernel-chatbot-api/discussions)

### Report Issues

[Open a bug report](https://github.com/yourusername/semantic-kernel-chatbot-api/issues/new) with:
- Detailed description
- Steps to reproduce
- Expected vs actual behavior
- Environment details (.NET version, OS)
- Error logs

---

## 🌟 Give Us a Star

If this project helps you, please star it on GitHub! ⭐

```bash
# Clone
git clone https://github.com/yourusername/semantic-kernel-chatbot-api.git

# Star on GitHub
# Visit: https://github.com/yourusername/semantic-kernel-chatbot-api
# Click the ⭐ button
```

---

## 🚀 Quick Links

- [Quick Start](#quick-start) - Get running in 5 minutes
- [API Endpoints](#api-endpoints) - Available endpoints
- [Configuration](#configuration) - Setup guide
- [Deployment](#deployment) - Deploy to Azure
- [Contributing](#contributing) - How to contribute

---

**Made with ❤️ for the Gen AI community**

Happy coding! 🚀