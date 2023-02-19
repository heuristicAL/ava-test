using Avalonia.Controls;
using Avalonia.ReactiveUI;

using AvaloniaApplication1.ViewModels;

namespace AvaloniaApplication1.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel> {
    public MainWindow() { InitializeComponent(); }
}
