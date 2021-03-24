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
        Player player;
        GameObject playerChild;
        GameObject playerChild2;

        public void Init()
        {
            stopwatch.Start();
            lastTime = stopwatch.ElapsedMilliseconds;

            if (Stopwatch.IsHighResolution)
            {
                Console.WriteLine("Stopwatch high-resolution frequency: {0} ticks per second", Stopwatch.Frequency);
            }

            //Initialize objects here
            game = new GameObject("../Images/face.png", new Vector2(350, 340), new Vector2(0.5f, 0.5f), 0);
            gameSprite = new GameObject("../Images/blue.png", new Vector2(300, 0), new Vector2(0.25f, 0.25f), 0, game);
            gameSprite2 = new GameObject("../Images/paint.png", new Vector2(0, 300), Vector2.One, 0, gameSprite);

            List<GameObject> l = new List<GameObject>();
            l.Add(game);
            scenes.Add(new Scene(l));
            player = new Player("../Images/player.png", new Vector2(250, 250), new Vector2(0.2f, 0.2f), 0, scenes[0]);
            playerChild = new GameObject("../Images/paint.png", new Vector2(0, 0), new Vector2(2f, 2f), 0, player);
            playerChild2 = new GameObject("../Images/paint.png", new Vector2(0, 500), Vector2.One /2, 0, playerChild);
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
            
            scenes[currentScene].Update(deltaTime); //per frame
            scenes[currentScene].UpdateTransforms();
            //game.AddRotation((float)Math.Cos(currentTime/ 1000.0f)/1000);
            //gameSprite.AddRotation((float)Math.Sin(currentTime/ 1000.0f)/ 1000);
            //game.SetPosition(new Vector2((float)Math.Sin(currentTime / 1000.0f) * 20 + 350, (float)Math.Cos(currentTime / 1000.0f) * 20 + 340));
            playerChild.AddRotation(5 * deltaTime);
            Vector2 m = GetMousePosition();
            gameSprite.GlobalPosition = m;
            
            //gameSprite.SetRotation((float)Math.Cos(currentTime/ 1000.0f));
            //gameSprite.SetGlobalRotation(0);
        }

        public void Draw()
        {
            BeginDrawing();

            ClearBackground(RLColor.WHITE);

            //Draw game objects here
            
            
            scenes[currentScene].Draw();
            Vector2 pos = player.GetGlobalTransform().GetTranslation();
            Vector2 m = pos + player.GetVelocity();
            DrawLine((int)m.x, (int)m.y, (int)pos.x, (int)pos.y, RLColor.BLACK);

            DrawText(fps.ToString(), 10, 10, 14, RLColor.RED);
            EndDrawing();
        }

    }
}
