using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication9.Component;

namespace WpfApplication9.LogicGate
{
    class OR : StandardComponent
    {

        public OR(int nbrinput)
            : base(nbrinput,1, "M 15,17 h 10 c 10,0 20,5 25,15 c -5,10 -15,15 -25,15 h -10 c 5,-10 5,-20 0,-30","OR")
        {

        }


        public override void Run()
        {

            update_input();
            outputs_tab.Add(false);
            outputs_tab[0] = false;
            foreach (bool tmp in inputs_tab)
            {
                if (tmp == true)
                {
                    outputs_tab[0] = true;
                }

            }
            update_output();
        }

    }
}
