/************************************************************
* COPYRIGHT:  2026
* PROJECT:    Name of Project or Assignment
* FILE NAME:  MoveRigidbody.cs
* DESCRIPTION: Base class for movement components.
*              Provides shared movement settings and controls.
*                    
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2026/09/27 | Akram Taghavi-Burris | Created class
*
************************************************************/


using UnityEngine;

namespace CSG.General.Movement
{
    
    /// <summary>
    /// Provides shared settings and controls for movement components.
    /// </summary>
    public abstract class MovementBase : MonoBehaviour
    {

        #region Constants

        /// <summary>
        /// Maximum speed allowed
        /// </summary>
        private const float MAX_SPEED = 20f;

        #endregion

        #region Fields & Properties

        [Header("Movement Settings")]
        [SerializeField]
        [Tooltip("Speed of the object's movement.")]
        [Range(0f, MAX_SPEED)]
        private float _speed = 5f;

        [SerializeField] [Tooltip("Direction vector for the object's movement.")]
        private Vector3 _direction = Vector3.right;

        [SerializeField]
        [Tooltip("Determines whether movement uses World Space or Self Space. Self Space is the default.")]
        private Space _movementSpace = Space.Self;

        [SerializeField] [Tooltip("Enable movement automatically when the scene starts.")]
        private bool _moveOnStart = true;

        /// <summary>
        /// Tracks whether the object is currently in an active movement state.
        /// </summary>
        private bool _isMoving = true;
        
        /// <summary>
        /// Gets or sets whether movement is currently active.
        /// </summary>
        protected bool IsMoving
        {
            get => _isMoving;
            set => _isMoving = value;
        }
        
        /// <summary>
        /// Gets or sets the coordinate space used to determine the movement direction.
        /// </summary>
        public Space MovementSpace
        {
            get => _movementSpace;
            set => _movementSpace = value;
        }

        /// <summary>
        /// Gets or sets the movement speed. Clamped between 0 and <see cref="MAX_SPEED" />.
        /// </summary>
        public float Speed
        {
            get => _speed;
            set => _speed = Mathf.Clamp(value, 0f, MAX_SPEED);
        }

        /// <summary>
        /// Gets or sets the movement direction. Values are automatically normalized upon assignment.
        /// </summary>
        public Vector3 Direction
        {
            get => _direction;
            set => _direction = value.normalized;
        }

        /// <summary>
        /// Gets or sets whether movement should begin on Start()
        /// </summary>
        public bool MoveOnStart
        {
            get => _moveOnStart;
            set => _moveOnStart = value;
        }
        

        #endregion

        #region Initialization & Activation

        // Awake is called once on initialization
        protected virtual void Awake()
        {

            // Re-assign fields through properties to enforce validation rules (clamping/normalization)
            MovementSpace = _movementSpace;
            Speed = _speed;
            Direction = _direction;
            MoveOnStart = _moveOnStart;

            Debug.Log("Initialized Vales - Movement Space: " + MovementSpace + ", Speed: " + Speed + ", Direction: " +
                      Direction + ", MoveOnStart: " + MoveOnStart);
        } //end Awake()

        // Start is called once before the first Update
        protected virtual void Start()
        {
            //Sync initial movement
            _isMoving = _moveOnStart;
            
        } //end Start()

        #endregion
        
        #region Feature Logic

        /// <summary>
        /// Moves the object using the movement behavior defined by the child class.
        /// </summary>
        public abstract void Move(); //end Move()

        /// <summary>
        /// Stops movement by disabling the movement state.
        /// </summary>
        public virtual void Stop()
        {
            _isMoving = false;
            
        }//end Stop()

        #endregion

    }//end  RigidBodyMovementBase

}//end namespace CSG.Physics.Movement