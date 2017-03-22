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
    /// Interaction logic for terminal.xaml
    /// </summary>
    public partial class Terminal : UserControl
    {
        public Boolean IsOutpt;//si vrai => ouput ,si false => intput;
        public ArrayList wires; //Un terminal de sortie peut être brancher à plusieurs entrés 
        public Boolean IsInversed;// 

        public Terminal()
        {
            InitializeComponent();
            IsInversed = false;
            wires = new ArrayList();
        }

        public void relier(object sender, MouseButtonEventArgs e)
        {
            Terminal terminal = UserClass.TryFindParent<Terminal>((Ellipse)sender);
            StandardComponent componentPere = UserClass.TryFindParent<StandardComponent>((Ellipse)sender);
            Canvas canvas = UserClass.TryFindParent<Canvas>((terminal));
  
            Wireclass wire = new Wireclass();
            wire.relier((Ellipse)sender);
        }

        //Recalcule de position de chaque terminal en recaluculant la pos de tout les fils
        public void recalculer()
        {
            foreach(Wireclass wire in wires)
            {
                wire.recalculer(IsOutpt);
            }
        }

        public void input_inversed()
        {
            if(IsInversed)
            {
                inverse_input.Visibility = Visibility.Visible;
            }
            else
            {
                inverse_input.Visibility = Visibility.Collapsed;
            }
        }

      
    }
}
