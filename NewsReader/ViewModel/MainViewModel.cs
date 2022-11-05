using NewsReader.Model.FileIO;
using NewsReader.Model.Domain;
using NewsReader.PropertyChanged;
using NewsReader.Singleton;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Linq;

namespace NewsReader.ViewModel
{
	public class MainViewModel : NotifyPropertyChangedHandler
	{

		#region Properties - Connection Status

		private SolidColorBrush connectionStatusColor = Brushes.Red;

		public SolidColorBrush ConnectionStatusColor
		{
			get { return connectionStatusColor; }
			set { connectionStatusColor = value; NotifyPropertyChanged(); }
		}

		private string connectionStatusText = "Not Connected";

		public string ConnectionStatusText
		{
			get { return connectionStatusText; }
			set { connectionStatusText = value; NotifyPropertyChanged(); }
		}

		private bool connectionStatusBool = false;

		public bool ConnectionStatusBool
		{
			get { return connectionStatusBool; }
			set { connectionStatusBool = value; NotifyPropertyChanged(); }
		}


		#endregion Properties - Connection Status

		#region Properties - Label

		private SolidColorBrush connectionColor = Brushes.Red;

		public SolidColorBrush ConnectionColor
		{
			get { return connectionColor; }
			set { connectionColor = value; NotifyPropertyChanged(); }
		}

		private string connectText = "";

		public string ConnectText
		{
			get { return connectText; }
			set { connectText = value; NotifyPropertyChanged(); }
		}

		private SolidColorBrush loginColor = Brushes.Red;

		public SolidColorBrush LoginColor
		{
			get { return loginColor; }
			set { loginColor = value; NotifyPropertyChanged(); }
		}

		private string loginText = "";

		public string LoginText
		{
			get { return loginText; }
			set { loginText = value; NotifyPropertyChanged(); }
		}

		private bool loginBool = false;

		public bool LoginBool
		{
			get { return loginBool; }
			set { loginBool = value; NotifyPropertyChanged(); }
		}

		#endregion Properties - Label Errors

		#region Properties - Quick Connect

		private string quickLoginText = "";

		public string QuickLoginText
		{
			get { return quickLoginText; }
			set { quickLoginText = value; NotifyPropertyChanged(); }
		}

		private string quickLoginDoingText = "";

		public string QuickLoginDoingText
		{
			get { return quickLoginDoingText; }
			set { quickLoginDoingText = value; NotifyPropertyChanged(); }
		}

		#endregion Properties - Quick Connect

		#region Constructor

		public MainViewModel()
		{

			try
			{
				QuickLoginText = "Looking For NewsServer";
				string s = FileHandler.ReadS();
				if (s.Length <= 0)
				{
					QuickLoginText = "No NewsServer Found";
					return;
				}
				ConnectNNTP(s);

				QuickLoginText = "Looking For User";
				string u = FileHandler.ReadU();
				if (u.Length <= 0)
				{
					QuickLoginText = "No User Found";
					return;
				}
				QuickLoginText = "Looking For Password";
				string p = FileHandler.ReadP();
				if (p.Length <= 0)
				{
					QuickLoginText = "No Password Found";
					return;
				}
				LoginNNTP(u, p);

				QuickLoginText = "Found All Information";
			} catch
			{ 
				
			}
			
		}

		#endregion Constructor

		#region Connect
		public void ConnectNNTP(string serverName)
		{

			ThreadPool.QueueUserWorkItem(ThreadConnectNNTP,new object[] { serverName });

			FileHandler.WriteS(serverName);

		}

		public void ThreadConnectNNTP(object serverName)
		{
			object[] array = serverName as object[];
			string name = (string)array[0];

			ConnectText = "Connecting ...";
			QuickLoginDoingText = "Connecting ...";
			ConnectionColor = Brushes.LightGray;

			if (NNTPSingleton.Instance.Connect(name))
			{
				ConnectionStatusText = "Connected";
				ConnectionStatusBool = true;
				ConnectionStatusColor = Brushes.Green;

				QuickLoginDoingText = "Connection Success";
				ConnectText = "Success";
				ConnectionColor = Brushes.Green;
			}
			else
			{
				ConnectionStatusText = "Not Connected";
				ConnectionStatusBool = false;
				ConnectionStatusColor = Brushes.Red;

				QuickLoginDoingText = "Failed Connection";
				ConnectText = "Failed Connection";
				ConnectionColor = Brushes.Red;
			}

		}

		#endregion Connect

		#region Login

		public void LoginNNTP(string username, string password)
		{

			ThreadPool.QueueUserWorkItem(ThreadLoginNNTP, new object[] { username, password });

			FileHandler.WriteU(username);
			FileHandler.WriteP(password);

			List<string> fav = FileHandler.ReadFav(username);
			if (fav.Count > 0)
			{
				GlobalInformationBuilderSingleton.Instance.GetGlobalInformation().GetFavoritNewsGroupList().SetList(fav);
				GlobalInformationBuilderSingleton.Instance.GetGlobalInformation().GetSelectedNewsGroupList().SetList(fav);
			}

		}

		public void ThreadLoginNNTP(object info)
		{

			object[] array = info as object[];
			string username = (string)array[0];
			string password = (string)array[1];

			while (!connectionStatusBool)
			{
				if (!LoginText.Equals("Awaiting Connection"))
				{
					LoginText = "Awaiting Connection";
					LoginColor = Brushes.LightBlue;
				}
				Thread.Sleep(2000);
			}

			LoginText = "Logging in ...";
			QuickLoginDoingText = "Logging in ...";
			LoginColor = Brushes.LightGray;

			if (NNTPSingleton.Instance.Login(username, password))
			{
				QuickLoginText = "Quick Login Active";
				QuickLoginDoingText = "Login Success";
				LoginText = "Success";
				LoginColor = Brushes.Green;
				LoginBool = true;
			} else
			{
				QuickLoginDoingText = "Failed Login";
				LoginText = "Failed Login";
				LoginColor = Brushes.Red;
				LoginBool = false;
			}


		}

		public void QuickConnectLogout()
		{
			FileHandler.WriteS("");
			FileHandler.WriteU("");
			FileHandler.WriteP("");

			GlobalInformationBuilderSingleton.Instance.GetGlobalInformation().GetFavoritNewsGroupList().SetList(new List<string>());
			GlobalInformationBuilderSingleton.Instance.GetGlobalInformation().GetSelectedNewsGroupList().SetList(new List<string>());

			NNTPSingleton.Instance.Disconnect();
			ConnectionStatusText = "Not Connected";
			ConnectionStatusBool = false;
			ConnectionStatusColor = Brushes.Red;
			ConnectText = "Disconnected";
			ConnectionColor = Brushes.LightGray;
			LoginText = "Logged out";
			LoginColor = Brushes.LightGray;
			LoginBool = false;

			QuickLoginText = "Logged Out & Disconnected";
			QuickLoginDoingText = "Success";
		}

		#endregion Login

	}
}
