using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication9.Component;


namespace WpfApplication9.LogicGate
{
    class XOR : StandardComponent
    {

        public XOR(int nbrinput)
            : base(nbrinput, "M 13,47 c 5,-10 5,-20 0,-30 M 13,17 c 5,10 5,20 0,30 M 18,17 h 7 c 10,0 20,5 25,15 c -5,10 -15,15 -25,15 h -7 c 5,-10 5,-20 0,-30")
        {

        }
    }
}

