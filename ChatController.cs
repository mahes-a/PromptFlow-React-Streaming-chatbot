using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    [HttpPost]
    public async Task Post([FromBody] ChatRequest request)
    {
        #region key
        string apiKey = "YOUR API KEY";// <-- Change this  
        #endregion

        using var client = new HttpClient();
        var requestBody = new
        {
            chat_input = request.chat_input,
            chat_history = request.chat_history
        };
        var jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
        var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/event-stream"));
        client.BaseAddress = new Uri("YOUR PROMPT FLOW URI/score"); // <-- Change this  

        using var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Post, "") { Content = content }, HttpCompletionOption.ResponseHeadersRead);

        Response.StatusCode = 200;
        Response.ContentType = "text/event-stream";
        Response.Headers.Add("Cache-Control", "no-cache");
        Response.Headers.Add("Connection", "keep-alive");

        using var responseStream = await response.Content.ReadAsStreamAsync();
        using var reader = new StreamReader(responseStream);

        var writer = Response.BodyWriter;

        await foreach (var chunk in GetStreamChunks(reader))
        {
            var sseData = Encoding.UTF8.GetBytes($"data: {chunk}\n\n");
            await writer.WriteAsync(sseData);
            await writer.FlushAsync();
        }
    }

    private async IAsyncEnumerable<string> GetStreamChunks(StreamReader reader)
    {
        string? line;
        while ((line = await reader.ReadLineAsync()) != null)
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                if (line.StartsWith("data: ")) line = line.Substring(6);
                string parsed = null;
                try
                {
                    var json = JObject.Parse(line);
                    parsed = json["chat_output"]?.ToString();
                }
                catch { }
                if (!string.IsNullOrWhiteSpace(parsed))
                    yield return parsed;
            }
        }
    }
}

// Add these model classes to the same file or a Models folder  
public class ChatRequest
{
    public string chat_input { get; set; }
    public ChatHistory[] chat_history { get; set; }
}
public class ChatHistory
{
    public ChatInput inputs { get; set; }
    public ChatOutput outputs { get; set; }
}
public class ChatInput
{
    public string question { get; set; }
}
public class ChatOutput
{
    public string answer { get; set; }
}