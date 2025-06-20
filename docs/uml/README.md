# UML Diagrams for PromptFlow React Streaming Chatbot

This directory contains UML diagrams that document the architecture and design of the PromptFlow React Streaming Chatbot application.

## Overview

The application is a streaming chatbot that consists of:
- **Frontend**: React/TypeScript component for the user interface
- **Backend**: ASP.NET Core Web API that acts as a proxy
- **External Service**: PromptFlow service for AI chat processing

## Diagrams

### 1. Architecture Overview (`architecture-overview.puml`)
High-level view of the system architecture and data flow:
- **User Browser**: React application with chatbot component
- **Web Server**: ASP.NET Core API acting as proxy
- **Azure Services**: PromptFlow and Azure OpenAI integration

**Data Flow:**
1. User interacts with React chatbot
2. Frontend sends POST request to backend API
3. Backend forwards request to PromptFlow with authentication
4. AI service streams response back through the proxy
5. Frontend displays real-time streaming updates

### 2. Class Diagram (`class-diagram.puml`)
Shows the data models and their relationships:
- **Backend Models**: `ChatRequest`, `ChatHistory`, `ChatInput`, `ChatOutput`
- **Controller**: `ChatController` with its methods
- **Frontend Component**: `Chatbot` React component
- **External Service**: `PromptFlowService` interface

**Key Relationships:**
- `ChatRequest` contains multiple `ChatHistory` entries
- Each `ChatHistory` has `ChatInput` and `ChatOutput`
- `ChatController` processes `ChatRequest` objects
- `Chatbot` component communicates with `ChatController`

### 3. Sequence Diagram (`sequence-diagram.puml`)
Illustrates the flow of a streaming chat request:
1. User loads the chatbot component
2. Frontend sends POST request to backend API
3. Backend forwards request to PromptFlow service
4. PromptFlow streams response back through backend
5. Frontend processes streaming chunks and updates UI

**Key Features:**
- Real-time streaming using Server-Sent Events (SSE)
- Backend acts as an authenticated proxy
- Frontend handles streaming data incrementally

### 4. Component Diagram (`component-diagram.puml`)
Shows the high-level architecture and component interactions:
- **Frontend Layer**: React app with Chatbot component
- **Backend Layer**: ASP.NET Core API with ChatController
- **External Services**: PromptFlow and Azure OpenAI
- **Data Models**: Request/response structures

**Key Interfaces:**
- REST API for request/response
- Server-Sent Events for streaming
- Stream processing for data handling

### 5. Deployment Diagram (`deployment-diagram.puml`)
Depicts the runtime deployment architecture:
- **Client Browser**: Hosts React application
- **Web Server**: Runs ASP.NET Core API
- **Azure Cloud**: PromptFlow and OpenAI services
- **Configuration**: API keys and endpoints

**Network Flow:**
- HTTPS communication between all components
- Bearer token authentication to PromptFlow
- SSE streaming for real-time responses

## How to View the Diagrams

These diagrams are written in PlantUML format. You can view them using:

1. **Online PlantUML Editor**: http://www.plantuml.com/plantuml/uml/
2. **VS Code Extension**: PlantUML extension
3. **IntelliJ/WebStorm**: Built-in PlantUML support
4. **Command Line**: Install PlantUML locally

### Example: Viewing with PlantUML Online
1. Copy the content of any `.puml` file
2. Go to http://www.plantuml.com/plantuml/uml/
3. Paste the content and click "Submit"

## Architecture Highlights

### Streaming Architecture
- The application uses Server-Sent Events (SSE) for real-time streaming
- Backend acts as a proxy to handle authentication and CORS
- Frontend processes streaming chunks incrementally for smooth UX

### Data Flow
1. **Request**: `ChatRequest` → `ChatController` → `PromptFlow`
2. **Response**: `PromptFlow` → `ChatController` → `Chatbot` (streamed)

### Key Design Patterns
- **Proxy Pattern**: Backend acts as proxy to external service
- **Observer Pattern**: Frontend observes streaming updates
- **Adapter Pattern**: Backend adapts PromptFlow responses for frontend

## Configuration Requirements

The application requires configuration of:
- **API Key**: Authentication token for PromptFlow service
- **Service URI**: Endpoint URL for PromptFlow service
- **CORS Settings**: For cross-origin requests (if needed)

See the main README.md for setup instructions.