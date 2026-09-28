/************************************************************
 * COPYRIGHT:  2026
 * PROJECT:    Sandbox
 * FILE NAME:  MoveTransform.cs
 * DESCRIPTION: Moves the object using either world-space position offsets or local-space transform translation.
 *
 * REVISION HISTORY:
 * Date [YYYY/MM/DD] | Author | Comments
 * ------------------------------------------------------------
 * 2026/9/14 | Akram Taghavi-Burris | Created class
 * 2026/9/20 | Akram Taghavi-Burris | Refactored with transfrom.Translate()
 *
 ************************************************************/

using UnityEngine;

namespace CSG.Transform.Movement
{
    /// <summary>
    /// Provides modular translation-based movement for Unity GameObjects.
    /// Supports movement via direct world-space position offset or local space via
    /// <see cref="Transform.Translate(Vector3)" />.
    /// </summary>
    public class MoveTransform : MonoBehaviour
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
        private void Awake()
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
        private void Start()
        {
            //Sync initial movement
            _isMoving = _moveOnStart;
        } //end Start()

        #endregion

        #region Update Game Loop

        // Update is called once per frame
        private void Update()
        {
            if (_isMoving)
            {
                Move(); 
            
            }//end if(_isMoving)
            
        } //end Update()

        #endregion

        #region Feature Logic

        /// <summary>
        /// Move an object in a specified direction at specified speed
        /// </summary>
        public void Move()
        {
            // Enable movement state 
            _isMoving = true;

            // Calculate the object's displacement (i.e. a change in positon; position delta) 
            var displacement = Speed * Time.deltaTime * Direction;

            // Move object using the selected coordinate space
            transform.Translate(displacement, MovementSpace);
        } //end Move()

        /// <summary>
        /// Stops movement by disabling the movement state.
        /// </summary>
        public void Stop()
        {
            _isMoving = false;
            
        }//end Stop()

        #endregion
        
    } //end MoveTransform
} //end namespace CSG.Transform.Movement