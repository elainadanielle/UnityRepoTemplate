/************************************************************
 * COPYRIGHT:  2026
 * PROJECT:    Sandbox
 * FILE NAME:  Axis.cs
 * DESCRIPTION: Defines standard 3D spatial axes for component operations.
 *
 * REVISION HISTORY:
 * Date [YYYY/MM/DD] | Author | Comments
 * ------------------------------------------------------------
 * 2026/09/24 | Akram Taghavi-Burris | Extracted Axis enum into shared namespace
 *
 ************************************************************/

namespace CSG.Transform.Core
{
    /// <summary>
    /// Specifies the X, Y, or Z spatial axis for rotational or translational movement.
    /// </summary>
    public enum Axis
    {
        /// <summary>
        /// Represents the horizontal X axis (Vector3.right).
        /// </summary>
        X,

        /// <summary>
        /// Represents the vertical Y axis (Vector3.up).
        /// </summary>
        Y,

        /// <summary>
        /// Represents the depth Z axis (Vector3.forward).
        /// </summary>
        Z
    } //end enum Axis
    
} //end namespace CSG.Transform.Core