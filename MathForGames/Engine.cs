using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Engine
    {
        private static bool _applicationShouldClose = false;
        private static int _currentSceneIndex;
        private Scene[] _scenes = new Scene[0];
        private Stopwatch _stopwatch = new Stopwatch();

        /// <summary>
        /// Called to begin the application
        /// </summary>
        public void Run()
        {
            //Called start for the entire application
            Start();

            float currentTime = 0;
            float lastTime = 0;
            float deltaTime = 0;

            //Loop until the application is told close
            while (!_applicationShouldClose && !Raylib.WindowShouldClose())
            {
                //Get how much time has passed since the application started
                currentTime = _stopwatch.ElapsedMilliseconds / 1000.0f;

                //Set delta time to be the difference in time from the last time to the current time
                deltaTime = currentTime - lastTime;

                //Update the application
                Update(deltaTime);
                //Draw all items
                Draw();

                //Set the last time recorded to be the current time
                lastTime = currentTime;
            }

            //Call end for the entire application
            End();
        }

        /// <summary>
        /// Called when the application starts
        /// </summary>
        private void Start()
        {
            _stopwatch.Start();

            //Create a window using Raylib
            Raylib.InitWindow(800, 450, "Math For Games");
            Raylib.SetTargetFPS(60);

            Scene scene = new Scene();
            
            Player player = new Player(30, 50, 100, "Player", "Images/player.png");
            player.SetScale(50, 50);
            player.SetTranslation(500, 300);             
            CircleCollider playerCircleCollider = new CircleCollider(15, player);
            AABBCollider playerBoxCollider = new AABBCollider(40, 40, player);
            player.Collider = playerCircleCollider;
        
            // Solar system
            Planet sun = new Planet(30, 50, 1, "Sun", "Images/bullet.png");
            sun.SetScale(100, 100);
            sun.SetTranslation(400, 225);
            CircleCollider sunCircleCollider = new CircleCollider(15, sun);
            sun.Collider = sunCircleCollider;

            Planet planet1 = new Planet(20, 30, 1, "Planet1", "Images/bullet.png");
            planet1.SetScale(1, 1);
            planet1.SetTranslation(1, 1);

            Planet planet2 = new Planet(20, 30, 1, "Planet2", "Images/bullet.png");
            planet2.SetScale(0.5f, 0.5f);
            planet2.SetTranslation(1, 0);

            sun.AddChild(planet1);
            planet1.AddChild(planet2);

            scene.AddActor(sun);
            scene.AddActor(planet1);
            scene.AddActor(planet2);

            _currentSceneIndex = AddScene(scene);

            _scenes[_currentSceneIndex].Start();

            Console.CursorVisible = false;
        }
        
        /// <summary>
        /// Called everytime the gane loops
        /// </summary>
        private void Update(float deltaTime)
        {
            _scenes[_currentSceneIndex].Update(deltaTime);
            
            while (Console.KeyAvailable)
                Console.ReadKey(true);
        }

        /// <summary>
        /// Called every time the game loops to update visuals
        /// </summary>
        private void Draw()
        {
            Raylib.BeginDrawing();

            Raylib.ClearBackground(Color.BLACK);

            //Adds all actor icons to buffer
            _scenes[_currentSceneIndex].Draw();
            _scenes[_currentSceneIndex].DrawUI();

            Raylib.EndDrawing();
        }

        /// <summary>
        /// Called when the application ends
        /// </summary>
        private void End()
        {
            _scenes[_currentSceneIndex].End();
            Raylib.CloseWindow();
        }

        /// <summary>
        /// Adds a scene to the engine's scene array
        /// </summary>
        /// <param name="scene">The scene that will be added to the scene array</param>
        /// <returns>The index where the new scene is located</returns>
        public int AddScene(Scene scene)
        {
            //Creates a new temporary array
            Scene[] tempArray = new Scene[_scenes.Length + 1];

            //Copy all values from old array into the new array
            for (int i = 0; i < _scenes.Length; i++)
            {
                tempArray[i] = _scenes[i];
            }

            //Set the last index to be the new scene
            tempArray[_scenes.Length] = scene;

            //Set the old array to be the new array
            _scenes = tempArray;

            //Return the last index
            return _scenes.Length - 1;
        }

        /// <summary>
        /// Gets the next key in the input stream
        /// </summary>
        /// <returns>The key that was pressed</returns>
        public static ConsoleKey GetNextKey()
        {
            //If there is no key being pressed...
            if (!Console.KeyAvailable)
                //...return
                return 0;

            //Return the current key being pressed
            return Console.ReadKey(true).Key;
        }

        /// <summary>
        /// Ends the application
        /// </summary>
        public static void CloseApplication()
        {
            _applicationShouldClose = true;
        }
    }
}
