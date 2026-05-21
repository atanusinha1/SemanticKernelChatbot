// File: Program.cs
// This file sets up the ASP.NET Core application and configures Semantic Kernel with Azure OpenAI

using Microsoft.SemanticKernel;
using Azure.Identity;
using Microsoft.SemanticKernel.ChatCompletion;
using SemanticKernelChatbot.Services;
//using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// ===== CONFIGURATION SETUP =====
// Load environment variables from .env file
DotNetEnv.Env.Load();

// Get Azure OpenAI credentials from environment variables
var endpoint = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");
var apiKey = Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY");
var deploymentName = Environment.GetEnvironmentVariable("AZURE_OPENAI_DEPLOYMENT_NAME");

// Validate that all required credentials are present
if (string.IsNullOrEmpty(endpoint) || string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(deploymentName))
{
    throw new InvalidOperationException(
        "Missing required environment variables: AZURE_OPENAI_ENDPOINT, AZURE_OPENAI_API_KEY, or AZURE_OPENAI_DEPLOYMENT_NAME");
}

// ===== SEMANTIC KERNEL SETUP =====
// Build the Semantic Kernel with Azure OpenAI
var builder2 = Kernel.CreateBuilder()
    .AddAzureOpenAIChatCompletion(
        deploymentName: deploymentName,
        endpoint: endpoint,
        apiKey: apiKey);

var kernel = builder2.Build();

// Register the kernel in dependency injection
builder.Services.AddSingleton(kernel);

// Add ConversationService to dependency injection
builder.Services.AddSingleton<IConversationService, ConversationService>();

// ===== STANDARD ASP.NET CORE SETUP =====
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ===== CORS CONFIGURATION =====
// Allow React frontend (running on localhost:3000) to communicate with this API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:3001")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// ===== MIDDLEWARE SETUP =====
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS with the policy we created
app.UseCors("AllowReactApp");

// Use HTTP (not HTTPS) for local development
app.UseRouting();
app.MapControllers();

// ===== SERVER CONFIGURATION =====
// Configure to run on port 5000
var urls = Environment.GetEnvironmentVariable("ASPNETCORE_URLS") ?? "http://localhost:5000";
app.Urls.Clear();
app.Urls.Add(urls);

Console.WriteLine($"🤖 API running on: {urls}");
Console.WriteLine($"📡 Connected to Azure OpenAI: {endpoint}");
Console.WriteLine($"🧠 Using deployment: {deploymentName}");

app.Run();