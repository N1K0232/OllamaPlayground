using System;
using System.Collections.Generic;
using System.Text;

namespace OllamaPlayground.ApiService.BusinessLayer.Settings;

public class SwaggerSettings
{
    public bool IsEnabled { get; init; } = true;

    public string? UserName { get; init; }

    public string? Password { get; init; }
}