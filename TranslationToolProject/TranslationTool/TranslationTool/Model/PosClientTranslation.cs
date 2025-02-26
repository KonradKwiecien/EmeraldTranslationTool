using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TranslationTool.Model;
public class PosClientTranslation
{
  private List<Resheader> _resheaderList = new();
  private List<Metadata> _metadataList = new();
  private List<Translation> _translatonList = new();

  public List<Resheader>? Resheaders { get => _resheaderList; }

  public List<Metadata>? Metadata { get => _metadataList; }

  public List<Translation>? Translations { get => _translatonList; }

  public void AddResheader(Resheader resheader)
  {
    if (resheader is not null)
    {
      _resheaderList.Add(resheader);
    }
  }

  public void AddMetadada(Metadata metadata)
  {
    if (metadata is not null)
    {
      _metadataList.Add(metadata);
    }
  }

  public void AddTranslation(Translation translation)
  {
    if (translation is not null)
    {
      _translatonList.Add(translation);
    }
  }

  public void AddResheaderList(List<Resheader> resheaderList)
  {
    if (resheaderList is not null)
    {
      _resheaderList.AddRange(resheaderList);
    }
  }

  public void AddMetadataList(List<Metadata> metadataList)
  {
    if (metadataList is not null)
    {
      _metadataList.AddRange(metadataList);
    }
  }

  public void AddTranslationList(List<Translation> translationList)
  {
    if (translationList is not null)
    {
      _translatonList.AddRange(translationList);
    }
  }
}

public class Resheader
{
  private string? _value;
  public string? _key;

  public string? Value { 
    get => _value; 
    init => _value = value;
  }

  public string? Key { 
    get => _key;
    init => _key = value;
  }
}

public class Metadata
{
  private string? _value;
  public string? _key;
  private string? _space;

  public string? Value { get => _value; }

  public string? Key { get => _key; }

}

public class Translation
{
  private string? _text;
  private string? _key;
  private string? _space;

  public string? Text { get => _text; }

  public string? Key { get => _key; }

  public string? Space { get => _space; }
}
