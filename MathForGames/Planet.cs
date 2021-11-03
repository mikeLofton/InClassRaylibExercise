﻿using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Planet : Actor
    {
        private float _speed;
        private Vector2 _velocity;

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public Planet(float x, float y, float speed, string name = "Actor", string path = "") :
            base(x, y, name, path)
        {
            _speed = speed;
        }

        public override void Update(float deltaTime)
        {
            this.Rotate(0.02f);
            base.Update(deltaTime);
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
