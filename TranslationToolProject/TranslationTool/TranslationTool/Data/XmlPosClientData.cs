using System.Collections.Generic;
using System.Xml.Serialization;

[XmlType(AnonymousType = true)]
[XmlRoot("root", Namespace = "", IsNullable = false)]
public partial class XmlPosClientData
{
  [XmlElement("resheader")]
  public List<XmlResheader>? Resheaders;

  [XmlElement("metadata")]
  public List<XmlMetadata>? Metadata;

  [XmlElement("data")]
  public List<XmlTranslation>? Translatons;
}

[XmlType(AnonymousType = true)]
public partial class XmlResheader
{
  [XmlElement("value")]
  public string? Value;

  [XmlAttribute("name")]
  public string? Key;
}

[XmlType(AnonymousType = true)]
public partial class XmlMetadata
{
  [XmlElement("value")]
  public string? Value;

  [XmlAttribute("name")]
  public string? Key;

  [XmlAttribute(AttributeName = "space", Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
  public string? Space;
}

[XmlType(AnonymousType = true)]
public partial class XmlTranslation
{
  [XmlElement("value")]
  public string? Text;

  [XmlAttribute("name")]
  public string? Key;

  [XmlAttribute(AttributeName = "space", Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
  public string? Space;
}

