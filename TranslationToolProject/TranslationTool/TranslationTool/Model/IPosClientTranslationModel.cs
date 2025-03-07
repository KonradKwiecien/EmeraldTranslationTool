using System.Collections.Generic;
using System.Xml.Linq;

namespace TranslationTool.Model;

public interface IPosClientTranslationModel
{
  XDocument? XmlTranslationsDocument { get; }
  List<ITranslation>? Translations { get; }
}

public interface ITranslation
{
  string? Space { get; }
  string Key { get; }
  string Text { get; }
}