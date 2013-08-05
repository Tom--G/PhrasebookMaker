using System;
using System.IO;

namespace PhrasebookMaker
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string pathToFile;

			Console.WriteLine ("PhrasebookMaker made by galgalesh");

			if (args.Length > 0) {
				var path = args [0];
				if (File.Exists (path)) {
					pathToFile = args [0];
				} else {
					Console.WriteLine ("file does not exist");
					return;
				}
			} else {
				Console.WriteLine ("please specify file");
				return;
			}

			PhrasebookMaker pm = new PhrasebookMaker();
			pm.Start(pathToFile);
		}
	}
}
