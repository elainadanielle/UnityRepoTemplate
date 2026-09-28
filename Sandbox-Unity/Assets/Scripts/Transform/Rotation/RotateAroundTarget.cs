/************************************************************
 * COPYRIGHT:  2026
 * PROJECT:    Sandbox
 * FILE NAME:  RotateTransform.cs
 * DESCRIPTION: Rotates an object around a target GameObject's position along a specified axis.
 *
 * REVISION HISTORY:
 * Date [YYYY/MM/DD] | Author | Comments
 * ------------------------------------------------------------
 * 2026/09/25 | Akram Taghavi-Burris | Created class
 * 2026/09/26 | Akram Taghavi-Burris | Integrated RotationUtils and Axis enum
 * 2026/09/26 | Akram Taghavi-Burris | Refactored class to inherit from abstract RotateTransform base class
 ************************************************************/

using UnityEngine;

namespace CSG.Transform.Rotation
{
    /// <summary>
    /// Provides modular rotation-based movement for Unity GameObjects around a target position.
    /// Supports continuous rotation along chosen axes via
    /// <see cref="UnityEngine.Transform.RotateAround(Vector3, Vector3, float)" />.
    /// Inherits shared rotation properties and lifecycle logic from <see cref="RotateTransformBase" />.
    /// </summary>
    public class RotateAroundTarget : RotateTransformBase
    {
        #region Fields & Properties

        [Header("Rotation Target Settings")] [SerializeField] [Tooltip("GameObject to rotate around")]
        private GameObject _rotationTarget;

        // Stores world position of _rotationTarget
        private Vector3 _rotationTargetPosition;

        [SerializeField] [Tooltip("Keep object aligned to target plane on initialization.")]
        private bool _alignToTargetPlane;

        [SerializeField] [Tooltip("Distance from the target object.")]
        private float _orbitRadius = 5f;

        /// <summary>
        /// Gets or sets the target GameObject to rotate around.
        /// Updating this recalculates the Stores target position and alignment.
        /// </summary>
        public GameObject RotationTarget
        {
            get => _rotationTarget;
            set
            {
                _rotationTarget = value;

                //If value set
                if (RotationTarget != null)
                {
                    //Set rotation target position
                    _rotationTargetPosition = RotationTarget.transform.position;

                    CheckAlignment();

                    Debug.Log($"Initialized Vales - Rotation Target: {RotationTarget.name}");
                } //end if(RotationTarget != null) 
            }
        }

        /// <summary>
        /// Gets or sets the distance (radius) maintained from the target object during rotation.
        /// Minimum value is 0.
        /// </summary>
        public float OrbitRadius
        {
            get => _orbitRadius;
            set => _orbitRadius = Mathf.Max(0f, value);
        }

        #endregion

        #region Editor Callbacks

#if UNITY_EDITOR
        // Called when a value is changed in the Unity Inspector.
        private void OnValidate()
        {
            // If component is enabled, Play mode, and target is set 
            if (enabled && Application.isPlaying && _rotationTarget != null)
                // Force setter to run when modified via Inspector during Play Mode
                RotationTarget = _rotationTarget; //end if(Application.isPlaying && _rotationTarget != null)
        }
#endif

        #endregion

        #region Initialization & Activation
        
        // OnEnable is called every time the component becomes enabled and active.
        protected virtual void OnEnable()
        {
            // When enabled assign rotation target
            RotationTarget = _rotationTarget;

            Debug.Log(
                $"Initialized Values - Rotation Target: {RotationTarget.name}, Angular Speed: {AngularSpeed}, Rotation Axis: {RotationAxis}, Continuous Rotation: {ContinuousRotation}");
            
        }//end OnEnabled()

        #endregion

        #region Feature Logic
        
        /// <summary>
        /// Rotates the object in an orbit around the target position based on the configured speed and axis.
        /// </summary>
        public override void Rotate()
        {
            base.Rotate();

            // Rotate object around the selected axis using the selected coordinate space
            transform.RotateAround(_rotationTargetPosition, AxisVector, RotationAmount);
            
        } //end Rotate()

        #endregion

        #region Gizmos

        /// <summary>
        /// Draws editor visual gizmos to represent the orbit radius and target connection when selected.
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            // Set the color of the Gizmo
            Gizmos.color = Color.cyan;

            // Draw a line from the target center to this orbiting object
            Gizmos.DrawLine(_rotationTargetPosition, transform.position);

            // Draw a wireframe sphere showing the orbital radius
            Gizmos.DrawWireSphere(_rotationTargetPosition, _orbitRadius);
        } //end OnDrawGizmosSelected()

        #endregion

        #region Helpers

        /// <summary>
        /// Aligns the object's position to the target position if alignment is enabled.
        /// </summary>
        private void CheckAlignment()
        {
            if (_alignToTargetPlane)
                // Move to target position
                transform.position = _rotationTargetPosition; //end if (_alignToTargetPlane)

            ApplyRadiusOffset();
        } //end CheckAlignment()

        /// <summary>
        /// Positions the object at the specified radius away from the target.
        /// </summary>
        private void ApplyRadiusOffset()
        {
            if (_rotationTarget == null) return;

            // Calculate direction from target to this object (default to forward if at exact same point)
            var direction = (transform.position - _rotationTargetPosition).normalized;
            if (direction == Vector3.zero) direction = Vector3.forward; //end if(direction == Vector3.zero)

            // Move the object away from the target by the orbit radius
            transform.position = transform.position + direction * _orbitRadius;
        } //end ApplyRadiusOffset()

        #endregion
        
    } //end RotateAroundTarget
} //end namespace CSG.Transform.Rotation