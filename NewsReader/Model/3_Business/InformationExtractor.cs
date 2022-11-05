using NewsReader.Model.Foundation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace NewsReader.Model.Business
{
	public class InformationExtractor
	{
		public enum Target {
			Path,
			MIMEVersion,
			UserAgent,
			Newsgroups,
			ContentLanguage,
			From,
			Subject,
			ContentType,
			ContentTransferEncoding,
			Lines,
			MessageID,
			XCompainsTo,
			NNTPPostingDate,
			Organization,
			Date,
			InjectionDate,
			InjectionInfo,
			XReceived,
			XRef
		}

		private InformationExtractor() { }

		public static string ExtractInformation(Target target, string text)
		{

			string result = "";

			string searchFor = "";
			switch (target)
			{
				case Target.Path: searchFor = "Path:"; break;
				case Target.MIMEVersion: searchFor = "MIME-Version:"; break;
				case Target.UserAgent: searchFor = "User-Agent:"; break;
				case Target.Newsgroups: searchFor = "Newsgroups:"; break;
				case Target.ContentLanguage: searchFor = "Content-Language:"; break;
				case Target.From: searchFor = "From:"; break;
				case Target.Subject: searchFor = "Subject:"; break;
				case Target.ContentType: searchFor = "Content-Type:"; break;
				case Target.ContentTransferEncoding: searchFor = "Content-Transfer-Encoding:"; break;
				case Target.Lines: searchFor = "Lines:"; break;
				case Target.MessageID: searchFor = "Message-ID:"; break;
				case Target.XCompainsTo: searchFor = "X-Complaints-To:"; break;
				case Target.NNTPPostingDate: searchFor = "NNTP-Posting-Date:"; break;
				case Target.Organization: searchFor = "Organization:"; break;
				case Target.Date: searchFor = "Date:"; break;
				case Target.InjectionDate: searchFor = "Injection-Date:"; break;
				case Target.InjectionInfo: searchFor = "Injection-Info:"; break;
				case Target.XReceived: searchFor = "X-Received-Bytes:"; break;
				case Target.XRef: searchFor = "Xref:"; break;
			}
			int cut = searchFor.Length + 1;

			string[] lines = ConverterOperations.SingleResponsLines(text);

			bool foundTarget = false;
			foreach (var item in lines)
			{
				if (foundTarget)
				{
					if (item.Contains(":"))
					{
						break;
					}
					// The 1 is to remove \r at the end
					result += "," + item.Substring(0,item.Length-1);
					continue;
				}
				if (item.Contains(searchFor))
				{
					// The 1 is to remove \r at the end
					result = item.Substring(cut,item.Length-cut-1);
					foundTarget = true;

					// As the last information about the article is Xref:, then it doesn't need to look further
					if (searchFor == "Xref:")
					{
						break;
					}
				}
			}

			if (result.Length<1)
			{
				result = "Not Found";
			}

			return result;
		}

		public static string ExtractContent(string text)
		{
			string result = "";

			string[] lines = ConverterOperations.SingleResponsLines(text);

			int skip = 0;
			foreach (var item in lines)
			{
				if (item.Contains("Xref:"))
				{
					skip++;
					break;
				} else
				{
					skip++;
				}
			}

			for (int i = skip; i < lines.Length; i++)
			{
				result += lines[i];
			}

			return result;
		}

	}
}
