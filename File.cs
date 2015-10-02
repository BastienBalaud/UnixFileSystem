//
//  EmptyClass.cs
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
	public class File
	{

		public string name { get; set; }

		public int permission { get; set; }

		public Directory parent { get; }

		public File (string name, Directory parrent)
		{
			this.name = name;
			this.permission = 6;
			this.parent = parrent;
		}

		public File (string name, int permission, Directory parrent)
		{
			this.name = name;
			this.permission = permission;
			this.parent = parrent;
		}

		public File () //Utilisé pour la construction de root
		{
		}

		public bool canWrite ()
		{

			return (permission & 2) > 0;

		}

		public bool canExecute ()
		{

			return (permission & 1) > 0;

		}

		public bool canRead ()
		{

			return (permission & 4) > 0;

		}

		public string getName ()
		{
			return this.name;
		}

		public bool chmod (int permission)
		{
			if (permission <= 7 && permission >=0) {
				this.permission = permission;
				return true;
			} else {
				return false;
			}

		}

		public bool isFile ()
		{
			return (this.GetType () == typeof(File));
		}

		public bool isDirectory ()
		{
			return (this.GetType () == typeof(Directory));
		}

		public string getPath ()
		{
			File parrent = this;
			string path = "";

			while (parrent != null) {
				if (parrent.name == "/") {
					path = parrent.name + path;
				} else {
					if (parrent.isFile ()) {

						path = parrent.name + path;
					} else {
						path = parrent.name + "/" + path;
					}
				}
				parrent = parrent.parent;
			} 

			return path;
		}

		public bool renameTo (string newName)
		{
			return false;
		}

		public File getParent ()
		{
			return parent;
		}

		public string getAccess ()
		{
			string acces = "";
			if (this.canRead ()) {
				acces += "r";
			} else {
				acces += "-";
			}
			if (this.canWrite ()) {
				acces += "w";
			} else {
				acces += "-";
			}
			if (this.canExecute ()) {
				acces += "x";
			} else {
				acces += "-";
			}

			return acces;
		}
	}
}


