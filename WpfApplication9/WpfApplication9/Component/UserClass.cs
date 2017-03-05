using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
namespace WpfApplication9.Component
{
    public static class UserClass
    {
        
        public static T TryFindParent<T>(DependencyObject current) where T : class
        {
            DependencyObject parent = VisualTreeHelper.GetParent(current);
            if (parent == null)
                return null;

            if (parent is T)
                return parent as T;
            else
                return TryFindParent<T>(parent);
        }

        public static void Intersection(Terminal terminal)
        {
            foreach(Wireclass wire in terminal.wires)
            {
                       
            }
        }
    }
}
