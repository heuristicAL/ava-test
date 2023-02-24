using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

using AvaloniaApplication1.ViewModels;

namespace AvaloniaApplication1.Views;

public partial class Login : ReactiveWindow<LoginViewModel> {
  public Login() {
    this.InitializeComponent();
#if DEBUG
    this.AttachDevTools();
#endif
  }

  private void InitializeComponent() { AvaloniaXamlLoader.Load(this); }
}

