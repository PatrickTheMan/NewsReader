using Microsoft.VisualBasic;
using NewsReader.Model.FileIO;
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
using System.Windows.Media;
using System.Windows.Threading;

namespace NewsReader.ViewModel
{
	public class NewsGroupsViewModel : NotifyPropertyChangedHandler
	{
		public ObservableCollection<string> Newsgroups { get; } = new ObservableCollection<string>();
		public ObservableCollection<string> SelectedNewsgroups { get; } = new ObservableCollection<string>();

		#region Properties

		private bool sNGSelectedBool = false;

		public bool SNGSelectedBool
		{
			get { return sNGSelectedBool; }
			set { sNGSelectedBool = value; NotifyPropertyChanged(); }
		}

		private bool aNGSelectedBool = false;

		public bool ANGSelectedBool
		{
			get { return aNGSelectedBool; }
			set { aNGSelectedBool = value; NotifyPropertyChanged(); }
		}

		private int amountANG;

		public int AmountANG
		{
			get { return amountANG; }
			set { amountANG = value; NotifyPropertyChanged(); }
		}

		private int amountSNG;

		public int AmountSNG
		{
			get { return amountSNG; }
			set { amountSNG = value; NotifyPropertyChanged(); }
		}

		private string textANG = "Awaiting Login ...";

		public string TextANG
		{
			get { return textANG; }
			set { textANG = value; NotifyPropertyChanged(); }
		}

		private string textSNG = "Searching ...";

		public string TextSNG
		{
			get { return textSNG; }
			set { textSNG = value; NotifyPropertyChanged(); }
		}

		#endregion Properties

		public NewsGroupsViewModel()
		{
			GetSelectedNewsgroups();

			ThreadPool.QueueUserWorkItem(ThreadGetNewsgroupsNNTP, new object());
		}

		public void RemoveSelectedNewsgroup(string newsgroup)
		{
			foreach (var item in SelectedNewsgroups)
			{
				if (item.Equals(newsgroup))
				{
					App.Current.Dispatcher.Invoke(() => 
					{ 
						SelectedNewsgroups.Remove(item); 
						
						if (item.EndsWith("*"))
						{
							Newsgroups.Add(item.Substring(0, item.Length - 1));
						} else
						{
							Newsgroups.Add(item);
						}
						
					});

					break;
				}
			}

			if (SelectedNewsgroups.Count > 0)
			{
				if (!TextSNG.Equals(""))
				{
					TextSNG = "";
				}
			}
			else
			{
				TextSNG = "Empty";
			}

			AmountANG = Newsgroups.Count;
			AmountSNG = SelectedNewsgroups.Count;

			UpdateGlobalInformationSelectedNewsGroupList();
			FileHandler.WriteFav(GlobalInformationBuilderSingleton.Instance.GetGlobalInformation().GetFavoritNewsGroupList().GetList());
		}

		public void AddSelectedNewsgroup(string newsgroup)
		{
			foreach (var item in Newsgroups)
			{
				if (item.Equals(newsgroup))
				{
					App.Current.Dispatcher.Invoke(() => { SelectedNewsgroups.Add(item); Newsgroups.Remove(item); });
					break;
				}
			}

			if (SelectedNewsgroups.Count > 0)
			{
				if (!TextSNG.Equals(""))
				{
					TextSNG = "";
				}
			}

			AmountANG = Newsgroups.Count;
			AmountSNG = SelectedNewsgroups.Count;

			UpdateGlobalInformationSelectedNewsGroupList();
		}

