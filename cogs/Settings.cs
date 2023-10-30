using Veylib.ICLI;

namespace TaxEvasion.cogs
{
    public class Settings
    {
        public static CLI.StartupProperties sp = new CLI.StartupProperties()
        {
            Logo = new CLI.StartupLogoProperties()
            {
                Text = @"_____   __    _         ____  _       __    __   _   ___   _     
 | |   / /\  \ \_/     | |_  \ \  /  / /\  ( (` | | / / \ | |\ | 
 |_|  /_/--\ /_/ \     |_|__  \_\/  /_/--\ _)_) |_| \_\_/ |_| \| "
            },

            MOTD = new CLI.StartupMOTDProperties()
            {
                Text = "Carnage 4 Life"
            },

            Author = new CLI.StartupAuthorProperties()
            {
                Name = "aelius",
                Url = "https://discord.gg/RdKzRs3pzq"
            },

            UseAutoVersioning = true,
            DebugMode = true,

            Title = new CLI.StartupConsoleTitleProperties()
            {
                Animated = true,
                Status = "aelius was here",
                Text = @"Fastest mass report tool..."
            },

            SplashScreen = new CLI.StartupSpashScreenProperties()
            {
                Content = "doxcentral runs you",
                DisplayProgressBar = false,
                AutoGenerate = true,
                DisplayTime = 5000,
                AutoCenter = true
            }
        };
    }
}
