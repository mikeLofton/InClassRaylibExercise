using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;

namespace MathForGames
{
    class AABBCollider : Collider
    {
        private float _width;
        private float _height;

        public float Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public float Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public float Left
        {
            get
            {

            }
        }

        public float Right
        {
            get
            {

            }
        }

        public float Top
        {
            get
            {

            }
        }

        public float Bottom
        {
            get
            {

            }
        }
    }
}
