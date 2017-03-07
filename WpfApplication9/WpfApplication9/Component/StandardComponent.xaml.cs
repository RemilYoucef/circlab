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
        public Path typeComponenet; //Le path pour dessiner le composant concerné (And,Or,...)
        protected ArrayList inputs_tab;
        protected ArrayList outputs_tab;

        //Constructeur de tout les composonts
        public StandardComponent(int nbrinput,int nbrOutput,string path)
        {
            inputs_tab = new ArrayList();
            outputs_tab = new ArrayList();
            InitializeComponent();
            Terminal terminal = new Terminal();//on crée un terminal 
            typeComponenet = new Path();//le nombre d'input ;
            
            for (int i = 0; i < nbrinput; i++)
            {
                terminal = new Terminal();
                terminal.IsOutpt = false;
                inputStack.Children.Add(terminal);
                if (nbrOutput!=1)
                terminal.Margin = new Thickness(0,((Math.Max(nbrinput, nbrOutput) * terminal.Height)) / (Math.Pow(2, nbrinput)) - terminal.Height/2, 0, terminal.Height / 2);
            }
     
            inputStack_Copy.Children.Remove(output);
            for (int i=0;i<nbrOutput; i++)
            {
                RotateTransform rt = new RotateTransform(180);
                terminal = new Terminal();
                terminal.Margin = new Thickness(0, ((Math.Max(nbrinput, nbrOutput) * terminal.Height)) / (Math.Pow(2, nbrOutput))-terminal.Height/2, 0,terminal.Height/2);
                
                terminal.terminal_grid.LayoutTransform = rt;
                terminal.IsOutpt = true;
                inputStack_Copy.Children.Add(terminal);
              
            }

            
            
            //output.Margin = new Thickness(15, 15, 15, 15);
            
            output.IsOutpt = true;//defini que c'est une sortie ; 
            if (nbrOutput != 0)
            {
                output = (Terminal)inputStack_Copy.Children[0];

            }//Pour dessiner le composant
            typeComponenet.Height = terminal.Height * Math.Max(nbrinput,nbrOutput);
            typeComponenet.Width = terminal.Width * 4;
            typeComponenet.Data = StreamGeometry.Parse(path);
            typeComponenet.Stretch = Stretch.Fill;
            typeComponenet.StrokeThickness = 0;
            typeComponenet.Fill = Brushes.RoyalBlue;
            typeComponenet.Margin = new Thickness(14, 0, 0, 0);
            typeComponenet.HorizontalAlignment = HorizontalAlignment.Left;
            typeComponenet.VerticalAlignment = VerticalAlignment.Top;
            grid.Height = terminal.Height * Math.Max(nbrinput, nbrOutput);
            grid.Children.Add(typeComponenet);
            
            //on ajoute le typecomponenent 
           // inputStack_Copy.Height =
                 foreach (Terminal terminal1 in inputStack_Copy.Children)
            {
                // terminal1.terminal_grid.Width = grid.Height / Math.Pow(2, Math.Max(nbrinput, nbrOutput));
              //  terminal1.BorderThickness = new Thickness(0,10,0,0);
            }

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
            StandardComponent component =UserClass.TryFindParent<StandardComponent>((((MenuItem)sender).Parent as ContextMenu).PlacementTarget);
            
            foreach(Terminal terminal in component.inputStack.Children )
            {
                //dans le cas d'output ou il ny'a aucune sortie
                    foreach (Wireclass wire in terminal.wires)
                    {
                        
                        wire.Destroy();
                       
                    }
                   terminal.wires.Clear();


            }

           
            foreach (Terminal terminal in component.inputStack_Copy.Children)
            {
                
                    foreach (Wireclass wire in terminal.wires)
                    {
                
                        wire.Destroy();
                
                    }
                
              
            }
            //MessageBox.Show(i.ToString());
            //Control component =(Control)sender;
            //StandardComponent test = UserClass.TryFindParent<StandardComponent>();
            // test.typeComponenet.Height = 100;
            //MessageBox.Show();
            canvas.Children.Remove(component);
            ///canvas.Children.Remove(UserClass.TryFindParent<StandardComponent>(text));

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
                    inputs_tab[i] = 0;
                }
                else
                {
                    foreach (Wireclass wire in terminal.wires)
                    {
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


