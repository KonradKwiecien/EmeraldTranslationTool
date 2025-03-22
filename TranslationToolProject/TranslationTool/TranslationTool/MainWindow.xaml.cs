using Microsoft.UI;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TranslationTool.Renderer;
using TranslationTool.ViewModel;

namespace TranslationTool;

public sealed partial class MainWindow : Window
{
  private readonly ITranslationFormatRenderer _translationFormatRenderer;

  public MainViewModel ViewModel { get; }

  public MainWindow(MainViewModel viewModel, ITranslationFormatRenderer translationFormatRenderer)
  {
    this.InitializeComponent();

    ViewModel = viewModel;
    _translationFormatRenderer = translationFormatRenderer;

    root.Loaded += Root_Loaded; ;
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

    string normalizedPath = Path.GetFullPath($@"{AppContext.BaseDirectory}\..\..\..\..\..\..\..\..\..\TranslationToolProject\TestFiles\Core\POSClient.en-US.POSClient.en-US.xml");
    ViewModel.LoadXmlFromRexsFile(normalizedPath);

    if (ViewModel.PosClientTranslationCoreModel is not null)
    {
      ResxFileTextBlock.Text = null;
      List<string> xmlElements = ["value"];
      List<string> xmlAttributes = ["name", "xml:space"];

      _translationFormatRenderer.SetColorForAttributes(ElementTheme.Light, xmlAttributes.First(), Colors.Crimson);
      _translationFormatRenderer.SetColorForAttributes(ElementTheme.Dark, xmlAttributes.First(), Colors.Orange);
      _translationFormatRenderer.FormatTranslations(ResxFileTextBlock,
                                                    ViewModel.PosClientTranslationCoreModel?.XmlTranslationsDocument,
                                                    xmlElements, xmlAttributes);
    }
  }
}

