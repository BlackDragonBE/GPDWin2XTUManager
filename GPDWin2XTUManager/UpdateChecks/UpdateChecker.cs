using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GPDWin2XTUManager.UpdateChecks
{
    public static class UpdateChecker
    {
        private static decimal _thisVersion;

        private static readonly string RELEASE_URL =
            @"https://api.github.com/repos/BlackDragonBE/GPDWin2XTUManager/releases/latest";

        public static async Task<GithubRelease> CheckForUpdates()
        {
            _thisVersion = Shared.VERSION;

            GithubRelease newestRelease = await GetNewestReleaseInfo();

            if (newestRelease != null)
            {
                var githubNewestVersion = Convert.ToDecimal(newestRelease.tag_name);

                if (githubNewestVersion > _thisVersion)
                {
                    // New version available
                    return newestRelease;
                }

                // Application is up to date
            }
            else
            {
                // Download error, ignore
            }

            return null;
        }

        private static async Task<GithubRelease> GetNewestReleaseInfo()
        {
            GithubRelease release = null;

            await Task.Run(() =>
            {
                try
                {
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("User-Agent", "GPDWin2XTUManager");
                    client.DefaultRequestHeaders
                        .Accept
                        .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    Task<HttpResponseMessage> response = client.GetAsync(RELEASE_URL);

                    release = JsonConvert.DeserializeObject<GithubRelease>(response.Result.Content.ReadAsStringAsync()
                        .Result);
                }
                catch (Exception e)
                {
                    // Update check failed, ignore
                }
            });

            return release;
        }
    }
}
