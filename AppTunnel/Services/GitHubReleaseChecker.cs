using System.Net.Http;
using System.Text.Json;

namespace AppTunnel.Services;

public sealed record GitHubReleaseInfo(
    Version Version,
    string TagName,
    string Name,
    string Url,
    bool IsPrerelease);

public static class GitHubReleaseChecker
{
    private const string LatestReleaseApi =
        "https://api.github.com/repos/MaxiFan/TunnelX/releases/latest";

    public static async Task<GitHubReleaseInfo?> GetLatestReleaseAsync(CancellationToken ct)
    {
        using var http = new HttpClient();
        http.DefaultRequestHeaders.UserAgent.ParseAdd("TunnelX");
        http.Timeout = TimeSpan.FromSeconds(8);

        using var response = await http.GetAsync(LatestReleaseApi, ct);
        if (!response.IsSuccessStatusCode)
            return null;

        await using var stream = await response.Content.ReadAsStreamAsync(ct);
        using var json = await JsonDocument.ParseAsync(stream, cancellationToken: ct);
        var root = json.RootElement;

        var tag = root.TryGetProperty("tag_name", out var tagElement)
            ? tagElement.GetString() ?? ""
            : "";
        if (!TryParseVersion(tag, out var version))
            return null;

        var name = root.TryGetProperty("name", out var nameElement)
            ? nameElement.GetString() ?? tag
            : tag;
        var url = root.TryGetProperty("html_url", out var urlElement)
            ? urlElement.GetString() ?? AppInfo.LatestReleaseUrl
            : AppInfo.LatestReleaseUrl;
        var prerelease = root.TryGetProperty("prerelease", out var preElement) &&
                         preElement.ValueKind == JsonValueKind.True;

        return new GitHubReleaseInfo(version, tag, name, url, prerelease);
    }

    public static bool TryParseVersion(string value, out Version version)
    {
        value = (value ?? "").Trim().TrimStart('v', 'V');
        return Version.TryParse(value, out version!);
    }
}
