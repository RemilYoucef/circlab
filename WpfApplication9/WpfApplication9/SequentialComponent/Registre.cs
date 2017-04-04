using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication9.Component;


namespace WpfApplication9.SequentialComponent
{
    class Registre : StandardComponent
    {
        public enum TriggerType
        {
            RisingEdge, FallingEdge, HighLevel, LowLevel
        }

        private int _nbroutput;
        private TriggerType _trigger = TriggerType.RisingEdge;
        private bool oldClockValue;
        public Registre(TriggerType trigger,int nbrinput)
            : base(nbrinput+2, nbrinput, "M 0,0 L 30,0 L 30,30 L 0,30 z", "Register")
        {
            _nbroutput = nbrinput;
            _trigger = trigger;
            
            oldClockValue = false;
            outputs_tab.Clear();
            for (int i = 0; i < _nbroutput; i++)
            {
                outputs_tab.Add(false);
            }
        }

        public override void Run()
        {
            //inouts_tab[0]==clear
            //inputs_tab[1]==load 

            update_input();
            bool newClockValue = (bool)inputs_tab[1];
            if ((bool)inputs_tab[0] == true) { for (int k = 0; k < inputs_tab.Count - 2; k++) outputs_tab[k] = false; }
            else
            {
                
                if ((_trigger == TriggerType.RisingEdge && newClockValue == true && oldClockValue == false) ||
                    (_trigger == TriggerType.FallingEdge && newClockValue == false && oldClockValue == true) ||
                    (_trigger == TriggerType.HighLevel && newClockValue == true) ||
                    (_trigger == TriggerType.LowLevel && newClockValue == false))
                {
                    for (int i = 0; i < _nbroutput; i++)
                    { outputs_tab[i] = inputs_tab[i + 2]; }
                }
                
            }
            oldClockValue = newClockValue;
            update_output();
        }
    }

}








