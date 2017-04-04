using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication9.Component;

namespace WpfApplication9.ComplexComponent
{
    class Decoder:StandardComponent
    {
        public Decoder(int nbrinput,int nbroutput)
            // DONT FORGET TO CHANGE THE SHAPE >_< 
            :base(nbrinput,nbroutput,0, "M 17,17 v 30 h 15 a 2,2 1 0 0 0,-30 h -15","Decoder") 
        {
            Terminal terminal;
            for (int i = 0; i < nbroutput - 1; i++)
            {
                terminal = new Terminal();
                terminal.IsOutpt = true;
                inputStack.Children.Add(terminal); 
            }

        }

        public override void Run()
        {


        }
    }
}
