﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApplication9.Component
{

    public partial class Wireclass : UserControl
    {

        
      
        private bool lastIsHorizental=true;
        private bool firstIsHorizental=true;//A descuter m3a jma3a 
        public ArrayList listeLine;//liste des lines dessiner 
        public Line l1 { get; set; }
        public Line l2 { get; set; }
        public Line l3 { get; set; }
        
        public static Boolean selected = false;
        public static Point btn1Point;
        public static Point btn2Point;
        public static Ellipse selection1;
        public static Ellipse selection2;
        public static Canvas myCanvas;
        public static Window mwindow;
        public  Terminal source;
        public  Terminal destination;
    
        public ArrayList destinations;
        public double x1;
        public double x2;
        public double y1;
        public double y2;
        private ContextMenu menu;
        public Ellipse btn111;
        public Ellipse btn222;
        public Ellipse noued;
        private Boolean _state;
        public Boolean state
        {
            get { return _state; }
            set
            {
                if (_state != value)
                {
                    _state = value;
                    foreach (StandardComponent standardcomponent in destinations)
                    {
                        standardcomponent.Run(); //Recalcule les résultats de tout les composants relier à ce fils
                    }
                }
                _state = value;

            }
        }


        public Wireclass()
        {
          
            
            destinations = new ArrayList();
            l1 = new Line();
            l2 = new Line();
            l3 = new Line();
            MenuItem supprim = new MenuItem();
            supprim.Header = "Supprime";
            supprim.Click += this.Suppression;
            menu = new ContextMenu();
            
            menu.Items.Add(supprim);
            l1.ContextMenu = menu;
            l2.ContextMenu = menu;
            l3.ContextMenu = menu;
            listeLine = new ArrayList();
            listeLine.Add(l1);
            listeLine.Add(l2);
            listeLine.Add(l3);
     
            myCanvas.Children.Add(l2);
            l2.PreviewMouseMove += MouseMove2;




            l2.PreviewMouseLeftButtonDown += this.MouseLeftButtonDown2;
           // l1.PreviewMouseMove += MouseMove1;//Partie a descuter 
         //   l1.PreviewMouseLeftButtonDown += this.MouseLeftButtonDown1;
            l3.PreviewMouseMove += MouseMove1;
            l3.PreviewMouseLeftButtonDown += this.MouseLeftButtonDown1;
            myCanvas.Children.Add(l1);
           
            myCanvas.Children.Add(l3);

    

        }

        public void relier()
        {

                Ellipse select;
                destination = UserClass.TryFindParent<Terminal>(selection2);
                source = UserClass.TryFindParent<Terminal>(selection1);

                if (source.IsOutpt == false)
                {
                    if (destination.IsOutpt == true)
                    {
                        select = selection2;
                        selection2 = selection1;
                        selection1 = select;
                        Terminal temp = destination;
                        destination = source;
                        source = temp;
                    }
                    else
                    {
                        selected = false;
                        return;
                    }
                }
                else
                {
                    if (destination.IsOutpt == true)
                    {
                        selected = false;
                        return;
                    }
                }

                if (destination.wires.Count >= 1)
                {
                    selected = false;
                    return;
                }

                btn111 = selection1;
                btn222 = selection2;
                destination.wires.Add(this);
                source.wires.Add(this);


                btn2Point = selection2.TransformToAncestor(myCanvas).Transform(new Point(0, 0));
                btn1Point = selection1.TransformToAncestor(myCanvas).Transform(new Point(0, 0));

     
                l1.Stroke = new SolidColorBrush(Colors.Black);
                l1.StrokeThickness = 2.0;
                l1.X1 = btn1Point.X ;
                l1.X2 = (btn1Point.X + btn2Point.X + 2 * selection1.ActualWidth) / 2;
                l1.Y1 = btn1Point.Y + selection1.ActualHeight / 2;
                l1.Y2 = btn1Point.Y + selection1.ActualHeight / 2;
                myCanvas.Children.Add(l1);

          
                l2.Stroke = new SolidColorBrush(Colors.Black);
                l2.StrokeThickness = 2.0;
                l2.X1 = l1.X2;
                l2.X2 = l1.X2;
                l2.Y1 = l1.Y1;
                l2.Y2 = btn2Point.Y - selection2.ActualHeight / 2;
                myCanvas.Children.Add(l2);

               
                l3.Stroke = new SolidColorBrush(Colors.Black);
                l3.StrokeThickness = 2.0;
                l3.X1 = l2.X2;
                l3.X2 = (btn2Point.X);
                l3.Y1 = l2.Y2;
                l3.Y2 = l2.Y2;
                myCanvas.Children.Add(l3);
         
       
            UserClass.TryFindParent<StandardComponent>(source).Run();
                selected = false;
                UserClass.TryFindParent<StandardComponent>(destination).Run();
                destinations.Add(UserClass.TryFindParent<StandardComponent>(destination));
            
            if (source.wires.Count<=1)
            {
                source.logestWire= this;
            }    
            else
            {
                if (Math.Abs((source.logestWire).l1.X1- (source.logestWire).l1.X2)<Math.Abs(this.l1.X1-this.l1.X2))
                {
                    source.logestWire = this;
           
                }
            }
            dessinernoued();

        }

        public void Destroy()
        {
            myCanvas.Children.Remove(this.noued);
            foreach (Line ltemp in listeLine)
            {
                myCanvas.Children.Remove(ltemp);
            }
            foreach (StandardComponent componenet in destinations)
            {
       
                foreach (Terminal terminal in componenet.inputStack.Children)
                {
                    terminal.wires.Remove(this);
                }
          
                componenet.Run();
            }
            try
            {
                source.wires.Remove(this);
            }
            catch (NullReferenceException)//Null exception
            { }

            try {
                if (this == source.logestWire)
                {
                    this.source.wires.Remove(source.logestWire);
                    if (source.wires.Count!=0)
                    {
                        source.logestWire = (Wireclass)this.source.wires[0];
                    }
             
                    foreach (Wireclass wiretmp in source.wires)
                    {
                        if (Math.Abs((this.source.logestWire).l1.X1 - (this.source.logestWire).l1.X2) < Math.Abs(wiretmp.l1.X1 - wiretmp.l1.X2))
                        {
                            this.source.logestWire = wiretmp;                          
                        }
                    }
                    dessinernoued();
                }
              
            }
            catch (NullReferenceException) { }
        }

        public void recalculer(Boolean source)
        {

          
        btn1Point = btn111.TransformToAncestor(myCanvas).Transform(new Point(0, 0));
            l1 = (Line)listeLine[0];
            l2 = (Line)listeLine[1];
            if (firstIsHorizental)
            {
                l1.X1 = btn1Point.X;
                l2.Y1 = btn1Point.Y;
                l1.Y1 = btn1Point.Y+btn111.ActualHeight/2;
                l1.Y2 = btn1Point.Y+btn111.ActualHeight/2;
                l2.Y1 = l1.Y1;
    
            }
            else
            {
                l1.X1 = btn1Point.X;
                l1.X2 = btn1Point.X;
                l1.Y1 = btn1Point.Y;
                l2.X1 = l1.X1;


            }

            btn2Point = btn222.TransformToAncestor(myCanvas).Transform(new Point(0, 0));


            l2 = (Line)(listeLine[listeLine.IndexOf(l3) - 1]);
            if (lastIsHorizental)
            {
        
                l3.X2 = btn2Point.X;
                l3.Y2 = btn2Point.Y-btn222.ActualHeight/2;
                l3.Y1 = btn2Point.Y-btn222.ActualHeight/2;
                l2.Y2 = l3.Y1;
            }
            else
            {
                l3.X2 = btn2Point.X+btn111.ActualWidth/2;
                l3.X1 = btn2Point.X+btn111.ActualWidth/2;
                l3.Y2 = btn2Point.Y;
                l2.X2 = l3.X1;
                
            }


            if (Math.Abs((this.source.logestWire).l1.X1 - (this.source.logestWire).l1.X2) < Math.Abs(this.l1.X1 - this.l1.X2))
            {
               this.source.logestWire = this;

            }

            dessinernoued();





        }

        public void Dessiner()
        {
    
           
            x1 = btn1Point.X;
            y1 = btn1Point.Y;
     
            x2 = btn2Point.X;
            y2 = btn2Point.Y;
    
            l1.Stroke = new SolidColorBrush(Colors.Black);
            l1.X1 = x1 ;
            l1.X2 = (x1 + x2 + 2 * selection1.ActualWidth) / 2;
            l1.Y1 = y1 + selection1.ActualHeight / 2;
            l1.Y2 = y1 + selection1.ActualHeight / 2;
    
            l2.Stroke = new SolidColorBrush(Colors.Black);
     
            l2.X1 = l1.X2;
            l2.X2 = l1.X2;
            l2.Y1 = l1.Y1;
            l2.Y2 = y2 - selection1.ActualHeight / 2;
        

       
            l3.Stroke = new SolidColorBrush(Colors.Black);
      
            l3.X1 = l2.X2;
            l3.X2 = (x2);
            l3.Y1 = l2.Y2;
            l3.Y2 = l2.Y2;
   
        }

        private new void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // In this event, we get the current mouse position on the control to use it in the MouseMove event.
            Ellipse img = sender as Ellipse;
            Canvas canvas = img.Parent as Canvas;
            MainWindow.sourceEllipse = img;
            Mouse.Capture(canvas);
            MainWindow.wire = new Wireclass();

        }


        Line l;
        private  void MouseLeftButtonDown2(object sender, MouseButtonEventArgs e)
        {

            Mouse.Capture(sender as Line);
            l = sender as Line;
            l.Cursor = Cursors.SizeWE;
        }

 

        private  void MouseMove2(object sender, MouseEventArgs e) 
        {
      
            Line lBefore;
            Line lAfter;
            btn2Point = btn222.TransformToAncestor(myCanvas).Transform(new Point(0, 0));
            btn1Point = btn111.TransformToAncestor(myCanvas).Transform(new Point(0, 0));
            if (e.LeftButton == MouseButtonState.Pressed && l==sender as Line)
            {
                int x =listeLine.IndexOf(l);
                try {
                    lBefore = (Line)(listeLine[x - 1]);      
                }
                catch(ArgumentOutOfRangeException ) {

                    lBefore = new Line();
                    lBefore.X1 = btn1Point.X;
                    lBefore.Y1 = btn1Point.Y+selection1.ActualHeight;
                    lBefore.Y2 = lBefore.Y1;
                    listeLine.Reverse();
                    listeLine.Add(lBefore);
                    listeLine.Reverse();
                    myCanvas.Children.Add(lBefore);
                    lBefore.Stroke = Brushes.Black;
                    lBefore.StrokeThickness = 2;
                    lBefore.ContextMenu = menu;
                    x = 1;
                    firstIsHorizental = true;
                }
               
                   lBefore.X2 = e.GetPosition(myCanvas).X;
                
                try
                {
               
                    lAfter = (Line)(listeLine[ x+1]);
              
                }
                catch (ArgumentOutOfRangeException  ){/* y'a pas d'element ba3do */

                    lAfter = new Line();
                    lAfter.X2 = btn2Point.X;
                    lAfter.Y1 = l.Y2-selection1.ActualHeight/2;
                    lAfter.Y2 = btn2Point.Y-selection1.ActualHeight/2;
                    l.Y2 = lAfter.Y1;
                    lAfter.ContextMenu = menu;
                    myCanvas.Children.Add(lAfter);
                    lAfter.Stroke = Brushes.Black;
                    lAfter.StrokeThickness = 2;

                    listeLine.Add(lAfter);
                    l3 = lAfter;
                    lAfter.PreviewMouseMove += MouseMove1;
                    lAfter.PreviewMouseLeftButtonDown += this.MouseLeftButtonDown1;
                    lastIsHorizental = true;
                }

                lAfter.X1 = e.GetPosition(myCanvas).X;
            
                l.X1 = e.GetPosition(myCanvas).X;
                l.X2 = lAfter.X1;
     
                if (Math.Abs((this.source.logestWire).l1.X1 - (this.source.logestWire).l1.X2) <= Math.Abs(this.l1.X1 - this.l1.X2))
                {

                    if (this.source.logestWire == this)
                    {
                        foreach (Wireclass wiretmp in source.wires)
                        {

                            if (Math.Abs((this.source.logestWire).l1.X1 - (this.source.logestWire).l1.X2) < Math.Abs(wiretmp.l1.X1 - wiretmp.l1.X2))
                            {

                                this.source.logestWire = wiretmp;

                            }
                        }
                    }
                    else
                    {
                         this.source.logestWire = this;
                    }
                  
                  
                }

                dessinernoued();

            }

        }

        private void Suppression(object sender, RoutedEventArgs e)
        {
            this.Destroy();
            
        }

 
        private void MouseLeftButtonDown1(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(sender as Line);
            l = sender as Line;
            l.Cursor = Cursors.SizeNS;

        }



        private void MouseMove1(object sender, MouseEventArgs e)
        {
            Line lBefore;
            Line lAfter;
            btn2Point = btn222.TransformToAncestor(myCanvas).Transform(new Point(0, 0));
            btn1Point = btn111.TransformToAncestor(myCanvas).Transform(new Point(0, 0));
            if (e.LeftButton == MouseButtonState.Pressed && l==sender as Line)   
            {
                l = sender as Line;

                int x = listeLine.IndexOf(l);
                try
                {
                    lBefore = (Line)(listeLine[x - 1]);

                }
                catch (ArgumentOutOfRangeException )
                {

                    lBefore = new Line();
                    listeLine.Reverse();
                    listeLine.Add(lBefore);
                    listeLine.Reverse();
                    lBefore.X1 = l.X1 - btn111.ActualWidth / 2;
                    lBefore.X2 = l.X1 - btn111.ActualWidth / 2;
                    lBefore.Y1 = btn1Point.Y + btn111.ActualHeight / 2;
                    x = 1;
                    lBefore.ContextMenu = menu;
                    //  lBefore.PreviewMouseMove += this.MouseMove2;
                    //   lBefore.PreviewMouseLeftButtonDown += this.MouseLeftButtonDown2;
                    myCanvas.Children.Add(lBefore);
                   // firstIsHorizental = false;
                }
                lBefore.Y2 = e.GetPosition(myCanvas).Y;
                lBefore.StrokeThickness = 2;
                lBefore.Stroke = new SolidColorBrush(Colors.Black);

               
            
             //   l.X1 = l.X1 - btn111.ActualWidth / 2;
                l.Y1 = e.GetPosition(myCanvas).Y;
                l.Y2 = e.GetPosition(myCanvas).Y;
                try
                {
                    lAfter = (Line)(listeLine[x + 1]);
             
                }
                catch
                {
                    lAfter = new Line();
                    lAfter.X1 = btn2Point.X+btn111.ActualWidth/2;
                    lAfter.X2 = btn2Point.X+btn111.ActualWidth/2;
                    l.X2 = lAfter.X2;
                    lAfter.Y2 = btn2Point.Y;
                    listeLine.Add(lAfter);
                    myCanvas.Children.Add(lAfter);
                    lAfter.PreviewMouseMove += this.MouseMove2;
                    lAfter.PreviewMouseLeftButtonDown += this.MouseLeftButtonDown2;
                    lastIsHorizental = false;
                    l3 = lAfter;
                    lAfter.ContextMenu = menu;

                }
                lAfter.StrokeThickness = 2;
                lAfter.Stroke = new SolidColorBrush(Colors.Black);
                lAfter.Y1 = l.Y2;
                if (Math.Abs((this.source.logestWire).l1.X1 - (this.source.logestWire).l1.X2) < Math.Abs(this.l1.X1 - this.l1.X2))
                {
                    this.source.logestWire = this;
                   

                }

                dessinernoued();
            }


    }
        private void dessinernoued()
        {
            
      
            foreach (Wireclass wiretmp in source.wires)
            {
                
                if (wiretmp!=source.logestWire)
                {
                   
                    
                    if (wiretmp.noued==null)
                    {
                        wiretmp.noued = new Ellipse();
                        myCanvas.Children.Add(wiretmp.noued);
                        wiretmp.noued.Height = 10;
                        wiretmp.noued.Width = 10;
                        wiretmp.noued.Fill = Brushes.Black;
                    }
                
                    Canvas.SetTop(wiretmp.noued,wiretmp.l1.Y1 - btn111.ActualHeight / 2);
                    Canvas.SetLeft(wiretmp.noued,wiretmp.l1.X2 - btn222.ActualHeight / 2);
                    

                }
                else
                {
                    if(wiretmp.noued!=noued)
                    {
                        myCanvas.Children.Remove(wiretmp.noued);
                        wiretmp.noued = null;
                        
                    }
                }
            }
      
        }


    }



}


