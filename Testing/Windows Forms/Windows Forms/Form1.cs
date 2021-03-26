using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Windows_Forms
{
	public partial class FormUno : Form
	{
		private System.Collections.Generic.List<System.Windows.Forms.ToolStripMenuItem> menuItemList = new System.Collections.Generic.List<System.Windows.Forms.ToolStripMenuItem>();
		private List<Label> wowLabels = new List<Label>();
		Random rand = new Random();

		public FormUno()
		{
			InitializeComponent();
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.fileToolStripMenuItem1});
			this.fileToolStripMenuItem.Name = "File0";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			this.fileToolStripMenuItem.DropDownOpening += new System.EventHandler(this.fileToolStripMenuItem_DropDownOpening);
			this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
			// 
			// fileToolStripMenuItem1
			// 
			this.fileToolStripMenuItem1.Name = "File1";
			this.fileToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
			this.fileToolStripMenuItem1.Text = "File";
			this.fileToolStripMenuItem1.DropDownOpening += new System.EventHandler(this.fileToolStripMenuItem_DropDownOpening);
			this.menuItemList.Add(this.fileToolStripMenuItem);
			this.menuItemList.Add(this.fileToolStripMenuItem1);

			this.timer2.Interval = 10;
			this.timer2.Tick += new System.EventHandler(this.picture_Tick);
			this.timer2.Enabled = true;
		}

		private void bigbutton_Click(object sender, EventArgs e)
		{
			
			this.BackColor = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			timer1.Enabled = true;
		}

		private void fileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			
		}



		private void fileToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
		{
			ToolStripMenuItem item = new ToolStripMenuItem();
			int count = menuItemList.Count;
			item.Name = $"File{count}";
			item.Size = new System.Drawing.Size(180, 22);
			item.Text = "File";
			item.DropDownOpening += new System.EventHandler(this.fileToolStripMenuItem_DropDownOpening);
			menuItemList.Add(item);
			menuItemList[count - 1].DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			menuItemList[count]});
			
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (checkBox1.Checked)
			{
				Label label = new Label();
				label.Text = "Wow!";

				label.AutoSize = true;
				label.Location = new Point(rand.Next(0, this.Size.Width), rand.Next(0, this.Size.Height));
				label.Size = new System.Drawing.Size(35, 13);
				label.TabIndex = 100;
				wowLabels.Add(label);
				this.Controls.Add(wowLabels[wowLabels.Count - 1]);
			}
			else if (wowLabels.Count > 0)
			{
				this.Controls.Remove(wowLabels[wowLabels.Count - 1]);
				wowLabels.RemoveAt(wowLabels.Count - 1);
			}
			
		}
		
		private System.Windows.Forms.Timer timer2 = new Timer();
		
		private void pictureBox1_Click(object sender, EventArgs e)
		{
			if (!timer2.Enabled)
			{
				
			}
			else
			{
				//timer2.Enabled = false;
				//pictureBox1.Image = null;
			}
		}

		private void picture_Tick(object sender, EventArgs e)
		{
			if (Form.ActiveForm != this)
				return;
			Control c = this.ActiveControl;


			Rectangle capture = new Rectangle(this.Bounds.X + 7, this.Bounds.Y + 7, Bounds.Width - 14, Bounds.Height - 14);

			Bitmap bmp = new Bitmap(capture.Width, capture.Height);

			Graphics captureGraphics = Graphics.FromImage(bmp);
			captureGraphics.RotateTransform(40);

			captureGraphics.CopyFromScreen(capture.Left, capture.Top, 0, 0, capture.Size);

			bigbutton.BackgroundImage = bmp;
			bigbutton.BackgroundImageLayout = ImageLayout.Stretch;
		}

		private void pictureBox1_Paint(object sender, PaintEventArgs e)
		{

		}
	}
}
