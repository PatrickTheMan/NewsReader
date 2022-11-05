using NewsReader.Model.Controller;
using NewsReader.Model.Domain;
using NewsReader.Model.Business;
using NewsReader.Singleton;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using NewsReader.ViewModel;
using NewsReader.View;
using NewsReader.Model.Foundation;
using NewsReader.Model.FileIO;

namespace NewsReader
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		MainViewModel _mainViewModel;
		public MainWindow()
		{

			this._mainViewModel = MainViewModelSingleton.Instance;
			this.DataContext = _mainViewModel;
			
			InitializeComponent();

		}

		private void BtnLogin_Click(object sender, RoutedEventArgs e)
		{
			if (TboxUsername.Text.Length > 0 && TboxPassword.Password.Length > 0)
			{
				_mainViewModel.LoginNNTP(TboxUsername.Text, TboxPassword.Password);
			}
		}

		private void BtnConnect_Click(object sender, RoutedEventArgs e)
		{
			if (TboxServername.Text.Length > 0)
			{ 
				_mainViewModel.ConnectNNTP(TboxServername.Text);
			}
		}

		private void BtnNewsGroups_Click(object sender, RoutedEventArgs e)
		{
			CControl.Content = new NewsGroupsView();
			BtnCloseLeft_Click(sender, e);
		}

		private void BtnArticles_Click(object sender, RoutedEventArgs e)
		{
			CControl.Content = new ArticleView();
			BtnCloseLeft_Click(sender, e);
		}

		private void BtnPost_Click(object sender, RoutedEventArgs e)
		{
			CControl.Content = new PostView();
			BtnCloseLeft_Click(sender, e);
		}

		private void BtnExit_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}


		private void BtnCloseLeft_Click(object sender, RoutedEventArgs e)
		{
			Storyboard sb = Resources["CloseMenuLeft"] as Storyboard;
			sb.Begin(LeftMenu);
		}
		private void BtnShowLeft_Click(object sender, RoutedEventArgs e)
		{
			Storyboard sb = Resources["OpenMenuLeft"] as Storyboard;
			sb.Begin(LeftMenu);
		}

		private void BtnCloseRight_Click(object sender, RoutedEventArgs e)
		{
			Storyboard sb = Resources["CloseMenuRight"] as Storyboard;
			sb.Begin(RightMenu);
		}
		private void BtnShowRight_Click(object sender, RoutedEventArgs e)
		{
			Storyboard sb = Resources["OpenMenuRight"] as Storyboard;
			sb.Begin(RightMenu);
		}
		private void Btn_RemoveData(object sender, RoutedEventArgs e)
		{
			MainViewModelSingleton.Instance.QuickConnectLogout();
		}

	}
}
