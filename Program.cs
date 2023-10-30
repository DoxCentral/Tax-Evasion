using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
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

            var token = CLI.ReadLine(Color.Green, "Enter your discord token : ");
            CLI.Clear();

            var guildID = CLI.ReadLine(Color.Green, "Enter the guild id : ");
            CLI.Clear();

            var channelID = CLI.ReadLine(Color.Green, "Enter the channel id : ");
            CLI.Clear();

            var messageID = CLI.ReadLine(Color.Green, "Enter the message id : ");
            CLI.Clear();

            CLI.WriteLine(Color.Blue, "Enter Reason/Why =>");
            var rsp = new SelectionMenu("1 : Illegal Conent", "2 : Harrassment", "3 : Spam or Phishing Links", "4 : Self harm", "5 : NSFW Content").Activate();

            switch (rsp)
            {
                case "1 : Illegal Conent":
                    {
                        int choosing = 1;
                        var amt = CLI.ReadLine(Color.Green, "How many reports? : ");
                        CLI.Clear();

                        await Events.MRPT(token, choosing, guildID, channelID, messageID, Convert.ToInt32(amt));

                        break;
                    }
                case "2 : Harrassment":
                    {
                        int choosing = 2;
                        var amt = CLI.ReadLine(Color.Green, "How many reports? : ");
                        CLI.Clear();

                        await Events.MRPT(token, choosing, guildID, channelID, messageID, Convert.ToInt32(amt));

                        break;
                    }
                case "3 : Spam or Phishing Links":
                    {
                        int choosing = 3;
                        var amt = CLI.ReadLine(Color.Green, "How many reports? : ");
                        CLI.Clear();

                        await Events.MRPT(token, choosing, guildID, channelID, messageID, Convert.ToInt32(amt));

                        break;
                    }
                case "4 : Self harm":
                    {
                        int choosing = 4;
                        var amt = CLI.ReadLine(Color.Green, "How many reports? : ");
                        CLI.Clear();

                        await Events.MRPT(token, choosing, guildID, channelID, messageID, Convert.ToInt32(amt));

                        break;
                    }
                case "5 : NSFW Content":
                    {
                        int choosing = 5;
                        var amt = CLI.ReadLine(Color.Green, "How many reports? : ");
                        CLI.Clear();

                        await Events.MRPT(token, choosing, guildID, channelID, messageID, Convert.ToInt32(amt));

                        break;
                    }
                default:
                    {
                        CLI.Clear();

                        CLI.WriteLine(Color.Red, "Incorrect Option.");
                        Thread.Sleep(2000);

                        Environment.Exit(0);
                        break;
                    }
            }
        }
    }
}
