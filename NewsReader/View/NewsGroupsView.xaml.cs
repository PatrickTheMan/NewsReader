using NewsReader.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
	/// Interaction logic for NewsGroupsView.xaml
	/// </summary>
	public partial class NewsGroupsView : UserControl
	{
		NewsGroupsViewModel _newsGroupsViewModel = new NewsGroupsViewModel();
		public NewsGroupsView()
		{

			this.DataContext = _newsGroupsViewModel;

			InitializeComponent();

			CollectionView viewANG = (CollectionView)CollectionViewSource.GetDefaultView(ListANG.ItemsSource);
			viewANG.Filter = FilterANG;

			CollectionView viewSNG = (CollectionView)CollectionViewSource.GetDefaultView(ListSNG.ItemsSource);
			viewSNG.Filter = FilterSNG;
		}

		private void ListViewItem_ListANG(object sender, MouseButtonEventArgs e)
		{
			var item = sender as ListViewItem;

			if (!_newsGroupsViewModel.ANGSelectedBool)
			{
				_newsGroupsViewModel.ANGSelectedBool = true;
			}

			if (item != null && item.IsSelected)
			{
				_newsGroupsViewModel.AddSelectedNewsgroup(ListANG.SelectedItem.ToString());
				_newsGroupsViewModel.ANGSelectedBool = false;
			}
		}

		private void ListViewItem_ListSNG(object sender, MouseButtonEventArgs e)
		{
			var item = sender as ListViewItem;

			if (!_newsGroupsViewModel.SNGSelectedBool)
			{
				_newsGroupsViewModel.SNGSelectedBool = true;
			}

			if (item != null && item.IsSelected)
			{
				_newsGroupsViewModel.RemoveSelectedNewsgroup(ListSNG.SelectedItem.ToString());
				_newsGroupsViewModel.SNGSelectedBool = false;
			}
		}

		private void Btn_Remove(object sender, RoutedEventArgs e)
		{
			_newsGroupsViewModel.RemoveSelectedNewsgroup(ListSNG.SelectedItem.ToString());
			_newsGroupsViewModel.SNGSelectedBool = false;
		}

		private void Btn_Add(object sender, RoutedEventArgs e)
		{
			_newsGroupsViewModel.AddSelectedNewsgroup(ListANG.SelectedItem.ToString());
			_newsGroupsViewModel.ANGSelectedBool = false;
		}

		private void Btn_Favorit(object sender, RoutedEventArgs e)
		{
			_newsGroupsViewModel.FavoritSelectedNewsgroup(ListSNG.SelectedItem.ToString());
			_newsGroupsViewModel.SNGSelectedBool = false;
		}

		#region ListFiltering

		private void TxtChanged_Search_ANG(object sender, RoutedEventArgs e)
		{
			CollectionViewSource.GetDefaultView(ListANG.ItemsSource).Refresh();
		}

		private void TxtChanged_Search_SNG(object sender, RoutedEventArgs e)
		{
			CollectionViewSource.GetDefaultView(ListSNG.ItemsSource).Refresh();
		}

		private bool FilterANG(object item)
		{
			if (String.IsNullOrEmpty(Tbox_Search_ANG.Text))
				return true;
			else
				return ((item as string).IndexOf(Tbox_Search_ANG.Text, StringComparison.OrdinalIgnoreCase) >= 0);
		}
		private bool FilterSNG(object item)
		{
			if (String.IsNullOrEmpty(Tbox_Search_SNG.Text))
				return true;
			else
				return ((item as string).IndexOf(Tbox_Search_SNG.Text, StringComparison.OrdinalIgnoreCase) >= 0);
		}

		#endregion ListFiltering

	}
}
