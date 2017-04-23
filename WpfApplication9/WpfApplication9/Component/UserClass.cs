using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApplication9.LogicGate;

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

        public static T TryFindLogicalParent<T>(FrameworkElement current) where T : class
        {
            DependencyObject parent = current.Parent;
            if (parent == null)
                return null;

            if (parent is T)
                return parent as T;
            else
                return TryFindLogicalParent<T>((FrameworkElement)parent);
        }

        public static ArrayList FiltreSrandardComponent(Canvas canvas)
        {
            ArrayList arrayList = new ArrayList();
            try {
                for (int i = 0; i < canvas.Children.Capacity; i++)
                {
               //     MessageBox.Show(canvas.Children.Capacity.ToString() + "   " + i.ToString());
                    if (canvas.Children[i] is StandardComponent)
                    {
                        arrayList.Add(canvas.Children[i]);
                    }
                }
            }
            catch { }
            return arrayList;
        }

        /*public enum InputChangeable{
            AND,NAND,NOR,OR
        };*/

        public static bool IsInputChangeable(StandardComponent component)
        {
            if (component is AND) return true;
            else if (component is NAND) return true;
            else if (component is NOR) return true;
            else if (component is OR) return true;
            else return false;

        }
    }
}
