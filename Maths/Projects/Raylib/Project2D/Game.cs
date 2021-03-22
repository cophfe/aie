using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using static Raylib.Raylib;
using Mlib;

namespace Project2D
{
    class Game
    {
        Stopwatch stopwatch = new Stopwatch();

        private long currentTime = 0;
        private long lastTime = 0;
        private float timer = 0;
        private int fps = 1;
        private int frames;

        private float deltaTime = 0.005f;

        //Scenes are gameobjects that hold every other gameobject
        List<Scene> scenes = new List<Scene>();
        int currentScene = 0;

        public Game()
        {
            
        }

        GameObject gameSprite;
        GameObject game;
        GameObject gameSprite2;

        public void Init()
        {
            stopwatch.Start();
            lastTime = stopwatch.ElapsedMilliseconds;

            if (Stopwatch.IsHighResolution)
            {
                Console.WriteLine("Stopwatch high-resolution frequency: {0} ticks per second", Stopwatch.Frequency);
            }

            //Initialize objects here
            game = new GameObject("../Images/face.png", new Vector2(350, 340), Vector2.One, 0);
            gameSprite = new GameObject("../Images/download.jpg", new Vector2(0, 200), new Vector2(0.5f, 0.5f), 0, game);
            gameSprite2 = new GameObject("../Images/blue.png", new Vector2(0, 200), Vector2.One, 0, gameSprite);
            List<GameObject> l = new List<GameObject>();
            l.Add(game);
            scenes.Add(new Scene(l));
            //scenes[0].Initialise();
		}

        public void Shutdown()
        {
        }

        public void Update()
        {
            lastTime = currentTime;
            currentTime = stopwatch.ElapsedMilliseconds;
            deltaTime = (currentTime - lastTime) / 1000.0f;
            timer += deltaTime;
            if (timer >= 1)
            {
                fps = frames;
                frames = 0;
                timer -= 1;
            }
            frames++;

            //Update game objects here       
            
            scenes[currentScene].Update(); //per frame
            scenes[currentScene].IteratePhysics(deltaTime); //based on deltaTime
            game.AddRotation(0.001f);
            gameSprite.SetGlobalRotation(0);
        }

        public void Draw()
        {
            BeginDrawing();

            ClearBackground(RLColor.WHITE);

            //Draw game objects here
            
            scenes[currentScene].UpdateTransforms();
            scenes[currentScene].Draw();

            
            DrawText(fps.ToString(), 10, 10, 14, RLColor.RED);
            EndDrawing();
        }

    }
}
