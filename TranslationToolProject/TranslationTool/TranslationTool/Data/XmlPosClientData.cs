using System.Collections.Generic;
using System.Xml.Serialization;
using TranslationTool.Model;

[XmlType(AnonymousType = true)]
[XmlRoot("root", Namespace = "", IsNullable = false)]
public partial class XmlPosClientData
{
  #region Xml Properties
  [XmlElement("resheader")]
  public List<XmlResheader>? Resheaders { get; set; }

  [XmlElement("metadata")]
  public List<XmlMetadata>? Metadata { get; set; }

  [XmlElement("data")]
  public List<XmlTranslation>? Translatons { get; set; }
  #endregion

  XmlPosClientData(IPosClientTranslationModel posClientTranslationModel)
  {
    if (posClientTranslationModel.Resheaders is not null)
    {
      List<XmlResheader> xmlResheaders = new();
      foreach (IResheader resheader in posClientTranslationModel.Resheaders)
      {
        xmlResheaders.Add(new XmlResheader() { Key = resheader.Key, Value = resheader.Value });
      }
    }

    if (posClientTranslationModel.Metadata is not null)
    {
      List<XmlMetadata> xmlMetadata = new();
      foreach (IMetadata metadata in posClientTranslationModel.Metadata)
      {
        xmlMetadata.Add(new XmlMetadata()
        {
          Key = metadata.Key,
          Value = metadata.Value,
          Space = metadata.Space
        });
      }
    }

    if (posClientTranslationModel.Translations is not null)
    {
      List<XmlTranslation> xmlTranslations = new();
      foreach (ITranslation translation in posClientTranslationModel.Translations)
      {
        xmlTranslations.Add(new XmlTranslation()
        {
          Key = translation.Key,
          Text = translation.Text,
          Space = translation.Space
        });
      }
    }
  }
}

[XmlType(AnonymousType = true)]
public partial class XmlResheader
{
  [XmlElement("value")]
  public string? Value { get; set; }

  [XmlAttribute("name")]
  public string? Key { get; set; }
}

[XmlType(AnonymousType = true)]
public partial class XmlMetadata
{
  [XmlElement("value")]
  public string? Value { get; set; }

  [XmlAttribute("name")]
  public string? Key { get; set; }

  [XmlAttribute(AttributeName = "space", Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
  public string? Space { get; set; }
}

[XmlType(AnonymousType = true)]
public partial class XmlTranslation
{
  [XmlElement("value")]
  public string? Text { get; set; }

  [XmlAttribute("name")]
  public string? Key { get; set; }

  [XmlAttribute(AttributeName = "space", Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
  public string? Space { get; set; }
}

