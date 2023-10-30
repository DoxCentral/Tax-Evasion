using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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
        /// The mass report function below!
        /// This was made using tasks so it's more on the newer side.
        /// This uses an older api /v6 for less rate limiting.
        /// </summary>
        /// <param name="proxies"></param>
        /// <param name="token"></param>
        /// <param name="reason"></param>
        /// <param name="guil_ID"></param>
        /// <param name="chan_ID"></param>
        /// <param name="mes_ID"></param>
        /// <param name="amt"></param>
        /// <returns></returns>
        public static async Task MRPT(string token, int reason, string guil_ID, string chan_ID, string mes_ID, int amt)
        {
            ulong totalreports = 0;
            List<Task> tasks = new List<Task>();
            List<string> prox_USE = File.ReadAllLines("Settings/proxies.txt").ToList();

            int proxyIndex = 0; // Initialize the proxy index

            /// <summary>
            /// For loop to set tasks
            /// </summary>
            for (int i = 0; i < amt; i++)
            {
                var currentProxy = prox_USE[proxyIndex]; // Get the current proxy

                tasks.Add(Task.Run(async () =>
                {
                    /// <summary>
                    /// Request function
                    /// </summary>
                    using (HttpClient client = new HttpClient())
                    {
                        // Use the next or current proxy
                        var handler = new HttpClientHandler
                        {
                            Proxy = new WebProxy(currentProxy),
                            UseProxy = true,
                        };
                        client.Timeout = TimeSpan.FromMinutes(1);
                        client.DefaultRequestHeaders.Add("Authorization", token);

                        // set json
                        var jsS = new
                        {
                            channel_id = chan_ID,
                            guild_id = guil_ID,
                            message_id = mes_ID,
                            reason = reason,
                        };

                        // build json
                        var serJS = JsonConvert.SerializeObject(jsS);
                        var content_JS = new StringContent(serJS, Encoding.UTF8, "application/json");

                        // send request
                        var response = await client.PostAsync("https://discord.com/api/v6/report", content_JS);

                        // if it works
                        if (response.IsSuccessStatusCode)
                        {
                            totalreports++;
                            CLI.WriteLine(Color.LimeGreen, $"Sent a report successfully: {totalreports}");
                        }
                        /// <summary>
                        /// Handler for rate limits
                        /// </summary>
                        else if ((int)response.StatusCode == 429)
                        {
                            var retryAfter = await response.Content.ReadAsStringAsync();
                            var CountGetter = JsonConvert.DeserializeObject<dynamic>(retryAfter);

                            CLI.WriteLine(Color.DarkRed, $"Rate Limited/Sleeping For {CountGetter["retry_after"]}");

                            if (float.TryParse(CountGetter["retry_after"].ToString(), out float wait))
                            {
                                int millisecondsToSleep = (int)Math.Round(wait * 1000);
                                await Task.Delay(millisecondsToSleep);
                            }
                            else
                            {
                                CLI.WriteLine(Color.DarkRed, $"Could Not Retry... => {CountGetter["retry_after"]}");
                            }
                        }
                    }
                }));

                // Update the proxy index
                proxyIndex = (proxyIndex + 1) % prox_USE.Count;
            }

            /// <summary>
            /// Run all of the tasks
            /// </summary>
            await Task.WhenAll(tasks);
            await Task.Delay(100);

            CLI.WriteLine(Color.Yellow, $"Sent a total of {totalreports} reports.");
        }
    }
}
