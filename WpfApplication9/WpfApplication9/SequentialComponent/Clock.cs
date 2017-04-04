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
    class Clock:StandardComponent, ISequential
    {
        private DispatcherTimer t;
        private bool _val;
        private int _highms;
        private int _lowms;

        public Clock(int lowLevlms, int highLevelms, float delay)
            : base(0,1, "M0.5,0.5 L27,0.5 L27,27.5 L0.5,27.5 z","Clock")
        {
            _val = false;
            this.typeComponenet.Fill = Brushes.Red;

            t = new DispatcherTimer();
            t.Interval = TimeSpan.FromMilliseconds(lowLevlms * Delay);
            t.Tick += timer_Tick;

            Delay = delay;
            HighLevelms = highLevelms;
            LowLevelms = lowLevlms;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            _val = !_val;
            if (_val)
            {
                this.typeComponenet.Fill = Brushes.Green;
                t.Interval = TimeSpan.FromMilliseconds(_highms * Delay);
            }
            else
            {
                this.typeComponenet.Fill = Brushes.Red;
                t.Interval = TimeSpan.FromMilliseconds(_lowms * Delay);
            }
            Run();
        }

        public float Delay { get; set; }

        public int HighLevelms
        {
            get
            {
                return _highms;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Milliseconds must be at least 0");

                _highms = value;
                
            }
        }

        public int LowLevelms
        {
            get
            {
                return _lowms;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Milliseconds must be at least 0");

                _lowms = value;
                t.Interval = TimeSpan.FromMilliseconds(_lowms);

            }
        }

        public void Stop()
        {
            t.Stop();
        }

        public void Start()
        {
            t.Start();
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
