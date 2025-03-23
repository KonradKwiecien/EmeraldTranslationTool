using Microsoft.UI.Xaml.Documents;
using System;
using System.Collections.Generic;
using TranslationTool.Model;

namespace TranslationTool.Data;
public interface IPOSClientResxResourceProvider
{
  [Obsolete("DeserializeFromResxFile is deprecated, please use LoadXmlFromRexsFile instead.")]
  IPosClientTranslationModel? DeserializeFromRexsFile(string fullResxFile);

  IPosClientTranslationModel? LoadToXDocumentFromRexsFile(string fullResxFile);

  IPosClientTranslationModel? LoadAsTextFileFromRexsFile(string fullResxFile);
}
