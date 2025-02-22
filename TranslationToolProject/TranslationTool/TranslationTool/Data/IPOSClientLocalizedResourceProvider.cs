using System.Collections.Generic;

namespace TranslationTool.Data;
public interface IPOSClientLocalizedResourceProvider
{
  List<string>? GetAllTrsnslations();
}
