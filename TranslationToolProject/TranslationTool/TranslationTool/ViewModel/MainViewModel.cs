using Microsoft.UI.Xaml.Documents;
using System;
using System.Reflection;
using System.Windows.Input;
using TranslationTool.Data;
using TranslationTool.Model;
using Windows.Storage;
using Windows.Storage.Pickers;
using WTranslationTool.Command;

namespace TranslationTool.ViewModel
{
  public class MainViewModel : ViewModelBase
  {
    private readonly IPOSClientResxResourceProvider _posClientResxResourceProvider;
    private bool _comboBoxResxFileVisibility;

    public IPosClientTranslationModel? PosClientTranslationCoreModel { get; private set; }
    public IPOSClientResxResourceProvider PosClientResxResourceProvider { get => _posClientResxResourceProvider; }

    public bool ComboBoxResxFileVisibility
    {
      get => _comboBoxResxFileVisibility;
      set
      {
        _comboBoxResxFileVisibility = value;
        RaisePropertyChanged();
      }
    }

    public ICommand OpenResxFileCommand { get; }

    public string TestLabel { get; set; } = "TestLabel";

    public MainViewModel(IPOSClientResxResourceProvider pOSClientResxResourceProvider)
    {
      _posClientResxResourceProvider = pOSClientResxResourceProvider;

      OpenResxFileCommand = new DelegateCommand(OpenResxFileAsync);
    }

    [Obsolete("DeserializeFromResxFile is deprecated, please use LoadXmlFromRexsFile instead.")]
    public void DeserializeFromResxFile(string resxFile)
    {
      // Deserialize and load PosModel
      PosClientTranslationCoreModel = _posClientResxResourceProvider.DeserializeFromRexsFile(resxFile);
    }

    public void LoadXmlFromRexsFile(string resxFile)
    {
      PosClientTranslationCoreModel = _posClientResxResourceProvider.LoadXmlFromRexsFile(resxFile);
    }

    private async void OpenResxFileAsync(object? parameter)
    {
      //JSONFileContext = "File Loaded";
      // Create a file picker
      var openPicker = new FileOpenPicker();

      // Retrieve the window handle (HWND) of the current WinUI 3 window.
      var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);

      // Initialize the file picker with the window handle (HWND).
      WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);

      // Set options for your file picker
      openPicker.ViewMode = PickerViewMode.List;
      openPicker.FileTypeFilter.Add(".resx");

      // Open the picker for the user to pick a file
      var selectedFile = await openPicker.PickSingleFileAsync();
      if (selectedFile != null)
      {
        var lines = await FileIO.ReadLinesAsync(selectedFile);

        //ResxFileContext = "File opened";
        Run r = new Run();
        r.Text = "Run Text";
        //foreach (var line in lines)
        //{
        //  //JSONFileContext = $"File {selectedFile.Name} opened".ToString();
        //  //resxFileTextBlock.De
        //  Paragraph pa = new Paragraph();
        //  Run run = new Run();
        //  run.Text = "wertzuiop";
        //  pa.Inlines.Add(run);

        //}
      }
    }
  }
}
