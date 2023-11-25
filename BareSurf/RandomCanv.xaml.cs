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

namespace BareSurf
{
    /// <summary>
    /// Interaction logic for RandomCanv.xaml
    /// </summary>
    public partial class RandomCanv : UserControl
    {
        public RandomCanv()
        {
            InitializeComponent();
        }
        private Random _random = new Random();
        private void DrawRandomShapes(Canvas canvas)
        {
            int rectangleCount = _random.Next(1, 25);
            canvas.Children.Clear();
            for(int i = 0; i < rectangleCount; i++)
            {
                int _rnd = _random.Next(0, 100);
                if(_rnd % 2 == 0)
                {
                    DrawRandomRectangle(canvas);
                }
                else
                {
                    DrawRandomEllipse(canvas);
                }
            }
        }
        private SolidColorBrush MakeRandomColor()
        {
            return new SolidColorBrush(Color.FromRgb((byte)_random.Next(0, 256), (byte)_random.Next(0, 256), (byte)_random.Next(0, 256)));
        }
        public void DrawRandomRectangle(Canvas canvas)
        {
            int rectMaxWidth = canvas.ActualWidth > 100 ? (int)canvas.ActualWidth : 100;
            int rectMaxHeight = canvas.ActualHeight > 100 ? (int)canvas.ActualHeight : 100;
            // Create a new rectangle
            Rectangle rect = new Rectangle
            {
                Width = _random.Next(50, rectMaxWidth), // Random width between 50 and 100
                Height = _random.Next(50, rectMaxHeight), // Random height between 50 and 100
                Fill = MakeRandomColor() // Random color
            };

            // Set random position for the rectangle
            Canvas.SetLeft(rect, _random.Next((int)Math.Abs(canvas.ActualWidth - rect.Width)));
            Canvas.SetTop(rect, _random.Next((int)Math.Abs(canvas.ActualHeight - rect.Height)));

            // Add the rectangle to the canvas
            canvas.Children.Add(rect);
        }
        public void DrawRandomEllipse(Canvas canvas)
        {
            int MaxWidth = canvas.ActualWidth > 100 ? (int)canvas.ActualWidth : 100;
            int MaxHeight = canvas.ActualHeight > 100 ? (int)canvas.ActualHeight : 100;
            // Create a new ellipse
            Ellipse ellipse = new Ellipse
            {
                Width = _random.Next(50, MaxWidth), // Random width between 50 and 100
                Height = _random.Next(50, MaxHeight), // Random height between 50 and 100
                Fill = MakeRandomColor() // Random color
            };

            // Set random position for the ellipse
            Canvas.SetLeft(ellipse, _random.Next((int)Math.Abs(canvas.ActualWidth - ellipse.Width)));
            Canvas.SetTop(ellipse, _random.Next((int)Math.Abs(canvas.ActualHeight - ellipse.Height)));

            // Add the ellipse to the canvas
            canvas.Children.Add(ellipse);
        }
        public void DrawSpiderWeb(Canvas canvas)
        {
            double centerX = canvas.ActualWidth / 2;
            double centerY = canvas.ActualHeight / 2;
            double radius = Math.Min(centerX, centerY) - 10; // Leave some margin
            canvas.Children.Clear();
            // Draw circles
            for (double r = radius; r > 0; r -= radius / 5)
            {
                Ellipse circle = new Ellipse
                {
                    Width = r * 2,
                    Height = r * 2,
                    Stroke = MakeRandomColor()
                };
                Canvas.SetLeft(circle, centerX - r);
                Canvas.SetTop(circle, centerY - r);
                canvas.Children.Add(circle);
            }

            // Draw lines from center to edge
            for (double angle = 0; angle < Math.PI * 2; angle += Math.PI / 6)
            {
                Line line = new Line
                {
                    X1 = centerX,
                    Y1 = centerY,
                    X2 = centerX + radius * Math.Cos(angle),
                    Y2 = centerY + radius * Math.Sin(angle),
                    Stroke = MakeRandomColor()
                };
                canvas.Children.Add(line);
            }
        }
        public void DrawMosaic(Canvas canvas, int rows, int columns)
        {
            double cellWidth = canvas.ActualWidth / columns;
            double cellHeight = canvas.ActualHeight / rows;

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    Rectangle rect = new Rectangle
                    {
                        Width = cellWidth,
                        Height = cellHeight,
                        Fill = MakeRandomColor()
                    };

                    Canvas.SetLeft(rect, column * cellWidth);
                    Canvas.SetTop(rect, row * cellHeight);

                    canvas.Children.Add(rect);
                }
            }
        }
        void DrawLineArt(Canvas canvas, ushort lineCount=1,double density=10)
        {
            canvas.Children.Clear();
            for(ushort i =0; i < lineCount; i++)
            {
                double x1=0;
                double x2 =((i* lineCount) + density)*i;
                double y1 = ((i/lineCount) + density)*i;
                double y2 = 0;
                addLine(canvas, x1,  x2, y1, y2, MakeRandomColor());
            }
        }
        void addLine(Canvas canvas, double x1, double x2, double y1, double y2, Brush brush)
        {
            Line line = new Line();
            line.X1 = x1;
            line.X2 = x2;
            line.Y1 = y1;
            line.Y2 = y2;

            line.StrokeThickness = 2;
            line.Stroke = brush;

            // https://stackoverflow.com/questions/2879033/how-do-you-draw-a-line-on-a-canvas-in-wpf-that-is-1-pixel-thick
            line.SnapsToDevicePixels = true;
            line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);

            canvas.Children.Add(line);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DrawRandomShapes(PlainCanvas);
        }

        private void browseBtn_Click(object sender, RoutedEventArgs e)
        {
            DaDock.Children.Clear();
            DaDock.Children.Add(new BrowserCompo());
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DrawRandomShapes(PlainCanvas);
        }

        private void spiderWebBtn_Click(object sender, RoutedEventArgs e)
        {
            DrawSpiderWeb(PlainCanvas);
        }

        private void MosaicBtn_Click(object sender, RoutedEventArgs e)
        {
            DrawMosaic(PlainCanvas, _random.Next(2, 7), _random.Next(1, 5));
        }
        private void Linear_Click(object sender, RoutedEventArgs e)
        {
            DrawLineArt(PlainCanvas,30, Math.Floor(PlainCanvas.ActualWidth / 30));
        }
    }
}
