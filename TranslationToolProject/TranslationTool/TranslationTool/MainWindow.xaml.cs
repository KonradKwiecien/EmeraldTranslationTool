using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
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
    //ViewModel.DeserializeFromResxFile($@"{AppContext.BaseDirectory}\..\..\..\..\..\..\..\..\TestFiles\Core\POSClient.en-US.POSClient.en-US.xml");

    string normalizedPath = Path.GetFullPath($@"{AppContext.BaseDirectory}\..\..\..\..\..\..\..\..\..\TranslationToolProject\TestFiles\Core\POSClient.en-US.POSClient.en-US.xml");
    ViewModel.LoadXmlFromRexsFile(normalizedPath);

    if (ViewModel.PosClientTranslationCoreModel is not null)
    {
      List<string> xmlElements = ["value"];
      List<string> xmlAttributes = ["version", "encoding", "name", "xml:space"];
      _translationFormatRenderer.FormatTranslations(ResxFileTextBlock,
                                                    ViewModel.PosClientTranslationCoreModel?.XmlTranslationsDocument,
                                                    xmlElements, xmlAttributes);
    }
  }

  private void TheneSwitch_Toggled(object sender, RoutedEventArgs e)
  {
    root.RequestedTheme = root.RequestedTheme == ElementTheme.Light
     ? ElementTheme.Dark : ElementTheme.Light;

    //if (ViewModel.PosClientTranslationCoreModel is not null)
    //{
    //  ResxFileTextBlock.Text = string.Empty;
    //  List<string> xmlElements = ["value"];
    //  List<string> xmlAttributes = ["name", "xml:space"];
    //  _translationFormatRenderer.FormatTranslations(ResxFileTextBlock, ViewModel.PosClientTranslationCoreModel?.XmlTranslationsDocument,
    //                                                xmlElements, xmlAttributes);
    //}
  }
}

