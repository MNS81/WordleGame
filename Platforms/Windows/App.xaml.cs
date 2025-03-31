using Microsoft.UI.Windowing;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Windows.Graphics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WordleGame.WinUI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : MauiWinUIApplication
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            base.OnLaunched(args);

            var window = MauiWinUIApplication.Current.Application.Windows[0].Handler.PlatformView as Microsoft.UI.Xaml.Window;

            var appWindow = AppWindow.GetFromWindowId(Win32Interop.GetWindowIdFromWindow(WinRT.Interop.WindowNative.GetWindowHandle(window)));
            if (appWindow != null)
            {
                // Размер окна
                int windowWidth = 600;
                int windowHeight = 670;

                appWindow.Resize(new SizeInt32(windowWidth, windowHeight));

                // Получение размеров экрана
                var displayInfo = DisplayArea.GetFromWindowId(appWindow.Id, DisplayAreaFallback.Primary);
                var screenWidth = displayInfo.WorkArea.Width;
                var screenHeight = displayInfo.WorkArea.Height;

                // Расчет центра экрана
                int positionX = (screenWidth - windowWidth) / 2;
                int positionY = (screenHeight - windowHeight) / 2;

                appWindow.Move(new PointInt32(positionX, positionY));
            }
        }
    }

}
