# PromptFlow-React-Streaming-chatbot

A streaming chatbot application that demonstrates real-time AI chat responses using React frontend and ASP.NET Core backend with PromptFlow integration.

## Architecture

This application consists of:
- **Frontend**: React/TypeScript component for chat interface
- **Backend**: ASP.NET Core Web API that proxies requests to PromptFlow
- **External Service**: Azure PromptFlow service for AI processing

## Documentation

📋 **[UML Diagrams](docs/uml/README.md)** - Comprehensive architectural documentation including:
- Class diagrams showing data models and relationships
- Sequence diagrams illustrating the streaming chat flow
- Component diagrams depicting the overall architecture
- Deployment diagrams showing runtime environment

## Key Features

- **Real-time Streaming**: Uses Server-Sent Events (SSE) for live response streaming
- **Proxy Architecture**: Backend handles authentication and forwards requests
- **React Integration**: Clean TypeScript React component for chat interface
- **Error Handling**: Proper abort handling and error management

## Setup Requirements

1. Configure your PromptFlow API key in `ChatController.cs`:
   ```csharp
   string apiKey = "YOUR API KEY"; // <-- Change this
   ```

2. Set your PromptFlow service URI in `ChatController.cs`:
   ```csharp
   client.BaseAddress = new Uri("YOUR PROMPT FLOW URI/score"); // <-- Change this
   ```

3. Update the backend URL in `Chatbot.tsx`:
   ```typescript
   const response = await fetch('https://localhost:XXXX/api/Chat', { // <-- Change this
   ```

## Disclaimer

This Sample Code is provided for the purpose of illustration only and is not intended to be used in a production environment. THIS SAMPLE CODE AND ANY RELATED INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE. We grant You a nonexclusive, royalty-free right to use and modify the Sample Code and to reproduce and distribute the object code its authors,or anyone else involved in the creation, production, or delivery of the scripts be liable for any damages whatsoever (including, without limitation, damages for loss of business profits, business interruption, loss of business information, or other pecuniary loss) arising out of the use of or inability to use the sample scripts or documentation, even if its has been advised of the possibility of such damages
