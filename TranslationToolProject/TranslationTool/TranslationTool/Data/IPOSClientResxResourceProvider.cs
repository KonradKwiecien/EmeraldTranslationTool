using Microsoft.UI.Xaml.Documents;
using System.Collections.Generic;
using TranslationTool.Model;

namespace TranslationTool.Data;
public interface IPOSClientResxResourceProvider
{
  PosClientTranslationModel? LoadRexsFile(string fullXmlFile);
}
