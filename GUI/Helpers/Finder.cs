using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace GUI.Helpers
{
    public class Finder
    {
        public static T? FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T ancestor)
                    return ancestor;

                current = VisualTreeHelper.GetParent(current);
            } while (current != null);

            return null;
        }
    }
}
