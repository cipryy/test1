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
        Dictionary<int, Rectangle> towerMap;
        int nextTowerId = 0;

        World[][] worldMap;
        int counter = 1;
        public Rectangle selectedWall { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            worldMap = CreateMap();
            towerMap = new Dictionary<int, Rectangle>();
            draw();
            testEnemies();
            selectedWall = null;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);          // Everytime timer ticks, timer_Tick will be called
            timer.Interval = TimeSpan.FromMilliseconds(100);    // Timer will tick evert second
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < counter; i++)
                animateEnemy(i);

            int range = 100;

            for (int i = 0; i < towerMap.Count; i++)
            {
                Rectangle tower = towerMap[i];

                double towerX = Canvas.GetLeft(tower);
                double towerY = Canvas.GetTop(tower);

                for (int j = 0; j < enemyMap.Count; j++)
                {
                    Rectangle enemy = enemyMap[j];

                    double enemyX = Canvas.GetLeft(enemy);
                    double enemyY = Canvas.GetTop(enemy);

                    double ys = Math.Pow(towerY - enemyY, 2);
                    double xs = Math.Pow(towerX - enemyX, 2);
                    double distance = Math.Sqrt(xs + ys);

                    if (distance > range)
                    {
                        tower.Fill = Brushes.Blue;
                    }
                    else
                    {
                        tower.Fill = Brushes.Red;
                        break;
                    }
                }
            }



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
                        r.PreviewMouseLeftButtonUp += rectangleClicked;

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

            if (r != selectedWall)
            {
                r.Fill = Brushes.Blue;
            }
        }

        private void rectangleLeave(object sender, MouseEventArgs e)
        {
            Rectangle r = sender as Rectangle;
            if (r != selectedWall)
            {
                ImageBrush backgroundBrush = new ImageBrush();
                Image img = Application.Current.Resources["Wall"] as Image;
                backgroundBrush.ImageSource = img.Source;
                r.Fill = backgroundBrush;
            }
        }

        private void rectangleClicked(object sender, MouseButtonEventArgs e)
        {
            Rectangle r = e.OriginalSource as Rectangle;

            int y = (int)Canvas.GetLeft(r) / 20;
            int x = (int)Canvas.GetTop(r) / 20;

            if (worldMap[x][y].Type.Type == "Wall")
            {
                Rectangle oldSelected = selectedWall;
                selectedWall = r;
                selectedWall.Fill = Brushes.Black;
                popup.HorizontalOffset = Canvas.GetLeft(r) - 35;
                popup.VerticalOffset = Canvas.GetTop(r) - 30;
                popup.IsOpen = true;

                if (oldSelected != null)
                    rectangleLeave(oldSelected, e);
            }
        }

        private void buildTower(object sender, RoutedEventArgs e)
        {
            Rectangle tower = new Rectangle();
            tower.Height = 16;
            tower.Width = 16;
            tower.Fill = Brushes.Blue;

            double top = Canvas.GetTop(selectedWall);
            double left = Canvas.GetLeft(selectedWall);

            Canvas.SetTop(tower, top + 2);
            Canvas.SetLeft(tower, left + 2);
            canvas.Children.Add(tower);

            towerMap[nextTowerId] = tower;

            cancel(sender, null);

            nextTowerId++;
        }

        private void cancel(object sender, MouseButtonEventArgs e)
        {
            Rectangle oldSelected = selectedWall;
            selectedWall = null;
            if (oldSelected != null)
                rectangleLeave(oldSelected, null);

            popup.IsOpen = false;
        }
    }
}
