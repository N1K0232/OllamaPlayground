namespace OllamaPlayground.ApiService.BusinessLayer.Settings;

public class AppSettings
{
    public string ApplicationName { get; init; } = "Ollama Playground";

    public string ApplicationDescription { get; init; } = "Chat with Ollama AI";

    public string[] SupportedCultures { get; set; } = [];
}