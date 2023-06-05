﻿#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif

using Journal.Views;

namespace Journal;

public partial class App : Application
{
    const int WindowWidth = 540;
    const int WindowHeight = 1000;
    public App()
	{
		InitializeComponent();

#if WINDOWS
#pragma warning disable CA1416 // Validate platform compatibility
        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>
        {
            var mauiWindow = handler.VirtualView;
            var nativeWindow = handler.PlatformView;
            nativeWindow.Activate();
            IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
            WindowId windowId = Win32Interop.GetWindowIdFromWindow(windowHandle);
            AppWindow appWindow = AppWindow.GetFromWindowId(windowId);
            appWindow.Resize(new SizeInt32(WindowWidth, WindowHeight));
        });
#pragma warning restore CA1416 // Validate platform compatibility
#endif

        MainPage = new NavigationPage(new AllJournals());
	}
}
