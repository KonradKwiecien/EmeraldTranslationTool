using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using TranslationTool.Model;

namespace TranslationTool.Data;
public class POSClientResxResourceProvider : IPOSClientResxResourceProvider
{
  public PosClientTranslation? LoadRexsFile(string fullXmlFile)
  {
    PosClientTranslation? clientTranslation = null;

    if ((fullXmlFile is not null) || File.Exists(fullXmlFile))
    {
      string xmlString = File.ReadAllText(fullXmlFile);

      // Create XML Serializer
      XmlSerializer serializer = new(typeof(XmlPosClientData));

      // Create a StringReader with the value from the file
      using (StringReader sr = new(xmlString))
      {
        // Convert the string to a product
        XmlPosClientData? xmldata = (XmlPosClientData?)serializer.Deserialize(sr);
        if (xmldata is not null)
        {
          clientTranslation = new();

          if (xmldata.Resheaders is not null)
          {
            List<Resheader> resList = new List<Resheader>();
            foreach (XmlResheader res in xmldata.Resheaders)
            {
              resList.Add(new() { Key = res.Key, Value = res.Value });
            }
            clientTranslation.AddResheaderList(resList);
          }
        }
      }
    }

    return clientTranslation;
  }

  public string FormatLine(Resheader resheader)
  {
    // Create empty namespaces with empty value
    XmlSerializerNamespaces emptyNamespaces = new XmlSerializerNamespaces();
    emptyNamespaces.Add("", "");

    XmlWriterSettings settings = new()
    {
      Indent = true,
      OmitXmlDeclaration = true,      
    };

    using (StringWriter stream = new StringWriter())
    using (var writer = XmlWriter.Create(stream, settings))
    {
      XmlSerializer serializer = new(typeof(XmlResheader));
      XmlResheader xmlRes = new() { Key = resheader.Key, Value = resheader.Value };
      serializer.Serialize(writer, xmlRes, emptyNamespaces);

      return stream.ToString();
    }
  }
}
