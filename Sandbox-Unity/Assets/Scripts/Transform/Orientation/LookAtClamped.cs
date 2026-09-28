/************************************************************
 * COPYRIGHT:    2026
 * PROJECT:      Sandbox
 * FILE NAME:    LookAtClamped.cs
 * DESCRIPTION:  Extends LookAtTarget to apply Pitch (X-Axis) and
 *               Yaw (Y-Axis) rotation constraints.
 *
 * REVISION HISTORY:
 * Date [YYYY/MM/DD] | Author | Comments
 * ------------------------------------------------------------
 * 2026/09/27 | Akram Taghavi-Burris | Created derived class with Pitch/Yaw clamping
 *
 ************************************************************/

using CSG.General.Math;
using UnityEngine;

namespace CSG.Transform.Orientation
{
    /// <summary>
    /// Extends <see cref="LookAtTarget" /> to constrain tracking rotation within specified Pitch (X-Axis) and Yaw (Y-Axis)
    /// angle limits.
    /// </summary>
    public class LookAtClamped : LookAtTarget
    {
        #region Fields & Properties

        [Header("Clamp Settings")] [SerializeField] [Tooltip("Enable Pitch (X-Axis vertical tilt) rotation clamping.")]
        private bool _clampPitch = true;

        [SerializeField] [Tooltip("Minimum and maximum Pitch angle in degrees.")]
        private Vector2 _pitchLimits = new(-45f, 45f);

        [SerializeField] [Tooltip("Enable Yaw (Y-Axis horizontal rotation) clamping.")]
        private bool _clampYaw = true;

        [SerializeField] [Tooltip("Minimum and maximum Yaw angle in degrees.")]
        private Vector2 _yawLimits = new(-60f, 60f);

        /// <summary>
        /// Gets or sets whether Pitch (vertical tilt) clamping is active.
        /// </summary>
        public bool ClampPitch
        {
            get => _clampPitch;
            set => _clampPitch = value;
        }

        /// <summary>
        /// Gets or sets whether Yaw (horizontal rotation) clamping is active.
        /// </summary>
        public bool ClampYaw
        {
            get => _clampYaw;
            set => _clampYaw = value;
        }

        #endregion

        #region Editor Callbacks

#if UNITY_EDITOR
        // Called when a value is changed in the Unity Inspector.
        private void OnValidate()
        {
            if (!_clampPitch && !_clampYaw)
                Debug.LogWarning(
                    $"{nameof(LookAtClamped)} has both Pitch and Yaw clamping disabled. " +
                    $"Use {nameof(LookAtTarget)} if no rotation limits are needed.",
                    this); //end if(!_clampPitch && !_clampYaw)
        } //end OnValidate()
#endif

        #endregion

        #region Feature Logic
        
        /// <summary>
        /// Overrides base tracking behavior to apply Pitch and Yaw rotational clamping.
        /// </summary>
        public override void OrientToTarget()
        {
            // Store the direction to the target
            var directionToTarget = TargetTransform.position - transform.position;

            // Return if direction is zero
            if (directionToTarget == Vector3.zero) return;

            // Store the target's rotational angles in degrees (euler angles)
            var targetEulerAngles = CalculateLookEulerAngles(directionToTarget);

            // Clamp Pitch (X-Axis vertical tilt) if enabled
            if (_clampPitch)
                targetEulerAngles.x = AngleMath.ClampAngle(
                    targetEulerAngles.x,
                    _pitchLimits.x,
                    _pitchLimits.y); //end if(_clampPitch)

            // Clamp Yaw (Y-Axis horizontal rotation) if enabled
            if (_clampYaw)
                targetEulerAngles.y = AngleMath.ClampAngle(
                    targetEulerAngles.y,
                    _yawLimits.x,
                    _yawLimits.y); //end if(_clampYaw)

            // Apply clamped Euler angles to transform
            transform.rotation = Quaternion.Euler(targetEulerAngles);
        } //end OrientToTarget()

        #endregion

        #region Helpers

        /// <summary>
        /// Calculates the Euler angles needed to face a direction using the inherited Up direction.
        /// </summary>
        /// <param name="direction">The direction to face.</param>
        /// <returns>Euler angles representing the calculated rotation.</returns>
        private Vector3 CalculateLookEulerAngles(Vector3 direction)
        {
            var rotation = Quaternion.LookRotation(direction, UpDirection);

            return rotation.eulerAngles;
        } //end CalculateLookEulerAngles

        #endregion

    } //end LookAtClamped
} //end namespace CSG.Transform.Orientation