using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

/// <summary>
/// User claims.
/// </summary>
public class CustomUserAccount : RemoteUserAccount
{
    /// <summary>
    /// User app roles.
    /// </summary>
    [JsonPropertyName("roles")]
    public string[] Roles { get; set; } = Array.Empty<string>();

    /// <summary>
    /// User tenant roles.
    /// </summary>
    [JsonPropertyName("wids")]
    public string[] Wids { get; set; } = Array.Empty<string>();

    /// <summary>
    /// User id.
    /// </summary>
    [JsonPropertyName("oid")]
    public string Oid { get; set; }
}