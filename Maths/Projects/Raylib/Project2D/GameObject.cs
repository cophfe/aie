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
		protected Vector2 position;
		protected Vector2 scale;
		protected float rotation;

		//physics
		protected bool hasPhysics = false;
		protected Collider collider = null;

		//id
		static ulong idCounter = 0;
		#endregion

		#region Initiation
		public GameObject()
		{
			
			isDrawn = false;
			hasPhysics = false;
			collider = null;


			Init(null, Vector2.Zero, Vector2.One, 0, null, new Colour(0xFF, 0xFF, 0xFF, 0xFF));
		}

		public GameObject(string fileName, Vector2 position, Vector2 scale, float rotation = 0, GameObject parent = null)
		{
			Init(fileName, position, scale, rotation, parent, new Colour(0xFF, 0xFF, 0xFF, 0xFF));
		}

		public GameObject(string fileName)
		{
			Init(fileName, Vector2.Zero, Vector2.One, 0, null, new Colour(0xFF, 0xFF, 0xFF, 0xFF));
		}

		void Init(string textureFile, Vector2 position, Vector2 scale, float rotation, GameObject parent, Colour colour)
		{
			id = idCounter;
			idCounter++;
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
			this.position = position;
			this.rotation = rotation;
			this.scale = scale;
			
			this.colour = colour;
			UpdateTransforms();

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
			UnloadTexture(texture);
		}
		#endregion

		public virtual void Update(float deltaTime)
		{

			foreach(var child in children)
			{
				child.Update(deltaTime);
			}
		}

		protected float id;

		public float Id
		{
			get
			{
				return id;
			}
		}

		private Vector2 globalPosition;
		private float globalRotation;
		private Vector2 globalScale;

		public virtual void Draw()
		{

			if (isDrawn)
			{
				globalTransform.GetAllTransformations(ref globalPosition, ref globalScale, ref globalRotation);
				
				spriteRectangle.width = texture.width * globalScale.x;
				spriteRectangle.height = texture.height * globalScale.y;
				origin.x = spriteRectangle.width / 2;
				origin.y = spriteRectangle.height / 2;

				spriteRectangle.x = globalPosition.x;
				spriteRectangle.y = globalPosition.y;

				DrawTexturePro(texture, textureRectangle, spriteRectangle, origin, globalRotation * convertToDegrees, colour);
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

		

		public void SetRotation(float rad) 
		{
			rotation = rad;
		}
		public void AddRotation(float rad)
		{
			rotation += rad;
		}

		public void SetPosition(Vector2 pos)
		{
			position = pos;
		}
		public void AddPosition(Vector2 pos)
		{
			position += pos;
		}

		public Vector2 LocalPosition
		{
			get
			{
				return position;
			}
			set
			{
				position = value;
			}
		}

		public Vector2 GlobalPosition
		{
			get
			{
				return globalTransform.GetTranslation();
			}
			set
			{
				Matrix3 changes = new Matrix3(m31: value.x, m32: value.y);
				changes = changes * globalTransform.Inverse();
				changes = new Matrix3(m31: changes.m31, m32: changes.m32);
				position = (changes).GetTranslation();
			}
		}

		public float GlobalRotation
		{
			get
			{
				return globalTransform.GetZRotation();
			}
			set
			{
				//value
			}
		}

		public float LocalRotation
		{
			get
			{
				return rotation;
			}
			set
			{

			}
		}



	}
}
