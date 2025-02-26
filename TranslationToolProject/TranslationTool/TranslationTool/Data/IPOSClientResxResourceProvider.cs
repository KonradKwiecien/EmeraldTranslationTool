using System.Collections.Generic;
using TranslationTool.Model;

namespace TranslationTool.Data;
public interface IPOSClientResxResourceProvider
{
  PosClientTranslation? LoadRexsFile(string fullXmlFile);

  string FormatLine(Resheader resheader);
}
