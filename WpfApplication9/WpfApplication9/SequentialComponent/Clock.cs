using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;
using WpfApplication9.Component;

namespace WpfApplication9.SequentialComponent
{
    class Clock:StandardComponent
    {
        private DispatcherTimer t;
        private bool _val;
        private int _ms;

        public Clock(int milliseconds)
            : base(0, "M0.5,0.5 L27,0.5 L27,27.5 L0.5,27.5 z","Clock")
        {
            _val = false;
            this.typeComponenet.Fill = Brushes.Red;

            t = new DispatcherTimer();
            t.Interval = TimeSpan.FromMilliseconds(milliseconds);
            t.Tick += timer_Tick;
            t.Start();

            Milliseconds = milliseconds;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            _val = !_val;
            if (_val)
            {
                this.typeComponenet.Fill = Brushes.Green;
            }
            else
            {
                this.typeComponenet.Fill = Brushes.Red;
            }
            Run();
        }

        public int Milliseconds
        {
            get
            {
                return _ms;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Milliseconds must be at least 0");

                _ms = value;
                t.Interval = TimeSpan.FromMilliseconds(_ms);

            }
        }

        public override void Run()
        {
            foreach (Wireclass wire in output.wires)
            {
                wire.state = this._val;
            }

        }
    }
}
