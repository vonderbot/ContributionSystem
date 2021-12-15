using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace ContributionSystem.UI.CustomAccounts
{
    /// <summary>
    /// User claims.
    /// </summary>
    public class CustomUserAccount : RemoteUserAccount
    {
        private const string RolesName = "roles";
        private const string WidsName = "wids";
        private const string OidName = "oid";

        /// <summary>
        /// User app roles.
        /// </summary>
        [JsonPropertyName(RolesName)]
        public string[] Roles { get; set; } = Array.Empty<string>();

        /// <summary>
        /// User tenant roles.
        /// </summary>
        [JsonPropertyName(WidsName)]
        public string[] Wids { get; set; } = Array.Empty<string>();

        /// <summary>
        /// User identifier.
        /// </summary>
        [JsonPropertyName(OidName)]
        public string Oid { get; set; }
    }
}