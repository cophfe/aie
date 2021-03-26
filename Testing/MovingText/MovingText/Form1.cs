using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace MovingText
{
	public partial class Form1 : Form
	{
		Timer gameTimer = null;
		
		public Form1()
		{
			InitializeComponent();

			gameTimer = new Timer();
			gameTimer.Interval = 10;//16;
			gameTimer.Tick += Update;
			gameTimer.Start();
		}

		public void Update(object sender, EventArgs e)
		{
			
			Refresh();
		}

		Random rand = new Random();
		List<CharInfo> characters = new List<CharInfo>(30);
		Brush b = Brushes.Black;
		Font f = new Font(FontFamily.GenericMonospace, 10f, FontStyle.Regular);
		Graphics g;
		const int outOfBoundsLimit = 50;
		Vector2 acceleration = new Vector2(0,-10);
		const float speedMultiplier = 5;
		const float angularSpeedMultiplier = 5;

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			
			//should dispose these at the end of the program
			g = e.Graphics;

			if (characters != null)
				for (int i = 0; i < characters.Count; i++)
				{
					if (characters[i] == null)
						continue;
					if(characters[i].position.X > -outOfBoundsLimit && characters[i].position.X < Width + outOfBoundsLimit && characters[i].position.Y > -outOfBoundsLimit && characters[i].position.Y < Height + outOfBoundsLimit)
					{
						characters[i].position += characters[i].velocity;
						characters[i].startAngle += characters[i].angularVelocity;
						DrawText(f, b, g, characters[i].character, characters[i].startAngle, characters[i].position);
						characters[i].velocity.Y += 0.1f;
					}
					else
					{
						characters[i] = null;
					}
				}


		}

		class CharInfo
		{
			public Vector2 velocity;
			public char character;
			public Vector2 position;
			public float startAngle;
			public float angularVelocity;
			public Vector2 acceleration;

			public CharInfo(Vector2 velocity, char character, Vector2 position, float angle, float angularVelocity)
			{
				this.velocity = velocity;
				this.character = character;
				this.position = position;
				this.startAngle = angle;
				this.angularVelocity = angularVelocity;
			}
		}

		void DrawText(Font font, Brush brush, Graphics g, char character, float angle, Vector2 position)
		{
			string str = $"{character}";
			g.PageUnit = GraphicsUnit.Pixel;
			SizeF stringSize = g.MeasureString(str, font);
			
			Rectangle rotated_bounds = new Rectangle(
				-(int)stringSize.Width/2, -(int)stringSize.Height/2, (int)stringSize.Width, (int)stringSize.Height);

			// Rotate.
			g.ResetTransform();
			g.RotateTransform(angle);

			// Translate to move the rectangle to the correct position.
			g.TranslateTransform((int)(position.X + stringSize.Width), (int)(position.Y +stringSize.Height),
				MatrixOrder.Append);
			// Draw the text.
			g.DrawString(str, font, brush, rotated_bounds);
		}

		
		private void button1_Click(object sender, EventArgs e)
		{
			Vector2 pos = new Vector2(button1.Location.X + button1.Width/3, button1.Location.Y - button1.Height/2);
			float angle = (float)(rand.NextDouble() * 60);
			for (int i = 0; i < 20; i++)
			{
				characters.Add(new CharInfo(new Vector2(((float)rand.NextDouble() - rand.Next(0, 2) )* speedMultiplier , ((float)rand.NextDouble() - rand.Next(0, 2)) * speedMultiplier), (char)rand.Next(60,80), pos, angle, (float)rand.NextDouble()*angularSpeedMultiplier));
			}
		}



		private void Form1_MouseMove(object sender, MouseEventArgs e)
		{
			
		}
	}
}
