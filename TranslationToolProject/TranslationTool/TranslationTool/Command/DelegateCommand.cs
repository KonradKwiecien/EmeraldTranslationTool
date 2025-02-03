using System;
using System.Windows.Input;

namespace WTranslationTool.Command;
public class DelegateCommand : ICommand
{
  private readonly Action<object?> _execute;
  private readonly Predicate<object?>? _canExecute;

  public event EventHandler? CanExecuteChanged;

  public DelegateCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
  {
    ArgumentNullException.ThrowIfNull(execute);
    _execute = execute;
    _canExecute = canExecute;
  }

  public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

  public bool CanExecute(object? parameter) => _canExecute is null || _canExecute(parameter);

  public void Execute(object? parameter) => _execute(parameter);
}
