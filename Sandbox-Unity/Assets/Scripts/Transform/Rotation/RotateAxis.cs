/************************************************************
 * COPYRIGHT:  2026
 * PROJECT:    Sandbox
 * FILE NAME:  RotateAxis.cs
 * DESCRIPTION: Rotates an object around a specified axis in self or world space.
 *
 * REVISION HISTORY:
 * Date [YYYY/MM/DD] | Author | Comments
 * ------------------------------------------------------------
 * 2026/09/23 | Akram Taghavi-Burris | Created class
 * 2026/09/24 | Akram Taghavi-Burris | Added continuousRotation option for full 360-degree rotation tracking
 * 2026/09/25 | Akram Taghavi-Burris | Integrated RotationUtils and Axis enum
 * 2026/09/25 | Akram Taghavi-Burris | Refactored class to inherit from abstract RotateTransform base class
 *
 ************************************************************/


using UnityEngine;

namespace CSG.Transform.Rotation
{
    /// <summary>
    /// Provides modular rotation-based movement for Unity GameObjects.
    /// Supports continuous rotation along chosen axes via <see cref="UnityEngine.Transform.Rotate(Vector3, Space)" />.
    /// Inherits shared rotation properties and lifecycle logic from <see cref="RotateTransformBase" />.
    /// </summary>
    public class RotateAxis : RotateTransformBase
    {
        #region Fields & Properties

        [SerializeField]
        [Tooltip("Determines whether rotation uses World Space or Self Space. Self Space is the default.")]
        private Space _rotationSpace = Space.Self;

        /// <summary>
        /// Gets or sets the coordinate space used to determine the movement direction.
        /// </summary>
        public Space RotationSpace
        {
            get => _rotationSpace;
            set => _rotationSpace = value;
        }

        #endregion

        #region Initialization & Activation
        
        // Awake is called once on initialization
        protected override void Awake()
        {
            // Call the base class Awake logic first
            base.Awake();

            // Re-assign child fields through properties to enforce validation rules 
            RotationSpace = _rotationSpace;

            Debug.Log(
                $"Initialized Values - Rotation Space: {RotationSpace}, Angular Speed: {AngularSpeed}, Rotation Axis: {RotationAxis}, Continuous Rotation: {ContinuousRotation}");
        } //end Awake()

        #endregion
        
        #region Feature Logic
        
        /// <summary>
        /// Rotates the object around its own axis or world axis based on the configured speed and coordinate space.
        /// </summary>
        public override void Rotate()
        {
            base.Rotate();

            // Rotate object around the selected axis using the selected coordinate space
            transform.Rotate(AxisVector, RotationAmount, RotationSpace);
        } //end Rotate()

        #endregion
        
    } //end RotateAxis
} //end namespace CSG.Transform.Rotation