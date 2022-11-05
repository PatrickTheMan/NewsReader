using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NewsReader.Model.Foundation
{
	public class ConverterOperations
	{

		public static string StreamToString(NetworkStream stream)
		{

			byte[] buffer = new byte[40960000];
			int bytesRead = stream.Read(buffer, 0, buffer.Length);
			return Encoding.UTF8.GetString(buffer, 0, bytesRead);

		}

		public static byte[] StringToBuffer(string str)
		{

			return Encoding.UTF8.GetBytes(str);

		}

		public static Stream StringToStream(string str)
		{

			var stream = new MemoryStream();
			var writer = new StreamWriter(stream);

			writer.Write(str);
			writer.Flush();
			stream.Position = 0;

			return stream;

		}

		public static string[] SingleResponsLines(string fullRespons)
		{

			char[] seps = { '\n', };
			return fullRespons.Split(seps);

		}

	}
}
