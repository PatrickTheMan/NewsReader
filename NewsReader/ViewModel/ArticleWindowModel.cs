using NewsReader.Model.Foundation;
using NewsReader.Model.Domain;
using NewsReader.PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsReader.ViewModel
{
	public class ArticleWindowModel : NotifyPropertyChangedHandler
	{
		public ObservableCollection<String> MetaData { get; } = new ObservableCollection<String>();

		private string from;

		public string From
		{
			get { return from; }
			set { from = value; NotifyPropertyChanged(); }
		}

		private string subject;

		public string Subject
		{
			get { return subject; }
			set { subject = value; NotifyPropertyChanged(); }
		}

		private string date;

		public string Date
		{
			get { return date; }
			set { date = value; NotifyPropertyChanged(); }
		}

		private string content;

		public string Content
		{
			get { return content; }
			set { content = value; NotifyPropertyChanged(); }
		}


		public ArticleWindowModel(NewsObject newsObject)
		{

			App.Current.Dispatcher.Invoke(() =>
			{
				MetaData.Add("Path: " + newsObject.Path);
				MetaData.Add("MIMEVersion: " + newsObject.MIMEVersion);
				MetaData.Add("UserAgent: " + newsObject.UserAgent);
				MetaData.Add("Newsgroups: " + newsObject.Newsgroup);
				MetaData.Add("ContentLanguage: " + newsObject.ContentLanguage);
				MetaData.Add("ContentType: " + newsObject.ContentType);
				MetaData.Add("ContentTransferEncoding: " + newsObject.ContentTransferEncoding);
				MetaData.Add("Lines: " + newsObject.Lines);
				MetaData.Add("MessageID: " + newsObject.MessageID);
				MetaData.Add("XComplainsTo: " + newsObject.XComplainsTo);
				MetaData.Add("NNTPPostingDate: " + newsObject.NNTPPostingDate);
				MetaData.Add("Organization: " + newsObject.Organization);
				MetaData.Add("InjectionDate: " + newsObject.InjectionDate);
				MetaData.Add("InjectionInfo: " + newsObject.InjectionInfo);
				MetaData.Add("XRecevied: " + newsObject.XReceived);
				MetaData.Add("XRef: " + newsObject.XRef);
			});

			From = newsObject.From;
			Subject = newsObject.Subject;
			Date = newsObject.Date;
			Content = Translator.FixStringViaUTF8Hex(newsObject.Content);
		}

	}
}
