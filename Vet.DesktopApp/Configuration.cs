using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace VetAmbulance.DesktopApp
{
    public static class Configuration
    {
        public static string File { get; set; }

        public static ConfigurationModel Load()
        {
            if (!System.IO.File.Exists(File))
            {
                return new ConfigurationModel();
            }

            var configurationModel = new ConfigurationModel();

            XDocument xml = XDocument.Load(File);

            var root = xml.Descendants("Configuration");
            foreach (var node in root.Descendants())
            {
                switch (node.Name.LocalName)
                {
                    case "IsAdmin":
                        configurationModel.IsAdmin = node.Attribute("Value").Value == "true";
                        break;
                    case "Email":
                        configurationModel.Name = node.Attribute("Value").Value;
                        break;
                    case "Password":
                        configurationModel.Password = node.Attribute("Value").Value;
                        break;
                }
            }

            return configurationModel;
        }

        public static void Save(ConfigurationModel configurationModel)
        {
            if (!System.IO.File.Exists(File))
            {
                var myFile = System.IO.File.Create(File);
                myFile.Close();
            }

            XDocument doc = new XDocument(new XElement("Configuration",
                new XElement("IsAdmin",
                    new XAttribute("Value", configurationModel.IsAdmin)),
                new XElement("Name",
                    new XAttribute("Value", configurationModel.Name)),
                new XElement("Password",
                    new XAttribute("Value", configurationModel.Password))
                ));

            doc.Save(File);
        }
    }
}
