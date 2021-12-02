using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Math;

namespace idz11
{
    public partial class Form1 : Form
    {
        double a = 0; 
        static Point[] bummerang = { new Point(0,0),
                             new Point(30,100),
                             new Point(0,200),
                             new Point(15,100) };
        MyPoint[] myPoint = new MyPoint[bummerang.Length];
        Point center = new Point(22, 100);

        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < bummerang.Length; i++) 
                myPoint[i] = new MyPoint(bummerang[i], center);// инициализация массива myPoint;
            #region подписка MoveB на таймер
            var t = new Timer();
            t.Interval = 1;
            t.Enabled = true;
            t.Tick += (s, o) =>  MoveB();
            #endregion 
        }

        private void MoveB()
        {
            
            
            for (int i = 0; i < bummerang.Length; i++)
            {
                
                bummerang[i] = new Point((int)(center.X + Math.Cos(myPoint[i].Deg + a ) * myPoint[i].Radius), (int)(center.Y + Math.Sin(myPoint[i].Deg + a) * myPoint[i].Radius));
            }
            
            a+=0.1;
            Invalidate(); 
        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.DrawPolygon(new Pen(Color.Black, 3f), bummerang);
        }
    }
    struct MyPoint
    {
        private double radius;
        public double Radius
        {
            get { return radius; }
            set
            {
                if (value < 0) throw new Exception("меньше нуля нельзя");
                else radius = value;
            }
        }
        private double deg;
        public double Deg { get { return deg; } }
        Point point;
        /// <summary>
        /// инициализация структуры myPoin, высчитывает радиус и начальный угол точки 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="center"></param>
        public MyPoint(Point point, Point center)
        {
            radius = Sqrt(Pow(point.X - center.X, 2) + Pow(point.Y - center.Y, 2));
            this.point = point;
            if(point.Y - center.X > 0)
                deg = (Math.Acos((point.X - center.X) / radius) * 180) / PI;
            else
                deg = -(Math.Acos((point.X - center.X) / radius) * 180) / PI;
        }
    }
    
}

