/************************************************************
 * COPYRIGHT:  2026
 * PROJECT:    CSG Modular Programming
 * FILE NAME:  MoveRigidbodyVelocity.cs
 * DESCRIPTION: Provides arcade-style physics movement by
 *              controlling a Rigidbody's linear velocity.
 *
 * REVISION HISTORY:
 * Date [YYYY/MM/DD] | Author | Comments
 * ------------------------------------------------------------
 * 2026/09/27 | Akram Taghavi-Burris | Created class
 *
 ************************************************************/

/* NOTE FOR STUDENTS: Velocity is a vector quantity of speed and direction.
// Speed tells us how fast, and Direction tells us where to move.
// Together, Speed and Direction create a velocity vector.
//
// Displacement is "how far" an object moves during one physics update.
// MovePosition uses displacement, so we multiply Speed by Time.fixedDeltaTime:
//
//     displacement = Direction * Speed * Time.fixedDeltaTime;
//
// Velocity is a rate of movement (how fast) measured per second.
// When we set Rigidbody.linearVelocity, we give Unity that rate directly:
//
//     linearVelocity = Direction * Speed;
//
// We do NOT multiply velocity by Time.fixedDeltaTime.
// Unity's physics system uses the velocity and the physics time step
// to calculate the object's movement.*/

using UnityEngine;
using CSG.General.Movement;

namespace CSG.Physics.Movement
{

    /// <summary>
    /// Provides arcade-style physics movement by directly setting the <see cref="UnityEngine.Rigidbody.linearVelocity"/>.
    /// Preserves existing vertical ($Y$) velocity to allow natural gravity and jumping interactions.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class MoveRigidbodyVelocity : MovementBase
    {

        #region Fields & Properties

        /// <summary>
        /// Reference to the attached <see cref="UnityEngine.Rigidbody"/> component used for physics movement.
        /// </summary>
        private Rigidbody _rigidBody;

        #endregion

        #region Initialization & Activation

        // Awake is called once on initialization before any Start methods.
        protected override void Awake()
        {
            // Store Rigidbody component reference
            _rigidBody = GetComponent<Rigidbody>();
            
            // Call the base class Awake logic 
            base.Awake();

        } //end Awake()

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
        /// Applies target movement velocity directly to the attached Rigidbody.
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

            // Calculate target horizontal velocity vector (meters per second)
            Vector3 targetVelocity = targetDirection * Speed;

            //Preserve existing Y velocity so gravity and jumping are unaffected
            targetVelocity.y = _rigidBody.linearVelocity.y;

            // Apply velocity directly while maintaining current Y velocity for gravity/jumping
            _rigidBody.linearVelocity =  targetVelocity;
            
        } //end Move()

        /// <summary>
        /// Stops active horizontal movement by resetting horizontal velocity components.
        /// </summary>
        public override void Stop()
        {
            // Call the base class Stop() logic
            base.Stop();

            // Zero out horizontal velocity upon stopping while leaving gravity intact
            if (_rigidBody != null)
            {
                _rigidBody.linearVelocity = new Vector3(0f, _rigidBody.linearVelocity.y, 0f);
            }
        } //end Stop()

        #endregion

    } //end MoveRigidbodyVelocity

} //end namespace CSG.Physics.Movement