/************************************************************
 * COPYRIGHT:  2026
 * PROJECT:    Sandbox
 * FILE NAME:  RotationUtils.cs
 * DESCRIPTION: Utility functions for axis vector conversions and rotational calculations.
 *
 * REVISION HISTORY:
 * Date [YYYY/MM/DD] | Author | Comments
 * ------------------------------------------------------------
 * 2026/09/26 | Akram Taghavi-Burris | Created class with shared rotation utility methods
 *
 ************************************************************/

using UnityEngine;
using CSG.Transform.Core;

namespace CSG.Transform.Rotation
{
    /// <summary>
    /// Utility functions for axis vector conversions and rotational calculations.
    /// </summary>
    public static class RotationUtils
    {
        #region Constants

        /// <summary>
        /// Total degrees in a complete circle rotation.
        /// </summary>
        public const float FULL_ROTATION_DEGREES = 360f;

        #endregion

        #region Vector Calculations

        /// <summary>
        /// Converts the Axis enum selection into a normalized direction vector.
        /// </summary>
        /// <param name="axis">The selected Axis enum value (X, Y, or Z).</param>
        /// <returns>A normalized <see cref="Vector3" /> representing the specified direction.</returns>
        public static Vector3 GetAxisVector(Axis axis)
        {
            return axis switch
            {
                Axis.X => Vector3.right,
                Axis.Y => Vector3.up,
                Axis.Z => Vector3.forward,
                _ => Vector3.up
            };
        } //end GetAxisVector()

        #endregion

        #region Clamping Calculations

        /// <summary>
        /// Clamps a frame degree step and updates the accumulated degree tracker.
        /// </summary>
        /// <param name="step">The unconstrained frame degree step.</param>
        /// <param name="accumulated">Reference to the running total of degrees rotated (will be updated directly).</param>
        /// <param name="fullRotationTarget">The target limit (e.g., 360 degrees).</param>
        /// <param name="isComplete">Out parameter set to true if the rotation has hit or exceeded the target limit.</param>
        /// <returns>The clamped degree step to apply for the current frame.</returns>
        public static float ClampRotationStep(float step, ref float accumulated, float fullRotationTarget,
            out bool isComplete)
        {
            if (accumulated + step >= fullRotationTarget)
            {
                var remainingStep = fullRotationTarget - accumulated;
                accumulated = fullRotationTarget;
                isComplete = true;
                return remainingStep;
            }

            accumulated += step;
            isComplete = false;
            return step;
        } //end ClampRotationStep()

        #endregion
        
    } //end class RotationUtils
} //end namespace CSG.Transform