using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Documents;
using System;
using System.Collections.Generic;
using TranslationTool.Model;
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
    ViewModel.DeserializeFromXml($@"{AppContext.BaseDirectory}\..\..\..\..\..\..\..\..\TestFiles\Core\POSClient.en-US.POSClient.en-US.xml");

    if (ViewModel.PosClientTranslation is not null)
    {
      _translationFormatRenderer.FormatLines(ResxFileTextBlock, ViewModel.PosClientTranslation);
      //bool addNewLine = false;
      //foreach (Resheader? resheader in ViewModel.PosClientTranslation.Resheaders)
      //{
      //  if (addNewLine)
      //  {
      //    ResxFileTextBlock.Inlines.Add(new LineBreak());
      //  }
      //  addNewLine = true;
      //  //Run r = new() { Text = resheader.Value };

      //  //List<Run> runs = ViewModel.PosClientResxResourceProvider.FormatLines(resheader);
      //  //foreach (var run in runs)
      //  //{
      //  //  ResxFileTextBlock.Inlines.Add(run);
      //  //}
      //}
    }
  }

  private void TheneSwitch_Toggled(object sender, RoutedEventArgs e)
  {
    root.RequestedTheme = root.RequestedTheme == ElementTheme.Light
     ? ElementTheme.Dark : ElementTheme.Light;
  }
}

