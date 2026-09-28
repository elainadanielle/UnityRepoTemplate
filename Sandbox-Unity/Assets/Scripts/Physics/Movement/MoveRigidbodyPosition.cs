/************************************************************
* COPYRIGHT:  2026
* PROJECT:    Name of Project or Assignment
* FILE NAME:  MoveRigidbodyPosition.cs
* DESCRIPTION: Moves a GameObject's Rigidbody position using a
*              specified direction, speed, and movement space.
*                    
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2026/09/27 | Akram Taghavi-Burris | Created class
*
************************************************************/


using UnityEngine;

namespace CSG.Physics.Movement
{
    /// <summary>
    /// Moves a GameObject's <see cref="Rigidbody"/> using a specified
    /// direction, speed, and movement space.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class MoveRigidbodyPosition : MonoBehaviour
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
        /// Reference to the attached <see cref="UnityEngine.Rigidbody"/> component used for physics movement.
        /// </summary>
        private Rigidbody _rigidBody;

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
            // Store Rigidbody component reference
            _rigidBody = GetComponent<Rigidbody>();
            
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

        #region Update Physics

        // FixedUpdate is called at a fixed interval for physics calculations.
        private void FixedUpdate()
        {
            if (_isMoving)
            {
                Move(); 
                
            }//end if(_isMoving)

        }//end FixedUpdate()

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

            
            if (MovementSpace == Space.Self)
            {
                // Convert the object’s local space direction into World Space
                displacement = transform.TransformDirection(displacement);
                
            }//end if(MovementSpace == Space.Self)

            // Move object using RigidBody's movement 
            _rigidBody.MovePosition(_rigidBody.position + displacement);
            
        } //end Move()

        /// <summary>
        /// Stops movement by disabling the movement state.
        /// </summary>
        public void Stop()
        {
            _isMoving = false;
            
        }//end Stop()

        #endregion

    }//end MoveRigidbody

}//end namespace CSG.Physics.Movement