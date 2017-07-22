/***********************************************
 * Name: Jesse Kuntz
 * Date: 11/03/16
 * This program creates a customizable fractal 
 * that shows up in either the ocean or the 
 * forest. Like a teleporter.
 * *********************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Collections;

namespace Fractal
{
    class Fern
    {
        // constant for trigonometry
        double DEG_TO_RAD = Math.PI / 180.0;
        // background number, 0 for forest and 1 for ocean
        int current_background = 0;
        // arrays holding the r, g, b values of the random fern colors
        int[] g0 = new int[3] { 99, 255, 0 };
        int[] g1 = new int[3] { 0, 221, 59 };
        int[] g2 = new int[3] { 6, 180, 0 };
        int[] g3 = new int[3] { 0, 141, 2 };
        int[] g4 = new int[3] { 6, 105, 22 };
        int[] g5 = new int[3] { 76, 175, 80 };
        int[] g6 = new int[3] { 46, 188, 118 };
        int[] g7 = new int[3] { 94, 216, 156 };
        int[] g8 = new int[3] { 36, 147, 36 };
        int[] g9 = new int[3] { 68, 157, 117 };
        // arrays holding the r, g, b values of the random coral colors
        int[] c0 = new int[3] { 255, 102, 102 };
        int[] c1 = new int[3] { 255, 115, 115 };
        int[] c2 = new int[3] { 132, 234, 211 };
        int[] c3 = new int[3] { 238, 151, 151 };
        int[] c4 = new int[3] { 186, 234, 216 };
        int[] c5 = new int[3] { 243, 144, 134 };
        int[] c6 = new int[3] { 255, 153, 153 };
        int[] c7 = new int[3] { 51, 204, 204 };
        int[] c8 = new int[3] { 127, 213, 195 };
        int[] c9 = new int[3] { 64, 224, 208 };

        // master function that takes in slider values
        public Fern(double size, double thickness, double turnbias, Canvas canvas)
        {
            // deletes old canvas contents
            canvas.Children.Clear();
            // set the image background of the canvas randomly (1st primitive) (1st random)
            create_background(canvas);
            // adds a random animal to the canvas (2nd random)
            animal(canvas);
            // draw a new fern at the center of the canvas with given parameters
            draw_fern(canvas.Width / 2, canvas.Height, -90, 9, size, thickness, turnbias, canvas);
            // adds a holder for the fern (2nd primitive)
            polygon(canvas);

        }

        // function that takes in more specific information, and is the one that is recursively called
        private void draw_fern(double x1, double y1, double angle, int depth, double size, double thickness, double turnbias, Canvas canvas)
        {
            // Random() object for the slight change in angle of branches (because nature is random)
            Random rnd = new Random();
            double x2;
            double y2;
            if (depth > 0)
            {
    
                // calculate the next points of the lines
                x2 = x1 + (Math.Cos(angle * DEG_TO_RAD) * depth * size);
                y2 = y1 + (Math.Sin(angle * DEG_TO_RAD) * depth * size);
                // lines to create the fractal (3rd primitive)
                line(x1, y1, x2, y2, thickness, canvas);
                // recursively branch to the left, slight randomness for angle (3rd random)
                draw_fern(x2, y2, angle - turnbias - rnd.Next(3), depth - 1, size, thickness, turnbias, canvas);
                // recursively branch to the right, slight randomness for angle
                draw_fern(x2, y2, angle + turnbias + rnd.Next(3), depth - 1, size, thickness, turnbias, canvas);
            }
        }

        // function to draw a line for the fractal
        private void line(double x1, double y1, double x2, double y2, double thickness, Canvas canvas)
        {
            Line myLine = new Line();
            // Random() object for picking the color of either the fern or the coral
            Random rnd = new Random();
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            byte r;
            byte g;
            byte b;
            // array to hold the chosen set of bytes
            int[] a = new int[3];
            // selects one of the 10 options randomly (4th random)
            int n = rnd.Next(0, 10);
            if(current_background == 0)
            {
                if (n == 0) a = g0;
                else if (n == 1) a = g1;
                else if (n == 2) a = g2;
                else if (n == 3) a = g3;
                else if (n == 4) a = g4;
                else if (n == 5) a = g5;
                else if (n == 6) a = g6;
                else if (n == 7) a = g7;
                else if (n == 8) a = g8;
                else a = g9;
            }
            else
            {
                if (n == 0) a = c0;
                else if (n == 1) a = c1;
                else if (n == 2) a = c2;
                else if (n == 3) a = c3;
                else if (n == 4) a = c4;
                else if (n == 5) a = c5;
                else if (n == 6) a = c6;
                else if (n == 7) a = c7;
                else if (n == 8) a = c8;
                else a = c9;
            }

            r = (byte)a[0];
            g = (byte)a[1];
            b = (byte)a[2];
            // give all of the properties to the line
            mySolidColorBrush.Color = Color.FromArgb(255, r, g, b);
            myLine.X1 = x1;
            myLine.Y1 = y1;
            myLine.X2 = x2;
            myLine.Y2 = y2;
            myLine.Stroke = mySolidColorBrush;
            myLine.VerticalAlignment = VerticalAlignment.Center;
            myLine.HorizontalAlignment = HorizontalAlignment.Left;
            // set the line to the thickness of the slider
            myLine.StrokeThickness = thickness;
            // add the line to the canvas
            canvas.Children.Add(myLine);
        }

        // function to create the background image of the picture
        private void create_background(Canvas canvas)
        {
            // Random() object to decide between one of the two choices of background
            Random rnd = new Random();
            // Rectangle() object to hold the image
            Rectangle background = new Rectangle();
            background.Height = 600;
            background.Width = 900;

            ImageBrush imgBrush = new ImageBrush();

            // randomly decide between ocean or forest
            current_background = rnd.Next(0, 2);
            string image = "forest.jpg";
            if (current_background == 0) image = "forest.jpg";
            else image = "ocean.jpg";

            imgBrush.ImageSource = new BitmapImage(new Uri(@image, UriKind.Relative));
            
            // fill the Rectangle with the image
            background.Fill = imgBrush;

            // add the Rectangle to the canvas
            canvas.Children.Add(background);
        }

        // function to put a random animal on the canvas
        private void animal(Canvas canvas)
        {
            // Random() object to decide between one of the three choices of animal
            Random rnd = new Random();
            // Rectangle() object to hold the image
            Rectangle animal = new Rectangle();
            animal.Height = 200;
            animal.Width = 200;
            

            ImageBrush imgBrush = new ImageBrush();

            // randomly pick one of the three animals based off the current background
            // aka we can't have an octopus swimming through the forest
            string image = "nothing.jpg";
            int n = rnd.Next(0, 3);
            if (current_background == 1)
            {
                if (n == 0) image = "nemo.jpg";
                else if (n == 1) image = "dory.jpg";
                else image = "octopus.jpg";
            }
            else
            {
                if (n == 0) image = "bird1.jpg";
                else if (n == 1) image = "bird2.jpg";
                else image = "bird3.jpg";
            }


            imgBrush.ImageSource = new BitmapImage(new Uri(@image, UriKind.Relative));

            // fill the Rectangle with the image
            animal.Fill = imgBrush;
            
            // add the Rectangle to the canvas
            canvas.Children.Add(animal);
        }

        private void polygon(Canvas canvas)
        {
            // create a new Polygon() object to hold either the splash of water or the dirt
            Polygon p = new Polygon();
            // creates the rich dirt where the fern is planted
            if (current_background == 0)
            {
                p.Stroke = Brushes.SaddleBrown;
                p.Fill = Brushes.Brown;
            }
            // creates a mini splash of water around the coral
            else
            {
                p.Stroke = Brushes.AliceBlue;
                p.Fill = Brushes.LightBlue;
            }
            p.StrokeThickness = 1;
            p.HorizontalAlignment = HorizontalAlignment.Left;
            p.VerticalAlignment = VerticalAlignment.Center;
            // set the points where we want the Polygon to appear
            p.Points = new PointCollection() { new Point(400, 600), new Point(500, 600), new Point(500, 575), new Point(400, 575), };
            // add the Polygon to the canvas
            canvas.Children.Add(p);
        }
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // creates a new fern when the program is started
            Fern f = new Fern(sizeSlider.Value, thickSlider.Value, biasSlider.Value, canvas);
        }


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            // creates a new fern when the draw button is clicked
            Fern f = new Fern(sizeSlider.Value, thickSlider.Value, biasSlider.Value, canvas);
        }
    }

}
