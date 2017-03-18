﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication9.Component;


namespace WpfApplication9.LogicGate
{
    class XNOR : StandardComponent
    {

        public XNOR(int nbrinput)
            : base(nbrinput,1, "M 13,47 c 5,-10 5,-20 0,-30 M 13,17 c 5,10 5,20 0,30 M 18,17 h 2 c 10,0 20,5 25,15 c -5,10 -15,15 -25,15 h -2 c 5,-10 5,-20 0,-30 M 46,33.5 a 3,3 1 1 1 0.1,0.1","XNOR")
        {

        }

        public override void Run()
        {

            Boolean tmp = true;
            int i = 0;
            foreach (Terminal terminal in inputStack.Children)
            {

                if (terminal.wires.Count == 0)
                {
                    if (i >= 1)
                    {
                        tmp = (tmp == false);
                    }
                    else
                    {
                        tmp = false;
                        i++;
                    }
                }
                else
                {
                    foreach (Wireclass wire in terminal.wires)
                    {

                        if (i >= 1)
                        {
                            tmp = (tmp == wire.state);
                        }
                        else
                        {
                            tmp = wire.state;
                            i++;
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

