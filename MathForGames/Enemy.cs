using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Enemy : Actor
    {
        private float _speed;
        private Vector3 _velocity;
        private Actor _target;

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Vector3 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public Enemy(float x, float y, float z, float speed, Actor target, string name = "Actor", Shape shape = Shape.CUBE) :
            base(x, y, z, name, shape)
        {
            _speed = speed;
            _target = target;   
        }

        public override void Update(float deltaTime)
        {
            Vector3 direction = new Vector3();
            float distance;

            direction = _target.LocalPosition - LocalPosition;

            direction.Normalize();

            Velocity = direction * Speed;

            distance = Vector3.Distance(_target.LocalPosition, LocalPosition);

            if (GetTargetInSight() && distance < 100)
                LocalPosition += Velocity * deltaTime;
            
            base.Update(deltaTime);
        }

        public bool GetTargetInSight()
        {
            Vector3 directionOfTarget = (_target.LocalPosition - LocalPosition).Normalized;

            return Math.Acos(Vector3.DotProduct(directionOfTarget, Forward)) * (180 / Math.PI) < 30;
        }

        public override void OnCollision(Actor actor)
        {
            Console.WriteLine("Collision occurred");
        }

        public override void Draw()
        {
            base.Draw();
            Collider.Draw();
        }
    }
}
