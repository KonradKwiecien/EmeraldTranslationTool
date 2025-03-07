using System.Collections.Generic;
using System.Xml.Linq;

namespace TranslationTool.Model;
public class PosClientTranslationModel : IPosClientTranslationModel
{

  private List<ITranslation>? _translatonList;
  private XDocument? _xmlTranslations;

  public XDocument? XmlTranslationsDocument
  {
    get => _xmlTranslations;
    set
    {
      _xmlTranslations = value;
      UpdateTranslationsList();

    }
  }

  public List<ITranslation>? Translations { get => _translatonList; }

  private void UpdateTranslationsList()
  {
    if(_xmlTranslations is null)
    {
      return;
    }
  }
}
