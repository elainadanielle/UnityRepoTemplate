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

        #endregion


        #region Initialization & Activation

        // Awake is called once on initialization
        private void Awake()
        {

        } //end Awake()

        // Start is called once before the first Update
        private void Start()
        {

        } //end Start()

        #endregion

        #region Updates

        // Update is called once per frame
        private void Update()
        {
            
            transform.position += _speed * Time.deltaTime * Vector3.right;

        } //end Update()

        #endregion


        #region [ Feature / Domain Name ]

        /// <summary>
        /// A custom method example.
        /// </summary>
        /// <param name="exampleParameter">A parameter that demonstrates passing data to the method.</param>
        private void CustomMethod(int exampleParameter)
        {

        } //end CustomMethod(int)

        #endregion


    } //end MoveTransform

}