using Microsoft.UI.Xaml;
using TranslationTool.ViewModel;

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
  }
}
