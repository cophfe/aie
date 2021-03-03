using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsText
{
	class Run
	{
		const float outOfBoundsLimit = 50;
		public float defaultGravity = 400;
		public float defaultMass = 1;
		public List<CharRenderObject> allObjects = new List<CharRenderObject>();
		public new List<CharRenderObject> scheduledToAdd = new List<CharRenderObject>();
		public List<int> scheduledToRemove = new List<int>();
		public int width, height;
		Stopwatch stopwatch = new Stopwatch();
		public bool isRunning = true;
		public bool isSimulating = true;
		public bool isCleaning = true;
		double lastTime;
		double nowTime;
		float deltaTime;

		public async void Start()
		{
			stopwatch.Start();
			CharRenderObject cRO = CharRenderObject.CreateCharObject('A', 0, new Vector2(50, 50), Vector2.One, new Size(40, 40), 1, 4, Vector2.Zero, 1, defaultGravity, 1);

			allObjects.Add(cRO);

			await Task.Run(() =>
			{
				while (isRunning)
				{
					if (isCleaning)
						Clean();
					if (isSimulating)
						IterateAllObjects();
				}
			});

		}

		void Clean()
		{
			int removeAmount = scheduledToRemove.Count, addAmount = scheduledToAdd.Count;
			
			if (removeAmount > 0)
			{
				for (int i = removeAmount - 1; i > -1 && isCleaning; i--)
				{
					allObjects[scheduledToRemove[i]] = null;
					scheduledToRemove.RemoveAt(i);
				}
			}
			if (addAmount > 0)
			{
				for (int i = addAmount - 1; i > -1 && isCleaning; i--)
				{
					allObjects.Add(scheduledToAdd[i]);
					scheduledToAdd.RemoveAt(i);
				}
			}
		}

		void IterateAllObjects()
		{
			
			for (int i = 0; i < allObjects.Count;i++)
			{

				if (allObjects[i] == null)
					continue;
				if (!(allObjects[i].position.X > -outOfBoundsLimit && allObjects[i].position.X < width + outOfBoundsLimit && allObjects[i].position.Y > -outOfBoundsLimit && allObjects[i].position.Y < height + outOfBoundsLimit))
				{
					scheduledToRemove.Add(i);
				}
				if (allObjects[i].isSimulated)
				{
					allObjects[i].Iterate(deltaTime);
				}
			}

			nowTime = stopwatch.Elapsed.TotalMilliseconds;
			deltaTime = (float)(nowTime - lastTime)/1000; //(in seconds)
			lastTime = nowTime;
		}
	}
}
