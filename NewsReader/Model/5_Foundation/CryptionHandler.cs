using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsReader.Model.Foundation
{
	public class CryptionHandler
	{
		private Random random = new Random();
		public string Encrypt(string key, string str)
		{
			string[] keyP = key.Split(' ');
			int x = random.Next(1, keyP[0].Length-1);
			int z;
			if (str.Length > 0)
			{
				z = random.Next(1, str.Length - 1);
			} else
			{
				return "";
			}
			string pt = x + "" + z + " ";
			string es = "";
			foreach (var i in str)
			{
				foreach (var j in keyP)
				{
					if (i.Equals(j[0]))
					{
						es += j[x];
						break;
					}
				}
			}
			string y = es;
			es = pt + y.Substring(z,es.Length-z) + y.Substring(0,z);
			return es;
		}

		public string Decrypt(string key, string str)
		{
			string[] keyP = key.Split(' ');
			int z = int.Parse("" + str[0]);
			int x = int.Parse("" + str[1]);
			string ds = "";
			foreach (var j in str.Skip(3))
			{
				foreach (var i in keyP)
				{
					if (j.Equals(i[z]))
					{
						ds += i[0];
					}
				}
			}
			string y = ds;
			ds =   y.Substring(ds.Length - x, x) + y.Substring(0, ds.Length - x);
			return ds;
		}

	}
}
