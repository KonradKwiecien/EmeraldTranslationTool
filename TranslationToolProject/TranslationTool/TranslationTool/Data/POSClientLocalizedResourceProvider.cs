using System.Collections.Generic;
using System.Threading.Tasks;

namespace TranslationTool.Data;

public interface IPOSClientLocalizedResourceProvider
{
  List<string>? GetAllTrsnslations();
}

public class POSClientLocalizedResourceProvider : IPOSClientLocalizedResourceProvider
{
  public List<string>? GetAllTrsnslations()
  {
    throw new System.NotImplementedException();
  }
}
