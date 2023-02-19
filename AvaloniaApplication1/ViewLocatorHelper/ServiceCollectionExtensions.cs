using Avalonia.Controls;

using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaApplication1.ViewLocatorHelper;

public static class ServiceCollectionExtensions {
	public static IServiceCollection AddView<TViewModel, TView>(this IServiceCollection services) where TView : Control, new()
	{
		services.AddSingleton(new ViewLocator.ViewLocationDescriptor(typeof(TViewModel), () => new TView()));
		return services;
	}
}
