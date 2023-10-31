using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Veylib.ICLI;

namespace TaxEvasion.cogs
{
    public class Events
    {
        /// <summary>
        /// Mass report function for Discord messages.
        /// </summary>
        /// <param name="token">Discord token for authentication.</param>
        /// <param name="reason">Reason for reporting (1-5, e.g., Illegal Content, Harassment).</param>
        /// <param name="guildId">Guild ID where the message is located.</param>
        /// <param name="channelId">Channel ID where the message is located.</param>
        /// <param name="messageId">Message ID to report.</param>
        /// <param name="reportCount">Number of reports to send.</param>
        /// <param name="proxies">List of proxy addresses to rotate.</param>
        /// <returns></returns>
        public static async Task MRPT(string token, int reason, string guildId, string channelId, string messageId, int reportCount, List<string> proxies)
        {
            ulong totalReports = 0;

            for (int i = 0; i < reportCount; i++)
            {
                string currentProxy = proxies[i % proxies.Count];

                try
                {
                    using (HttpClient client = CreateHttpClientWithProxy(currentProxy, token))
                    {
                        var reportData = new
                        {
                            channel_id = channelId,
                            guild_id = guildId,
                            message_id = messageId,
                            reason = reason,
                        };

                        string jsonData = JsonConvert.SerializeObject(reportData);
                        HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                        HttpResponseMessage response = await client.PostAsync("https://discord.com/api/v6/report", content);

                        if (response.IsSuccessStatusCode)
                        {
                            totalReports++;
                            CLI.WriteLine(Color.LimeGreen, $"Sent a report successfully: {totalReports}");
                        }
                        else if ((int)response.StatusCode == 429)
                        {
                            var retryAfter = await response.Content.ReadAsStringAsync();
                            var retryData = JsonConvert.DeserializeObject<dynamic>(retryAfter);

                            int waitMilliseconds = (int)Math.Round((float)retryData["retry_after"] * 1000);
                            await Task.Delay(waitMilliseconds);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions or errors here
                    CLI.WriteLine(Color.Red, $"Error while sending report: {ex.Message}");
                }
            }

            CLI.WriteLine(Color.Yellow, $"Sent a total of {totalReports} reports.");
        }

        private static HttpClient CreateHttpClientWithProxy(string proxy, string token)
        {
            var handler = new HttpClientHandler
            {
                Proxy = new WebProxy(proxy),
                UseProxy = true,
            };

            HttpClient client = new HttpClient(handler);
            client.Timeout = TimeSpan.FromMinutes(1);
            client.DefaultRequestHeaders.Add("Authorization", token);

            return client;
        }
    }
}
