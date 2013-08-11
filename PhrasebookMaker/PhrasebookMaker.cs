using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace PhrasebookMaker
{
	public class PhrasebookMaker
	{
		public PhrasebookMaker ()
		{

		}



		public void Start (string pathToFile)
		{
			Console.WriteLine ("Making Phrasebook from " + pathToFile);

			//Get Translations and Language from file
			XDocument translationSource = XDocument.Load (pathToFile);
			string language = translationSource.Descendants("TS").First().Attribute("language").Value;
			var messages = (from message in translationSource.Descendants ("message")
			           where (string)message.Element ("translation") != ""
			           select message).DistinctBy(message =>message.Element("source").Value);


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
			phrases.Save (Path.ChangeExtension(pathToFile,".qph"));

			Console.WriteLine ("Phrasebook successfully written to " + Path.ChangeExtension(pathToFile,".qph"));
		}
	}
}

