﻿using System;
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
            update_input();
            outputs_tab.Add(false);
            outputs_tab[0] = true;
            foreach(bool tmp in inputs_tab)
            {
                if(tmp == false)
                {
                    outputs_tab[0] = false;                   
                }
           
            }
            update_output();
            

        }

        public override void redessiner(string path)
        {
            base.redessiner("M 17,17 v 30 h 15 a 2,2 1 0 0 0,-30 h -15");
        }

    }
}
