using NewsReader.Model.Foundation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace NewsReader.Model.Domain
{
	public class NNTP : System.Net.Sockets.TcpClient
	{

		#region Connect, Login & Disconnect

		public bool Connect(string server)
		{
			string response;

			try
			{
				Connect(server, 119);
			} catch (SocketException se)
			{
				return false;
			}

			response = ConverterOperations.StreamToString(GetStream());
			GetStream().Flush();
			if (response.Length <= 0)
			{
				return false;
			}
			if (response.Substring(0, 3) != "200")
			{
				//throw new NNTPException("An error has accoured while trying to connect to the nntp-server: Server Response { "+response+" }");
				return false;
			}

			return true;
		}

		public bool Login(string email, string password)
		{
			if (!this.Connected)
			{
				return false;
			}

			string message;
			string response;

			int loginTryes = 3;

			for (int i = 1; i <= loginTryes; i++)
			{
				message = "AUTHINFO USER " + email + "\r\n";
				GetStream().Write(ConverterOperations.StringToBuffer(message), 0, message.Length);

				response = ConverterOperations.StreamToString(GetStream());
				GetStream().Flush();
				if (response.Substring(0, 3) == "381")
				{
					break;
				}

				if (i == loginTryes)
				{
					//throw new NNTPException("An error has accoured while trying to login to the nntp-server: Server Response { " + response + " }");
					return false;
				}
			}


			message = "AUTHINFO PASS " + password + "\r\n";
			GetStream().Write(ConverterOperations.StringToBuffer(message), 0, message.Length);

			response = ConverterOperations.StreamToString(GetStream());
			GetStream().Flush();
			if (response.Length <= 0)
			{
				return false;
			}
			if (response.Substring(0, 3) != "281")
			{
				//throw new NNTPException("An error has accoured while trying to login to the nntp-server: Server Response { " + response + " }");
				return false;
			}

			return true;
		}

		public bool Disconnect()
		{
			if (!this.Connected)
			{
				return false;
			}

			string message;
			string response;

			message = "QUIT\r\n";
			GetStream().Write(ConverterOperations.StringToBuffer(message), 0, message.Length);

			response = ConverterOperations.StreamToString(GetStream());
			GetStream().Flush();
			if (response.Length <= 0)
			{
				return false;
			}
			if (response.Substring(0, 3) != "205")
			{
				//throw new NNTPException("An error has accoured while trying to disconnect from the nntp-server: Server Response { " + response + " }");
				return false;
			}

			return true;
		}

		#endregion Connect, Login & Disconnect

		#region Get Methods

		public List<string> GetNewsgroups()
		{
			string message;
			string response;

			List<string> retval = null;
			message = "LIST\r\n";
			GetStream().Write(ConverterOperations.StringToBuffer(message), 0, message.Length);

			response = ConverterOperations.StreamToString(GetStream());
			if (response.Substring(0, 3) != "215")
			{
				throw new NNTPException("An error has accoured while trying to get the newsgroups: Server Response { " + response + " }");
			}

			bool hasNotFoundEnd = true;
			do
			{
				response += ConverterOperations.StreamToString(GetStream());

				retval = new List<string>();
				char[] seps = { ' ', '\n' };
				string[] values = response.Split(seps);

				// Only adding every 4th as that is the names of the groups
				int counter = 4;
				foreach (var item in values)
				{
					if (counter % 4 == 0)
					{
						if (item.Equals(".\r"))
						{
							hasNotFoundEnd = false;
						} else
						{
							if (!item.Equals("215") && !item.Equals("\"group"))
							{
								retval.Add(item);
							}
							
						}
					}
					counter += 1;
				}
				
			} while (hasNotFoundEnd);
			GetStream().Flush();

			return retval;
		}

		public List<string> GetNews(string newsgroup)
		{
			string message;
			string response;


			List<string> retval = new List<string>();

			message = "GROUP " + newsgroup + "\r\n";
			GetStream().Write(ConverterOperations.StringToBuffer(message), 0, message.Length);

			response = ConverterOperations.StreamToString(GetStream());
			GetStream().Flush();
			if (response.Substring(0, 3) != "211")
			{
				throw new NNTPException("An error has accoured while trying to get the news from { "+newsgroup+" }: Server Response { " + response + " }");
			}

			char[] seps = { ' ' };
			string[] values = response.Split(seps);

			long start = Int32.Parse(values[2]);
			long end = Int32.Parse(values[3]);

			if (start + 100 < end && end > 100)
			{
				start = end - 100;
			}

			for (long i = start; i <= end; i++)
			{

				message = "ARTICLE " + i + "\r\n";
				GetStream().Write(ConverterOperations.StringToBuffer(message), 0, message.Length);

				response = ConverterOperations.StreamToString(GetStream());
				GetStream().Flush();
				if (response.Substring(0, 3) == "423")
				{
					continue;
				}
				if (response.Substring(0, 3) != "220")
				{
					throw new NNTPException("An error has accoured while trying to get the article { "+i+" }: Server Response { " + response + " }");
				}

				string article = response+"\n";
				while (true)
				{
					response = ConverterOperations.StreamToString(GetStream());

					article += response;

					if (ConverterOperations.SingleResponsLines(response).Contains(".\r\n") || ConverterOperations.SingleResponsLines(response).Contains(".\r"))
					{
						break;
					}

				}

				retval.Add(article.Substring(0,article.Length-3)) ;

			}

			return retval;
		}

		#endregion Get Methods

		#region Post Method

		public bool Post(string from, string subject, string content, string newsgroup = "dk.test")
		{
			string message;
			string response;

			message = "POST\r\n";
			GetStream().Write(ConverterOperations.StringToBuffer(message), 0, message.Length);

			response = ConverterOperations.StreamToString(GetStream());
			GetStream().Flush();
			if (response.Substring(0, 3) != "340")
			{
				//throw new NNTPException("An error has accoured while trying to post to news-server: Server Response { " + response + " }");
				return false;
			}

			message = "From: " + from + "\r\n"
				+ "Newsgroups: " + newsgroup + "\r\n"
				+ "Subject: " + subject + "\r\n\r\n"
				+ content + "\r\n.\r\n";
			GetStream().Write(ConverterOperations.StringToBuffer(message), 0, message.Length);

			response = ConverterOperations.StreamToString(GetStream());
			GetStream().Flush();
			if (response.Substring(0, 3) != "240")
			{
				/*
				throw new NNTPException("An error has accoured while to post: Server Response { " + response + " }:\n" +
					"From - " + from + "\n" +
					"NewsGroup - " + newsgroup + "\n" +
					"Subject - " + subject + "\n" +
					"Content - \n" + content + "\n");
				*/
				return false;
			}
			return true;
		}

		#endregion Post Method

	}
}
