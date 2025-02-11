

using System;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

const string XML_FILE = @".\..\..\..\..\..\TestFiles\OfficeCCM_LocalizedResourceMaintenance.LocalizedResourceMaintenance";

//<? xml version = "1.0" ?>
//<Root Att1 = "AttributeContent">
//  <Child>
//    Some text
//    <GrandChild>
//      element content
//    </GrandChild>
//  </Child>
//</Root>
XElement xmlTree = new XElement("Root",
    new XAttribute("Att1", "AttributeContent"),
    new XElement("Child",
        new XText("Some text"),
        new XElement("GrandChild", "element content")
    )
);
IEnumerable<XElement> de0 =
    from el in xmlTree.Descendants("Child")
    select el;
foreach (XElement el in de0)
  Console.WriteLine(el.Name);

//XDocument doc = XDocument.Load(XML_FILE);
var xDoc = XDocument.Load(XML_FILE);
IEnumerable<XElement> de =
    from el in xDoc.Descendants("data")
    select el;

foreach (XElement el in de)
  Console.WriteLine(el.Name);

Console.ReadKey();



