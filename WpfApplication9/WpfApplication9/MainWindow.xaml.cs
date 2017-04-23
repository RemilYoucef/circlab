using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApplication9.Component;
using WpfApplication9.LogicGate;
using WpfApplication9.SequentialComponent;
using MaterialDesignThemes.Wpf;
using System.IO;
using System.Windows.Markup;

namespace WpfApplication9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string APP_TITLE = "CircLab";
        public static List<StandardComponent> elementsSelected= new List<StandardComponent>();
        private string _filename;
        public static float Delay = 1;

        public MainWindow()
        {
            InitializeComponent();
            var ph = new PaletteHelper();
            ph.ReplacePrimaryColor("deeppurple");
            ph.ReplaceAccentColor("deeppurple");
            Wireclass.myCanvas = canvas;
            desactiveProp();
            StandardComponent.canvas = canvas;
            canvas.PreviewMouseMove += this.MouseMove2;
            canvas.PreviewMouseLeftButtonUp += this.PreviewMouseLeftButtonUp2;
            sourceEllipse = null;
        }

        private object movingObject;
        private double firstXPos, firstYPos;
     //   private Wireclass wire ;
        public static Ellipse sourceEllipse;
        public static Wireclass wire;
        public static bool SelectedTerminalIsSource;

   
        private new void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // In this event, we get the current mouse position on the control to use it in the MouseMove event.
            Control img = sender as Control;
            Canvas canvas = img.Parent as Canvas;

            firstXPos = e.GetPosition(canvas).X;
            firstYPos = e.GetPosition(canvas).Y;

            movingObject = sender;
           
            
            
            
            // Put the image currently being dragged on top of the others
            int top = Canvas.GetZIndex(img);
            try
            {
                foreach (Control child in canvas.Children)
                    if (top < Canvas.GetZIndex(child))
                        top = Canvas.GetZIndex(child);
            }
            catch (Exception) { }
            Canvas.SetZIndex(img, top + 1);
            Mouse.Capture(img);
        }


        private new void PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            Control img = sender as Control;
            Canvas canvas = img.Parent as Canvas;

            movingObject = null;

            // Put the image currently being dragged on top of the others
            int top = Canvas.GetZIndex(img);
            try
            {
                foreach (Control child in canvas.Children)
                    if (top > Canvas.GetZIndex(child))
                        top = Canvas.GetZIndex(child);
            }
            catch (Exception) { }
            Canvas.SetZIndex(img, top + 1);

            Mouse.Capture(null);
        }


        public void modifieProperties()
        {
            if (elementsSelected!=null && elementsSelected.Count == 1)
            {
                   
                if (UserClass.IsInputChangeable(elementsSelected[0]))
                {
                    ClockFrequency.Visibility = Visibility.Collapsed;
                    Frequency.Visibility = Visibility.Collapsed;
                    Type.Visibility = Visibility.Collapsed;
                    Compteur.Visibility = Visibility.Collapsed;

                    if (elementsSelected[0].nbrInputs() != 8)
                        ComboBoxProperties.SelectedIndex = elementsSelected[0].nbrInputs() - 2;
                    else ComboBoxProperties.SelectedIndex = 3;

                    if(elementsSelected[0].nbrInputs()==2)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            ((CheckBox)GridCheckBox.Children[i]).IsChecked = ((Terminal)elementsSelected[0].inputStack.Children[i]).IsInversed;
                            ((CheckBox)GridCheckBox.Children[i]).Visibility = Visibility.Visible;
                        }
                        for (int i = 2; i < 8; i++)
                        {
                            ((CheckBox)GridCheckBox.Children[i]).IsChecked = false;
                            ((CheckBox)GridCheckBox.Children[i]).Visibility = Visibility.Collapsed;
                        }

                    }
                    else if(elementsSelected[0].nbrInputs()==3)
                    {
                        
                        for(int i = 0;i< 3; i++){
                            ((CheckBox)GridCheckBox.Children[i]).IsChecked= ((Terminal)elementsSelected[0].inputStack.Children[i]).IsInversed;
                            ((CheckBox)GridCheckBox.Children[i]).Visibility = Visibility.Visible;
                        }
                        for (int i = 3; i < 8; i++)
                        {
                            ((CheckBox)GridCheckBox.Children[i]).IsChecked = false;
                            ((CheckBox)GridCheckBox.Children[i]).Visibility = Visibility.Collapsed;
                        }
                       
                    }
                    else if(elementsSelected[0].nbrInputs()==4)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            ((CheckBox)GridCheckBox.Children[i]).IsChecked = ((Terminal)elementsSelected[0].inputStack.Children[i]).IsInversed;
                            ((CheckBox)GridCheckBox.Children[i]).Visibility = Visibility.Visible;
                        }
                        for (int i = 4; i < 8; i++)
                        {
                            ((CheckBox)GridCheckBox.Children[i]).IsChecked = false;
                            ((CheckBox)GridCheckBox.Children[i]).Visibility = Visibility.Collapsed;
                        }
                    }
                    else if (elementsSelected[0].nbrInputs() == 5)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            ((CheckBox)GridCheckBox.Children[i]).IsChecked = ((Terminal)elementsSelected[0].inputStack.Children[i]).IsInversed;
                            ((CheckBox)GridCheckBox.Children[i]).Visibility = Visibility.Visible;
                        }
                        for (int i = 5; i < 8; i++)
                        {
                            ((CheckBox)GridCheckBox.Children[i]).IsChecked = false;
                            ((CheckBox)GridCheckBox.Children[i]).Visibility = Visibility.Collapsed;
                        }
                    }
                    else if (elementsSelected[0].nbrInputs() == 6)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            ((CheckBox)GridCheckBox.Children[i]).IsChecked = ((Terminal)elementsSelected[0].inputStack.Children[i]).IsInversed;
                            ((CheckBox)GridCheckBox.Children[i]).Visibility = Visibility.Visible;
                        }
                        for (int i = 6; i < 8; i++)
                        {
                            ((CheckBox)GridCheckBox.Children[i]).IsChecked = false;
                            ((CheckBox)GridCheckBox.Children[i]).Visibility = Visibility.Collapsed;
                        }
                    }
                    else if (elementsSelected[0].nbrInputs() == 7)
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            ((CheckBox)GridCheckBox.Children[i]).IsChecked = ((Terminal)elementsSelected[0].inputStack.Children[i]).IsInversed;
                            ((CheckBox)GridCheckBox.Children[i]).Visibility = Visibility.Visible;
                        }
                       
                        ((CheckBox)GridCheckBox.Children[7]).IsChecked = false;
                        ((CheckBox)GridCheckBox.Children[7]).Visibility = Visibility.Collapsed;
                        
                    }
                    else if (elementsSelected[0].nbrInputs() == 8)
                    {
                        for (int i = 0; i <8 ; i++)
                        {
                            ((CheckBox)GridCheckBox.Children[i]).IsChecked = ((Terminal)elementsSelected[0].inputStack.Children[i]).IsInversed;
                            ((CheckBox)GridCheckBox.Children[i]).Visibility = Visibility.Visible;
                        }
                    }
                    elementsSelected[0].recalculer_pos();
                }
                else if (elementsSelected[0] is SequentialComponent.Clock)
                {
                    Type.Visibility = Visibility.Collapsed;
                    Frequency.Text = ((SequentialComponent.Clock)elementsSelected[0]).Delay.ToString();
                    ClockFrequency.Visibility = Visibility.Visible;
                    Frequency.Visibility = Visibility.Visible;
                               
                }
                else if(UserClass.IsFrontChangeable(elementsSelected[0]) && UserClass.IsNiveauChangeable(elementsSelected[0]))
                {
                    NiveauBas.Visibility = Visibility.Visible;
                    NiveauHaut.Visibility = Visibility.Visible;
                    Type.Visibility = Visibility.Visible;

                    if (elementsSelected[0] is SequentialComponent.FlipFlop)
                    {
                        comboBoxtype(((SequentialComponent.FlipFlop)elementsSelected[0]).Trigger.ToString());
                    }
                    else if((elementsSelected[0] is SequentialComponent.Registre))
                    {
                        comboBoxtype(((SequentialComponent.Registre)elementsSelected[0]).Trigger.ToString());
                    }
                    else if ((elementsSelected[0] is SequentialComponent.CirculerRegister))
                    {
                        comboBoxtype(((SequentialComponent.CirculerRegister)elementsSelected[0]).Trigger.ToString());
                    }
                    else if ((elementsSelected[0] is SequentialComponent.programmablRegister))
                    {
                        comboBoxtype(((SequentialComponent.programmablRegister)elementsSelected[0]).Trigger.ToString());
                    }
                    Type.Visibility = Visibility.Visible;
                }
                else if (UserClass.IsFrontChangeable(elementsSelected[0]))
                {
                    Type.Visibility = Visibility.Visible;
                    Compteur.Visibility = Visibility.Collapsed;
                    comboBoxtype(((SequentialComponent.JKLatch)elementsSelected[0]).Trigger.ToString());
                    NiveauBas.Visibility = Visibility.Collapsed;
                    NiveauHaut.Visibility = Visibility.Collapsed;
                }
                else if (UserClass.IsCompteur(elementsSelected[0]))
                {
                    Compteur.Visibility = Visibility.Visible;
                    if(elementsSelected[0] is SequentialComponent.compteurN)
                    {
                        ComboBoxCompteur.SelectedIndex = ((SequentialComponent.compteurN)elementsSelected[0]).Val - 2;
                        TextCompteur.Text = "Le nombre max";
                    }
                    else if (elementsSelected[0] is SequentialComponent.CompteurModN)
                    {
                        ComboBoxCompteur.SelectedIndex = ((SequentialComponent.CompteurModN)elementsSelected[0]).Val - 2;
                       
                        TextCompteur.Text = "Modulo";
                    }
                    else if (elementsSelected[0] is SequentialComponent.DecompteurN)
                    {
                        ComboBoxCompteur.SelectedIndex = ((SequentialComponent.DecompteurN)elementsSelected[0]).Val - 2;
                        TextCompteur.Text = "Le nombre max";
                    }
                    else if (elementsSelected[0] is SequentialComponent.DecompteurModN)
                    {
                        ComboBoxCompteur.SelectedIndex = ((SequentialComponent.DecompteurModN)elementsSelected[0]).Val - 2;
                        TextCompteur.Text = "Modulo";
                    }
                }
            }
          
            
        }
        //RisingEdge, FallingEdge, HighLevel, LowLevel
        private void comboBoxtype(string type)
        {
            if (type == "RisingEdge") ComboBoxFrontNiveau.SelectedIndex = 0;
            else if(type== "FallingEdge") ComboBoxFrontNiveau.SelectedIndex = 1;
            else if (type == "HighLevel") ComboBoxFrontNiveau.SelectedIndex = 2;
            else if (type == "LowLevel") ComboBoxFrontNiveau.SelectedIndex = 3;
        }
        private void addAND(object sender, RoutedEventArgs e)
        {
            //canvas.Children.Clear();
            AND img = new AND(2);
            canvas.Children.Add(img);
            img.AllowDrop = true;
            img.PreviewMouseLeftButtonDown += this.MouseLeftButtonDown;
            img.PreviewMouseMove += this.MouseMove;
            img.PreviewMouseLeftButtonUp += this.PreviewMouseLeftButtonUp;
        }

        private void addOR(object sender, RoutedEventArgs e)
        {
            OR img = new OR(2);
            canvas.Children.Add(img);
            img.AllowDrop = true;
            img.PreviewMouseLeftButtonDown += this.MouseLeftButtonDown;
            img.PreviewMouseMove += this.MouseMove;
            img.PreviewMouseLeftButtonUp += this.PreviewMouseLeftButtonUp;
        }

        private void addXOR(object sender, RoutedEventArgs e)
        {
            XOR img = new XOR(2);
            canvas.Children.Add(img);
            img.AllowDrop = true;
            img.PreviewMouseLeftButtonDown += this.MouseLeftButtonDown;
            img.PreviewMouseMove += this.MouseMove;
            img.PreviewMouseLeftButtonUp += this.PreviewMouseLeftButtonUp;

        }

        private void addNAND(object sender, RoutedEventArgs e)
        {
            NAND img = new NAND(2);
            canvas.Children.Add(img);
            img.AllowDrop = true;
            img.PreviewMouseLeftButtonDown += this.MouseLeftButtonDown;
            img.PreviewMouseMove += this.MouseMove;
            img.PreviewMouseLeftButtonUp += this.PreviewMouseLeftButtonUp;
        }
        private void add7segments(object sender, RoutedEventArgs e)
        {
            SeptSegmentsClass img = new SeptSegmentsClass();
            canvas.Children.Add(img);
            img.AllowDrop = true;
            img.PreviewMouseLeftButtonDown += this.MouseLeftButtonDown;
            img.PreviewMouseMove += this.MouseMove;
            img.PreviewMouseLeftButtonUp += this.PreviewMouseLeftButtonUp;
        }

        private void addNOR(object sender, RoutedEventArgs e)
        {
            NOR img = new NOR(2);
            canvas.Children.Add(img);
            img.AllowDrop = true;
            img.PreviewMouseLeftButtonDown += this.MouseLeftButtonDown;
            img.PreviewMouseMove += this.MouseMove;
            img.PreviewMouseLeftButtonUp += this.PreviewMouseLeftButtonUp;

        }


      
        
        double differnceX;//Calculer la difference de deplacement des absices
        double differenceY;//meme chose :3

        private new void MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && sender == movingObject)
            {
                Control img = sender as Control;
                Canvas canvas = img.Parent as Canvas;
                differnceX = Mouse.GetPosition(canvas).X - firstXPos;
                differenceY = Mouse.GetPosition(canvas).Y - firstYPos;
                firstXPos = Mouse.GetPosition(canvas).X;
                firstYPos = Mouse.GetPosition(canvas).Y;






                foreach (StandardComponent component in elementsSelected)
                {



                    double newLeft = differnceX + ((StandardComponent)component).PosX - canvas.Margin.Left;
                    ((StandardComponent)component).PosX = differnceX + ((StandardComponent)component).PosX;
                    // newLeft inside canvas right-border?
                    if (newLeft > canvas.Margin.Left + canvas.ActualWidth - img.ActualWidth)
                        newLeft = canvas.Margin.Left + canvas.ActualWidth - img.ActualWidth;
                    // newLeft inside canvas left-border?
                    else if (newLeft < canvas.Margin.Left)
                        newLeft = canvas.Margin.Left;
                    component.SetValue(Canvas.LeftProperty, newLeft);

                    double newTop = differenceY + ((StandardComponent)component).PosY - canvas.Margin.Top;
                    ((StandardComponent)component).PosY = differenceY + ((StandardComponent)component).PosY;
                    // newTop inside canvas bottom-border?
                    if (newTop > canvas.Margin.Top + canvas.ActualHeight - img.ActualHeight)
                        newTop = canvas.Margin.Top + canvas.ActualHeight - img.ActualHeight;
                    // newTop inside canvas top-border?
                    else if (newTop < canvas.Margin.Top)
                        newTop = canvas.Margin.Top;
                    component.SetValue(Canvas.TopProperty, newTop);
                    component.recalculer_pos();
                    try
                    {
                        StandardComponent component1 = sender as StandardComponent;
                        component1.recalculer_pos();
                    }
                    catch { };
                }

            }
        }


        private void addXNOR(object sender, RoutedEventArgs e)
        {
            XNOR img = new XNOR(2);
            canvas.Children.Add(img);
            img.AllowDrop = true;
            img.PreviewMouseLeftButtonDown += this.MouseLeftButtonDown;
            img.PreviewMouseMove += this.MouseMove;
            img.PreviewMouseLeftButtonUp += this.PreviewMouseLeftButtonUp;

        }

        private void addInput(object sender, RoutedEventArgs e)
        {
            Input img = new Input();
            canvas.Children.Add(img);
            img.AllowDrop = true;
            img.PreviewMouseLeftButtonDown += this.MouseLeftButtonDown;
            img.PreviewMouseMove += this.MouseMove;
            img.PreviewMouseLeftButtonUp += this.PreviewMouseLeftButtonUp;
            

        }

        private void addOutput(object sender, RoutedEventArgs e)
        {
            Output img = new Output();
            canvas.Children.Add(img);
            img.AllowDrop = true;
            img.PreviewMouseLeftButtonDown += this.MouseLeftButtonDown;
            img.PreviewMouseMove += this.MouseMove;
            img.PreviewMouseLeftButtonUp += this.PreviewMouseLeftButtonUp;


        }

        private void addClock(object sender, RoutedEventArgs e)
        {
            var img = new SequentialComponent.Clock(500,1000, (float)delay.Value);
            canvas.Children.Add(img);
            img.AllowDrop = true;
            img.PreviewMouseLeftButtonDown += this.MouseLeftButtonDown;
            img.PreviewMouseMove += this.MouseMove;
            img.PreviewMouseLeftButtonUp += this.PreviewMouseLeftButtonUp;


        }

        private void addFlipFlop(object sender, RoutedEventArgs e)
        {
            FlipFlop img = new FlipFlop(FlipFlop.TriggerType.HighLevel );
            canvas.Children.Add(img);
            img.AllowDrop = true;
            img.PreviewMouseLeftButtonDown += this.MouseLeftButtonDown;
            img.PreviewMouseMove += this.MouseMove;
            img.PreviewMouseLeftButtonUp += this.PreviewMouseLeftButtonUp;


        }

        private void addST(object sender, RoutedEventArgs e)
        {
            SynchToogle img = new SynchToogle();
            canvas.Children.Add(img);
            img.AllowDrop = true;
            img.PreviewMouseLeftButtonDown += this.MouseLeftButtonDown;
            img.PreviewMouseMove += this.MouseMove;
            img.PreviewMouseLeftButtonUp += this.PreviewMouseLeftButtonUp;


        }
        private void addT(object sender, RoutedEventArgs e)
        {
            AsynchToogle img = new AsynchToogle();
            canvas.Children.Add(img);
            img.AllowDrop = true;
            img.PreviewMouseLeftButtonDown += this.MouseLeftButtonDown;
            img.PreviewMouseMove += this.MouseMove;
            img.PreviewMouseLeftButtonUp += this.PreviewMouseLeftButtonUp;


        }
        private void addRS(object sender, RoutedEventArgs e)
        {
            RSLatche img = new RSLatche();
            canvas.Children.Add(img);
            img.AllowDrop = true;
            img.PreviewMouseLeftButtonDown += this.MouseLeftButtonDown;
            img.PreviewMouseMove += this.MouseMove;
            img.PreviewMouseLeftButtonUp += this.PreviewMouseLeftButtonUp;


        }
        private void addRST(object sender, RoutedEventArgs e)
        {
            RSHLatche img = new RSHLatche();
            canvas.Children.Add(img);
            img.AllowDrop = true;
            img.PreviewMouseLeftButtonDown += this.MouseLeftButtonDown;
            img.PreviewMouseMove += this.MouseMove;
            img.PreviewMouseLeftButtonUp += this.PreviewMouseLeftButtonUp;


        }
        private void addJK(object sender, RoutedEventArgs e)
        {
            JKLatch img = new JKLatch(JKLatch.TriggerType.RisingEdge);
            canvas.Children.Add(img);
            img.AllowDrop = true;
            img.PreviewMouseLeftButtonDown += this.MouseLeftButtonDown;
            img.PreviewMouseMove += this.MouseMove;
            img.PreviewMouseLeftButtonUp += this.PreviewMouseLeftButtonUp;


        }
        private void addRegister(object sender, RoutedEventArgs e)
        {
            Registre img = new Registre(Registre.TriggerType.RisingEdge, 4);
            canvas.Children.Add(img);
            img.AllowDrop = true;
            img.PreviewMouseLeftButtonDown += this.MouseLeftButtonDown;
            img.PreviewMouseMove += this.MouseMove;
            img.PreviewMouseLeftButtonUp += this.PreviewMouseLeftButtonUp;


        }
        private void addPRegister(object sender, RoutedEventArgs e)
        {
            programmablRegister img = new programmablRegister(programmablRegister.TriggerType.RisingEdge, 3);
            canvas.Children.Add(img);
            img.AllowDrop = true;
            img.PreviewMouseLeftButtonDown += this.MouseLeftButtonDown;
            img.PreviewMouseMove += this.MouseMove;
            img.PreviewMouseLeftButtonUp += this.PreviewMouseLeftButtonUp;


        }

        private void addCregister(object sender, RoutedEventArgs e)
        {
            CirculerRegister img = new CirculerRegister(CirculerRegister.TriggerType.RisingEdge, 4, CirculerRegister.Type.Left);
            canvas.Children.Add(img);
            img.AllowDrop = true;
            img.PreviewMouseLeftButtonDown += this.MouseLeftButtonDown;
            img.PreviewMouseMove += this.MouseMove;
            img.PreviewMouseLeftButtonUp += this.PreviewMouseLeftButtonUp;


        }
        private void addFD(object sender, RoutedEventArgs e)
        {
            FrequencyDevider img = new FrequencyDevider();
            canvas.Children.Add(img);
            img.AllowDrop = true;
            img.PreviewMouseLeftButtonDown += this.MouseLeftButtonDown;
            img.PreviewMouseMove += this.MouseMove;
            img.PreviewMouseLeftButtonUp += this.PreviewMouseLeftButtonUp;


        }

        private void addCounterN(object sender, RoutedEventArgs e)
        {
            compteurN img = new compteurN(6, 3);
            canvas.Children.Add(img);
            img.AllowDrop = true;
            img.PreviewMouseLeftButtonDown += this.MouseLeftButtonDown;
            img.PreviewMouseMove += this.MouseMove;
            img.PreviewMouseLeftButtonUp += this.PreviewMouseLeftButtonUp;


        }
        private void addCMN(object sender, RoutedEventArgs e)
        {
            CompteurModN img = new CompteurModN(6, 3);
            canvas.Children.Add(img);
            img.AllowDrop = true;
            img.PreviewMouseLeftButtonDown += this.MouseLeftButtonDown;
            img.PreviewMouseMove += this.MouseMove;
            img.PreviewMouseLeftButtonUp += this.PreviewMouseLeftButtonUp;


        }
        private void addCDN(object sender, RoutedEventArgs e)
        {
            DecompteurN img = new DecompteurN(6, 3);
            canvas.Children.Add(img);
            img.AllowDrop = true;
            img.PreviewMouseLeftButtonDown += this.MouseLeftButtonDown;
            img.PreviewMouseMove += this.MouseMove;
            img.PreviewMouseLeftButtonUp += this.PreviewMouseLeftButtonUp;


        }
        private void addCDMN(object sender, RoutedEventArgs e)
        {
            DecompteurModN img = new DecompteurModN(6, 3);
            canvas.Children.Add(img);
            img.AllowDrop = true;
            img.PreviewMouseLeftButtonDown += this.MouseLeftButtonDown;
            img.PreviewMouseMove += this.MouseMove;
            img.PreviewMouseLeftButtonUp += this.PreviewMouseLeftButtonUp;


        }

        private void addComment(object sender, RoutedEventArgs e)
        {
            Comment img = new Comment("Label");
            canvas.Children.Add(img);
            img.AllowDrop = true;
            img.PreviewMouseLeftButtonDown += this.MouseLeftButtonDown;
            img.PreviewMouseMove += this.MouseMove;
            img.PreviewMouseLeftButtonUp += this.PreviewMouseLeftButtonUp;


        }


        private void addNot(object sender, RoutedEventArgs e)
        {
            Not img = new Not();
            canvas.Children.Add(img);
            img.AllowDrop = true;
            img.PreviewMouseLeftButtonDown += this.MouseLeftButtonDown;
            img.PreviewMouseMove += this.MouseMove;
            img.PreviewMouseLeftButtonUp += this.PreviewMouseLeftButtonUp;


        }

     /*   private void annuler(object sender, MouseButtonEventArgs e)
        {
            Wireclass.selected = false;
        }*/

      

        private void closeWindow(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void RightDrawerHostOpen(object sender, RoutedEventArgs e)
        {
            drawerHost.IsRightDrawerOpen = true;
            drawerHost.IsLeftDrawerOpen = false;
        }

        private void ComboBoxProperties_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(elementsSelected!=null)
            {
                if (elementsSelected.Count != 0)
                {
                    if (UserClass.IsInputChangeable(elementsSelected[0]))
                    {
                        int selecteVal = ComboBoxProperties.SelectedIndex + 2;
                        if (ComboBoxProperties.SelectedIndex == 3) selecteVal = 8;

                        if (selecteVal != elementsSelected[0].nbrInputs())
                        {
                            while (selecteVal > elementsSelected[0].nbrInputs())
                            {
                                elementsSelected[0].AddInputs();
                            }
                            while (selecteVal < elementsSelected[0].nbrInputs())
                            {
                                elementsSelected[0].RemoveInputs();
                            }

                            elementsSelected[0].redessiner(elementsSelected[0].path);
                            canvas.UpdateLayout();
                            elementsSelected[0].Run();
                        }
                    }
                    modifieProperties();

                }
            }
            
        }

        public void desactiveProp()
        {
            NbrEntreText.Visibility = Visibility.Collapsed;
            ComboBoxProperties.Visibility=Visibility.Collapsed;
            GridCheckBox.Visibility = Visibility.Collapsed;
            ClockFrequency.Visibility = Visibility.Collapsed;
            Frequency.Visibility = Visibility.Collapsed;
            Type.Visibility = Visibility.Collapsed;
            Compteur.Visibility = Visibility.Collapsed;
        }

        public void activeProp()
        {
            NbrEntreText.Visibility = Visibility.Visible;
            ComboBoxProperties.Visibility = Visibility.Visible ;
            GridCheckBox.Visibility = Visibility.Visible;
        }


        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Canvas && elementsSelected!=null)
            {
                foreach(StandardComponent elementSelected in elementsSelected)
                {
                    elementSelected.typeComponenet.Stroke = Brushes.RoyalBlue;
                }
                elementsSelected.Clear();
                
            }
            if (elementsSelected.Count() > 1 || elementsSelected.Count()==0) desactiveProp();
        }

               
        private void checkBox2_Click(object sender, RoutedEventArgs e)
        {
            Terminal terminal = (Terminal)elementsSelected[0].inputStack.Children[1];
            terminal.IsInversed = checkBox2.IsChecked.Value;
            terminal.input_inversed();
            elementsSelected[0].Run();
        }

        private void checkBox3_Click(object sender, RoutedEventArgs e)
        {
           Terminal terminal = (Terminal)elementsSelected[0].inputStack.Children[2];
            terminal.IsInversed = checkBox3.IsChecked.Value;
            terminal.input_inversed();
            elementsSelected[0].Run();
        }

        private void checkBox4_Click(object sender, RoutedEventArgs e)
        {
            Terminal terminal = (Terminal)elementsSelected[0].inputStack.Children[3];
            terminal.IsInversed = checkBox4.IsChecked.Value;
            terminal.input_inversed();
            elementsSelected[0].Run();
        }

        private void checkBox5_Click(object sender, RoutedEventArgs e)
        {
            Terminal terminal = (Terminal)elementsSelected[0].inputStack.Children[4];
            terminal.IsInversed = checkBox5.IsChecked.Value;
            terminal.input_inversed();
            elementsSelected[0].Run();
        }

        private void checkBox6_Click(object sender, RoutedEventArgs e)
        {
            Terminal terminal = (Terminal)elementsSelected[0].inputStack.Children[5];
            terminal.IsInversed = checkBox6.IsChecked.Value;
            terminal.input_inversed();
            elementsSelected[0].Run();
        }

        private void checkBox7_Click(object sender, RoutedEventArgs e)
        {
            Terminal terminal = (Terminal)elementsSelected[0].inputStack.Children[6];
            terminal.IsInversed = checkBox7.IsChecked.Value;
            terminal.input_inversed();
            elementsSelected[0].Run();
        }

        private void checkBox8_Click(object sender, RoutedEventArgs e)
        {
            Terminal terminal = (Terminal)elementsSelected[0].inputStack.Children[7];
            terminal.IsInversed = checkBox8.IsChecked.Value;
            terminal.input_inversed();
            elementsSelected[0].Run();
        }

        private void checkBox1_Click(object sender, RoutedEventArgs e)
        {
            Terminal terminal = (Terminal)elementsSelected[0].inputStack.Children[0];
            terminal.IsInversed = checkBox1.IsChecked.Value;
            terminal.input_inversed();
            elementsSelected[0].Run();
        }

        

        private void BottomDrawerHostOpen(object sender, RoutedEventArgs e)
        {
            drawerHost.IsBottomDrawerOpen = true;
            //MessageBox.Show("hi");
        }



        private  void PreviewMouseLeftButtonUp2(object sender, MouseButtonEventArgs e)
        {

            if (sourceEllipse != null)
            {

                Wireclass.selection1 = sourceEllipse;
                // (int)canvas.Children.Capacity;
                //MessageBox.Show(i.ToString);


                ArrayList arraylist = UserClass.FiltreSrandardComponent(canvas);

               

                foreach(StandardComponent standardcomponent in arraylist)
                {

                
                    foreach (Terminal terminal in standardcomponent.inputStack.Children)
                    { 
                      
                        
                        if (Math.Abs(terminal.elSelector.TransformToAncestor(canvas).Transform(new Point(0, 0)).X - e.GetPosition(canvas).X) < 20 && Math.Abs(terminal.elSelector.TransformToAncestor(canvas).Transform(new Point(0, 0)).Y - e.GetPosition(canvas).Y) < 20)
                        {
                            //MessageBox.Show("911");
                            Wireclass.selection2 = terminal.elSelector;
                        }
                    }
                    foreach (Terminal terminal in standardcomponent.OutputStack.Children)
                    {


                        if (Math.Abs(terminal.elSelector.TransformToAncestor(canvas).Transform(new Point(0, 0)).X - e.GetPosition(canvas).X) < 20 && Math.Abs(terminal.elSelector.TransformToAncestor(canvas).Transform(new Point(0, 0)).Y - e.GetPosition(canvas).Y) < 20)
                        {
                            //MessageBox.Show("911");
                            Wireclass.selection2 = terminal.elSelector;
                        }
                    }

                }
                
                 wire.Destroy();
                if (Wireclass.selection2!=null)
                {
                    wire.relier();
                }
                
            }

          
            Mouse.Capture(null);
            sourceEllipse = null;
        }

        private void SimulationStart_ButtonClick(object sender, RoutedEventArgs e)
        {
            if(simIcon.Kind == MaterialDesignThemes.Wpf.PackIconKind.Play)
            {
                simIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Stop;
                foreach (object component in canvas.Children)
                {
                    if (component is ISequential)
                    {
                        var tmp = component as ISequential;
                        tmp.Start();
                    }
                }
            }
            else
            {
                simIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Play;
                foreach (object component in canvas.Children)
                {
                    if (component is ISequential)
                    {
                        var tmp = component as ISequential;
                        tmp.Stop();
                    }
                }
            }
        }

        private void delay_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (canvas != null) {
                foreach (object component in canvas.Children)
                {
                    if (component is ISequential)
                    {
                        var tmp = component as ISequential;
                        tmp.Delay = (float)delay.Value;
                    }
                }
                MainWindow.Delay = (float)delay.Value;
            }
            
        }

        private void ChangeTheme(object sender, SelectionChangedEventArgs e)
        {
            var cb = sender as ComboBox;
            var ph = new PaletteHelper();
            switch (cb.SelectedIndex)
            {
                case 0:
                    {
                        ph.ReplacePrimaryColor("deeppurple");
                        ph.ReplaceAccentColor("deeppurple");
                    } break;
                case 1:
                    {
                        ph.ReplacePrimaryColor("indigo");
                        ph.ReplaceAccentColor("indigo");
                    }break;
                case 2:
                    {
                        ph.ReplacePrimaryColor("deeporange");
                        ph.ReplaceAccentColor("deeporange");
                    }break;
                case 3:
                    {
                        ph.ReplacePrimaryColor("red");
                        ph.ReplaceAccentColor("red");
                    }
                    break;
            }
        }

        private void btnSaveAs_click(object sender, MouseButtonEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".clc";
            dlg.Filter = "CircLab Circuit (.clc)|*.clc";
            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                try
                {
                    CircuitXML.Save(dlg.FileName, canvas);
                    _filename = dlg.FileName;
                    btnSave.IsEnabled = true;
                    UpdateTitle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to save circuit as requested: " + ex.ToString());
                }
            }
        }

        private void MouseMove2(object sender, MouseEventArgs e)
        {
            
            if (e.LeftButton == MouseButtonState.Pressed && sourceEllipse!=null)
            {
              //  wire = new Wireclass();
                Wireclass.btn2Point.X = e.GetPosition(canvas).X;
                Wireclass.btn2Point.Y = e.GetPosition(canvas).Y;
                Wireclass.selection1 = sourceEllipse;
                Wireclass.btn1Point = sourceEllipse.TransformToAncestor(canvas).Transform(new Point(0, 0));
                wire.Dessiner();
         

                //On dessine 
            }
        }

        private void btnOpen_click(object sender, MouseButtonEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".clc";
            dlg.Filter = "CircLab Circuit (.clc)|*.clc";
            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                foreach (Window w in Application.Current.Windows)
                {
                    if (w != this)
                        w.Close();
                }
                try
                {
                    CircuitXML.Load(dlg.FileName, ref canvas);
                    
                    btnSave.IsEnabled = true;
                    _filename = dlg.FileName;
                    UpdateTitle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to load circuit as requested: " + ex.ToString());
                }

            }
        }

        private void Frequency_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                ((SequentialComponent.Clock)elementsSelected[0]).Delay = float.Parse(Frequency.Text);
            }
            catch
            {

            }
        }
        private void addChrono(object sender, RoutedEventArgs e)
        {
            Chronogramme img = new Chronogramme(2);
            canvas.Children.Add(img);
            img.AllowDrop = true;
            img.PreviewMouseLeftButtonDown += this.MouseLeftButtonDown;
            img.PreviewMouseMove += this.MouseMove;
            img.PreviewMouseLeftButtonUp += this.PreviewMouseLeftButtonUp;


        private void ComboBoxFrontNiveau_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (ComboBoxFrontNiveau.SelectedIndex == 0) //Front montant 
                {
                    if (elementsSelected[0] is SequentialComponent.FlipFlop)
                    {
                        ((SequentialComponent.FlipFlop)elementsSelected[0]).Trigger = FlipFlop.TriggerType.RisingEdge;
                    }
                    else if ((elementsSelected[0] is SequentialComponent.Registre))
                    {
                        ((SequentialComponent.Registre)elementsSelected[0]).Trigger = Registre.TriggerType.RisingEdge;
                    }
                    else if ((elementsSelected[0] is SequentialComponent.CirculerRegister))
                    {
                        ((SequentialComponent.CirculerRegister)elementsSelected[0]).Trigger = CirculerRegister.TriggerType.RisingEdge; ;
                    }
                    else if ((elementsSelected[0] is SequentialComponent.programmablRegister))
                    {
                        ((SequentialComponent.programmablRegister)elementsSelected[0]).Trigger = programmablRegister.TriggerType.RisingEdge;
                    }
                    else if ((elementsSelected[0] is SequentialComponent.JKLatch))
                    {
                        ((SequentialComponent.JKLatch)elementsSelected[0]).Trigger = JKLatch.TriggerType.RisingEdge;
                    }
                }
                else if (ComboBoxFrontNiveau.SelectedIndex == 1) //front descandant
                {
                    if (elementsSelected[0] is SequentialComponent.FlipFlop)
                    {
                        ((SequentialComponent.FlipFlop)elementsSelected[0]).Trigger = FlipFlop.TriggerType.FallingEdge;
                    }
                    else if ((elementsSelected[0] is SequentialComponent.Registre))
                    {
                        ((SequentialComponent.Registre)elementsSelected[0]).Trigger = Registre.TriggerType.FallingEdge;
                    }
                    else if ((elementsSelected[0] is SequentialComponent.CirculerRegister))
                    {
                        ((SequentialComponent.CirculerRegister)elementsSelected[0]).Trigger = CirculerRegister.TriggerType.FallingEdge;
                    }
                    else if ((elementsSelected[0] is SequentialComponent.programmablRegister))
                    {
                        ((SequentialComponent.programmablRegister)elementsSelected[0]).Trigger = programmablRegister.TriggerType.FallingEdge;
                    }
                    else if ((elementsSelected[0] is SequentialComponent.JKLatch))
                    {
                        ((SequentialComponent.JKLatch)elementsSelected[0]).Trigger = JKLatch.TriggerType.FallingEdge;
                    }
                }
                else if (ComboBoxFrontNiveau.SelectedIndex == 2) //Niveau haut
                {
                    if (elementsSelected[0] is SequentialComponent.FlipFlop)
                    {
                        ((SequentialComponent.FlipFlop)elementsSelected[0]).Trigger = FlipFlop.TriggerType.HighLevel;
                    }
                    else if ((elementsSelected[0] is SequentialComponent.Registre))
                    {
                        ((SequentialComponent.Registre)elementsSelected[0]).Trigger = Registre.TriggerType.HighLevel;
                    }
                    else if ((elementsSelected[0] is SequentialComponent.CirculerRegister))
                    {
                        ((SequentialComponent.CirculerRegister)elementsSelected[0]).Trigger = CirculerRegister.TriggerType.HighLevel;
                    }
                    else if ((elementsSelected[0] is SequentialComponent.programmablRegister))
                    {
                        ((SequentialComponent.programmablRegister)elementsSelected[0]).Trigger = programmablRegister.TriggerType.HighLevel;
                    }

                }
                else
                {
                    if (elementsSelected[0] is SequentialComponent.FlipFlop)
                    {
                        ((SequentialComponent.FlipFlop)elementsSelected[0]).Trigger = FlipFlop.TriggerType.LowLevel;
                    }
                    else if ((elementsSelected[0] is SequentialComponent.Registre))
                    {
                        ((SequentialComponent.Registre)elementsSelected[0]).Trigger = Registre.TriggerType.LowLevel;
                    }
                    else if ((elementsSelected[0] is SequentialComponent.CirculerRegister))
                    {
                        ((SequentialComponent.CirculerRegister)elementsSelected[0]).Trigger = CirculerRegister.TriggerType.LowLevel;
                    }
                    else if ((elementsSelected[0] is SequentialComponent.programmablRegister))
                    {
                        ((SequentialComponent.programmablRegister)elementsSelected[0]).Trigger = programmablRegister.TriggerType.LowLevel;
                    }
                }
            }
            catch { }
            
        }

        private void ComboBoxCompteur_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (elementsSelected[0] is SequentialComponent.compteurN)
                {
                    ((SequentialComponent.compteurN)elementsSelected[0]).Val = ComboBoxCompteur.SelectedIndex + 2;
                    ((SequentialComponent.compteurN)elementsSelected[0]).Nbroutputs = (int)Math.Floor(Math.Log(ComboBoxCompteur.SelectedIndex + 2, 2))+1;
                    ((SequentialComponent.compteurN)elementsSelected[0]).redessiner("M 0,0 L 30,0 L 30,30 L 0,30 z");

                }
                else if (elementsSelected[0] is SequentialComponent.CompteurModN)
                {
                    ((SequentialComponent.CompteurModN)elementsSelected[0]).Val = ComboBoxCompteur.SelectedIndex + 2;
                    ((SequentialComponent.CompteurModN)elementsSelected[0]).Nbroutputs = (int)Math.Floor(Math.Log(ComboBoxCompteur.SelectedIndex + 2, 2))+1;
                    ((SequentialComponent.CompteurModN)elementsSelected[0]).redessiner("M 0,0 L 30,0 L 30,30 L 0,30 z");
                }
                else if (elementsSelected[0] is SequentialComponent.DecompteurN)
                {
                    ((SequentialComponent.DecompteurN)elementsSelected[0]).Val = ComboBoxCompteur.SelectedIndex + 2;
                    ((SequentialComponent.DecompteurN)elementsSelected[0]).Nbroutputs = (int)Math.Floor(Math.Log(ComboBoxCompteur.SelectedIndex + 2, 2))+1;
                    ((SequentialComponent.DecompteurN)elementsSelected[0]).redessiner("M 0,0 L 30,0 L 30,30 L 0,30 z");
                }
                else if (elementsSelected[0] is SequentialComponent.DecompteurModN)
                {
                    ((SequentialComponent.DecompteurModN)elementsSelected[0]).Val = ComboBoxCompteur.SelectedIndex + 2;
                    ((SequentialComponent.DecompteurModN)elementsSelected[0]).Nbroutputs = (int)Math.Floor(Math.Log(ComboBoxCompteur.SelectedIndex + 2, 2))+1;
                    ((SequentialComponent.DecompteurModN)elementsSelected[0]).redessiner("M 0,0 L 30,0 L 30,30 L 0,30 z");
                }
            }
            catch
            {

            }
           
        }

        private void UpdateTitle()
        {
            StringBuilder ttl = new StringBuilder();

            if (String.IsNullOrEmpty(_filename))
            {
                ttl.Append("[Untitled]");
            }
            else
            {
                ttl.Append(_filename.Substring(_filename.LastIndexOf(@"\") + 1));
            }

            ttl.Append(" - ");

            ttl.Append(APP_TITLE);

            Title = ttl.ToString();
            CircuitName.Text = ttl.ToString();
        }
    }


}


