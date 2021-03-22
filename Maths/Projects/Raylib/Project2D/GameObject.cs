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
		#region Variables

		const float convertToDegrees = (float)(180 / Math.PI);
		
		//scene tree stuff
		protected GameObject parent = null;
		protected List<GameObject> children = new List<GameObject>();

		//matrices
		protected Matrix3 localTransform = new Matrix3();
		protected Matrix3 globalTransform = new Matrix3();

		//drawing
		protected bool isDrawn = false;
		private Texture2D texture;
		private Colour colour;
		Rectangle spriteRectangle = new Rectangle();
		Rectangle textureRectangle = new Rectangle();
		RLVector2 origin = new RLVector2();

		//local information
		public Vector2 position;
		public float rotation;
		Vector2 scale;

		//physics
		protected bool hasPhysics = false;
		protected Collider collider = null;
		#endregion

		#region Initiation
		public GameObject()
		{
			isDrawn = false;
			hasPhysics = false;
			collider = null;


			Init(null, Vector2.Zero, Vector2.One, 0, null, new Colour());
		}

		public GameObject(string fileName, Vector2 position, Vector2 scale, float rotation = 0, GameObject parent = null)
		{
			Init(fileName, position, scale, rotation, parent, new Colour());
		}

		public GameObject(string fileName)
		{
			Init(fileName, Vector2.Zero, Vector2.One, 0, null, new Colour());
		}

		void Init(string textureFile, Vector2 position, Vector2 scale, float rotation, GameObject parent, Colour colour)
		{
			if (textureFile != null)
			{
				isDrawn = true;
				texture = LoadTexture(textureFile);

				textureRectangle.width = texture.width;
				textureRectangle.height = texture.height;
				spriteRectangle.width = texture.width * scale.x;
				spriteRectangle.height = texture.height * scale.y;
				origin.x = spriteRectangle.width / 2;
				origin.y = spriteRectangle.height / 2;
			}
			if (parent != null)
				parent.addChild(this);

			//position and scale will be zero if no values are given
			localTransform = Matrix3.GetTranslation(position) * Matrix3.GetRotateZ(rotation) * Matrix3.GetScale(scale);
			UpdateTransforms();

			this.position = position;
			this.rotation = rotation;
			this.scale = scale;

		}
		#endregion

		#region Scene Tree Methods
		protected void addChild(GameObject child)
		{
			children.Add(child);
			child.SetParent(this);
		}

		public void SetParent(GameObject parent)
		{
			if (parent != null)
			{
				RemoveChild(this);
			}
			this.parent = parent;
		}

		void RemoveChild(GameObject child)
		{
			children.Remove(child);
		}

		public void Delete()
		{
			foreach (var child in children)
			{
				child.Delete();
			}
			parent.RemoveChild(this);
		}
		#endregion

		public virtual void Update()
		{

			foreach(var child in children)
			{
				child.Update();
			}
		}
		
		public virtual void Draw()
		{
			

			if (isDrawn)
			{
				
				spriteRectangle.width = texture.width * (float)Math.Sqrt(globalTransform.m11 * globalTransform.m11 + globalTransform.m21 * globalTransform.m21);
				spriteRectangle.height = texture.height * (float)Math.Sqrt(globalTransform.m12 * globalTransform.m12 + globalTransform.m22 * globalTransform.m22);
				origin.x = spriteRectangle.width / 2;
				origin.y = spriteRectangle.height / 2;

				spriteRectangle.x = globalTransform.m13 - spriteRectangle.width / 2;
				spriteRectangle.y = globalTransform.m23 - spriteRectangle.height / 2;
				DrawTexturePro(texture, textureRectangle, spriteRectangle, origin, (float)Math.Atan2(globalTransform.m21, globalTransform.m11) * convertToDegrees, RLColor.WHITE);
				
			}


			foreach (var child in children)
			{
				child.Draw();
			}
		}


		public virtual void UpdateTransforms()
		{
			localTransform = Matrix3.GetTranslation(position) * Matrix3.GetRotateZ(rotation) * Matrix3.GetScale(scale);

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

		public void SetGlobalRotation(float rad)
		{
			//localTransform = Matrix3.GetTranslation(position) * Matrix3.GetRotateZ(rad - (float)Math.Atan2(parent.GetGlobalTransform().m21, parent.GetGlobalTransform().m11)) * Matrix3.GetScale(scale);
		}

		public void SetRotation(float rad) 
		{
			rotation = rad;
			UpdateTransforms();
		}
		public void AddRotation(float rad)
		{
			rotation += rad;
			UpdateTransforms();
		}

		public void SetPosition(Vector2 pos)
		{
			position = pos;
			UpdateTransforms();
		}
		public void AddPosition(Vector2 pos)
		{
			position += pos;
			UpdateTransforms();
		}





	}
}
