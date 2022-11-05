using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NewsReader.Model.Domain
{
	public class NewsObject
	{

		#region Information

		public string Path { get; }
		public string MIMEVersion { get; }
		public string UserAgent { get; }
		public string Newsgroups { get; }
		public string ContentLanguage { get; }
		public string From { get; }
		public string Subject { get; }
		public string ContentType { get; }
		public string ContentTransferEncoding { get; }
		public int Lines { get; }
		public string MessageID { get; }
		public string XComplainsTo { get; }
		public string NNTPPostingDate { get; }
		public string Organization { get; }
		public string Date { get; }
		public string InjectionDate { get; }
		public string InjectionInfo { get; }
		public string XReceived { get; }
		public string XRef { get; }

		#endregion Information

		#region Newsgroup

		//MAYBE
		public string Newsgroup { get; }

		#endregion Newsgroup

		#region Content

		public string Content { get; }

		#endregion Content

		#region Constructor

		public NewsObject(string path, string MIMEVersion, string userAgent, string newsGroups, string contentLanguage, string from,
							string subject, string contentType, string contentTransferEncoding, string lines, string messageID, string xComplainsTo,
							string NNTPPostingDate, string organization, string date, string injectionDate, string injectionInfo, string xReceived,
							string xRef, string content)
		{
			this.Path = path;
			this.MIMEVersion = MIMEVersion;
			this.UserAgent = userAgent;
			this.Newsgroups = newsGroups;
			this.ContentLanguage = contentLanguage;
			this.From = from;
			this.Subject = subject;
			this.ContentType = contentType;
			this.ContentTransferEncoding = contentTransferEncoding;
			if (!lines.Equals("Not Found"))
			{
				this.Lines = int.Parse(lines);
			} else
			{
				this.Lines = -1;
			}
			this.MessageID = messageID;
			this.XComplainsTo = xComplainsTo;
			this.NNTPPostingDate = NNTPPostingDate;
			this.Organization = organization;
			this.Date = date;
			this.InjectionDate = injectionDate;
			this.InjectionInfo = injectionInfo;
			this.XReceived = xReceived;
			this.XRef = xRef;

			//this.Newsgroup = newsgroup;
			this.Content = content;
		}

		#endregion Constructor

	}
}
