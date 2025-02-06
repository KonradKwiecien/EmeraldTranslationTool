using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
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
    }

    private void TheneSwitch_Toggled(object sender, RoutedEventArgs e)
    {
      root.RequestedTheme = root.RequestedTheme == ElementTheme.Light
       ? ElementTheme.Dark : ElementTheme.Light;
    }
  }
}

