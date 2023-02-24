using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using AvaloniaApplication1.Models;
using AvaloniaApplication1.ViewModels;
using AvaloniaApplication1.Views;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using ReactiveUI;

using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;
using Splat.Microsoft.Extensions.Logging;

namespace AvaloniaApplication1;

public partial class App : Application {
  private IHost? host;
  public readonly string thingTest;
  public IServiceProvider Container { get; private set; }


  public override void Initialize() { AvaloniaXamlLoader.Load(this); }

  public override void OnFrameworkInitializationCompleted() {
    if (this.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
      // display dialog first
      var dialog = new Login { DataContext = new LoginViewModel(), };

      // .. and subscribe to its "Apply" button, which returns the dialog result
      dialog.ViewModel!.ApplyCommand
          .ObserveOn(RxApp.MainThreadScheduler).SubscribeOn(RxApp.MainThreadScheduler)
          .Subscribe(async result => {
            await this.Init(result);
            desktop.ShutdownRequested += (sender, args) => this.host?.Dispose();
            Console.WriteLine("dialog apply button hit!");
            desktop.MainWindow = this.Container.GetRequiredService<MainWindow>();
            desktop.MainWindow.Show();
            dialog.Close();
            Console.WriteLine("new main window instantiated!");

            // both of these lines are printed, but the window doesn't change
          });

      var testThing = 0;
      Console.WriteLine(testThing);
      desktop.MainWindow = dialog;
    }

    base.OnFrameworkInitializationCompleted();
  }

  private async Task Init(LoginResult loginResult) {
    this.host = Host.CreateDefaultBuilder()
        .ConfigureServices((_, services) => {
          services.UseMicrosoftDependencyResolver();
          var resolver = Locator.CurrentMutable;
          resolver.InitializeSplat();
          resolver.InitializeReactiveUI();

          // Configure our local services and access the host configuration
          this.ConfigureServices(services);
        })
        .ConfigureLogging(loggingBuilder => {
          /*
  //remove loggers incompatible with UWP
  {
    var eventLoggers = loggingBuilder.Services
      .Where(l => l.ImplementationType == typeof(EventLogLoggerProvider))
      .ToList();

    foreach (var el in eventLoggers)
      loggingBuilder.Services.Remove(el);
  }
  */

          loggingBuilder.AddSplat();
        })
        .Build();

    // Since MS DI container is a different type,

    // we need to re-register the built container with Splat again
    this.Container = this.host.Services;
    this.Container.UseMicrosoftDependencyResolver();

  }

  void ConfigureServices(IServiceCollection services) {
    services.AddTransient<MainWindow>();
    services.AddTransient<MainWindowViewModel>();

    // // register your personal services here, for example
    // services.AddSingleton<MainViewModel>(); //Implements IScreen
    //
    // // this passes IScreen resolution through to the previous viewmodel registration.
    // // this is to prevent multiple instances by mistake.
    // services.AddSingleton<IScreen, MainViewModel>(x => x.GetRequiredService<MainViewModel>());
    //
    // services.AddSingleton<IViewFor<MainViewModel>, MainPage>();
    //
    // //alternatively search assembly for `IRoutedViewFor` implementations
    // //see https://reactiveui.net/docs/handbook/routing to learn more about routing in RxUI
    // services.AddTransient<IViewFor<SecondaryViewModel>, SecondaryPage>();
    // services.AddTransient<SecondaryViewModel>();
  }
}
