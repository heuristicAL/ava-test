using Avalonia;
using Avalonia.ReactiveUI;

using System;

namespace AvaloniaApplication1;

class Program {
	// Initialization code. Don't use any Avalonia, third-party APIs or any
	// SynchronizationContext-reliant code before AppMain is called: things aren't initialized
	// yet and stuff might break.
	[STAThread]
	public static void Main(string[] args) {
		var appBuilder = BuildAvaloniaApp();
		appBuilder.StartWithClassicDesktopLifetime(args);
		var app = (App)appBuilder.Instance!;
		app.host.Dispose();
	}

	// Avalonia configuration, don't remove; also used by visual designer.
	public static AppBuilder BuildAvaloniaApp() =>
		AppBuilder.Configure<App>().UsePlatformDetect().LogToTrace().UseReactiveUI();
}
