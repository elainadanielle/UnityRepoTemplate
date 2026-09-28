/************************************************************
 * COPYRIGHT:  2026
 * PROJECT:    Sandbox
 * FILE NAME:  AngleMath.cs
 * DESCRIPTION: Utility functions for normalizing and clamping angles.
 *
 * REVISION HISTORY:
 * Date [YYYY/MM/DD] | Author | Comments
 * ------------------------------------------------------------
 * 2026/09/26 | Akram Taghavi-Burris | Created class
 *
 * 
 *************************************************************/

using UnityEngine;

namespace CSG.General.Math
{
    /// <summary>
    /// Provides reusable calculations for working with angles.
    /// </summary>
    public static class AngleMath
    {
        
        /// <summary>
        /// Limits an angle to a specified minimum and maximum range.
        /// </summary>
        /// <param name="angle">The angle in degrees to limit.</param>
        /// <param name="minimum">The minimum allowed angle in degrees.</param>
        /// <param name="maximum">The maximum allowed angle in degrees.</param>
        /// <returns>The angle limited to the specified range.</returns>
        public static float ClampAngle(float angle, float minimum, float maximum)
        {
            // Convert the angle to the signed -180 to 180 degree range.
            angle = NormalizeAngle(angle);

            // Limit the normalized angle to the specified range.
            return Mathf.Clamp(angle, minimum, maximum);
            
        } //end ClampAngle

        /// <summary>
        /// Converts an angle to the signed -180 to 180 degree range.
        /// </summary>
        /// <param name="angle">The angle in degrees to normalize.</param>
        /// <returns>The normalized angle between -180 and 180 degrees.</returns>
        private static float NormalizeAngle(float angle)
        {
            // Move angles greater than 180 degrees into the negative range.
            while (angle > 180f)
            {
                angle -= 360f;
            }//end while

            // Move angles less than -180 degrees into the positive range.
            while (angle < -180f)
            {
                angle += 360f;
            }//end while

            return angle;
        } //end NormalizeAngle
        
    } //end AngleMath
} //end namespace CSG.Transform.Orientation