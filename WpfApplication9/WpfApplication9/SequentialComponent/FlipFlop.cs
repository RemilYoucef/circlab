using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication9.Component;

namespace WpfApplication9.SequentialComponent
{
    class FlipFlop:StandardComponent
    {
        public enum TriggerType
        {
            RisingEdge, FallingEdge, HighLevel, LowLevel
        }

        private bool _val;
        private TriggerType _trigger = TriggerType.RisingEdge;
        private bool oldClockValue;
        public FlipFlop(TriggerType trigger)
            :base(2, 1,"M 0,0 L 30,0 L 30,30 L 0,30 z","FlipFlop")
        {
            _trigger = trigger;
            oldClockValue = false;
        }

        public override void Run()
        {
            update_input();
            bool newClockValue = (bool)inputs_tab[0];
            if ((_trigger == TriggerType.RisingEdge && newClockValue == true && oldClockValue == false) ||
                (_trigger == TriggerType.FallingEdge && newClockValue == false && oldClockValue == true) ||
                (_trigger == TriggerType.HighLevel && newClockValue == true) ||
                (_trigger == TriggerType.LowLevel && newClockValue == false))
            {
                _val = (bool)inputs_tab[1];
                outputs_tab[0] = _val;
            }
            else
            {
                outputs_tab[0] = _val;
            }
            oldClockValue = newClockValue;
            update_output();
        }
    }
}
