using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhysicsText
{
	public partial class MainForm : Form
	{

		public MainForm()
		{
			InitializeComponent();
			CharRenderObject cRO = CharRenderObject.CreateCharObject(0, new Vector2(50, 50), Vector2.One, new Size(40, 40), 1, 4, 1, 10, 1);

			cRO.character = 'A';
			allObjects.Add(cRO);
		}

		List<CharRenderObject> allObjects = new List<CharRenderObject>();
		Stopwatch stopwatch = new Stopwatch();
		const float outOfBoundsLimit = 50;
		Brush brushes = Brushes.Black;
		Font font = new Font(FontFamily.GenericMonospace, 10f, FontStyle.Regular);

		public void Loop()
		{
			//while (true)
			//{
			//	IterateAllObjects();
			//	Refresh();
			//}
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			DrawAllObjects(e.Graphics);
		}

		void DrawAllObjects(Graphics graphics)
		{
			graphics.PageUnit = GraphicsUnit.Pixel;
			foreach (CharRenderObject obj in allObjects)
			{
				string str = $"{obj.character}";
				SizeF stringSize = graphics.MeasureString(str, font);

				Rectangle rotated_bounds = new Rectangle(
					-(int)stringSize.Width / 2, -(int)stringSize.Height / 2, (int)stringSize.Width, (int)stringSize.Height);

				// Rotate.
				graphics.ResetTransform();
				graphics.RotateTransform(obj.rotation);

				// Translate to move the rectangle to the correct position.
				graphics.TranslateTransform((int)(obj.position.X + stringSize.Width), (int)(obj.position.Y + stringSize.Height),
					MatrixOrder.Append);
				// Draw the text.
				graphics.DrawString(str, font, brushes, rotated_bounds);
			}
		}

		void IterateAllObjects()
		{
			stopwatch.Stop();
			long deltaTime = stopwatch.ElapsedTicks;
			stopwatch.Start();
			foreach(CharRenderObject obj in allObjects)
			{
				obj.Iterate(deltaTime);
				if (obj.position.X > -outOfBoundsLimit && obj.position.X < Width + outOfBoundsLimit && obj.position.Y > -outOfBoundsLimit && obj.position.Y < Height + outOfBoundsLimit)
				{
					allObjects.Remove(obj);
				}
			}
		}

		private void PhysicsTimer_Tick(object sender, EventArgs e)
		{
			IterateAllObjects();
			Refresh();
		}
	}
}
