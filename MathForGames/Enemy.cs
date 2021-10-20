﻿using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Enemy : Actor
    {
        private float _speed;
        private Vector2 _velocity;
        private Actor _target;

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

        public Enemy(char icon, float x, float y, float speed, Actor target, Color color, string name = "Actor") :
            base(icon, x, y, color, name)
        {
            _speed = speed;
            _target = target;   
        }

        public override void Update(float deltaTime)
        {
            Vector2 direction = new Vector2();
            float distance;

            direction = _target.Position - Position;

            direction.Normalize();

            Velocity = direction * Speed;

            distance = Vector2.Distance(Position, _target.Position);

            if (GetTargetInSight() && distance < 70)
                Position += Velocity * deltaTime;
            
            base.Update(deltaTime);
        }

        public bool GetTargetInSight()
        {
            Vector2 directionOfTarget = (_target.Position - Position).Normalized;

            return Vector2.DotProduct(directionOfTarget, Forward) > 0;
        }

        public override void OnCollision(Actor actor)
        {
            Console.WriteLine("Collision occurred");
        }
    }
}
