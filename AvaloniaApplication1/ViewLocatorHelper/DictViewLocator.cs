#region

using System;
using System.Collections.Generic;
using System.Linq;

using Avalonia.Controls;
using Avalonia.Controls.Templates;

#endregion

namespace AvaloniaApplication1.ViewLocatorHelper;

public class DictViewLocator : IDataTemplate {
  private readonly Dictionary<Type, Func<Control>> _dic;

  public DictViewLocator(IEnumerable<ViewLocationDescriptor> descriptors) {
    this._dic = descriptors.ToDictionary(x => x.ViewModel, x => x.Factory);
  }

  public record ViewLocationDescriptor(Type ViewModel, Func<Control> Factory);

  public Control Build(object param) => this._dic[param.GetType()]();

  public bool Match(object data) => this._dic.ContainsKey(data.GetType());
}
