using System.Collections.Generic;
using System.Xml.Linq;

namespace TranslationTool.Model;

public class PosClientTranslationModel : IPosClientTranslationModel
{
  private XDocument? _xmlTranslationsXDocument;
  private string[]? _xmlTranslationsFile;
  private List<ITranslation>? _translatonList;

  public string[]? XMLlTranslationsFile
  {
    get => _xmlTranslationsFile;
    set
    {
      _xmlTranslationsFile = value;
      UpdateTranslationsList();
    }
  }

  public XDocument? XmlTranslationsDocument
  {
    get => _xmlTranslationsXDocument;
    set
    {
      _xmlTranslationsXDocument = value;
      UpdateTranslationsList();
    }
  }

  public List<ITranslation>? Translations
  {
    get => _translatonList;
    private set => _translatonList = value;
  }

  private void UpdateTranslationsList()
  {
    if (_xmlTranslationsXDocument is null)
    {
      return;
    }
  }
}
