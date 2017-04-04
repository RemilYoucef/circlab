using System;
using System.Collections;
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


namespace WpfApplication9.Component
{
    /// <summary>
    /// Interaction logic for StandardComponent.xaml
    /// </summary>
    public partial class Comment : UserControl
    {
        //Constructeur de tout les composonts
        public Comment(string text)
        {
            InitializeComponent();
            comment.Text = text;
            comment.Foreground = Brushes.Black;

        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            Canvas canvas = UserClass.TryFindParent<Canvas>(this);
            canvas.Children.Remove(this);
        }
    }
}

