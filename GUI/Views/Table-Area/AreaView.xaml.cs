using DTO;
using GUI.Helpers;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GUI.Views
{
    /// <summary>
    /// Interaction logic for TableView.xaml
    /// </summary>
    public partial class AreaView : UserControl
    {
        public AreaView()
        {
            InitializeComponent();
        }
        private void txtSearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtBoxSearch.Focus();

        }

        private void txtBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtBoxSearch.Text))
                txtSearch.Visibility = Visibility.Collapsed;
            else txtSearch.Visibility = Visibility.Visible;
        }

        private void dataGrid_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            // Get right clicked row
            DependencyObject? dpObj = e.OriginalSource as DependencyObject; /*Copied - This one is crazy*/
            if (dpObj == null)
            {
                e.Handled = true;
                return;
            }

            // Checks if user is right click on a row or not. Then hide or show menu context
            DataGridRow? selectedRow = Finder.FindAncestor<DataGridRow>(dpObj);
            if (selectedRow != null)
            {
                dataGrid.ContextMenu.Visibility = Visibility.Visible;
                dataGrid.ContextMenu.IsOpen = false;
            }
            else
                dataGrid.ContextMenu.Visibility = Visibility.Collapsed;
        }

        public string? IsValidObj(EmployeeDTO? obj)
        {
            if (obj == null) return "Doi tuong null";
            if (obj.UserName == null) return "Name trong";
            if (obj.UserName.Length == 0) return "Name trong";
            if (obj.Email == null) return "Email trong";
            return null;
        }


        //private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        //{
        //    RegionDemo? selectedData = dataGrid.SelectedItem as RegionDemo;

        //    if (selectedData != null)
        //    {
        //        MessageBox.Show($"Edit item ID: {selectedData.ID}, {selectedData.Name}");
        //    }
        //}

        //private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        //{
        //    RegionDemo? selectedData = dataGrid.SelectedItem as RegionDemo;

        //    if (selectedData != null)
        //    {
        //        MessageBox.Show($"Edit item ID: {selectedData.ID}, {selectedData.Name}");
        //    }
        //}
    }
}
