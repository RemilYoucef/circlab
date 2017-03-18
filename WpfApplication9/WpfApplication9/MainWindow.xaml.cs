﻿using Microsoft.Win32;
using System;
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

namespace WpfApplication9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static StandardComponent elementSelected;

        public MainWindow()
        {
            InitializeComponent();
            Wireclass.mwindow = mwindow;
            Wireclass.myCanvas = canvas;
            StandardComponent.canvas = canvas;
            

        }
        private object movingObject;
        private double firstXPos, firstYPos;
        private new void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // In this event, we get the current mouse position on the control to use it in the MouseMove event.
            Control img = sender as Control;
            Canvas canvas = img.Parent as Canvas;

            firstXPos = e.GetPosition(img).X;
            firstYPos = e.GetPosition(img).Y;

            movingObject = sender;

            // Put the image currently being dragged on top of the others
            int top = Canvas.GetZIndex(img);
            try { foreach (Control child in canvas.Children)
                    if (top < Canvas.GetZIndex(child))
                        top = Canvas.GetZIndex(child);
            }
            catch (Exception){ }
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
            try { foreach (Control child in canvas.Children)
                    if (top > Canvas.GetZIndex(child))
                        top = Canvas.GetZIndex(child); }
            catch (Exception) { }
            Canvas.SetZIndex(img, top + 1);
         
            Mouse.Capture(null);
        }

  
        public void modifieProperties()
        {
      
            if (elementSelected.nbrInputs() != 8 )
                ComboBoxProperties.SelectedIndex = elementSelected.nbrInputs() - 2;
            else ComboBoxProperties.SelectedIndex = 3;
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
            NAND img = new NAND(4);
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

        private new void MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && sender == movingObject)
            {
                Control img = sender as Control;
                Canvas canvas = img.Parent as Canvas;

                double newLeft = e.GetPosition(canvas).X - firstXPos - canvas.Margin.Left;
                // newLeft inside canvas right-border?
                if (newLeft > canvas.Margin.Left + canvas.ActualWidth - img.ActualWidth)
                    newLeft = canvas.Margin.Left + canvas.ActualWidth - img.ActualWidth;
                // newLeft inside canvas left-border?
                else if (newLeft < canvas.Margin.Left)
                    newLeft = canvas.Margin.Left;
                img.SetValue(Canvas.LeftProperty, newLeft);

                double newTop = e.GetPosition(canvas).Y - firstYPos - canvas.Margin.Top;
                // newTop inside canvas bottom-border?
                if (newTop > canvas.Margin.Top + canvas.ActualHeight - img.ActualHeight)
                    newTop = canvas.Margin.Top + canvas.ActualHeight - img.ActualHeight;
                // newTop inside canvas top-border?
                else if (newTop < canvas.Margin.Top)
                    newTop = canvas.Margin.Top;
                img.SetValue(Canvas.TopProperty, newTop);
                try
                {
                    StandardComponent component = sender as StandardComponent;
                    component.recalculer_pos();
                }
                catch { };


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
            Clock img = new Clock(500);
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

        private void addLabel(object sender, RoutedEventArgs e)
        {
            TextBlock img = new TextBlock();
            img.Inlines.Add("Label");
            img.Foreground = Brushes.Black;
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

        private void annuler(object sender, MouseButtonEventArgs e)
        {
            Wireclass.selected = false;
        }

      

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
            if(elementSelected!=null && !(elementSelected is Input) && !(elementSelected is Output) && !(elementSelected is Not))
            {
                int selecteVal=ComboBoxProperties.SelectedIndex+2;
                if (ComboBoxProperties.SelectedIndex == 3) selecteVal = 8;

                if (selecteVal!= elementSelected.nbrInputs())
                {
                    while (selecteVal > elementSelected.nbrInputs())
                    {
                        elementSelected.AddInputs();
                    }
                    while (selecteVal < elementSelected.nbrInputs())
                    {
                        elementSelected.RemoveInputs();
                    }

                    elementSelected.redessiner(elementSelected.path);
                    canvas.UpdateLayout();
                    elementSelected.Run();
                }
                
            }
          

        }

        public void desactiveComboBox()
        {
            NbrEntreText.Visibility = Visibility.Collapsed;
            ComboBoxProperties.Visibility=Visibility.Collapsed;
            
        }

        public void activeComboBox()
        {
            NbrEntreText.Visibility = Visibility.Visible;
            ComboBoxProperties.Visibility = Visibility.Visible ;
        }

        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
             if (e.OriginalSource is Canvas && elementSelected!=null)
            {
                elementSelected.typeComponenet.Stroke = Brushes.RoyalBlue;
                elementSelected = null;

            }
        }

        private void BottomDrawerHostOpen(object sender, RoutedEventArgs e)
        {
            drawerHost.IsBottomDrawerOpen = true;
            //MessageBox.Show("hi");
        }


    }


}


