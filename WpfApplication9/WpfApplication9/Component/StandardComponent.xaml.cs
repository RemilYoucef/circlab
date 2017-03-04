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

namespace WpfApplication9.Component
{
    /// <summary>
    /// Interaction logic for StandardComponent.xaml
    /// </summary>
    public partial class StandardComponent : UserControl
    {
        public static Canvas canvas;//le canvas de l'interface 
        public Path typeComponenet;
        public StandardComponent(int nbrinput,string path)
        {

           
            InitializeComponent();
            Terminal terminal = new Terminal();//on crée un terminal 
            typeComponenet = new Path();//le nombre d'input ;
            
            for (int i = 0; i < nbrinput; i++)
            {

                terminal = new Terminal();
               
                terminal.source = false;
                inputStack.Children.Add(terminal);
            }
            if (nbrinput==0)
            {
                nbrinput = 1;
            }
            output.Margin = new Thickness(4.5, 11 * (nbrinput - 1), 4.5, 0);
            grid.Height = nbrinput * 22;

            output.source = true;//defini que c'est une sortie ; 
            typeComponenet.Height = terminal.Height * nbrinput;
            typeComponenet.Width = terminal.Width * 4;
            typeComponenet.Data = StreamGeometry.Parse(path);
            typeComponenet.Stretch = Stretch.Fill;
            typeComponenet.StrokeThickness = 0;
            typeComponenet.Fill = Brushes.RoyalBlue;
            typeComponenet.Margin = new Thickness(14, 0, 0, 0);
            typeComponenet.HorizontalAlignment = HorizontalAlignment.Left;
            typeComponenet.VerticalAlignment = VerticalAlignment.Top;
            grid.Children.Add(typeComponenet);//on ajoute le typecomponenent 
        }


        public void recalculer_pos()
        {
                foreach (Terminal terminal in inputStack.Children)
                    {
                         terminal.recalculer(); 
                    }
            output.recalculer();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            StandardComponent component = sender as StandardComponent;
            canvas.Children.Remove(component);

        }

        public virtual void Run()
        {

        }

        /* public  Path create(int nbrinput, String path)
         {
             StackPanel stackPanel = new StackPanel();
             stackPanel.Orientation = Orientation.Vertical;
             Input input = new Input();
             input.create(nbrinput);
             stackPanel.Children.Add(input);
             Path ph = new Path();
             ph.StrokeEndLineCap = PenLineCap.Square;
             ph.StrokeStartLineCap = PenLineCap.Triangle;
             ph.Data = StreamGeometry.Parse(path);
             ph.Stroke = Brushes.Black;
             ph.StrokeThickness = 2;
             ph.Fill = Brushes.White;
            // stackPanel.Children.Add(ph);
             //stackPanel.Background = Brushes.Blue;
             return ph;
         }*/

    }
}


