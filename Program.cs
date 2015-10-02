//
//  Program.cs
//
//  Author:
//       Bastien Balaud <bastien.balaud@gmail.com>
//
//  Copyright (c) 2015 Bastien Balaud
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem
{
	class MainClass
	{
		static char[] delimiterChars = { ' ', '/', '-' };

		public static void Main (string[] args)
		{
			string[] commande;
			Directory currentDirectory;
			File root = new Directory ();
			currentDirectory = (Directory)root;

			do {
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.Write ("root@C# : " + currentDirectory.getPath () + " ");
				Console.ForegroundColor = ConsoleColor.White;
				commande = commandeSplit (Console.ReadLine ());

				switch (commande [0]) {
				default:
					Console.WriteLine ("Nothing to do");
					break;

				case "create":
					if (commande.Length > 1 && currentDirectory.createNewFile (commande [1])) {
						Console.WriteLine ("Fichier : " + commande [1] + " créé");
					} else {
						Console.WriteLine ("Impossible de créer le fichier");
					}
					break;
				case "ls":
					foreach (File contenu in currentDirectory.ls()) {
						if (contenu.isFile ()) {
							Console.Write ("dir : ");
						} else {
							Console.Write ("fil : ");
						}
						Console.Write (contenu.getAccess () + " ");
						Console.Write (contenu.name);
						Console.WriteLine ();
					}
					break;
				case "mkdir":
					if (commande.Length > 1 && currentDirectory.mkdir (commande [1])) {
						Console.WriteLine ("Répertoire créé avec succés");
					} else {
						Console.WriteLine ("Impossible de créer le répertoire");
					}
					break;
				case "cd":
					if (commande.Length > 1 && (Directory)currentDirectory.cd (commande [1])!=null) {
						currentDirectory = (Directory)currentDirectory.cd (commande [1]);
					} else {
						Console.WriteLine ("Impossible de se déplacer dans le dossier spécifié");
					}
					break;
				case "root":
					currentDirectory = (Directory)root;
					break;
				case "chmod":
					if (commande.Length > 1 && currentDirectory.chmod( int.Parse (commande [1]))) {
						Console.WriteLine("Droit changé");
					} else {
						Console.WriteLine ("Valeur spécifier incorrect");
					}
					break;
				case "parent": //Recoder Parent corectement il doit permettre de 
					if (currentDirectory.getParent () == null) {
						Console.WriteLine ("Vous êtes déjà au bout du monde");
					} else {
						currentDirectory = (Directory)currentDirectory.getParent ();
					}
					break;
				case "path":
					Console.WriteLine (currentDirectory.getPath ());
					break;
				case "rename":
					if (commande.Length > 2 && currentDirectory.rename (commande [1], commande [2]) && commande [1] != null && commande [2] != null) {
						Console.WriteLine ("Action réussie");
					} else {
						Console.WriteLine ("Impossible d'effectuer l'action");
					}
					break;
				case "remove":

					if (currentDirectory.delete (commande [1])) {
						Console.WriteLine ("Action réussie");
					} else {
						Console.WriteLine ("Impossible d'effectuer l'action");
					}
					break;
				case "search":
					if (commande.Length > 1) {
						foreach (File result in currentDirectory.search(commande[1])) {
							Console.WriteLine (result.getPath ());
						}
					} else {
						Console.WriteLine ("Paramétre spécifié incorect");
					}
					break;
				case "exit":
					break;
				}


			} while(commande [0] != "exit");
			Console.WriteLine ("Sytem Shutdown NOW");
		}

		public static string[] commandeSplit (string commande)
		{
			commande = commande.Replace ("/", "_");
			string[] output = commande.Split (delimiterChars);
			return output;
		}
	}
}


