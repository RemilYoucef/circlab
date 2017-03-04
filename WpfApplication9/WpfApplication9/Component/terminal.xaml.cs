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
        public Boolean source;//si vrai => ouput ,si false => intput;
        public ArrayList wires;
        public Boolean etat;

        public Terminal()
        {
            InitializeComponent();
            wires = new ArrayList();
        }

        public void relier(object sender, MouseButtonEventArgs e)
        {
            Terminal term = Class1.TryFindParent<Terminal>((Ellipse)sender);
            StandardComponent componentPere = Class1.TryFindParent<StandardComponent>((Ellipse)sender);
            Canvas canvas = Class1.TryFindParent<Canvas>((term));
     
            
            Wireclass wi = new Wireclass();
            wi.relier((Ellipse)sender);
        }

        public void recalculer()
        {
            foreach(Wireclass wire in wires)
            {
                wire.recalculer(source);
            }
        }

      
    }
}
