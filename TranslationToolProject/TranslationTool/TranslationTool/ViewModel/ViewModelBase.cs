using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TranslationTool.ViewModel;

public class ViewModelBase : INotifyPropertyChanged
{
  public event PropertyChangedEventHandler? PropertyChanged;

  protected virtual void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
  {
    //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    if (PropertyChanged != null)
    {
      PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}