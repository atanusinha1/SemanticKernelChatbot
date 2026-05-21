# 🤖 Semantic Kernel ChatBot with Azure OpenAI

A production-ready AI chatbot built with **Semantic Kernel**, **C# / ASP.NET Core**, **React**, and **Azure OpenAI** (GPT-3.5/4). This project demonstrates how to build modern AI applications using Microsoft's Semantic Kernel framework.

> **Perfect for**: Learning Gen AI development, building intelligent chat applications, understanding Semantic Kernel, and deploying to Azure.

---

## ✨ Features

### Current (Week 1)
- ✅ **Real-time Chat Interface** - Beautiful React UI with purple gradient theme
- ✅ **Azure OpenAI Integration** - Powered by GPT-3.5 or GPT-4
- ✅ **Semantic Kernel** - Modern AI orchestration library
- ✅ **REST API** - Clean C# backend with error handling
- ✅ **Responsive Design** - Works on desktop, tablet, and mobile
- ✅ **Message Timestamps** - Track when messages were sent
- ✅ **Loading States** - Visual feedback during processing
- ✅ **Error Handling** - Comprehensive error messages

### Coming Soon (Enterprise Roadmap)
- 🔄 Conversation Memory - Remember chat history
- 🔧 Function Calling - AI can trigger your code
- 💾 Database Integration - Persistent storage
- 🔐 Authentication - User login/roles
- 📚 RAG (Knowledge Base) - Connect to your documents
- 📊 Analytics & Monitoring - Track usage
- ☁️ Azure Deployment - Production-ready hosting

---

## 🏗️ Architecture

```
┌─────────────────────────────────────────────────────────┐
│                  Web Browser (React)                    │
│              http://localhost:3000                      │
│                                                         │
│  ┌──────────────────────────────────────────────────┐  │
│  │  ChatBox Component + Beautiful UI                │  │
│  │  - Message display and input                     │  │
│  │  - Real-time API calls                           │  │
│  └──────────────────────────────────────────────────┘  │
└─────────────────────┬──────────────────────────────────┘
                      │ HTTP POST
                      ↓
┌─────────────────────────────────────────────────────────┐
│         C# Backend API (ASP.NET Core)                   │
│              http://localhost:5000                      │
│                                                         │
│  ┌──────────────────────────────────────────────────┐  │
│  │  ChatController                                  │  │
│  │  - Handles /api/chat endpoint                    │  │
│  │  - Validates user input                          │  │
│  │  - Manages chat history                          │  │
│  └──────────────────────────────────────────────────┘  │
│                      ↓                                  │
│  ┌──────────────────────────────────────────────────┐  │
│  │  Semantic Kernel                                 │  │
│  │  - Formats prompts                               │  │
│  │  - Manages context                               │  │
│  │  - Handles AI orchestration                      │  │
│  └──────────────────────────────────────────────────┘  │
└─────────────────────┬──────────────────────────────────┘
                      │ HTTPS API Call
                      ↓
        ┌─────────────────────────────┐
        │   Azure OpenAI (Cloud)      │
        │                             │
        │   GPT-3.5 / GPT-4 Model     │
        │   - Processes prompt        │
        │   - Generates response      │
        │                             │
        └─────────────────────────────┘
```

---

