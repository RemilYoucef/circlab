
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication9.Component;

namespace WpfApplication9.SequentialComponent
{
    class programmablRegister : StandardComponent
    {
        public enum TriggerType
        {
            RisingEdge, FallingEdge, HighLevel, LowLevel
        }

        private int _nbroutputs;
        private TriggerType _trigger = TriggerType.RisingEdge;
        private bool oldClockValue;
        public programmablRegister(TriggerType trigger, int nbrinput)
            : base(nbrinput+6, nbrinput,0, "M 0,0 L 30,0 L 30,30 L 0,30 z", "ProgrammablRegister")
        {
            _trigger = trigger;
            _nbroutputs = nbrinput;

            oldClockValue = false;
            outputs_tab.Clear();
            for (int i = 0; i < _nbroutputs; i++)
            {
                outputs_tab.Add(false);
            }
        }

        public override void Run()
        {
            //inouts_tab[0]==clear
            //inputs_tab[1]==clock 
            //inputs_tab[2]==so
            //inputs_tab[3]==s1
            //inputs_tab[4]==eg
            //inputs_tab[count-1]==ed

            update_input();
            bool newClockValue = (bool)inputs_tab[1];
            if ((bool)inputs_tab[0] == true) { for (int k = 0; k < _nbroutputs; k++) outputs_tab[k] = false; }
            else
            {
                
                if ((_trigger == TriggerType.RisingEdge && newClockValue == true && oldClockValue == false) ||
                    (_trigger == TriggerType.FallingEdge && newClockValue == false && oldClockValue == true) ||
                    (_trigger == TriggerType.HighLevel && newClockValue == true) ||
                    (_trigger == TriggerType.LowLevel && newClockValue == false))
                {
                    if ((bool)inputs_tab[2] == true && (bool)inputs_tab[3] == true)
                    {
                        for (int i = 0; i < _nbroutputs ; i++)
                        { outputs_tab[i] = inputs_tab[i + 5]; }
                    }
                    if((bool)inputs_tab[2]==true && (bool)inputs_tab[3] == false)
                    {
                        for (int i = 0; i < _nbroutputs-1; i++) { outputs_tab[i] = outputs_tab[i + 1]; }
                        outputs_tab[_nbroutputs - 1] = inputs_tab[inputs_tab.Count - 1];        
                    }
                    if ((bool)inputs_tab[2] == false && (bool)inputs_tab[3] == true)
                    {
                        for (int i = _nbroutputs - 1; i>0 ; i--) { outputs_tab[i] = outputs_tab[i - 1]; }
                        outputs_tab[0] = inputs_tab[4];
                    }
                }
                
            }
            oldClockValue = newClockValue;
            update_output();
        }
    }

}
