//
//  Directory.cs
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
	public class Directory : File
	{
		public List<File> content = new List<File> ();

		public Directory (string name, Directory parrent) : base (name, parrent)
		{
		}

		public Directory () : base ()
		{
			this.name = "/";
			this.permission = 7;

			Console.WriteLine ("C# File engine starting...");
			Console.WriteLine ("Sytem ready");
		}
		public bool delete(string name) {
			if(this.canWrite() && this.exist(name))
			{
				foreach (File contenu in content) {
					if (contenu.name.Equals (name)) {
						content.Remove (contenu);
						return true;
					}
				}
			}
			return false;
		}
		public List<File> ls() {
			return content;
		}
		public bool mkdir (string name)
		{
			if (name != null && base.canWrite() && !this.exist(name)) {
				File dossier = new Directory (name,(Directory) this);
				content.Add (dossier);
				return true;
			} else {

				return false;
			}

		}
		public bool createNewFile (string name)
		{
			if (name != null && base.canWrite() && !this.exist(name)) {
				File fichier = new File (name, this);
				content.Add (fichier);
				return true;
			} else {
				return false;
			}

		}
		public bool exist (string name)// Fonction vérifiant qu'il n'existe pas un fichier portant le nom passer en paramétre
		{
			foreach (File contenu in content) {
				if (contenu.name.Equals (name)) {
					return true;
				}
			}
			return false;
		}
		public File cd (string name)
		{
			foreach (File contenu in content) {
				if (contenu.name.Equals (name) && contenu.isDirectory () && contenu.canRead() && name!=null) {
					return contenu;
				}
			}
			return null;
		}
		public bool rename(string currentName, string newName)
		{
			if (this.exist (currentName)&& !this.exist(newName)&& this.canWrite()) {
				foreach (File contenu in content) {
					if (contenu.name.Equals (currentName)) {
						contenu.name = newName;
						return true;
					}
				}
				return false;
			} else {
				return false;
			}
		}
		public List<File> search(string name)
		{
			List<File> result = new List<File> ();
			foreach (File contenu in content) {
				if (contenu.name == name) {
					result.Add (contenu);
				}
				if (contenu.isDirectory()) {
					Directory current = (Directory)contenu;
					result.AddRange(current.search (name));
				}
			}
			return result;
		}

	}
}



