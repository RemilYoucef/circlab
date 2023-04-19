﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    }
}
