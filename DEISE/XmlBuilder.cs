using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace DEISE
{
    public static class XmlBuilder
    {
        public static XDocument BuildInputSettings(List<Command> graph)
        {
            var xml = new XElement("InputSettings");

            foreach (var item in graph)
            {
                xml.Add(buildCommand(item));
            }

            var root = new XDocument(new XDeclaration("1.0", "utf-8", "no"), xml);

            return root;
        }

        private static XElement buildCommand(Command cmd)
        {
            var xml = new XElement("Command", new XAttribute("Name", cmd.Name));

            foreach (var item in cmd.Triggers)
            {
                xml.Add(buildTrigger(item));
            }

            return xml;
        }

        private static XElement buildTrigger(Trigger trigger)
        {
            var xml = new XElement("Trigger", new XAttribute("Name", trigger.Name), new XAttribute("Button", getEnumValue(trigger.Button)), new XAttribute("State", getEnumValue(trigger.State)));

            foreach (var item in trigger.Modifiers)
            {
                xml.Add(new XElement("Modifier", new XAttribute("Button", item)));
            }

            return xml;
        }

        private static string getEnumValue(Enum e)
        {
            if (e == null)
            {
                return string.Empty;
            }
            else
            {
                return e.ToString();
            }
        }
    }
}
