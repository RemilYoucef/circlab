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
            update_input();
            outputs_tab.Add(false);
            if ((bool)inputs_tab[0] != (bool)inputs_tab[1]) outputs_tab[0] = false;
            else outputs_tab[0] = true;
            update_output();
        }
    }
}

