using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using TranslationTool.Data;
using Windows.Storage;
using Windows.Storage.Pickers;
using WTranslationTool.Command;

namespace TranslationTool.ViewModel
{
  public class MainViewModel : ViewModelBase
  {
    private readonly IPOSClientLocalizedResourceProvider _pOSClientLocalizedResourceProvider;

    private string? _JSONFileContext;
    private bool _CBJsonFileVisibility;

    public bool CBJsonFileVisibility
    {
      get => _CBJsonFileVisibility;
      set
      {
        _CBJsonFileVisibility = value;
        RaisePropertyChanged();
      }
    }

    public string? JSONFileContext
    {
      get => _JSONFileContext;
      set
      {
        _JSONFileContext = value;
        CBJsonFileVisibility = true;
        RaisePropertyChanged();
      }
    }

    public ICommand OpenJSONFileCommand { get; }

    public string TestLabel { get; set; } = "TestLabel";

    public MainViewModel(IPOSClientLocalizedResourceProvider pOSClientLocalizedResourceProvider)
    {
      _pOSClientLocalizedResourceProvider = pOSClientLocalizedResourceProvider;

      OpenJSONFileCommand = new DelegateCommand(OpenJSONFileAsync);
    }

    public void LoadTranslations()
    {
      List<string>? translations = _pOSClientLocalizedResourceProvider.GetAllTrsnslations();
    }

    private async void OpenJSONFileAsync(object? parameter)
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
      openPicker.FileTypeFilter.Add(".json");

      // Open the picker for the user to pick a file
      var selectedFile = await openPicker.PickSingleFileAsync();
      if (selectedFile != null)
      {
        var lines = await FileIO.ReadLinesAsync(selectedFile);
      }
    }
  }
}
