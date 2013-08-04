using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace PhrasebookMaker
{
	public class PhrasebookMaker
	{
		public PhrasebookMaker ()
		{

		}

		public void Start ()
		{
			Console.WriteLine ("PhrasebookMaker Start");

			//Get Translations from file
			XElement xelement = XElement.Load ("../../wpsresource.ts");
			string language = xelement.Element("TS").Attribute("language").Value;


			var messages = from message in xelement.Descendants ("message")
			           where (string)message.Element ("translation") != ""
			           select message;


			//Make new Phrasebook
			XDocument phrases = new XDocument(
			    new XDocumentType("QPH", null, null , null),
			    new XElement("QPH", new XAttribute("language", language))
			);

			//Populate Phrasebook
			foreach (var message in messages) {
				phrases.Element ("QPH").Add( new XElement( "phrase", 
				                             new XElement("source", message.Element("source").Value),
				                             new XElement("target", message.Element("translation").Value)
				                             )
				               );
			}

			//Save Phrasebook
			phrases.Save ("../../wpsresource.qph");
		}
	}
}

