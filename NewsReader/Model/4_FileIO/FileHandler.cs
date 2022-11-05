using NewsReader.Model.Foundation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsReader.Model.FileIO
{
	public class FileHandler
	{

		private static string PATH = "C:\\Users\\patri\\source\\repos\\NewsReader\\NewsReader\\Model\\4_FileIO\\Files";

		private static string KEYPATH = "\\keyFile.txt";
		private static string UPATH = "\\u.txt";
		private static string PPATH = "\\p.txt";
		private static string SPATH = "\\s.txt";
		private static string FAVPATH = "\\fav\\";

		private static CryptionHandler CryptionHandler = new CryptionHandler();
		private static string _key;

		public static string ReadKey()
		{
			_key = File.ReadAllText(PATH + KEYPATH);
			return _key;
		}
		public static string ReadS()
		{
			return File.ReadAllText(PATH + SPATH);
		}

		public static void WriteS(string s)
		{
			File.WriteAllText(PATH + SPATH, s);
		}

		public static string ReadU()
		{
			return File.ReadAllText(PATH + UPATH);
		}

		public static void WriteU(string u)
		{
			File.WriteAllText(PATH + UPATH, u);
		}

		public static string ReadP()
		{
			if (_key == null || _key.Length <= 0)
			{
				ReadKey();
			}

			return CryptionHandler.Decrypt(_key, File.ReadAllText(PATH + PPATH));
		}

		public static void WriteP(string p)
		{
			if (_key == null || _key.Length <= 0)
			{
				ReadKey();
			}

			File.WriteAllText(PATH + PPATH, CryptionHandler.Encrypt(_key, p));
		}

		public static List<string> ReadFav(string username)
		{
			bool found = false;
			List<string> favoritNewsGroups = new List<string>();

			foreach (var item in Directory.GetFiles(PATH + FAVPATH))
			{
				if (item.Equals(PATH + FAVPATH + username + ".txt"))
				{
					found = true;
				}
			}
			if (!found)
			{
				return new List<string>();
			}

			string formattetFNG = File.ReadAllText(PATH + FAVPATH + username + ".txt");
			string[] newsGroups = formattetFNG.Split('\n');

			foreach (var item in newsGroups)
			{
				favoritNewsGroups.Add(item);
			}

			return favoritNewsGroups;
		}

		public static void WriteFav(List<string> favoritNewsGroups)
		{
			string formattetFNG = "";

			foreach (var item in favoritNewsGroups)
			{
				formattetFNG += item + "\n";
			}

			if (formattetFNG.Equals(""))
			{
				File.WriteAllText(PATH + FAVPATH + ReadU() + ".txt", "");
			} else
			{
				File.WriteAllText(PATH + FAVPATH + ReadU() + ".txt", formattetFNG.Substring(0, formattetFNG.Length - 1));
			}
			
		}

	}
}
