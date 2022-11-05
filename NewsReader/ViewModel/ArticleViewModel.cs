using NewsReader.Model.Domain;
using NewsReader.PropertyChanged;
using NewsReader.Singleton;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace NewsReader.ViewModel
{
	public class ArticleViewModel : NotifyPropertyChangedHandler
	{

		public ObservableCollection<string> SelectedNewsgroups { get; }
		public ObservableCollection<NewsObject> Articles { get; } = new ObservableCollection<NewsObject>();

		private string labelNoArticles = "Awaiting Login ...";

		public string LabelNoArticles
		{
			get { return labelNoArticles; }
			set { labelNoArticles = value; NotifyPropertyChanged(); }
		}


		public ArticleViewModel()
		{
			SelectedNewsgroups = new ObservableCollection<string>();

			foreach (var item in GlobalInformationBuilderSingleton.Instance.GetGlobalInformation().GetSelectedNewsGroupList().GetList())
			{
				SelectedNewsgroups.Add(item);
			}

			GetArticlesNNTP();
		}

		public void GetArticlesNNTP()
		{
			ThreadPool.QueueUserWorkItem(ThreadGetArticlesNNTP, new object());
		}

		public void ThreadGetArticlesNNTP(object info)
		{

			while (!MainViewModelSingleton.Instance.LoginBool)
			{
				Thread.Sleep(2000);
			}
			Thread.Sleep(2000);

			LabelNoArticles = "Searching ...";

			GlobalInformationBuilderSingleton.Instance.GetGlobalInformation().GetNewsObjects().ClearObjects();
	
			foreach (var item in SelectedNewsgroups)
			{
				GlobalInformationBuilderSingleton.Instance.BuildNewsObjects(item);
			}

			if (GlobalInformationBuilderSingleton.Instance.GetGlobalInformation().GetNewsObjects().GetObjects() != null)
			{
				foreach (var item in GlobalInformationBuilderSingleton.Instance.GetGlobalInformation().GetNewsObjects().GetObjects())
				{
					App.Current.Dispatcher.Invoke(() => { Articles.Add(item); });
				}
			}

			if (Articles.Count>0)
			{
				LabelNoArticles = "";
			} else
			{
				LabelNoArticles = "No Articles Found";
			}

		}

	}
}
