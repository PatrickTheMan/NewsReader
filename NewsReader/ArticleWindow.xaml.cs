using NewsReader.Model.Domain;
using NewsReader.ViewModel;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace NewsReader
{
	/// <summary>
	/// Interaction logic for ArticleWindow.xaml
	/// </summary>
	public partial class ArticleWindow : Window
	{
		ArticleWindowModel _articleWindowModel;
		public ArticleWindow(NewsObject newsObject)
		{
			_articleWindowModel = new ArticleWindowModel(newsObject);

			this.DataContext = _articleWindowModel;

			InitializeComponent();
		}
	}
}
