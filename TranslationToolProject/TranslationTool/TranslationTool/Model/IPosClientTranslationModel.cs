using System.Collections.Generic;

namespace TranslationTool.Model;

public interface IPosClientTranslationModel
{
  List<IMetadata>? Metadata { get; }
  List<IResheader>? Resheaders { get; }
  List<ITranslation>? Translations { get; }
}

public interface IResheader
{
  string Key { get; }
  string Value { get; }
}
public interface IMetadata
{
  string? Space { get; }
  string Key { get; }
  string Value { get; }
}

public interface ITranslation
{
  string? Space { get; }
  string Key { get; }
  string? Text { get; }
}