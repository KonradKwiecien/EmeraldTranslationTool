using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using TranslationTool.Model;

namespace TranslationTool.Data;
public class POSClientResxResourceProvider : IPOSClientResxResourceProvider
{
  public PosClientTranslationModel? LoadRexsFile(string fullXmlFile)
  {
    PosClientTranslationModel? clientTranslation = null;

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
}
