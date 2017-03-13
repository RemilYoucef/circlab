using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication9.Component;


namespace WpfApplication9.LogicGate
{
    class NOR : StandardComponent
    {

        public NOR(int nbrinput)
            : base(nbrinput, 1,"M 15,17 h 5 c 10,0 20,5 25,15 c -5,10 -15,15 -25,15 h -5 c 5,-10 5,-20 0,-30 M 46,33.5 a 3,3 1 1 1 0.1,0.1")
        {

        }

        public override void Run()
        {

            Boolean tmp = true;

            foreach (Terminal terminal in inputStack.Children)
            {
               
                foreach (Wireclass wire in terminal.wires)
                {


                    if (wire.state == true)
                    {

                        tmp = false;
                    }


                }
                

            }
            foreach (Wireclass wire in this.output.wires)
            {
                wire.state = tmp;
            }
        }
    }
}
