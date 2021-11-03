using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{

    public enum Shape
    {
        CUBE,
        SPHERE
    }

    class Actor
    {
        private string _name;
        private bool _started;
        private Vector3 _forward = new Vector3(0, 0, 1);
        private Collider _collider;
        private Matrix4 _globalTransform = Matrix4.Identity;
        private Matrix4 _localTransform = Matrix4.Identity;
        private Matrix4 _translation = Matrix4.Identity;
        private Matrix4 _rotation = Matrix4.Identity;
        private Matrix4 _scale = Matrix4.Identity;
        private Actor[] _children = new Actor[0];
        private Actor _parent;
        private Shape _shape;

        /// <summary>
        /// True if the start functions has been called for this actor
        /// </summary>
        public bool Started
        {
            get { return _started; }
        }

        public Vector3 LocalPosition
        {
            get { return new Vector3(_translation.M03, _translation.M13, _translation.M23); }
            set 
            {
                SetTranslation(value.X, value.Y);
            }
        }

        /// <summary>
        /// The position of this actor in the world
        /// </summary>
        public Vector3 WorldPosition
        {
            //Return the global transform's T column
            get { return new Vector3(_globalTransform.M03, _globalTransform.M13, _globalTransform.M23); }
            set 
            {
                //If the actor has a parent...
                if (Parent != null)
                {
                    float xOffset = (value.X - Parent.WorldPosition.X) / new Vector3(GlobalTransform.M00, GlobalTransform.M10, GlobalTransform.M20).Magnitude;
                    float yOffset = (value.Y - Parent.WorldPosition.Y) / new Vector3(GlobalTransform.M00, GlobalTransform.M10, GlobalTransform.M20).Magnitude;
                    float zOffset = (value.Z - Parent.WorldPosition.Z) / new Vector3(GlobalTransform.M00, GlobalTransform.M10, GlobalTransform.M20).Magnitude;
                }
                else
                {
                    LocalPosition = value;
                }                
            }
        }

        public Matrix4 GlobalTransform
        {
            get { return _globalTransform; } 
            private set { _globalTransform = value; }
        }

        public Matrix4 LocalTransform
        {
            get { return _localTransform; }
            private set { _localTransform = value; }
        }

        public Actor Parent
        {
            get {return _parent; }
            set { _parent = value; }
        }

        public Actor[] Children
        {
            get { return _children; }
        }

        public Vector3 Size
        {
            get
            {
                float xScale = new Vector3(_scale.M00, _scale.M10, _scale.M20).Magnitude;
                float yScale = new Vector3(_scale.M01, _scale.M11, _scale.M21).Magnitude;
                float zScale = new Vector3(_scale.M02, _scale.M12, _scale.M22).Magnitude;

                return new Vector3(xScale, yScale, zScale);
            }
            set { SetScale(value.X, value.Y, value.Z); }
        }

        public Vector3 Forward
        {
            get
            {
                return new Vector3();
            }
        }

        /// <summary>
        /// The collider attached to the actor
        /// </summary>
        public Collider Collider
        {
            get { return _collider; }
            set { _collider = value; }
        }

        public Actor(float x, float y, string name = "Actor", Shape shape = Shape.CUBE) : 
            this(new Vector3 { X = x, Y = y}, name, shape) {}

        public Actor(Vector3 position, string name = "Actor", Shape shape = Shape.CUBE)
        {
            LocalPosition = position;
            _name = name;
            _shape = shape;

        }

        public void UpdateTransforms()
        {
            _localTransform = _translation * _rotation * _scale;

            if (_parent != null)
                GlobalTransform = _parent.GlobalTransform * LocalTransform;
            else
                GlobalTransform = LocalTransform;
        }

        public void AddChild(Actor child)
        {
            //Create a temp array larger than the original
            Actor[] tempArray = new Actor[_children.Length + 1];

            //Copy all values from the orginal array into the temp array
            for (int i = 0; i < _children.Length; i++)
            {
                tempArray[i] = _children[i];
            }

            //Add the new child to the end of the new array
            tempArray[_children.Length] = child;

            //Set the parent of the actor to be this actor
            child.Parent = this;

            //Set the old array to be the new array
            _children = tempArray;          
        }

        public bool RemoveChild(Actor child)
        {
            //Create a variable to store if the removal was successful
            bool childRemoved = false;

            //Create a new array that is smaller than the original
            Actor[] tempArray = new Actor[_children.Length - 1];

            //Copy all values except the actor we don't want into the new array
            int j = 0;

            for (int i = 0; i < _children.Length; i++)
            {
                //If the actor that the loop is on is not the one to remove...
                if (_children[i] != child)
                {
                    //...add the actor into the new array and increment the temp array counter
                    tempArray[j] = _children[i];
                    j++;
                }
                //Otherwise if this actor is the one to remove...
                else
                {
                    //...set childRemoved to true
                    childRemoved = true;
                }
            }

            //If actor removed is successful set children to temp array
            if (childRemoved)
            {
                _children = tempArray;
                //Set the parent of the child to be nothing
                child.Parent = null;
            }

            return childRemoved;
        }

        public virtual void Start()
        {
            _started = true;
        }

        public virtual void Update(float deltaTime)
        {           
            UpdateTransforms();
            Console.WriteLine(_name + ": " + WorldPosition.X + ", " + WorldPosition.Y);
        }

        public virtual void Draw()
        {
            System.Numerics.Vector3 position = new System.Numerics.Vector3(WorldPosition.X, WorldPosition.Y, WorldPosition.Z);

            switch (_shape)
            {
                case Shape.CUBE:
                    Raylib.DrawCube(position, Size.X, Size.Y, Size.Z, Color.BLACK);
                    break;
                case Shape.SPHERE:
                    Raylib.DrawSphere(position, Size.X, Color.BLACK);
                    break;
            }
        }

        public void End()
        {

        }

        public virtual void OnCollision(Actor actor)
        {

        }

        /// <summary>
        /// Checks if this actor collided with another actor
        /// </summary>
        /// <param name="other">The actor to check for a collision against</param>
        /// <returns>True if the distance between the actors is less than the radii of the two combined</returns>
        public virtual bool CheckForCollision(Actor other)
        {
            //Return false if either actor doesn't have a collider attached
            if (Collider == null || other.Collider == null)
                return false;

            return Collider.CheckCollison(other);
        }

        /// <summary>
        /// Sets the position of the actor
        /// </summary>
        /// <param name="translationX">The new x position</param>
        /// <param name="translationY">The new y position</param>
        public void SetTranslation(float translationX, float translationY)
        {
            //_translation = Matrix4.CreateTranslation(translationX, translationY);
        }

        /// <summary>
        /// Applies the given values to the current translation
        /// </summary>
        /// <param name="translationX">The amount to move on the x</param>
        /// <param name="translationY">The amount to move on the y</param>
        public void Translate(float translationX, float translationY)
        {
            //_translation *= Matrix4.CreateTranslation(translationX, translationY);
        }

        /// <summary>
        /// Set the rotation of the actor.
        /// </summary>
        /// <param name="radians">The angle of the new rotation in radians.</param>
        public void SetRotation(float radiansX, float radiansY, float radiansZ)
        {
            Matrix4 rotationX = Matrix4.CreateRotationX(radiansX);
            Matrix4 rotationY = Matrix4.CreateRotationY(radiansY);
            Matrix4 rotationZ = Matrix4.CreateRotationZ(radiansZ);
            _rotation = rotationX * rotationY * rotationZ;
        }

        /// <summary>
        /// Adds a rotation to the current transform's rotation.
        /// </summary>
        /// <param name="radians">The angle in the radians to turn.</param>
        public void Rotate(float radiansX, float radiansY, float radiansZ)
        {
            Matrix4 rotationX = Matrix4.CreateRotationX(radiansX);
            Matrix4 rotationY = Matrix4.CreateRotationY(radiansY);
            Matrix4 rotationZ = Matrix4.CreateRotationZ(radiansZ);
            _rotation *= rotationX * rotationY * rotationZ;
        }

        /// <summary>
        /// Sets the scale of the actor
        /// </summary>
        /// <param name="x">The value to scale on the x axis.</param>
        /// <param name="y">The value to scale on the y axis</param>
        public void SetScale(float x, float y, float z)
        {
            _scale = Matrix4.CreateScale(new Vector3(x, y, z));
        }

        /// <summary>
        /// Scales the actor by the given amount
        /// </summary>
        /// <param name="x">The value to scale on the x axis.</param>
        /// <param name="y">The value to scale on the y axis</param>
        public void Scale(float x, float y, float z)
        {
            _scale *= Matrix4.CreateScale(new Vector3(x, y, z));
        }

        /// <summary>
        /// Rotates the actor to face the given position
        /// </summary>
        /// <param name="position">The position the actor should be looking towards</param>
        public void LookAt(Vector2 position)
        {
            ////Find the direction the actor should look in
            //Vector2 direction = (position - LocalPosition).Normalized;

            ////Use the dot product to find the angle the actor needs to rotate
            //float dotProd = Vector2.DotProduct(direction, Forward);

            //if (dotProd > 1)
            //    dotProd = 1;

            //float angle = (float)Math.Acos(dotProd);

            ////Find the perpindicular vector to the direction
            //Vector2 perpDirection = new Vector2(direction.Y, -direction.X);

            ////Find the dot product of the perpindicular vector and the current forward
            //float perpDot = Vector2.DotProduct(perpDirection, Forward);

            ////if the result isn't 0, use it to change the sign of the angle to either be positive or negative
            //if (perpDot != 0)
            //    angle *= -perpDot / Math.Abs(perpDot);

            //Rotate(angle);
        }
    }
}
