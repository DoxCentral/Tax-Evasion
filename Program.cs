using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using TaxEvasion.cogs;
using Veylib.ICLI;

namespace TaxEvasion
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            CLI.Start(Settings.sp);

            try
            {
                var token = GetInput("Enter your Discord token: ", Color.Green);
                var guildID = GetInput("Enter the guild ID: ", Color.Green);
                var channelID = GetInput("Enter the channel ID: ", Color.Green);
                var messageID = GetInput("Enter the message ID: ", Color.Green);

                CLI.WriteLine(Color.Blue, "Enter Reason/Why =>");
                var rsp = new SelectionMenu("1 : Illegal Content", "2 : Harassment", "3 : Spam or Phishing Links", "4 : Self harm", "5 : NSFW Content").Activate();

                int choosing = 0;

                switch (rsp)
                {
                    case "1 : Illegal Content": choosing = 1; break;
                    case "2 : Harassment": choosing = 2; break;
                    case "3 : Spam or Phishing Links": choosing = 3; break;
                    case "4 : Self harm": choosing = 4; break;
                    case "5 : NSFW Content": choosing = 5; break;
                    default:
                        CLI.Clear();
                        CLI.WriteLine(Color.Red, "Incorrect Option.");
                        Thread.Sleep(2000);
                        Environment.Exit(0);
                        break;
                }

                var amt = GetInput("How many reports?: ", Color.Green);
                await Events.MRPT(token, choosing, guildID, channelID, messageID, Convert.ToInt32(amt));
            }
            catch (Exception ex)
            {
                CLI.WriteLine(Color.Red, $"An error occurred: {ex.Message}");
            }
        }

        private static string GetInput(string prompt, Color color)
        {
            CLI.Clear();
            return CLI.ReadLine(color, prompt);
        }
    }
}

