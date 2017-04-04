using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication9.Component;
using WpfApplication9.ComplexComponent;

namespace WpfApplication9.SequentialComponent
{
    class DecompteurN : StandardComponent
    {

        private int _nbroutputs;
        private int _val;
        private bool oldClockValue;
        public DecompteurN(int N, int nbr)
            :base(1,nbr,0, "M 0,0 L 30,0 L 30,30 L 0,30 z","frequencyDivider")
        {
            _nbroutputs = nbr;
            outputs_tab=ClassConverter.ConvertToBinary(N,_nbroutputs);
            _val = N;
            oldClockValue = false;
            
        }

        public override void Run()
        {
            update_input();
            bool newClockValue = (bool)inputs_tab[0];
            if (newClockValue == true && oldClockValue == false)
            {
                int number = ClassConverter.ConvertToInt(outputs_tab);
                number--;
                if (number == -1) { number = _val; }
                outputs_tab = ClassConverter.ConvertToBinary(number,_nbroutputs);
            }
            oldClockValue = newClockValue;
            update_output();
        }
    }
}


