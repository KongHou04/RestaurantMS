using DTO;
using GUI.Helpers;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI.Views
{
    /// <summary>
    /// Interaction logic for TableView.xaml
    /// </summary>
    public partial class TableView : UserControl
    {

        //TableDTO? selectedData;

        public TableView()
        {
            InitializeComponent();
        }
        //private void txtSearch_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    txtBoxSearch.Focus();
        //}

        //private void txtBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (!String.IsNullOrEmpty(txtBoxSearch.Text))
        //        txtSearch.Visibility = Visibility.Collapsed;
        //    else txtSearch.Visibility = Visibility.Visible;
        //}

        private void DataGrid_ContextMenuOpening(object sender, ContextMenuEventArgs e)
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
                dataGrid.ContextMenu.IsOpen = false;
                dataGrid.ContextMenu.Visibility = Visibility.Visible;
            }
            else
                dataGrid.ContextMenu.Visibility = Visibility.Collapsed;
        }

        // Handle Update Action
        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            TableDTO? selectedData = dataGrid.SelectedItem as TableDTO;

            if (selectedData != null)
            {
                MessageBox.Show($"Edit item ID: {selectedData.ID}, {selectedData.Name}");
            }
        }

        // Handle Delete Action
        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            TableDTO? selectedData = dataGrid.SelectedItem as TableDTO;

            if (selectedData != null)
            {
                MessageBox.Show($"Edit item ID: {selectedData.ID}, {selectedData.Name}");
            }
        }

    }
}
