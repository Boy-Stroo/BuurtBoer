using Microsoft.Maui.Controls.Compatibility.Platform;
using Microsoft.Maui.Controls.PlatformConfiguration;

namespace Mobile_App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("CursorColor", (handler, entry) =>
            {
                entry.CursorPosition = 0;
            });

            MainPage = new AppShell();
        }
    }
}