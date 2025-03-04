using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using TranslationTool.Model;

namespace TranslationTool.Data;
public class POSClientResxResourceProvider : IPOSClientResxResourceProvider
{
  public PosClientTranslationModel? LoadRexsFile(string fullXmlFile)
  {
    PosClientTranslationModel? posClientTranslationModel = null;

    if ((fullXmlFile is not null) && File.Exists(fullXmlFile))
    {
      string xmlString = File.ReadAllText(fullXmlFile);
      // Create XML Serializer
      XmlSerializer serializer = new(typeof(XmlPosClientData));
      // Create a StringReader with the value from the file
      using (StringReader stringReader = new(xmlString))
      {
        // Convert the string to a product
        XmlPosClientData? xmldata = (XmlPosClientData?)serializer.Deserialize(stringReader);
        if (xmldata is not null)
        {
          posClientTranslationModel = new();
          if (xmldata.Resheaders is not null)
          {
            foreach (XmlResheader xmlResheader in xmldata.Resheaders)
            {
              posClientTranslationModel.AddResheader(
                new Resheader()
                { Key = xmlResheader.Key, Value = xmlResheader.Value });
            }
          }

          if (xmldata.Metadata is not null)
          {
            foreach (XmlMetadata xmlMetatada in xmldata.Metadata)
            {
              posClientTranslationModel.AddMetadada(
                new Metadata()
                {
                  Key = xmlMetatada.Key, Value = xmlMetatada.Value, Space = xmlMetatada.Space
                });
            }
          }

          if (xmldata.Translatons is not null)
          {
            foreach (XmlTranslation xmlTranslation in xmldata.Translatons)
            {
              posClientTranslationModel.AddTranslation(
                new Translation()
                {
                  Key = xmlTranslation.Key, Text = xmlTranslation.Text, Space = xmlTranslation.Space
                });
            }
          }
        }
      }
    }

    return posClientTranslationModel;
  }
}
