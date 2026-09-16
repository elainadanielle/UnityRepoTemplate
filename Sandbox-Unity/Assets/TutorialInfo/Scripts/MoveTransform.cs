/************************************************************
* COPYRIGHT:  2026
* PROJECT:    Sandbox
* FILE NAME:  MoveTransform.cs
* DESCRIPTION: Move the object with transform using world or translate position
*                    
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2026/09/14 | Elaina Martin | Created class
*
************************************************************/

using UnityEngine;

namespace CSG.Transform
{
    
    /// <summary>
    /// What does this specific class do when I reference it in C#?
    /// </summary>
    public class MoveTransform : MonoBehaviour
    {
        #region Fields & Properties
        
        
        //Maximum speed allowed
        private const float MAX_SPEED = 20f;

        [SerializeField] 
        [Tooltip("Speed of the object's movement")]
        [Range(0f, MAX_SPEED)]
        private float _speed = 5f;
        
        [SerializeField]
        [Tooltip("Direction of the object's movement")]
        private Vector3 _direction = Vector3.right;
        
        [SerializeField]
        [Tooltip("Enable move on start")]
        private bool _moveOnStart = true;

        [SerializeField] [Tooltip("Use Translate to move along the object's local axes")]
        private bool _moveOnTranslate = false;
        
        
        //Runtime movement flag
        private bool _isMoving = true;
        
        
        
        
        public float Speed
        {
            get { return _speed; }
            //validate that the speed is not greater than MAX_SPEED
            set => _speed = Mathf.Clamp(value, 0f, MAX_SPEED);
        }

        public Vector3 Direction
        {
            get => _direction;
            set => _direction = value.normalized;
        }
        
        
        #endregion


        #region Initialization & Activation

        // Awake is called once on initialization
        private void Awake()
        {
            
            //Validate initial speed and direction
            Speed = _speed;
            Direction = _direction;
            
            Debug.Log("Initialized Values: "+Speed+" "+Direction);

        } //end Awake()

        // Start is called once before the first Update
        private void Start()
        {
            //sync initial movement
            _isMoving = _moveOnStart;

        } //end Start()

        #endregion

        #region Updates

        // Update is called once per frame
        private void Update()
        {
            if (_isMoving)
            {
                Move();
                
            }//end if(_isMoving
            
            
        } //end Update()

        #endregion

        #region Helpers
        
        
        private void ResolveMovementValues(float? speed, Vector3? direction)
        {
            
            //use the provided values if not null, otherwise keep the current Speed and Direction
            Speed = speed ?? Speed;
            Direction = direction ?? Direction;
            
        }
        #endregion
        
        
        #region [ Feature / Domain Name ]
        

        /// <summary>
        /// Move an object in a specified direction at a specified speed 
        /// </summary>
        /// <param name="speed">The speed at which the object should move.</param>
        public void Move(float? speed = null, Vector3? direction= null)
        {
            ResolveMovementValues(speed, direction);
            
            if(_moveOnTranslate)
            {
                //This moves relative to the object's local axes by default
                transform.Translate(Speed * Time.deltaTime * Direction);
            }
            else
            { 
                //move object by world position
                transform.position += _speed * Time.deltaTime * _direction;
            }//end if(_moveOnTranslate)
            
        } //end CustomMethod(int)

        #endregion


    } //end MoveTransform

}