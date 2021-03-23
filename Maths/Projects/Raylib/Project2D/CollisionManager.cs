using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2D
{
	static class CollisionManager
	{
		public static List<PhysicsObject> objList = new List<PhysicsObject>();

		public static void AddObject(PhysicsObject obj)
		{
			objList.Add(obj);
		}

		public static void CheckCollision()
		{
			for (int i = 0; i < objList.Count -1; i++)
			{
				for (int j = i+1; j < objList.Count; j++)
				{

				}
			}
			//foreach(var first in objList)
			//{
			//	foreach (var second in objList)
			//	{
			//		if (first.Id == second.Id)
			//			return;

			//		//
			//	}
			//}
		}
	}
}
