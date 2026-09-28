/************************************************************
 * COPYRIGHT:  2026
 * PROJECT:    Sandbox
 * FILE NAME:  MoveRigidbodyForce.cs
 * DESCRIPTION: Controls GameObject movement by applying continuous physical forces
 *              to the attached Rigidbody, allowing mass, drag, and momentum to dictate motion.
 *
 * REVISION HISTORY:
 * Date [YYYY/MM/DD] | Author | Comments
 * ------------------------------------------------------------
 * 2026/09/27 | Akram Taghavi-Burris | Created class
 *
 ************************************************************/

using UnityEngine;
using CSG.General.Movement;

namespace CSG.Physics.Movement
{
    /// <summary>
    /// Applies continuous physical forces to the attached <see cref="UnityEngine.Rigidbody"/>.
    /// Movement is subject to acceleration, mass, momentum, and friction/drag.
    /// </summary>
    [RequireComponent(typeof(UnityEngine.Rigidbody))]
    public class MoveRigidbodyForce : MovementBase
    {

        #region Fields & Properties

        [Header("Force Settings")]
        [SerializeField]
        [Tooltip("The mode used to apply force to the Rigidbody. \n Force uses force and depends on mass and time. \nAcceleration uses acceleration and depends on time, but not mass. \nImpulse is a sudden push and depends on mass, but not time. \nVelocityChange directly changes velocity and depends on neither.")]
        private ForceMode _forceMode = ForceMode.Force;
        
        /// <summary>
        /// Reference to the attached <see cref="UnityEngine.Rigidbody"/> component used for physics movement.
        /// </summary>
        private Rigidbody _rigidBody;

        /// <summary>
        /// Gets or sets the <see cref="UnityEngine.ForceMode"/> applied during movement.
        /// </summary>
        public ForceMode ForceMode
        {
            get => _forceMode;
            set => _forceMode = value;
        }

        #endregion

        #region Initialization & Activation

        // Awake is called once on initialization before any Start methods.
        protected override void Awake()
        {
            // Store Rigidbody component reference
            _rigidBody = GetComponent<Rigidbody>();
            
            // Call the base class Awake logic 
            base.Awake();
            
        }//end Awake()

        // Start is called once before the first Update frame.
        protected override void Start()
        {
            
        }//end Start()

        #endregion

        #region Update Physics 

        // FixedUpdate is called at a fixed interval for physics calculations.
        private void FixedUpdate()
        {
            if (IsMoving)
            {
                Move();
                
            } //end if(_isMoving)
            
        } //end FixedUpdate()

        #endregion

        #region Feature Logic

        /// <summary>
        /// Applies continuous physics force to the attached Rigidbody in the target direction.
        /// </summary>
        public override void Move()
        {
            IsMoving = true;

            // Resolve target direction relative to world or local coordinate space
            Vector3 targetDirection = Direction;

            if (MovementSpace == Space.Self)
            {
                // Convert the object's local space direction into World Space
                targetDirection = transform.TransformDirection(Direction);
            } //end if(MovementSpace == Space.Self)

            // Calculate force vector (Magnitude * Direction)
            Vector3 forceVector = targetDirection * Speed;

            // Apply force to the physics engine
            _rigidBody.AddForce(forceVector, ForceMode);
            
        } //end Move()
        
/* NOTE FOR STUDENTS: The Move() methods in our velocity and force components share some of the same logic. We repeat that logic in both classes, while that breaks DRY principle it keeps component easier to read and maintain in small projects.*/
 
        #endregion

    }//end MoveRigidbodyForce

} //end namespace CSG.Physics.Movement