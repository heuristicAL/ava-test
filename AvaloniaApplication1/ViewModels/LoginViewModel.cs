using System.Reactive;

using AvaloniaApplication1.Models;

using ReactiveUI;

namespace AvaloniaApplication1.ViewModels;

public class LoginViewModel : ViewModelBase {
	public ReactiveCommand<Unit, LoginResult> ApplyCommand { get; }

	public LoginViewModel()
	{
		ApplyCommand = ReactiveCommand.Create(() => new LoginResult {Message = "apply!"});
	}

}
