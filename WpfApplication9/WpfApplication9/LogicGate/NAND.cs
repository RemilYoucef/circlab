using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication9.Component;


namespace WpfApplication9.LogicGate
{
    class NAND : StandardComponent
    {

        public NAND(int nbrinput)
            : base(nbrinput, "M 15,17 v 30 h 15 a 2,2 1 0 0 0,-30 h -15 M 46,33.5 a 3,3 1 1 1 0.1,0.1","NAND")
        {

        }


        public override void Run()
        {

            Boolean tmp = false;

            foreach (Terminal terminal in inputStack.Children)
            {
                if (terminal.wires.Count == 0)
                {

                    tmp = true;
                }
                else
                {
                    foreach (Wireclass wire in terminal.wires)
                    {


                        if (wire.state == false)
                        {

                            tmp = true;
                        }


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
