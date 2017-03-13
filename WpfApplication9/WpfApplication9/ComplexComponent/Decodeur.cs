using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication9.ComplexComponent;
using WpfApplication9.Component;

namespace WpfApplication9.ComplexComponent
{
    class Decodeur : StandardComponent
    {
        private int _nbroutput;
        public Decodeur(int nbrinput, int nbroutput)
            : base(nbrinput, nbroutput, "M89.952818,1.7220322 L171.3401,84.390446 171.34011,171.05845 91.287337,251.05988 z")
        {

            _nbroutput = nbroutput;

        }

        public override void Run()
        {
            update_input();
            outputs_tab.Clear();
            for (int i = 0; i < _nbroutput; i++)
            {
                outputs_tab.Add(false);
            }

            int val = ClassConverter.ConvertToInt(inputs_tab);
            outputs_tab[val] = true;
            update_output();
        }
    }
}
