using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication9.Component;

namespace WpfApplication9.SequentialComponent
{
    class CirculerRegister : StandardComponent
    {
        public enum TriggerType
        {
            RisingEdge, FallingEdge, HighLevel, LowLevel
        }
        public enum Type
        {
            Right, Left
        }
        private Type _type = Type.Left;
        private int _nbroutputs;
        private TriggerType _trigger = TriggerType.RisingEdge;
        public TriggerType Trigger
        {
            get { return _trigger; }
            set { _trigger = value; }
        }
        private bool oldClockValue;
        public CirculerRegister(TriggerType trigger, int nbrinput, Type ty)
            : base(nbrinput + 3, nbrinput,0, "M 0,0 L 30,0 L 30,30 L 0,30 z", "CirculaRegister")
        {
            _nbroutputs = nbrinput;
            _trigger = trigger;
            _type = ty;

            oldClockValue = false;
            outputs_tab.Clear();
            for (int i = 0; i < _nbroutputs; i++)
            {
                outputs_tab.Add(false);
            }

        }

        public override void Run()
        {
            //inputs_tab[0]==clock
            //inouts_tab[1]==clear
            //inputs_tab[2]==load

            bool _val;
            update_input();
            bool newClockValue = (bool)inputs_tab[0];
            if ((bool)inputs_tab[1] == true) { for (int k = 0; k < _nbroutputs; k++) outputs_tab[k] = false; }
            else
            {
                if ((bool)inputs_tab[2] == true) { for (int k = 0; k < _nbroutputs; k++) outputs_tab[k] = inputs_tab[k + 3]; }
                else { 
                
                if ((_trigger == TriggerType.RisingEdge && newClockValue == true && oldClockValue == false) ||
                    (_trigger == TriggerType.FallingEdge && newClockValue == false && oldClockValue == true) ||
                    (_trigger == TriggerType.HighLevel && newClockValue == true) ||
                    (_trigger == TriggerType.LowLevel && newClockValue == false))
                
                    {
                        if (_type == Type.Left)
                        {
                            _val = (bool)outputs_tab[0];
                            for (int k = 0; k < _nbroutputs - 1; k++) { outputs_tab[k] = outputs_tab[k + 1]; }
                            outputs_tab[_nbroutputs - 1] = _val;
                        }
                        if (_type == Type.Right)
                        {
                            _val = (bool)outputs_tab[_nbroutputs - 1];
                            for (int k = _nbroutputs - 1; k > 0; k--) { outputs_tab[k] = outputs_tab[k - 1]; }
                            outputs_tab[0] = _val;
                        }
                    }
                }

            }
            oldClockValue = newClockValue;

            update_output();
        }
    }
}
