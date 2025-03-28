using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using TranslationTool.Data;
using TranslationTool.Renderer;
using TranslationTool.ViewModel;
using Windows.Graphics;
using WinRT.Interop;

namespace TranslationTool;

public partial class App : Application
{
  static public Window? MainWindow;
  private readonly ServiceProvider _serviceProvider;

  public App()
  {
    this.InitializeComponent();
    ServiceCollection services = new();
    ConfigureServices(services);
    _serviceProvider = services.BuildServiceProvider();

    // Returns an instance of MainViewModel
    // _serviceProvider.GetService<MainViewModel>();
  }

  private void ConfigureServices(ServiceCollection services)
  {
    services.AddTransient<MainWindow>();
    services.AddTransient<MainViewModel>();
    services.AddTransient<IPOSClientResxResourceProvider, POSClientResxResourceProvider>();
    services.AddTransient<ITranslationPreviewRendered, XMLTranslationPreviewRendered>();
  }

  protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
  {
    MainWindow = _serviceProvider.GetService<MainWindow>();
    MainWindow?.Activate();
  }
}
