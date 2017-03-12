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
    public abstract partial class StandardComponent : UserControl
    {
        public static Canvas canvas;//le canvas de l'interface
        protected Path typeComponenet; //Le path pour dessiner le composant concerné (And,Or,...)
        protected ArrayList inputs_tab;
        protected ArrayList outputs_tab;

        //Constructeur de tout les composonts
        public StandardComponent(int nbrinput,string path)
        {   
            InitializeComponent();
            Terminal terminal = new Terminal();//on crée un terminal 
            typeComponenet = new Path();//le nombre d'input ;
            
            for (int i = 0; i < nbrinput; i++)
            {
                terminal = new Terminal();
                terminal.IsOutpt = false;
                inputStack.Children.Add(terminal);
            }
            if (nbrinput==0)
            {
                nbrinput = 1;
            }

            output.Margin = new Thickness(4.5, 11 * (nbrinput - 1), 4.5, 0);
            grid.Height = nbrinput * 22;
            output.IsOutpt = true;//defini que c'est une sortie ; 

            //Pour dessiner le composant
            typeComponenet.Height = terminal.Height * nbrinput;
            typeComponenet.Width = terminal.Width * 4;
            typeComponenet.Data = StreamGeometry.Parse(path); 
            typeComponenet.Stretch = Stretch.Fill;
            typeComponenet.StrokeThickness = 0;
            typeComponenet.Fill = Brushes.RoyalBlue;
            typeComponenet.Margin = new Thickness(14, 0, 0, 0);
            typeComponenet.HorizontalAlignment = HorizontalAlignment.Left;
            typeComponenet.VerticalAlignment = VerticalAlignment.Top;
            grid.Children.Add(typeComponenet);//on ajoute le composant dans la grid 

            inputs_tab = new ArrayList();
            outputs_tab = new ArrayList();
            outputs_tab.Add(false);
        }

        //Methode pour recalculer la position du composants, on calcule la pos de chaque terminal
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

        public abstract void Run();

        public void update_input()
        {
            inputs_tab.Clear();
            int i = 0;
            foreach (Terminal terminal in inputStack.Children)
            {
                if (terminal.wires.Count == 0)
                {
                    inputs_tab.Add(false);
                    inputs_tab[i] = false;
                }
                else
                {
                    foreach (Wireclass wire in terminal.wires)
                    {
                        inputs_tab.Add(false);
                        inputs_tab[i] = wire.state;
                    }
                   
                }
                i++;
            }
        }

        public void update_output()
        {
            int i = 0;
            foreach (Terminal terminal in inputStack_Copy.Children)
            {
                foreach (Wireclass wire in terminal.wires)
                {
                    wire.state =(Boolean)outputs_tab[i];
                }
                i++;
            }
        }

    }
}


