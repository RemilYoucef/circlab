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
using WpfApplication9.LogicGate;

namespace WpfApplication9.Component
{
    /// <summary>
    /// Interaction logic for StandardComponent.xaml
    /// </summary>
    public abstract partial class StandardComponent : UserControl
    {
        public static Canvas canvas;//le canvas de l'interface 
        public Path typeComponenet; //Le path pour dessiner le composant concerné (And,Or,...)
        public String path;
       

        protected ArrayList inputs_tab;
        protected ArrayList outputs_tab;

        //Constructeur de tout les composonts
        public StandardComponent(int nbrinput,string path)
        {   
            InitializeComponent();
            this.path = path;
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

            output.IsOutpt = true;//defini que c'est une sortie ; 
          
            //Pour dessiner le composant
            this.redessiner(path);
            grid.Children.Add(typeComponenet);//on ajoute le composant dans la grid 

            inputs_tab = new ArrayList();
            outputs_tab = new ArrayList();
            outputs_tab.Add(false);
        }

        //Methode pour recalculer la position du composants, on calcule la pos de chaque terminal prenant en considération les filles liées à lui
        public void recalculer_pos()
        {
            foreach (Terminal terminal in inputStack.Children)
            {
                terminal.recalculer(); 
            }
            
            output.recalculer();
            canvas.UpdateLayout();

        }

        private void Delete(object sender, RoutedEventArgs e)
        {


            StandardComponent component =UserClass.TryFindParent<StandardComponent>((((MenuItem)sender).Parent as ContextMenu).PlacementTarget);
            
            foreach(Terminal terminal in component.inputStack.Children )
            {
                try {//dans le cas d'output ou il ny'a aucune sortie
                    foreach (Wireclass wire in terminal.wires)
                    {
                        wire.Destroy();
                    }
                }
                catch { }
            }
            foreach (Terminal terminal in component.inputStack_Copy.Children)
            {
                foreach (Wireclass wire in terminal.wires)
                {
                    wire.Destroy();
                }
            }
            //Control component =(Control)sender;
            //StandardComponent test = UserClass.TryFindParent<StandardComponent>();
            // test.typeComponenet.Height = 100;
            //MessageBox.Show();
            canvas.Children.Remove(component);
            ///canvas.Children.Remove(UserClass.TryFindParent<StandardComponent>(text));

        }

        public abstract void Run();
        
        public void AddInputs()
        {
            Terminal terminal = new Terminal();
            terminal.IsOutpt = false;
            inputStack.Children.Add(terminal);
           
            
        }

        public void RemoveInputs()
        {
            Terminal terminal=null;
            Wireclass wire=null;
            foreach(Terminal tmp in inputStack.Children)
            {
                terminal = tmp;
            }
            foreach(Wireclass tmp in terminal.wires)
            {
                wire = tmp;
            }
            if(wire!=null) wire.Destroy();
            inputStack.Children.Remove(terminal);
    
            
        }

        private void standardcomponent_MouseDown(object sender, MouseButtonEventArgs e)
        {

            MainWindow.elementSelected = this;
            MainWindow window = UserClass.TryFindParent<MainWindow>(canvas);
            if (!(sender is Input) && !(sender is Output))
            {
                window.modifieProperties();
            }

            this.typeComponenet.StrokeThickness = 2;
            this.typeComponenet.Stroke = Brushes.Black;
        }

        public int nbrInputs()
        {
            int tmp = 0;
            foreach(Terminal terminal in inputStack.Children)
            {
                tmp++;
            }
            return tmp;
        }

        public virtual void redessiner(string path)
        {
            Terminal terminal = new Terminal();
            int nbrInput;
            foreach(Terminal tmp in inputStack.Children)
            {
                terminal = tmp;
            }

            if (this.nbrInputs() == 0)
            {
                nbrInput = 1;
            }
            else nbrInput = this.nbrInputs();

            output.Margin = new Thickness(4.5, 11 * (nbrInput - 1), 4.5, 0);
            grid.Height = nbrInput * 22;
            typeComponenet.Height = terminal.Height * nbrInput;
            typeComponenet.Width = terminal.Width * 4;
           
            typeComponenet.Data = StreamGeometry.Parse(path);
            typeComponenet.Stretch = Stretch.Fill;
            typeComponenet.StrokeThickness = 0;
            typeComponenet.Fill = Brushes.RoyalBlue;
            typeComponenet.Margin = new Thickness(14, 0, 0, 0);
            typeComponenet.HorizontalAlignment = HorizontalAlignment.Left;
            typeComponenet.VerticalAlignment = VerticalAlignment.Top;
            recalculer_pos();
           
        }

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


