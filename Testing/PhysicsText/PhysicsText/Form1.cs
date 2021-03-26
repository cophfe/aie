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

		Run run;

		public MainForm()
		{
			InitializeComponent();
			characterSize = CharRenderObject.FindMonospaceCharSizeFromFont(font, CreateGraphics());
			run = new Run();
			run.width = Width;
			run.height = Height;
			run.isSimulating = false;
			run.Start();

			RenderTimer_Tick(null,null);
		}


		

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			if (run.allObjects.Count > 0)
				DrawAllObjects(e.Graphics, run.allObjects);
		}

		
		Brush brushes = Brushes.Black;
		Font font = new Font(FontFamily.GenericMonospace, 10f, FontStyle.Regular);
		SizeF characterSize;
		
		void DrawAllObjects(Graphics graphics, List<CharRenderObject> allObjects)
		{
			graphics.PageUnit = GraphicsUnit.Pixel;
			run.isCleaning = false;
			for (int i = 0; i < allObjects.Count; i++)
			{
				if (allObjects[i] == null)
					continue;
				string str = $"{allObjects[i].character}";
				SizeF stringSize = graphics.MeasureString(str, font);

				Rectangle rotated_bounds = new Rectangle(
					-(int)stringSize.Width / 2, -(int)stringSize.Height / 2, (int)stringSize.Width, (int)stringSize.Height);

				// Rotate.
				graphics.ResetTransform();
				graphics.RotateTransform(allObjects[i].rotation);

				// Translate to move the rectangle to the correct position.
				graphics.TranslateTransform((int)(allObjects[i].position.X + stringSize.Width), (int)(allObjects[i].position.Y + stringSize.Height),
					MatrixOrder.Append);

				// Draw the text.
				graphics.DrawString(str, font, brushes, rotated_bounds);
			}
			run.isCleaning = true;
		}

		private void RenderTimer_Tick(object sender, EventArgs e)
		{
			Invalidate();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			run.isSimulating = true;
		}

		Random rand = new Random();
		const float speedMultiplier = 200, angularSpeedMultiplier = 2;
		private void button1_MouseClick(object sender, MouseEventArgs e)
		{
			
			Vector2 pos = new Vector2(button1.Location.X + button1.Width / 3, button1.Location.Y - button1.Height / 2);
			float angle = (float)(rand.NextDouble() * 60);
			run.isCleaning = false; //CRASHES WITHOUT THIS!!!! (run uses allObjects too so changing its length while run it is in a for loop creates an error)
			for (int i = 0; i < 20; i++)
			{
				run.scheduledToAdd.Add(CharRenderObject.CreateCharObject((char)rand.Next(60, 80), angle, pos, Vector2.One, characterSize, 1, (float)rand.NextDouble() * angularSpeedMultiplier, new Vector2(((float)rand.NextDouble() - rand.Next(0, 2)) * speedMultiplier, ((float)rand.NextDouble() - rand.Next(0, 2)) * speedMultiplier),  1, run.defaultGravity, 1));

			}
			run.isCleaning = true;
		}
	}
}
