using WTranslationTool.Command;

namespace TranslationTool.ViewModel
{
  public class MainViewModel: ViewModelBase
  {
    private string? _JSONFileContext;

    public bool CommandBarJsonVisibility => _JSONFileContext is not null;

    public string? JSONFileContext
    {
      get => _JSONFileContext;
      set
      {
        _JSONFileContext = value;
        RaisePropertyChanged();
        RaisePropertyChanged(nameof(CommandBarJsonVisibility));
      }
    }

    public DelegateCommand OpenJSONFileCommand { get; }

    public MainViewModel()
    {
      OpenJSONFileCommand = new DelegateCommand(OpenJSONFile);
    }

    private void OpenJSONFile(object? parameter)
    {
      JSONFileContext = "File Loaded";
    }
  }
}
