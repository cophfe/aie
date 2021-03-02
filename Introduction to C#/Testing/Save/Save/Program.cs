using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Save
{
	class Program
	{
		static void Main(string[] args)
		{
			CreateSaveData();
			Save();

			SaveData ld = Load();
			
			
			Console.ReadLine();
		}

		private static SaveData saveData = null;

		static void CreateSaveData()
		{
			saveData = new SaveData();

			saveData.health = 10;
			saveData.playerName = "Joe";
			saveData.playerPos.x = 40.4f;
			saveData.playerPos.y = 60.2f;

			saveData.enemyHealthList = new int[3];
			saveData.enemyNameList = new string[3];
			saveData.enemyPosList = new Vector2[3];

			for (int i = 0; i < 3; i++)
			{
				saveData.enemyHealthList[i] = i * 10;
				saveData.enemyNameList[i] = $"{(i * 10)}";
				saveData.enemyPosList[i].x = 10 - i;
				saveData.enemyPosList[i].y = i - 10;
			}

		}
		
		static void Save()
		{
			using (FileStream fS = new FileStream("Save.teletubby", FileMode.OpenOrCreate))
			{
				BinaryFormatter bF = new BinaryFormatter();

				try
				{
					bF.Serialize(fS, saveData);

				}
				catch (Exception e)
				{
					Console.WriteLine($"Error Saving: {e.Message}");
				}
				finally
				{
					fS.Close();
				}

			}

		}

		static SaveData Load()
		{
			SaveData loadedData = null;

			using(FileStream fS = new FileStream("Save.teletubby", FileMode.Open))
			{
				BinaryFormatter bF = new BinaryFormatter();

				try
				{
					loadedData = (SaveData)bF.Deserialize(fS);
				}
				catch (Exception e)
				{
					Console.WriteLine($"Error Reading: {e.Message}");
					loadedData = null;
				}
				finally
				{
					fS.Close();
				}
			}
			return loadedData;
		}
	}

	[Serializable]
	class SaveData
	{
		//do not save classes, use structs. Classes may not save properly

		//Player
		public string playerName;
		public Vector2 playerPos;
		public int health;

		//Enemies
		public string[] enemyNameList;
		public Vector2[] enemyPosList;
		public int[] enemyHealthList;
		

	}

	[Serializable]
	struct Vector2
	{
		public float x, y;
	}
}
