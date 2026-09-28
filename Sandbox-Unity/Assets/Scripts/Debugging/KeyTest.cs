/************************************************************
* COPYRIGHT:  2026
* PROJECT:    CSG Debugging Tools
* FILE NAME:  Tester.cs
* DESCRIPTION: Maps physical key inputs to UnityEvents for rapid 
*              method testing inside the Unity Inspector.
* 
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2026/9/18 | Akram Taghavi-Burris | Created class
*
************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace CSG.Debugging
{
    /// <summary>
    /// Binds specific hotkey presses directly to Inspector-configured UnityEvents for rapid method testing.
    /// Automatically checks for a sibling <see cref="Tester"/> component to respect global debug permissions 
    /// before evaluating key presses during Update.
    /// </summary>
    public class KeyTest : MonoBehaviour
    {
        #region Fields & Properties

        [SerializeField] 
        [Tooltip("Assign the method to test with the D key")]
        private UnityEvent onDKeyPressed;

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

        #region Game Updates

        // Update is called once per frame
        private void Update()
        {
            if (Keyboard.current.dKey.wasPressedThisFrame)
            {
                onDKeyPressed?.Invoke();
            }
            
            

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


    } //end KeyTest
    
}//end namespace