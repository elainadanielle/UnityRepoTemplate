/************************************************************
* COPYRIGHT:  2026
* PROJECT:    CSG Debugging Tools
* FILE NAME:  Tester.cs
* DESCRIPTION: Provides master controller for in-game debugging tools
* 
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2026/9/18 | Akram Taghavi-Burris | Created class
*
************************************************************/

using UnityEngine;

namespace CSG.Debugging
{
    /// <summary>
    /// Acts as the master permission controller for in-game debug tools and hotkeys.
    /// Determines whether debug execution is permitted based on the environment 
    /// (Unity Editor vs. Development Build vs. Release Build) and enables or disables 
    /// itself accordingly to prevent debug functionality from running in production.
    /// </summary>
    public class Tester : MonoBehaviour
    {
        #region Fields & Properties

        [Header("Master Settings")]
        [Tooltip("Master switch to enable/disable all attached debug tests.")]
        [SerializeField] private bool enableInEditor = true;

        [Tooltip("If true, this debugger will also work in Development Builds (non-Editor).")]
        [SerializeField] private bool allowInDevelopmentBuilds = false;
        
        // Determine if the environment allows debugging
        bool shouldBeActive = false;
        

        #endregion


        #region Initialization & Activation

        // Awake is called once on initialization
        private void Awake()
        {

#if UNITY_EDITOR
            shouldBeActive = enableInEditor;
#else
            // Enable tester if allowed in development builds AND this is actually a development build
            shouldBeActive = allowInDevelopmentBuilds && Debug.isDebugBuild;
#endif
            // Set component state
            enabled = shouldBeActive;
            
            //Set the tester object state
            gameObject.SetActive(shouldBeActive);

            // Log whenever the component is active, regardless of environment
            if (shouldBeActive)
            {
                Debug.LogWarning($"[Debugging] '{gameObject.name}' has active debug hotkeys!", this);
            }
            
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


    } //end Tester
}//end namspace