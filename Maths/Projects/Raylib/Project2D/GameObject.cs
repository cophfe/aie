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

		//private Image sprite; //probably dont need this
		private Texture2D texture;
		private Colour colour;
		float scale = 1;

		RLVector2 position;
		float rotation;

		public GameObject()
		{
			isDrawn = false;
			hasPhysics = false;
			collider = null;
			colour = new Colour();

			r1.width = texture.width;
			r1.height = texture.height;
			r2.width = texture.width * scale;
			r2.height = texture.height * scale;
			origin.x = r2.width / 2;
			origin.y = r2.height / 2;
		}

		public GameObject(string fileName, Vector2 position, float rotation = 0, float scale = 1, GameObject parent = null)
		{
			if (fileName != null)
			{
				isDrawn = true;
				texture = LoadTexture(fileName);
			}
			if (parent != null)
				parent.addChild(this);
				
			localTransform = Matrix3.GetRotateZ(rotation) * Matrix3.GetTranslation(position) * Matrix3.GetScale(scale,scale);
			UpdateTransforms();

			r1.width = texture.width;
			r1.height = texture.height;
			r2.width = texture.width * scale;
			r2.height = texture.height * scale;
			origin.x = r2.width / 2;
			origin.y = r2.height / 2;
		}

		public GameObject(string fileName)
		{
			isDrawn = true;
			//sprite = LoadImage(fileName);
			texture = LoadTexture(fileName);
			//texture = LoadTextureFromImage(sprite);
			
			scale = 1;
			r1.width = texture.width;
			r1.height = texture.height;
			r2.width = texture.width * scale;
			r2.height = texture.height * scale;
			origin.x = r2.width / 2;
			origin.y = r2.height / 2;

		}

		void addChild(GameObject child)
		{
			children.Add(child);
			child.SetParent(this);
		}

		void SetParent(GameObject parent)
		{
			this.parent = parent;
		}

		public virtual void Update()
		{
			localTransform *= Matrix3.GetRotateZ(0.001f);
			foreach(var child in children)
			{
				child.Update();
			}
		}


		Rectangle r2 = new Rectangle();
		Rectangle r1 = new Rectangle();
		float i = 0;

		RLVector2 origin = new RLVector2();
		public virtual void Draw()
		{
			foreach (var child in children)
			{
				child.Draw();
			}

			if (!isDrawn)
				return;

			position = Renderer.ToRLVector2(globalTransform.GetTranslation());

			
			
			r2.x = position.x - r2.width/2;
			r2.y = position.y - r2.height / 2;
			//DrawTextureEx(texture, Renderer.ToRLVector2(globalTransform.GetTranslation()), 0, scale, RLColor.RED);
			DrawTexturePro(texture, r1, r2, origin, i, RLColor.WHITE) ;
			i+= 0.01f;
			localTransform.SetTranslation((float)Math.Sin(i/10)*40 + 300, (float)Math.Cos(i/10)*40 + 300);
			//Console.WriteLine(Math.Atan2(globalTransform.m[2], globalTransform.m[3]) / Math.PI * 180);
			
			//DrawTexturePro(texture, (Rectangle){ 0,0, texture.width, texture.height}, (Rectangle){ 0,0, texture.width, texture.height}, )

			
		}

		public void SetPosition(Vector2 pos)
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
