using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication9.Component;
using WpfApplication9.ComplexComponent;

namespace WpfApplication9.SequentialComponent
{
    class compteurN : StandardComponent
    {

        private int _nbroutputs;
        private int _val;
        private bool oldClockValue;
        public compteurN(int N,int nbr)
            :base(1,nbr, "M 0,0 L 30,0 L 30,30 L 0,30 z","frequencyDivider")
        {
            for(int k = 0; k < inputs_tab.Count; k++) { outputs_tab[k] = false; }
            _val = N ;
            oldClockValue = false;
            _nbroutputs = nbr;
            outputs_tab.Clear();
            for (int i = 0; i < nbr; i++)
            {
                outputs_tab.Add(false);
            }

        }

        public override void Run()
        {
            update_input();
            bool newClockValue = (bool)inputs_tab[0];
            if (newClockValue == true && oldClockValue == false)
            {
                int number=ClassConverter.ConvertToInt(outputs_tab);
                number=number+1;
                if (number == _val+1 ) { number = 0; }
               outputs_tab= ClassConverter.ConvertToBinary(number,_nbroutputs);
                            }
            oldClockValue = newClockValue;
            update_output();
        }
    }
}


