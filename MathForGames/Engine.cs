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
        private Camera3D _camera = new Camera3D();
        private Player _player;

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


        private void InitializeCamera()
        {
            //Camera Position
            _camera.position = new System.Numerics.Vector3(0, 10, 10);
            //Point the camera is focused on
            _camera.target = new System.Numerics.Vector3(0, 0, 0);
            //Camera up vector (rotation towards target)
            _camera.up = new System.Numerics.Vector3(0, 1, 0);
            //Camera field of view Y
            _camera.fovy = 45;
            //Camera mode type
            _camera.projection = CameraProjection.CAMERA_PERSPECTIVE; 
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

            InitializeCamera();

            Scene scene = new Scene();

            Player player = new Player(1, 0, 1, 30, "Player", Shape.CUBE);
            player.SetScale(1, 1, 1);
            player.LookAt(Vector3.RIGHT);
            player.SetColor(new Vector4(60, 40, 20, 100));
            //CircleCollider playerCircleCollider = new CircleCollider(15, player);
            //AABBCollider playerBoxCollider = new AABBCollider(40, 40, player);
            //player.Collider = playerCircleCollider;
            _player = player;

            Enemy enemy = new Enemy(5, 0, 1, 10, player, "Enemy", Shape.SPHERE);

            scene.AddActor(player);

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

            _camera.position = new System.Numerics.Vector3(_player.WorldPosition.X, _player.WorldPosition.Y + 10, _player.WorldPosition.Z + 10);
            //Point the camera is focused on
            _camera.target = new System.Numerics.Vector3(_player.WorldPosition.X, _player.WorldPosition.Y, _player.WorldPosition.Z);

            while (Console.KeyAvailable)
                Console.ReadKey(true);
        }

        /// <summary>
        /// Called every time the game loops to update visuals
        /// </summary>
        private void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.BeginMode3D(_camera);

            Raylib.ClearBackground(Color.GRAY);
            Raylib.DrawGrid(50, 1);

            //Adds all actor icons to buffer
            _scenes[_currentSceneIndex].Draw();
            _scenes[_currentSceneIndex].DrawUI();

            Raylib.EndMode3D();
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
