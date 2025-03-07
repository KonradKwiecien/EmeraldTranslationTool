using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using TranslationTool.Model;

namespace TranslationTool.Renderer;
public interface ITranslationFormatRenderer
{
  void FormatTranslations(TextBlock textBlock, XDocument? xmlDocument,
                          List<string> xmlElements, List<string> xmlAttribute);

  [Obsolete("FormatTranslations(TextBlock, IPosClientTranslationModel) is deprecated, please use FormatTranslations(TextBlock, XMLDocument) instead.")]
  void FormatTranslations(TextBlock textBlock, IPosClientTranslationModel posClientTranslationModel);
}
