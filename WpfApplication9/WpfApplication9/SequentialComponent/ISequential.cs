namespace WpfApplication9.SequentialComponent
{
    internal interface ISequential
    {
        float Delay { get; set; }
        void Start();
        void Stop();
    }
}