using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication9.Component;

namespace WpfApplication9.ComplexComponent
{
    class Encodeur : StandardComponent
    {
        private int _nbroutput;
        public Encodeur(int nbrinput, int nbroutput)

            : base(nbrinput, nbroutput, "M75.361126,25.361736 L171.91376,89.155414 171.91374,185.708 75.361126,249.50168 z")
        {
            Terminal terminal;
            for (int i = 0; i < nbroutput - 1; i++)
            {
                terminal = new Terminal();
                terminal.IsOutpt = true;
                inputStack_Copy.Children.Add(terminal);
            }
            _nbroutput = nbroutput;

        }

        public override void Run()
        {

            update_input();
            outputs_tab.Clear();
            // inputs_tab.Reverse();
            int val = 0;
            //System.Windows.MessageBox.Show(inputs_tab.Count.ToString());
            while ((val < inputs_tab.Count) && ((bool)inputs_tab[val] == false)) { val++; }
            if (val == inputs_tab.Count) val = 0;
            outputs_tab = ClassConverter.ConvertToBinary(val, _nbroutput);
            update_output();
        }
    }
}