## 🚀 Quick Start

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download) or later
- [Node.js 18+](https://nodejs.org/)
- [Visual Studio Code](https://code.visualstudio.com/)
- [Azure Account](https://azure.microsoft.com/free/) (with OpenAI resource)
- Git

### 1️⃣ Azure Setup (5 minutes)

1. Go to [Azure Portal](https://portal.azure.com)
2. Create "Azure OpenAI" resource
3. Deploy a model (GPT-3.5 or GPT-4)
4. Get your **API Key** and **Endpoint**

### 2️⃣ Clone Repository

```bash
git clone https://github.com/yourusername/semantic-kernel-chatbot.git
cd semantic-kernel-chatbot
```

### 3️⃣ Backend Setup

```bash
cd backend

# Install dependencies
dotnet restore

# Configure Azure credentials
cp .env.template .env
# Edit .env with your Azure OpenAI credentials:
# AZURE_OPENAI_ENDPOINT=https://your-resource.openai.azure.com/
# AZURE_OPENAI_API_KEY=your-api-key
# AZURE_OPENAI_DEPLOYMENT_NAME=chatbot-gpt35

# Run backend
dotnet run
# ✅ Backend running on http://localhost:5000
```

### 4️⃣ Frontend Setup (New Terminal)

```bash
cd frontend

# Install dependencies
npm install

# Start React app
npm start
# ✅ Frontend opens at http://localhost:3000
```

### 5️⃣ Test It! 🎉

1. Open http://localhost:3000
2. Type: "Hello, how are you?"
3. See AI response appear!

---

## 📁 Project Structure

```
semantic-kernel-chatbot/
│
├── backend/                          # C# ASP.NET Core API
│   ├── Controllers/
│   │   └── ChatController.cs         # Main API endpoints
│   ├── Models/
│   │   └── ChatRequest.cs            # Data models
│   ├── Program.cs                    # Application startup
│   ├── SemanticKernelChatbot.csproj  # Project file
│   ├── .env.template                 # Configuration template
│   └── .gitignore
│
├── frontend/                         # React Application
│   ├── src/
│   │   ├── components/
│   │   │   ├── ChatBox.jsx           # Main chat component
│   │   │   └── ChatBox.css           # Chat styling
│   │   ├── App.js                    # Root component
│   │   ├── App.css                   # Global styles
│   │   └── index.js                  # Entry point
│   ├── public/
│   │   └── index.html
│   ├── package.json                  # NPM dependencies
│   └── .env                          # Frontend config
│
├── docs/                             # Documentation
│   ├── SETUP_GUIDE.md                # Detailed setup
│   ├── QUICK_START.md                # Step-by-step guide
│   ├── ENTERPRISE_ROADMAP.md         # Enhancement path
│   └── TROUBLESHOOTING.md            # Common issues
│
├── README.md                         # This file
├── LICENSE
└── .gitignore
```

---

## ⚙️ Configuration

### Backend (.env)

```env
# Azure OpenAI
AZURE_OPENAI_ENDPOINT=https://your-resource.openai.azure.com/
AZURE_OPENAI_API_KEY=your-api-key-here
AZURE_OPENAI_DEPLOYMENT_NAME=chatbot-gpt35

# (Optional)
ASPNETCORE_URLS=http://localhost:5000
ASPNETCORE_ENVIRONMENT=Development
```

### Frontend (.env)

```env
REACT_APP_API_URL=http://localhost:5000/api
REACT_APP_ENVIRONMENT=development
```

### Important Security Notes

⚠️ **Never commit `.env` files to version control!**

- Add `.env` to `.gitignore`
- Use Azure Key Vault in production
- Rotate API keys regularly
- Use environment variables for secrets

---

## 🧪 Usage Examples

### Basic Chat

```javascript
// Frontend - Send message
const response = await axios.post('http://localhost:5000/api/chat', {
  message: 'What is machine learning?'
});

console.log(response.data.response); 
// Output: "Machine learning is a subset of AI..."
```

### Health Check

```bash
curl http://localhost:5000/api/chat/health

# Output:
# {"status":"healthy","timestamp":"2024-05-21T10:30:00Z"}
```

### Customize Bot Behavior

Edit `Program.cs` and modify the system prompt:

```csharp
var systemPrompt = @"You are a helpful customer service bot.
- Always be polite
- Keep responses under 100 words
- Offer solutions, not apologies";
```

---

## 🛠️ Technology Stack

| Layer | Technology | Version | Purpose |
|-------|-----------|---------|---------|
| **Frontend** | React | 18+ | Web UI |
| **Styling** | CSS3 | - | Beautiful UI |
| **Backend** | ASP.NET Core | 8.0 | REST API |
| **AI Framework** | Semantic Kernel | 1.8.0+ | AI Orchestration |
| **LLM** | Azure OpenAI | GPT-3.5/4 | Language Model |
| **API** | REST | - | Communication |
| **HTTP Client** | Axios | - | API Requests |

---

## 📊 Cost Estimates

| Usage Level | Monthly Cost | Example Use |
|-------------|-------------|------------|
| **Learning** | $0-10 | 100-500 messages |
| **Testing** | $10-50 | 1,000-5,000 messages |
| **Development** | $50-200 | 10,000+ messages |
| **Production** | $500+ | Commercial service |

### Cost Breakdown

- **Azure OpenAI (GPT-3.5)**: ~$0.005 per 1K tokens (75%)
- **Hosting**: ~$10-50/month (15%)
- **Database**: ~$5-20/month (5%)
- **Monitoring**: ~$5/month (5%)

---

## 🚢 Deployment

### Deploy to Azure App Service

```bash
# Publish backend
cd backend
dotnet publish -c Release -o ./publish

# Deploy
az webapp create --resource-group mygroup --plan myplan \
  --name my-chatbot-api --runtime "DOTNETCORE|8.0"

# Deploy frontend
cd frontend
npm run build
az webapp deployment source config-zip --resource-group mygroup \
  --name my-chatbot-web --src dist.zip
```

### Deploy to Docker

```bash
# Build Docker image
docker build -t chatbot-api:latest ./backend

# Run container
docker run -p 5000:5000 --env-file .env chatbot-api:latest
```

---

## 🐛 Troubleshooting

### Common Issues

#### Build Error: "IKernel not found"
```bash
# Solution: Update using statements
# Add to ChatController.cs:
using Microsoft.SemanticKernel;
```

#### "401 Unauthorized" from Azure
- ✅ Verify API key is correct (no extra spaces)
- ✅ Check endpoint URL ends with `/`
- ✅ Verify deployment name matches exactly

#### Frontend shows "Network error"
- ✅ Ensure backend is running on `http://localhost:5000`
- ✅ Check CORS is enabled in `Program.cs`
- ✅ Verify API URL in React `.env`

#### Port 5000 already in use
```bash
# macOS/Linux
lsof -ti:5000 | xargs kill -9

# Windows
netstat -ano | findstr :5000
taskkill /PID <PID> /F
```

---

## 📚 Documentation

- **[QUICK_START.md](docs/QUICK_START.md)** - Step-by-step setup guide
- **[SETUP_GUIDE.md](docs/SETUP_GUIDE.md)** - Detailed concepts and explanations
- **[ENTERPRISE_ROADMAP.md](docs/ENTERPRISE_ROADMAP.md)** - Path to production
- **[CHEAT_SHEET.md](docs/CHEAT_SHEET.md)** - Commands and code snippets

---

## 🎯 Learning Resources

### Official Documentation
- [Semantic Kernel Docs](https://learn.microsoft.com/semantic-kernel/)
- [Azure OpenAI Service](https://learn.microsoft.com/azure/ai-services/openai/)
- [ASP.NET Core Documentation](https://learn.microsoft.com/aspnet/core/)
- [React Documentation](https://react.dev/)

### Tutorials
- [Microsoft Learn - Azure AI](https://learn.microsoft.com/training/modules/use-openai-api/)
- [Semantic Kernel Getting Started](https://learn.microsoft.com/semantic-kernel/get-started/)

---

## 🗺️ Roadmap

### Phase 1: Foundation ✅ (Current)
- [x] Basic chatbot working
- [x] React frontend
- [x] C# backend with Semantic Kernel
- [x] Azure OpenAI integration

### Phase 2: Enhancement (Weeks 2-4)
- [ ] Conversation memory
- [ ] Chat history persistence
- [ ] Function calling
- [ ] Custom integrations

### Phase 3: Enterprise (Weeks 5-8)
- [ ] Database integration (SQL)
- [ ] User authentication
- [ ] Rate limiting
- [ ] Monitoring & logging

### Phase 4: Production (Weeks 9+)
- [ ] RAG (Knowledge base)
- [ ] Azure deployment
- [ ] Security hardening
- [ ] Performance optimization

---

## 🤝 Contributing

Contributions are welcome! Here's how:

1. **Fork** the repository
2. **Create** a feature branch (`git checkout -b feature/amazing-feature`)
3. **Commit** your changes (`git commit -m 'Add amazing feature'`)
4. **Push** to the branch (`git push origin feature/amazing-feature`)
5. **Open** a Pull Request

---

## 📝 License

This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details.

---

## 🙏 Acknowledgments

- [Microsoft Semantic Kernel](https://github.com/microsoft/semantic-kernel)
- [Azure OpenAI Service](https://azure.microsoft.com/en-us/services/cognitive-services/openai-service/)
- [React](https://react.dev/)
- Community feedback and contributions

---

## 📞 Support & Contact

### Get Help

- 📖 Check the [Documentation](docs/)
- 🐛 Search [GitHub Issues](https://github.com/yourusername/semantic-kernel-chatbot/issues)
- 💬 Ask on [GitHub Discussions](https://github.com/yourusername/semantic-kernel-chatbot/discussions)

### Report Issues

Found a bug? Please [open an issue](https://github.com/yourusername/semantic-kernel-chatbot/issues/new)

---

## 🌟 Star This Repo

If this project helped you, please give it a ⭐ on GitHub!

---

**Made with ❤️ for the Gen AI community**

Happy coding! 🚀