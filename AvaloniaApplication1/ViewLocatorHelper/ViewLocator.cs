#region

using System;
using System.Collections.Generic;
using System.Linq;

using Avalonia.Controls;
using Avalonia.Controls.Templates;

#endregion

namespace AvaloniaApplication1.ViewLocatorHelper;

public class ViewLocator : IDataTemplate {
	private readonly Dictionary<Type, Func<Control>> _dic;

	public ViewLocator(IEnumerable<ViewLocationDescriptor> descriptors) {
		_dic = descriptors.ToDictionary(x => x.ViewModel, x => x.Factory);
	}

	public record ViewLocationDescriptor(Type ViewModel, Func<Control> Factory);

	public Control Build(object param) => _dic[param.GetType()]();

	public bool Match(object data) => _dic.ContainsKey(data.GetType());
}
