/************************************************************
* COPYRIGHT:  2026
* PROJECT:    Hello World
* FILE NAME:  HelloWorld.cs
* DESCRIPTION: Send debug message to console
*                    
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2026/09/14 | Elaina Martin | Created class
*
************************************************************/

using UnityEngine;


/// <summary>
///Testing class for demonstrating the Debug methods.Log methods
/// </summary>
public class HelloWorld : MonoBehaviour
{
    #region Fields & Properties
    [Header("General Settings")]
    
    [SerializeField]
    [Tooltip("The message displayed in the console debug log.")]
    private string Message = "Hello World";

    #endregion


   #region Initialization & Activation

    // Awake is called once on initialization
    private void Awake()
    {
        Debug.Log(Message);
        
    }//end Awake()

    // Start is called once before the first Update
    private void Start()
    {
        Debug.Log("Starting");
    }//end Start()
    
   #endregion
   
   #region Updates

    // Update is called once per frame
    private void Update()
    {
        Debug.Log("<color=yellow>Updating</color>");
    }//end Update()
    
    #endregion


    #region [ Feature / Domain Name ]

    /// <summary>
    /// A custom method example.
    /// </summary>
    /// <param name="exampleParameter">A parameter that demonstrates passing data to the method.</param>
    private void CustomMethod(int exampleParameter)
    {
        
    }//end CustomMethod(int)

    #endregion
    

}//end HelloWorld