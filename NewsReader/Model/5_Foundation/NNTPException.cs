using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsReader.Model.Foundation
{
	public class NNTPException : System.ApplicationException
	{

		public NNTPException(string str) : base(str)
		{

		}

	}
}
