using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TowerDefense
{
    public enum Celltype
    {
        Wall,
        Open,
        StartPoint,
        EndPoint,
        Up,
        Down,
        Left,
        Right
    }


    public partial class MainWindow : Window
    {
        Dictionary<int, Rectangle> enemyMap;

        World[][] worldMap;
        int counter = 1;

        public MainWindow()
        {
            InitializeComponent();
            worldMap = CreateMap();
            draw();
            testEnemies();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);          // Everytime timer ticks, timer_Tick will be called
            timer.Interval = TimeSpan.FromMilliseconds(100);    // Timer will tick evert second
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < counter; i++)
                animateEnemy(i);

            if (counter < 10)
                counter++;
        }

        void testEnemies()
        {
            enemyMap = new Dictionary<int, Rectangle>();
            for (int i = 0; i < 10; i++)
            {
                createEnemy(0, 0, i);
            }
        }

        void createEnemy(int pozLin,int pozCol, int id)
        {
            Rectangle enem = new Rectangle();
            
            enem.Height = 10;
            enem.Width = 10;
            enem.Fill = Brushes.Red;

            canvas.Children.Add(enem);
            Canvas.SetTop(enem, pozLin * 20 + 5);
            Canvas.SetLeft(enem, pozCol * 20 + 5);

            enemyMap.Add(id, enem);
        }

        void animateEnemy(int id)
        {
            Rectangle drpt;
            enemyMap.TryGetValue(id, out drpt);

            int y = (int)Canvas.GetLeft(drpt) / 20;
            int x = (int)Canvas.GetTop(drpt) / 20;
            //while (true)
            {
                if (worldMap[x][y].Type.Type == "Down")
                    Canvas.SetTop(drpt, Canvas.GetTop(drpt) + 20);
                else if (worldMap[x][y].Type.Type == "Right")
                    Canvas.SetLeft(drpt, Canvas.GetLeft(drpt) + 20);
                else if (worldMap[x][y].Type.Type == "Up")
                    Canvas.SetTop(drpt, Canvas.GetTop(drpt) - 20);
                else if (worldMap[x][y].Type.Type == "Left")
                    Canvas.SetLeft(drpt, Canvas.GetLeft(drpt) - 20);
            }
        }

        void GenerateDatabase()
        {

        }

       



        public World[][] CreateMap()
        {

            Celltype[][] ct = new Celltype[25][]
             {
                new Celltype[] {Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Down},
                new Celltype[] {Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Down},
                new Celltype[] {Celltype.Down,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left},
                new Celltype[] {Celltype.Down,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall},
                new Celltype[] {Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Down},
                new Celltype[] {Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Down},
                new Celltype[] {Celltype.Down,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left},
                new Celltype[] {Celltype.Down,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall},
                new Celltype[] {Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Down},
                new Celltype[] {Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Down},
                new Celltype[] {Celltype.Down,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left},
                new Celltype[] {Celltype.Down,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall},
                new Celltype[] {Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Down},
                new Celltype[] {Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Down},
                new Celltype[] {Celltype.Down,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left},
                new Celltype[] {Celltype.Down,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall},
                new Celltype[] {Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Down},
                new Celltype[] {Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Down},
                new Celltype[] {Celltype.Down,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left},
                new Celltype[] {Celltype.Down,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall},
                new Celltype[] {Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Right,Celltype.Down},
                new Celltype[] {Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Down},
                new Celltype[] {Celltype.Down,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left,Celltype.Left},
                new Celltype[] {Celltype.Down,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall,Celltype.Wall},
                new Celltype[] {Celltype.Right, Celltype.Right, Celltype.Right, Celltype.Right, Celltype.Right, Celltype.Right, Celltype.Right, Celltype.Right, Celltype.Right, Celltype.Right, Celltype.Right, Celltype.Right, Celltype.Right, Celltype.Right, Celltype.Right, Celltype.Right, Celltype.Right, Celltype.Right, Celltype.Right, Celltype.Right, Celltype.Right, Celltype.Right, Celltype.Right, Celltype.Right, Celltype.Right, Celltype.Right, Celltype.Right, Celltype.Right, Celltype.Right, Celltype.Right, Celltype.Right, Celltype.Right, Celltype.Right, Celltype.Right, Celltype.EndPoint},
            };

            World[][] map = new World[25][];
            for (int i = 0; i < 25; i++)
                map[i] = new World[35];

            for (int i = 0; i < 25; i++)
                for (int j = 0; j < 35; j++)
                {
                    switch (ct[i][j])
                    {
                        case Celltype.Wall:
                            map[i][j] = new World();
                            map[i][j].Type = new CellType();
                            map[i][j].Type.Type = "Wall";
                            map[i][j].X = j * 20;
                            map[i][j].Y = i * 20;
                            break;
                        case Celltype.Open:
                        case Celltype.Up:
                        case Celltype.Down:
                        case Celltype.Left:
                        case Celltype.Right:
                            map[i][j] = new World();
                            map[i][j].Type = new CellType();
                            map[i][j].X = j * 20;
                            map[i][j].Y = i * 20;

                            if (ct[i][j] == Celltype.Open)
                                map[i][j].Type.Type = "Open";
                            if (ct[i][j] == Celltype.Up)
                                map[i][j].Type.Type = "Up";
                            if (ct[i][j] == Celltype.Down)
                                map[i][j].Type.Type = "Down";
                            if (ct[i][j] == Celltype.Left)
                                map[i][j].Type.Type = "Left";
                            if (ct[i][j] == Celltype.Right)
                                map[i][j].Type.Type = "Right";
                            break;
                        case Celltype.StartPoint:
                            map[i][j] = new World();
                            map[i][j].Type = new CellType();
                            map[i][j].Type.Type = "StartPoint";
                            map[i][j].X = j * 20;
                            map[i][j].Y = i * 20;
                            break;
                        case Celltype.EndPoint:
                            map[i][j] = new World();
                            map[i][j].Type = new CellType();
                            map[i][j].Type.Type = "EndPoint";
                            map[i][j].X = j * 20;
                            map[i][j].Y = i * 20;
                            break;
                    }
                }
            return map;

        }
        public void draw()
        {
            World[][] map = worldMap;
            int i, j;
            for (i = 0; i < 25; i++)
                for (j = 0; j < 35; j++)
                {

                    ImageBrush backgroundBrush = new ImageBrush();

                    Rectangle r = new Rectangle();
                    r.Height = 20;
                    r.Width = 20;
                    
                    if (map[i][j].Type.Type == "Wall")
                    {
                        r.MouseEnter += rectangleEnter;
                        r.MouseLeave += rectangleLeave;

                        Image img = Application.Current.Resources["Wall"] as Image;
                        backgroundBrush.ImageSource = img.Source;
                        r.Fill = backgroundBrush;
                    }
                    if (map[i][j].Type.Type == "Open" || map[i][j].Type.Type == "Right" || map[i][j].Type.Type == "Left" || map[i][j].Type.Type == "Down" || map[i][j].Type.Type == "Up")
                    {
                        Image img = Application.Current.Resources["Open"] as Image;
                        backgroundBrush.ImageSource = img.Source;
                        r.Fill = backgroundBrush;
                    }
                    if (map[i][j].Type.Type == "EndPoint")
                    {
                        Image img = Application.Current.Resources["EndPoint"] as Image;
                        backgroundBrush.ImageSource = img.Source;
                        r.Fill = backgroundBrush;
                    }
                    if (map[i][j].Type.Type == "StartPoint")
                    {
                        Image img = Application.Current.Resources["StartPoint"] as Image;
                        backgroundBrush.ImageSource = img.Source;
                        r.Fill = backgroundBrush;
                    }

                    Canvas.SetLeft(r, j * 20);
                    Canvas.SetTop(r, i * 20);
                    canvas.Children.Add(r);


                }
        }

        private void rectangleEnter(object sender, MouseEventArgs e)
        {
            
            Rectangle r = sender as Rectangle;
            
            r.Fill = Brushes.Blue;

        }

        private void rectangleLeave(object sender, MouseEventArgs e)
        {
            Rectangle r = sender as Rectangle;
            ImageBrush backgroundBrush = new ImageBrush();
            Image img = Application.Current.Resources["Wall"] as Image;
            backgroundBrush.ImageSource = img.Source;
            r.Fill = backgroundBrush;
        }
    }
}
