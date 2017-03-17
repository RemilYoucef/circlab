using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApplication9.Component;


namespace WpfApplication9.LogicGate
{
    class AND:StandardComponent
    {
        public AND(int nbrinput)
            :base(nbrinput,1, "M 17,17 v 30 h 15 a 2,2 1 0 0 0,-30 h -15","AND")
        {
            
        }

        public override void Run()
        {

            Boolean tmp = true;
         
            foreach(Terminal terminal in inputStack.Children)
            {
                if (terminal.wires.Count == 0)
                {
                   
                    tmp = false;
                }
                else
                {
                    foreach (Wireclass wire in terminal.wires)
                    {
                        if (wire.state == false)
                        {

                            tmp = false;
                        }
                    }
                }
              
            }

            foreach(Wireclass wire in this.output.wires)
            {
                wire.state = tmp;
            }
        }

        public override void redessiner(string path)
        {
            base.redessiner("M 17,17 v 30 h 15 a 2,2 1 0 0 0,-30 h -15");
        }

    }
}
