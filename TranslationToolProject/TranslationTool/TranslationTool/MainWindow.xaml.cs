using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TranslationTool.Renderer;
using TranslationTool.ViewModel;
using Windows.Graphics;
using WinRT.Interop;

namespace TranslationTool;

public sealed partial class MainWindow : Window
{
  private bool centered;
  private readonly ITranslationFormatRenderer _translationFormatRenderer;

  public MainViewModel ViewModel { get; }

  public MainWindow(MainViewModel viewModel, ITranslationFormatRenderer translationFormatRenderer)
  {
    this.InitializeComponent();
    this.Activated += MainWindow_Activated;

    ViewModel = viewModel;
    _translationFormatRenderer = translationFormatRenderer;

    root.Loaded += Root_Loaded; ;
  }
  private void MainWindow_Activated(object sender, WindowActivatedEventArgs args)
  {
    if (this.centered is false)
    {
      CenterWindow(this);
      centered = true;
    }
  }

  private static void CenterWindow(Window window)
  {
    IntPtr hWnd = WindowNative.GetWindowHandle(window);
    WindowId windowId = Win32Interop.GetWindowIdFromWindow(hWnd);

    if ((AppWindow.GetFromWindowId(windowId) is AppWindow appWindow) &&
        (DisplayArea.GetFromWindowId(windowId, DisplayAreaFallback.Nearest) is DisplayArea displayArea))
    {
      appWindow.Resize(new SizeInt32((int)(displayArea.WorkArea.Width * 0.9), (int)(displayArea.WorkArea.Height * 0.9)));
      PointInt32 CenteredPosition = appWindow.Position;
      CenteredPosition.X = (displayArea.WorkArea.Width - appWindow.Size.Width) / 2;
      CenteredPosition.Y = (displayArea.WorkArea.Height - appWindow.Size.Height) / 2;
      appWindow.Move(CenteredPosition);
    }
  }

  private void Root_Loaded(object sender, RoutedEventArgs e)
  {
    LoadTestXMLFile();
  }

  private void TheneSwitch_Toggled(object sender, RoutedEventArgs e)
  {
    root.RequestedTheme = root.RequestedTheme == ElementTheme.Light
     ? ElementTheme.Dark : ElementTheme.Light;

    LoadTestXMLFile();
  }

  private void LoadTestXMLFile()
  {
    //ViewModel.DeserializeFromResxFile($@"{AppContext.BaseDirectory}\..\..\..\..\..\..\..\..\TestFiles\Core\POSClient.en-US.POSClient.en-US.xml");

    //string fileName = "POSClient.en-US.POSClient.en-USsmall.xml";
    string fileName = "POSClient.en-US.POSClient.en-US.resx";
    string normalizedPath = Path.GetFullPath($@"{AppContext.BaseDirectory}\..\..\..\..\..\..\..\..\TranslationToolProject\TestFiles\Core\{fileName}");
    ViewModel.LoadToXDocumentFromRexsFile(normalizedPath);
    ViewModel.LoadAsTextFileFromRexsFile(normalizedPath);

    if (ViewModel.PosClientTranslationCoreModel is not null)
    {
      ResxFileTextBlock.Text = null;
      List<string> xmlElements = ["value"];
      List<string> xmlAttributes = ["name", "xml:space"];

      _translationFormatRenderer.SetColorForAttributes(ElementTheme.Light, xmlAttributes.First(), Colors.Crimson);
      _translationFormatRenderer.SetColorForAttributes(ElementTheme.Dark, xmlAttributes.First(), Colors.Orange);
      //string[]? xmlFile = ViewModel.PosClientTranslationCoreModel?.XmlTranslationsDocument?.ToString().Split(Environment.NewLine);
      string[]? xmlFile = ViewModel.PosClientTranslationCoreModel?.XMLlTranslationsFile;

      _translationFormatRenderer.FormatTranslations(ResxFileTextBlock, xmlFile, xmlElements, xmlAttributes);
    }
  }
}

