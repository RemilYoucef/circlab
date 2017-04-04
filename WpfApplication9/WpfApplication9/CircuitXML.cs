using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;
using WpfApplication9.Component;
using WpfApplication9.LogicGate;
using WpfApplication9.SequentialComponent;

namespace WpfApplication9
{
    static class CircuitXML
    {
        static public XElement CreateCircuitXML(Canvas canvas)
        {
            XElement circuit = new XElement("Circuit");

            XElement gates = new XElement("Gates");
            var gid = new Dictionary<StandardComponent, int>();
            int id = 1;
            foreach (object shape in canvas.Children)
            {
                if (shape is StandardComponent)
                {
                    var g = shape as StandardComponent;
                    XElement gt = new XElement("Gate");
                    gt.SetAttributeValue("Type", g.GetType().Name);
                    gt.SetAttributeValue("ID", id);
                    gt.SetAttributeValue("X", g.PosX);
                    gt.SetAttributeValue("Y", g.PosY);
                    gt.SetAttributeValue("NumInputs", g.nbrInputs());
                    if (shape is Clock)
                    {
                        gt.SetAttributeValue("HighLevelms", ((Clock)g).HighLevelms);
                        gt.SetAttributeValue("LowLevelms", ((Clock)g).LowLevelms);
                    }
                    gates.Add(gt);
                    gid.Add(g, id);
                    id++;
                }
            }

            XElement wires = new XElement("Wires");
            foreach (object shape in canvas.Children)
            {
                if (shape is StandardComponent)
                {
                    var g = shape as StandardComponent;
                    for (int i = 0; i < g.inputStack.Children.Count; i++)
                    {
                        var input = g.inputStack.Children[i] as Terminal;
                        if (input.wires.Count > 0)
                        {
                            XElement wire = new XElement("Wire",
                                    new XElement("From"), new XElement("To"));
                            Terminal sourceTerminal = ((Wireclass)input.wires[0]).source;
                            StandardComponent sourceComponent = UserClass.TryFindParent<StandardComponent>(sourceTerminal);
                            wire.Element("From").SetAttributeValue("ID", gid[sourceComponent]);
                            wire.Element("From").SetAttributeValue("Port", sourceComponent.inputStack_Copy.Children.IndexOf(sourceTerminal));
                            wire.Element("To").SetAttributeValue("ID", gid[g]);
                            wire.Element("To").SetAttributeValue("Port", i);
                            wires.Add(wire);
                        }
                    }
                }
            }

            circuit.Add(gates);
            circuit.Add(wires);
            return circuit;
        }

        public static void Save(string path, Canvas canvas)
        {
            CreateCircuitXML(canvas).Save(path);
        }

        private static StandardComponent CreateGate(XElement gate)
        {
            int numInputs = int.Parse(gate.Attribute("NumInputs").Value);
            switch (gate.Attribute("Type").Value)
            {

                case "AND":
                    return new AND(numInputs);
                case "NAND":
                    return new NAND(numInputs);
                case "NOR":
                    return new NOR(numInputs);
                case "Not":
                    return new Not();
                case "OR":
                    return new OR(numInputs);
                case "XNOR":
                    return new XNOR(numInputs);
                case "XOR":
                    return new XOR(numInputs);
                case "Input":
                    return new Input();
                case "Output":
                    return new Output();
                case "Clock":
                    int highms = int.Parse(gate.Attribute("HighLevelms").Value);
                    int lowms = int.Parse(gate.Attribute("LowLevelms").Value);
                    return new Clock(highms, lowms, MainWindow.Delay);
                case "FlipFlop":
                    // Add this Attribute and retrieve it from Xml
                    return new FlipFlop(FlipFlop.TriggerType.HighLevel);
            }
            throw new ArgumentException("unknown gate");
        }
        public static void Load(string path, ref Canvas canvas)
        {
            var circuit = XElement.Load(path);
            canvas.Children.Clear();
            foreach (XElement gate in circuit.Element("Gates").Elements())
            {
                StandardComponent shape = CreateGate(gate);
                canvas.Children.Add(shape);
                shape.SetValue(Canvas.LeftProperty, double.Parse(gate.Attribute("X").Value));
                shape.SetValue(Canvas.TopProperty, double.Parse(gate.Attribute("Y").Value));
            }
        }
    }
}