		public void FavoritSelectedNewsgroup(string newsgroup)
		{
			foreach (var item in SelectedNewsgroups)
			{
				if (item.Equals(newsgroup))
				{
					App.Current.Dispatcher.Invoke(() => 
					{
						if (!item.EndsWith("*")) 
						{
							SelectedNewsgroups.Remove(item); 

							ObservableCollection<string> values = new ObservableCollection<string>();

							values.Add(item + "*");

							foreach (var item1 in SelectedNewsgroups)
							{
								values.Add(item1);
							}

							SelectedNewsgroups.Clear();

							foreach (var item1 in values)
							{
								SelectedNewsgroups.Add(item1);
							}
						}
						else 
						{ 
							SelectedNewsgroups.Remove(item); 
							SelectedNewsgroups.Add(item.Substring(0, item.Length - 1)); 
						}
					});
					break;
				}
			}

			UpdateGlobalInformationSelectedNewsGroupList();
			FileHandler.WriteFav(GlobalInformationBuilderSingleton.Instance.GetGlobalInformation().GetFavoritNewsGroupList().GetList());
		}

		public void GetSelectedNewsgroups()
		{
			foreach (var item in GlobalInformationBuilderSingleton.Instance.GetGlobalInformation().GetFavoritNewsGroupList().GetList())
			{
				App.Current.Dispatcher.Invoke(() => { SelectedNewsgroups.Add(item + "*"); });
			}

			foreach (var item in GlobalInformationBuilderSingleton.Instance.GetGlobalInformation().GetSelectedNewsGroupList().GetList())
			{
				if (!SelectedNewsgroups.Contains(item+"*"))
				{
					App.Current.Dispatcher.Invoke(() => { SelectedNewsgroups.Add(item); });
				}
			}

			if (SelectedNewsgroups.Count > 0)
			{
				TextSNG = "";
			} else
			{
				TextSNG = "Empty";
			}

		}

		public void ThreadGetNewsgroupsNNTP(object info)
		{

			while (!MainViewModelSingleton.Instance.LoginBool)
			{
				Thread.Sleep(2000);
			}

			Thread.Sleep(2000);

			TextANG = "Searching ...";

			if (GlobalInformationBuilderSingleton.Instance.GetGlobalInformation().GetNewsGroupList().GetList().Count <= 1000)
			{
				GlobalInformationBuilderSingleton.Instance.BuildNewsGroupList();
			}

			App.Current.Dispatcher.Invoke(() => { SelectedNewsgroups.Clear(); });
			foreach (var item in GlobalInformationBuilderSingleton.Instance.GetGlobalInformation().GetFavoritNewsGroupList().GetList())
			{
				App.Current.Dispatcher.Invoke(() => { SelectedNewsgroups.Add(item+"*"); });
			}

			foreach (var item in GlobalInformationBuilderSingleton.Instance.GetGlobalInformation().GetNewsGroupList().GetList())
			{
				App.Current.Dispatcher.Invoke(() => { if (!SelectedNewsgroups.Contains(item) && !SelectedNewsgroups.Contains(item+"*")) { Newsgroups.Add(item); } });
			}

			if (Newsgroups.Count > 0)
			{
				if (!TextANG.Equals(""))
				{
					TextANG = "";
				}
			}
			else
			{
				TextANG = "Empty";
			}

			AmountANG = Newsgroups.Count;
			AmountSNG = SelectedNewsgroups.Count;
		}
		
		private void UpdateGlobalInformationSelectedNewsGroupList()
		{

			NewsGroupList sNG = new NewsGroupList();
			NewsGroupList fNG = new NewsGroupList();

			App.Current.Dispatcher.Invoke(() => {

				foreach (var item in SelectedNewsgroups)
				{
					if (item.EndsWith("*"))
					{
						fNG.GetList().Add(item.Substring(0,item.Length - 1));
						sNG.GetList().Add(item.Substring(0, item.Length - 1));
					} else
					{
						sNG.GetList().Add(item);
					}
				}

			});

			GlobalInformationBuilderSingleton.Instance.GetGlobalInformation().SetSelectedNewsGroupList(sNG);
			GlobalInformationBuilderSingleton.Instance.GetGlobalInformation().SetFavoritNewsGroupList(fNG);
		}

	}
}
