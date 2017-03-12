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
    class Output : StandardComponent
    {

        public Output()
            : base(1, "M0.5,0.5 L27,0.5 L27,27.5 L0.5,27.5 z")
        {
            state = false;
            this.typeComponenet.Fill = Brushes.Red;
            inputStack_Copy.Children.Clear();
        }


        private Boolean _state;
        public Boolean state
        {
            get { return _state; }
            set
            {
                _state = value;
                
                if (state==true)
                {
                    this.typeComponenet.Fill = Brushes.Green;
                }
                else
                {
                    this.typeComponenet.Fill = Brushes.Red;
                }
            }
        }

        public override void Run()
        {
           foreach(Terminal terminal in inputStack.Children)
            {
                foreach(Wireclass wire in terminal.wires)
                {
                    state = wire.state;
                }
            }
        }


    }
}
