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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NewsReader.View
{
	/// <summary>
	/// Interaction logic for PostView.xaml
	/// </summary>
	public partial class PostView : UserControl
	{
		PostViewModel _postViewModel = new PostViewModel();
		public PostView()
		{
			this.DataContext = _postViewModel;

			InitializeComponent();
		}

		private void BtnPost(object sender, RoutedEventArgs e)
		{
			if (Tbox_From.Text.Length > 0)
			{
				if (Tbox_Subject.Text.Length > 0)
				{
					if (Tbox_Content.Text.Length > 0)
					{
						if (Tbox_Newsgroup.Text.Length > 0)
						{
							_postViewModel.Post(Tbox_From.Text,
												Tbox_Subject.Text,
												Tbox_Content.Text,
												Tbox_Newsgroup.Text
												);

							Tbox_From.Text = "";
							Tbox_Subject.Text = "";
							Tbox_Content.Text = "";
							Tbox_Newsgroup.Text = "dk.test";
						}
					}
				}
			}
		}
	}
}
