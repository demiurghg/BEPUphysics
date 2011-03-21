using BEPUphysics.Collidables.MobileCollidables;
using BEPUphysics.EntityStateManagement;
using Microsoft.Xna.Framework;
using BEPUphysics.CollisionShapes.ConvexShapes;

namespace BEPUphysics.Entities.Prefabs
{
    /// <summary>
    /// Pill-shaped object that can collide and move.  After making an entity, add it to a Space so that the engine can manage it.
    /// </summary>
    public class Capsule : Entity<ConvexCollidable<CapsuleShape>>
    {
        /// <summary>
        /// Gets or sets the length of the capsule.
        /// </summary>
        public float Length
        {
            get
            {
                return CollisionInformation.Shape.Length;
            }
            set
            {
                CollisionInformation.Shape.Length = value;
            }
        }

        /// <summary>
        /// Gets or sets the radius of the capsule.
        /// </summary>
        public float Radius
        {
            get
            {
                return CollisionInformation.Shape.Radius;
            }
            set
            {
                CollisionInformation.Shape.Radius = value;
            }
        }

        private Capsule(float len, float rad)
            : base(new ConvexCollidable<CapsuleShape>(new CapsuleShape(len, rad)))
        {
        }

        private Capsule(float len, float rad, float mass)
            : base(new ConvexCollidable<CapsuleShape>(new CapsuleShape(len, rad)), mass)
        {
        }



        ///<summary>
        /// Computes an orientation and length from a line segment.
        ///</summary>
        ///<param name="start">Starting point of the line segment.</param>
        ///<param name="end">Endpoint of the line segment.</param>
        ///<param name="orientation">Orientation of a line that fits the line segment.</param>
        ///<param name="length">Length of the line segment.</param>
        public static void GetCapsuleInformation(ref Vector3 start, ref Vector3 end, out Quaternion orientation, out float length)
        {
            Vector3 segmentDirection;
            Vector3.Subtract(ref end, ref start, out segmentDirection);
            length = segmentDirection.Length();
            if (length > 0)
            {
                Vector3.Divide(ref segmentDirection, length, out segmentDirection);
                Toolbox.GetQuaternionBetweenNormalizedVectors(ref segmentDirection, ref Toolbox.UpVector, out orientation);
            }
            else
                orientation = Quaternion.Identity;
        }

        ///<summary>
        /// Constructs a new kinematic capsule.
        ///</summary>
        ///<param name="start">Line segment start point.</param>
        ///<param name="end">Line segment end point.</param>
        ///<param name="radius">Radius of the capsule to expand the line segment by.</param>
        public Capsule(Vector3 start, Vector3 end, float radius)
            : this((end - start).Length(), radius)
        {
            float length;
            GetCapsuleInformation(ref start, ref end, out orientation, out length);
        }


        ///<summary>
        /// Constructs a new dynamic capsule.
        ///</summary>
        ///<param name="start">Line segment start point.</param>
        ///<param name="end">Line segment end point.</param>
        ///<param name="radius">Radius of the capsule to expand the line segment by.</param>
        /// <param name="mass">Mass of the entity.</param>
        public Capsule(Vector3 start, Vector3 end, float radius, float mass)
            : this((end - start).Length(), radius, mass)
        {
            float length;
            GetCapsuleInformation(ref start, ref end, out orientation, out length);
        }

        /// <summary>
        /// Constructs a physically simulated capsule.
        /// </summary>
        /// <param name="pos">Position of the capsule.</param>
        /// <param name="len">Length of the capsule.</param>
        /// <param name="rad">Radius of the capsule.</param>
        /// <param name="m">Mass of the object.</param>
        public Capsule(Vector3 pos, float len, float rad, float m)
            : this(len, rad, m)
        {
            Position = pos;
        }

        /// <summary>
        /// Constructs a nondynamic capsule.
        /// </summary>
        /// <param name="pos">Position of the capsule.</param>
        /// <param name="len">Length of the capsule.</param>
        /// <param name="rad">Radius of the capsule.</param>
        public Capsule(Vector3 pos, float len, float rad)
            : this(len, rad)
        {
            Position = pos;
        }

        /// <summary>
        /// Constructs a dynamic capsule.
        /// </summary>
        /// <param name="motionState">Motion state specifying the entity's initial state.</param>
        /// <param name="len">Length of the capsule.</param>
        /// <param name="rad">Radius of the capsule.</param>
        /// <param name="m">Mass of the object.</param>
        public Capsule(MotionState motionState, float len, float rad, float m)
            : this(len, rad, m)
        {
            MotionState = motionState;
        }

        /// <summary>
        /// Constructs a nondynamic capsule.
        /// </summary>
        /// <param name="motionState">Motion state specifying the entity's initial state.</param>
        /// <param name="len">Length of the capsule.</param>
        /// <param name="rad">Radius of the capsule.</param>
        public Capsule(MotionState motionState, float len, float rad)
            : this(len, rad)
        {
            MotionState = motionState;
        }

    }
}