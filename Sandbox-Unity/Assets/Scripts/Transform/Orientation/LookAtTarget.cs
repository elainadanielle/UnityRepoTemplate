/************************************************************
 * COPYRIGHT:    2026
 * PROJECT:      Sandbox
 * FILE NAME:    LookAtTarget.cs
 * DESCRIPTION:  Rotates a GameObject to continuously track and face
 *               a designated target position using global World Up or
 *               local Transform Up as the Up direction reference.
 *
 * REVISION HISTORY:
 * Date [YYYY/MM/DD] | Author | Comments
 * ------------------------------------------------------------
 * 2026/09/26 | Akram Taghavi-Burris | created class
 *
 ************************************************************/

using UnityEngine;

namespace CSG.Transform.Orientation
{
    /// <summary>
    /// Rotates this <see cref="GameObject" /> to align its forward facing vector toward a specified target.
    /// <para>
    /// Supports switching between global World Up (<see cref="Vector3.up" />) and local Transform Up (
    /// <see cref="UnityEngine.Transform.up" />) for the tracking Up direction.
    /// </para>
    /// </summary>
    public class LookAtTarget : MonoBehaviour
    {
        #region Fields & Properties

        [Header("General Settings")] [SerializeField] [Tooltip("Game object this object will look at.")]
        private GameObject _targetObject;

        [SerializeField]
        [Tooltip(
            "Use this object's Transform.up as the Up direction when looking at the target. If unchecked, use World Up.")]
        private bool _useTransformUp;

        /* NOTE FOR STUDENTS: It is best practice to allow for protected property with a getter and  private setter instead of a raw protected field. This follows the Principle of Encapsulation: child classes can READ the target's Transform safely, but cannot accidentally overwrite or corrupt the reference in the base class.*/

        /// <summary>
        /// Stores the target's Transform for tracking
        /// </summary>
        protected UnityEngine.Transform TargetTransform { get; private set; }

        /// <summary>
        /// Stores the direction that should be treated as Up when looking at the target.
        /// </summary>
        protected Vector3 UpDirection { get; private set; }

        /// <summary>
        /// Flag indicating whether a valid target Transform reference exists.
        /// </summary>
        protected bool HasTargetTransform;
        /* NOTE FOR STUDENTS: Checking 'if (TargetObject == null)' inside Update() executes Unity's custom '==' operator, which makes an expensive native C++ engine call every frame. Using a cached boolean flag ('HasTargetTransform') allows Update() to perform a pure C# boolean check instead, saving performance. */

        /// <summary>
        /// Gets or sets the game object this object will look at.
        /// </summary>
        public GameObject TargetObject
        {
            get => _targetObject;
            set
            {
                _targetObject = value;
                SetTargetTransform();
            }
        }

        /// <summary>
        /// Gets or sets whether this object's Up direction is used instead of World Up.
        /// </summary>
        public bool UseTransformUp
        {
            get => _useTransformUp;
            set
            {
                _useTransformUp = value;
                SetUpDirection();
            }
        }

        #endregion

        #region Initialization & Activation

        #region Initialization

        // Awake is called once on initialization
        protected virtual void Awake()
        {
            // Re-assign fields through properties to enforce validation rules 
            TargetObject = _targetObject;
            UseTransformUp = _useTransformUp;
        } //end Awake()

        #endregion

        #endregion

        #region Update Game Loop

        // Update is called once per frame
        protected virtual void Update()
        {
            // If a target is set, orientate to target
            if (HasTargetTransform) OrientToTarget(); //en if(HasTargetTransform)
        } //end Update()

        #endregion

        #region Feature Logic
        
        /// <summary>
        /// Rotates this GameObject to face the current target position.
        /// </summary>
        public virtual void OrientToTarget()
        {
            // Orient toward target using the assigned up direction
            transform.LookAt(TargetTransform.position, UpDirection);
        } //end OrientToTarget()

        #endregion

        #region Helpers

        /// <summary>
        /// Stores the target's Transform and updates the target status flag.
        /// </summary>
        private void SetTargetTransform()
        {
            if (_targetObject != null)
            {
                TargetTransform = _targetObject.transform;
                HasTargetTransform = true;
            }
            else
            {
                TargetTransform = null;
                HasTargetTransform = false;

                Debug.LogWarning(
                    $"[{nameof(LookAtTarget)}] Target Object is not assigned on '{gameObject.name}'. Target tracking will be disabled.",
                    this);
            } //end if(_targetObject != null)
        } //end SetTargetTransform()

        /// <summary>
        /// Sets the Up direction used when looking at the target.
        /// </summary>
        private void SetUpDirection()
        {
            if (_useTransformUp)
                // Use this object's own Up direction.
                UpDirection = transform.up;
            else
                // Use the world's Up direction.
                UpDirection = Vector3.up;
        } //end SetUpDirection()

        #endregion
    } //end LookAtTarget
} //end namespace CSG.Transform.Orientation