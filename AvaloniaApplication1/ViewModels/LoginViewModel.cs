using System.Reactive;

using ReactiveUI;

namespace AvaloniaApplication1.ViewModels;

public class LoginViewModel : ViewModelBase {
	public ReactiveCommand<Unit, string> ApplyCommand { get; }

	public LoginViewModel()
	{
		ApplyCommand = ReactiveCommand.Create(() => "apply!");
	}

}
