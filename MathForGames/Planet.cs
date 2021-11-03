using System;
using System.Collections.Generic;
using System.Text;

namespace MathForGames
{
    class Planet : Actor
    {
        private float _speed;

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Planet(float x, float y, float speed, string name = "Actor", string path = "") :
            base(x, y, name, path)
        {
            _speed = speed;
        }

        public override void Update(float deltaTime)
        {

            base.Update(deltaTime);
        }
    }
}
