using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mlib;
using Raylib;
using static Raylib.Raylib;

namespace Project2D
{
	class GameObject
	{
		protected GameObject parent = null;
		protected List<GameObject> children = new List<GameObject>();

		protected Matrix3 localTransform = new Matrix3();
		protected Matrix3 globalTransform = new Matrix3();

		protected bool hasPhysics;
		protected bool isDrawn;

		protected Collider collider;

		private Image sprite; //probably dont need this
		private Texture2D texture;
		private Rectangle textureBounds;
		private Rectangle spriteRect;
		private Colour colour;
		Vector2 size;

		RLVector2 origin;
		float rotation;

		public GameObject()
		{

		}

		public GameObject(string fileName)
		{
			isDrawn = true;
			sprite = LoadImage(fileName);
			//texture = LoadTexture(fileName);
			texture = LoadTextureFromImage(sprite);

			
			
			textureBounds.height = texture.height;
			textureBounds.width = texture.height;

			spriteRect.width = texture.width;
			spriteRect.height = texture.height;

			origin.x = spriteRect.width / 2;
			origin.y = spriteRect.height / 2;
		}

		public virtual void Update()
		{
			foreach(var child in children)
			{
				child.Update();
			}
		}

		public virtual void Draw()
		{
			if (!isDrawn)
				return;

			rotation = (float)Math.Acos(globalTransform.m[0]);
			//Vector2 position = globalTransform.GetTranslation();
			
			origin.x = globalTransform.m[2];
			origin.y = globalTransform.m[5];

			
			spriteRect.x = origin.x;
			spriteRect.y = origin.y;
			

			DrawTexturePro(texture, textureBounds, spriteRect, origin, rotation, RLColor.BLANK);
			
			//{
			//	DrawTexture(texture, (int)position.x, (int)position.y, RLColor.WHITE);
			//}

			foreach(var child in children)
			{
				child.Draw();
			}
		}

		public void SetPosition(Vector3 pos)
		{
			localTransform.SetTranslation(pos);
		}

		public void SetRotation(float rad)
		{
			Matrix3 rotation = Matrix3.GetRotateZ(rad);

			localTransform = localTransform * rotation;
		}

		public virtual void UpdateTransforms()
		{
			if (parent == null)
			{
				globalTransform = localTransform;
			}
			else
			{
				globalTransform = parent.GetGlobalTransform() * localTransform;
			}
			foreach(var child in children)
			{
				child.UpdateTransforms();
			}
		}

		public Matrix3 GetGlobalTransform()
		{
			return globalTransform;
		}

		public void Delete()
		{
			foreach (var child in children)
			{
				child.Delete();
			}
			parent.children.Remove(this);
		}

		
	}
}
