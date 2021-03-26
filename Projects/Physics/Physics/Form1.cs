using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Diagnostics;

namespace Physics
{
	public partial class Form1 : Form
	{
		Stopwatch stopwatch = new Stopwatch();
		double dt;
		double currentTime;
		double lastTime;

		public Form1()
		{
			InitializeComponent();
			//CircleObject circle = new CircleObject(new Vector2(50, 30), 0, 1, 0.05f, 0.5f, 5, 1000000, 0.005f, 0.005f, true);
			//CircleObject circle2 = new CircleObject(new Vector2(47, 100), 0, 1, 0.05f, 0.5f, 5, 1000000, 0.005f, 0.005f, true);
			//circle2.rotation = MathF.PI/2;
			//Physics.objectList.Add(circle);
			//Physics.objectList.Add(circle2);

			stopwatch.Start();
			Physics.sceneBounds = new Bounds(Vector2.Zero, new Vector2(Width/2, Height/2));
			Timer t = new Timer();
			t.Interval = 10;
			t.Tick += Tock;
			t.Start();
		}

		Brush b = Brushes.Red;
		Pen p = Pens.Black;
		Font f = new Font(FontFamily.GenericMonospace, 12);
		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			if (Physics.objectList.Count < 25)
			Physics.objectList.Add(new CircleObject(new Vector2(Physics.objectList.Count * 14 + 10, 30), 0, 1, 00f, 0.5f, 5, 1000000, 0f, 0.00f, true));
			if (Physics.objectList.Count < 50)
				Physics.objectList.Add(new CircleObject(new Vector2(Physics.objectList.Count/2 * 16 + 10, 50), 0, 1, 00f, 0.5f, 5, 1000000, 0f, 0.0f, true));
			if (Physics.objectList.Count < 75)
				Physics.objectList.Add(new CircleObject(new Vector2(Physics.objectList.Count/3 * 14 + 10, 70), 0, 1, 0f, 0.5f, 5, 1000000, 0f, 0f, true));
			if (Physics.objectList.Count < 100)
				Physics.objectList.Add(new CircleObject(new Vector2(Physics.objectList.Count / 4* 16 + 10, 100), 0, 1, 0f, 0.5f, 5, 1000000, 0f, 0f, true));

			int v = 0;
			e.Graphics.DrawRectangle(p, new Rectangle(0, 0, Width/2, Height/2));
			for (int i = 0; i < Physics.objectList.Count; i++)
			{
				e.Graphics.FillEllipse(b, Physics.objectList[i].position.X - Physics.objectList[i].radius, (int)Physics.objectList[i].position.Y - Physics.objectList[i].radius, Physics.objectList[i].radius * 2, Physics.objectList[i].radius * 2);
				//e.Graphics.DrawLine(p, Physics.objectList[i].position.X, Physics.objectList[i].position.Y, Physics.objectList[i].position.X + 20 * MathF.Cos(Physics.objectList[i].rotation), Physics.objectList[i].position.Y + 20 * MathF.Sin(Physics.objectList[i].rotation));
				
			}
		}

		private void Tock(object sender, EventArgs e)
		{
			currentTime = stopwatch.Elapsed.TotalSeconds;
			dt = currentTime - lastTime;
			lastTime = currentTime;
			Physics.Iterate(dt);
			Invalidate();

		}
	}
}
