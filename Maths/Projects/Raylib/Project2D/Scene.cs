using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mlib;

namespace Project2D
{
	class Scene : GameObject
	{
		
		public Scene(List<GameObject> gameObjects)
		{
			globalTransform = Matrix3.Identity;
			foreach (GameObject kid in gameObjects)
			{
				addChild(kid);
			}
		}

		public override void Update() //currently the same as in GameObject
		{
			foreach( var child in children)
			{
				child.Update();
			}
		}

		public override void UpdateTransforms()
		{
			foreach (var child in children)
			{
				child.UpdateTransforms();
			}
		}

		public override void Draw()
		{
			foreach (var child in children)
			{
				child.Draw();
			}
		}

		public void IteratePhysics(float deltaTime)
		{
			foreach (var child in children)
			{
				if (child is PhysicsObject)
					((PhysicsObject)child).Iterate(deltaTime);
			}
		}


	}

}
