using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using WpfApplication9.Component;


namespace WpfApplication9.LogicGate
{
    class Input : StandardComponent
    {
    
        public Input()
            : base(0, "M0.5,0.5 L27,0.5 L27,27.5 L0.5,27.5 z","INPUT")
        {
            state = false;
            this.typeComponenet.Fill = Brushes.Red;
            typeComponenet.MouseLeftButtonUp += this.MouseClick;
        }


        private Boolean _state;
        public Boolean state
        {
            get { return _state; }
            set
            {
                if (_state != value)
                {
                    _state = value;
                    foreach(Terminal terminal in inputStack_Copy.Children)//en réalité il y'a un seul element mais pour facilter le parcours dert haka :3
                    {
                        foreach(Wireclass wire in terminal.wires)
                        {
                            wire.state = _state ;
                        }
                    }
                }
                _state = value;
            }
        }

        private void MouseClick(object sender, RoutedEventArgs e)
        {
            state = !state;
            if (state==true)
            {
                this.typeComponenet.Fill = Brushes.Green;
            }
            else
            {
                this.typeComponenet.Fill = Brushes.Red;
            }
        }

        public override void Run()
        {
            foreach(Wireclass wire in output.wires)
            {
                wire.state = this.state;
            }

        }




    }
}
