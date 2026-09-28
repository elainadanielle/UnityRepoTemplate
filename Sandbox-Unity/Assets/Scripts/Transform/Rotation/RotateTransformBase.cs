/************************************************************
 * COPYRIGHT:  2026
 * PROJECT:    Sandbox
 * FILE NAME:  RotateTransform.cs
 * DESCRIPTION: Abstract base class for components that apply rotational movement to GameObjects.
 *
 * REVISION HISTORY:
 * Date [YYYY/MM/DD] | Author | Comments
 * ------------------------------------------------------------
 * 2026/09/26 | Akram Taghavi-Burris | Created abstract base class to consolidate shared rotation properties
 *
 ************************************************************/

using CSG.Transform.Core;
using UnityEngine;

namespace CSG.Transform.Rotation
{
    /// <summary>
    /// Serves as the abstract base class for rotational transformation components.
    /// Manages core settings, runtime rotation states, and lifecycle hooks for derived rotation behaviors.
    /// </summary>
    public abstract class RotateTransformBase : MonoBehaviour
    {
        #region Constants

        /// <summary>
        /// Maximum angular speed allowed
        /// </summary>
        protected const float MAX_ANGULAR_SPEED = 360f;

        #endregion

        #region Fields & Properties

        [Header("Rotation Settings")] [SerializeField] [Tooltip("Axis of object's rotation.")]
        private Axis _rotationAxis = Axis.Y;

        [SerializeField]
        [Tooltip("Angular speed of object's rotation.\nSpecifies degrees rotated.")]
        [Range(0f, MAX_ANGULAR_SPEED)]
        private float _angularSpeed = 45f;

        [SerializeField]
        [Tooltip(
            "If checked, rotation continues indefinitely. If unchecked, stops automatically after completing a full 360-degree rotation.")]
        private bool _continuousRotation = true;

        [SerializeField] [Tooltip("Enable rotation automatically when the scene starts.")]
        private bool _rotateOnStart = true;

        /// <summary>
        /// Indicates whether the object is currently performing an active rotation.
        /// </summary>
        private bool _isRotating = true;

        /// <summary>
        /// Tracks the total degrees rotated during the current rotation cycle.
        /// </summary>
        private float _accumulatedRotation;

        /// <summary>
        /// Stores the calculated degree step to apply for the current frame.
        /// </summary>
        private float _rotationAmount;

        /* NOTE FOR STUDENTS: It is best practice to allow for protected property with a getter and  private setter instead of a raw protected field. This follows the Principle of Encapsulation: child classes can READ the target's Transform safely, but cannot accidentally overwrite or corrupt the reference in the base class.*/

        /// <summary>
        /// Stores the calculated degree step to apply for the current frame.
        /// Exposed as protected because child classes pass this into transform rotation calls.
        /// </summary>
        protected float RotationAmount
        {
            get => _rotationAmount;
            set => _rotationAmount = value;
        }

        /// <summary>
        /// Stores the normalized directional vector for the current frame.
        /// Exposed as protected because child classes pass this into transform rotation calls.
        /// </summary>
        protected Vector3 AxisVector { get; set; }

        /// <summary>
        /// Gets or sets the axis of the object's rotation
        /// </summary>
        public Axis RotationAxis
        {
            get => _rotationAxis;
            set => _rotationAxis = value;
        }

        /// <summary>
        /// Gets or sets the angular speed. Clamped between 0 and <see cref="MAX_ANGULAR_SPEED" />.
        /// </summary>
        public float AngularSpeed
        {
            get => _angularSpeed;
            set => _angularSpeed = Mathf.Clamp(value, 0f, MAX_ANGULAR_SPEED);
        }

        /// <summary>
        /// Gets or sets whether rotation runs indefinitely or halts after a full 360-degree cycle.
        /// </summary>
        public bool ContinuousRotation
        {
            get => _continuousRotation;
            set => _continuousRotation = value;
        }

        #endregion

        #region Initialization & Activation

        // Awake is called once on initialization
        protected virtual void Awake()
        {
            // Re-assign fields through properties to enforce validation rules 
            AngularSpeed = _angularSpeed;
            RotationAxis = _rotationAxis;
            ContinuousRotation = _continuousRotation;
        } //end Awake()

        // Start is called once before the first Update
        protected virtual void Start()
        {
            //Sync initial rotation
            _isRotating = _rotateOnStart;
        } //end Start()

        #endregion

        #region Update Game Loop

        // Update is called once per frame
        protected virtual void Update()
        {
            if (_isRotating) Rotate(); //end if(_isRotating)
        } //end Update()

        #endregion

        #region Feature Logic

        /// <summary>
        /// Executes base frame rotation calculations, state updates, and step clamping.
        /// Override in derived classes to apply specific spatial transformations.
        /// </summary>
        public virtual void Rotate()
        {
            StartRotation();

            // Determine direction and frame step
            AxisVector = RotationUtils.GetAxisVector(RotationAxis);
            _rotationAmount = AngularSpeed * Time.deltaTime;

            // If rotation is not continuous, limit the rotation to one full rotation
            if (!_continuousRotation) ProcessClampedStep(); //end if (!_continuousRotation)
        } //end Rotate()

        /// <summary>
        /// Enables active rotation state and resets accumulated progress if starting a new rotation cycle.
        /// </summary>
        protected virtual void StartRotation()
        {
            // If we finished a previous 360-degree rotation, reset progress back to 0
            if (!_isRotating &&
                _accumulatedRotation >=
                RotationUtils.FULL_ROTATION_DEGREES)
                _accumulatedRotation = 0f; //end if(!_isRotating && _accumulatedRotation >= FULL_ROTATION_DEGREES)

            // Enable rotation state 
            _isRotating = true;
        } //end StartRotation()

        /// <summary>
        /// Stops rotation by disabling the rotation state.
        /// </summary>
        public void StopRotate()
        {
            _isRotating = false;
        } //end StopRotate()

        /// <summary>
        /// Clamps the frame degree step to prevent exceeding 360 degrees and halts rotation upon cycle completion.
        /// </summary>
        private void ProcessClampedStep()
        {
            // Clamps step, updates _accumulatedRotation directly, and stops rotation if complete
            RotationAmount = RotationUtils.ClampRotationStep(
                RotationAmount,
                ref _accumulatedRotation,
                RotationUtils.FULL_ROTATION_DEGREES,
                out var isComplete
            );

            // Stop rotating once the 360-degree target is reached
            if (isComplete) StopRotate(); //end if(isComplete)
        } //end ProcessClampedStep()

        #endregion
        
    } //end RotateTransform
} //end namespace CSG.Transform.Rotation