using NewsReader.Model.Domain;
using NewsReader.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
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

namespace NewsReader.View
{
	/// <summary>
	/// Interaction logic for ArticleView.xaml
	/// </summary>
	public partial class ArticleView : UserControl
	{
		ArticleViewModel _articleViewModel = new ArticleViewModel();
		public ArticleView()
		{
			DataContext = _articleViewModel;

			InitializeComponent();
		}

		private void ListViewItem_ArticleList(object sender, MouseButtonEventArgs e)
		{
			var item = sender as ListViewItem;
			if (item != null && item.IsSelected)
			{
				var obj = ListView_ArticleList.SelectedItem as NewsObject;
				ArticleWindow aw = new ArticleWindow(obj);
				aw.Title = obj.Subject;

				Image icon = new Image();
				aw.Show();
			}
			
		}

	}
}
