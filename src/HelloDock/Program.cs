using System;
using Avalonia;

namespace HelloDock
{
    // Nothing special here for docking.
    internal static class Program
    {
        [STAThread]
        public static void Main(string[] args) =>
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder
                .Configure<App>()
                .UsePlatformDetect()
                .LogToTrace();
    }
}
