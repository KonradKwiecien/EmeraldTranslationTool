using WTranslationTool.Command;

namespace TranslationTool.ViewModel
{
  public class MainViewModel: ViewModelBase
  {

    private string _JSONFileContext;

    public string JSONFileContext
    {
      get => _JSONFileContext;
      set
      {
        _JSONFileContext = value;
        RaisePropertyChanged();
      }
    }

    public DelegateCommand OpenJSONFileCommand { get; }

    public MainViewModel()
    {
      OpenJSONFileCommand = new DelegateCommand(OpenJSONFile);
    }

    private void OpenJSONFile(object? parameter)
    {
      JSONFileContext = "JSON File Loaded";
    }
  }
}
