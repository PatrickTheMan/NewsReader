using NewsReader.PropertyChanged;
using NewsReader.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NewsReader.ViewModel
{
	public class PostViewModel : NotifyPropertyChangedHandler
	{

		public void Post(string from, string subject, string content, string newsgroup )
		{
			ThreadPool.QueueUserWorkItem(ThreadPost, new object[] { from, subject, content, newsgroup });
		}

		public void ThreadPost(object info)
		{

			object[] array = info as object[];
			string from = (string)array[0];
			string subject = (string)array[1];
			string content = (string)array[2];
			string newsgroup = (string)array[3];

			while (!MainViewModelSingleton.Instance.LoginBool)
			{
				Thread.Sleep(2000);
			}

			Thread.Sleep(1000);

			NNTPSingleton.Instance.Post(from, subject, content, newsgroup);
		}

	}
}
