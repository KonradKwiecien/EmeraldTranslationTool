using Microsoft.UI.Xaml;
using System;
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
      _translationFormatRenderer.FormatTranslations(ResxFileTextBlock, ViewModel.PosClientTranslation);
    }
  }

  private void TheneSwitch_Toggled(object sender, RoutedEventArgs e)
  {
    root.RequestedTheme = root.RequestedTheme == ElementTheme.Light
     ? ElementTheme.Dark : ElementTheme.Light;

    if (ViewModel.PosClientTranslation is not null)
    {
      ResxFileTextBlock.Text = string.Empty;
      _translationFormatRenderer.FormatTranslations(ResxFileTextBlock, ViewModel.PosClientTranslation);
    }
  }
}

