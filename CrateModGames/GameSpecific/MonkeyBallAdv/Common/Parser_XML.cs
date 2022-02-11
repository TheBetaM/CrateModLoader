using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System;
using System.Text;

namespace CrateModLoader.GameSpecific.MonkeyBallAdv
{
    public class Parser_XML : ModParser<XmlDocument>
    {
        public Parser_XML(Modder mod) : base(mod) { }

        public override List<string> Extensions => new List<string>() { ".XML" };

        public override XmlDocument LoadObject(string filePath)
        {
            XmlDocument xml = new XmlDocument();
            try
            {
                xml.Load(filePath);
            }
            catch (XmlException ex)
            {
                Console.WriteLine("HELP " + filePath);
                Console.WriteLine("XML: " + ex.Message);
            }
            
            return xml;
        }

        public override void SaveObject(XmlDocument thing, string filePath)
        {
            try
            {
                thing.Save(filePath);
            }
            catch (XmlException ex)
            {
                Console.WriteLine("SAVE ME " + filePath);
                Console.WriteLine("XML: " + ex.Message);
            }
        }
    }
}
