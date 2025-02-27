using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using System.Collections.Generic;
using TranslationTool.Model;

namespace TranslationTool.Renderer;
public interface ITranslationFormatRenderer
{
  void FormatTranslations(TextBlock textBlock, PosClientTranslationModel posClientTranslationModel);
}
