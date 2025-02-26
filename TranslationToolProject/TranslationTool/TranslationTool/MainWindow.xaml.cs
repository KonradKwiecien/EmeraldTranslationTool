using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using System;
using System.Collections.Generic;
using TranslationTool.Data;
using TranslationTool.Model;
using TranslationTool.ViewModel;
using Windows.Storage.Pickers;

namespace TranslationTool
{
  public sealed partial class MainWindow : Window
  {
    public MainViewModel ViewModel { get; }

    public MainWindow(MainViewModel viewModel)
    {
      this.InitializeComponent();
      ViewModel = viewModel;
      root.Loaded += Root_Loaded; ;
    }

    private void Root_Loaded(object sender, RoutedEventArgs e)
    {
      ViewModel.DeserializeFromXml($@"{AppContext.BaseDirectory}\..\..\..\..\..\..\..\..\TestFiles\Core\POSClient.en-US.POSClient.en-US.xml");

      if (ViewModel.PosClientTranslation is not null && ViewModel.PosClientTranslation.Resheaders is not null)
      {
        bool addNewLine = false;
        foreach (Resheader? resheader in ViewModel.PosClientTranslation.Resheaders)
        {
          if (addNewLine)
          {
            ResxFileTextBlock.Inlines.Add(new LineBreak());
          }
          addNewLine = true;
          //Run r = new() { Text = resheader.Value };

          string formattedLine = ViewModel.PosClientResxResourceProvider.FormatLine(resheader);          
          ResxFileTextBlock.Inlines.Add(new Run() { Text = formattedLine });
        }
      }
    }

    private void TheneSwitch_Toggled(object sender, RoutedEventArgs e)
    {
      root.RequestedTheme = root.RequestedTheme == ElementTheme.Light
       ? ElementTheme.Dark : ElementTheme.Light;
    }
  }
}

