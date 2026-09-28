/************************************************************
 * COPYRIGHT:  2026
 * PROJECT:    Sandbox
 * FILE NAME:  ScaleOverTime.cs
 * DESCRIPTION: Scalable utility for modifying Unity GameObject local scale over time with multiple repeat modes.
 *
 * REVISION HISTORY:
 * Date [YYYY/MM/DD] | Author | Comments
 * ------------------------------------------------------------
 * 2026/09/25 | Akram Taghavi-Burris | Created class
 *
 ************************************************************/


using UnityEngine;

namespace CSG.Transform.Scale
{
    /// <summary>
    /// Provides modular scaling transitions for Unity GameObjects over a set duration.
    /// Supports various scaling behaviors including single execution, continuous looping, back-and-forth ping-pong, and
    /// smooth pulsing.
    /// </summary>
    public class ScaleOverTime : MonoBehaviour
    {
        #region Enums

        /// <summary>
        /// Defines the repetition behavior for the scale animation.
        /// </summary>
        public enum RepeatMode
        {
            Once,
            Loop,
            PingPong,
            Pulse
        }

        #endregion

        #region Constants

        /// <summary>
        /// Moves the cosine value up by 1.
        /// </summary>
        private const float COSINE_OFFSET = 1f;

        /// <summary>
        /// Represents one full circle (360 degrees) in radians (2 * PI).
        /// </summary>
        private const float FULL_CIRCULAR_RADIANS = 2f * Mathf.PI;

        /// <summary>
        /// Divides the value by 2 to fit it into a 0-to-1 range.
        /// </summary>
        private const float HALF_RANGE = 0.5f;

        #endregion

        #region Fields & Properties

        [Header("Scale Settings")] [SerializeField] [Tooltip("Target scale to reach at the end of the scaling cycle.")]
        private Vector3 _targetScale = Vector3.one * 2f;

        [SerializeField] [Tooltip("Time in seconds required to transition from start scale to target scale.")]
        private float _scaleDuration = 1f;

        [SerializeField]
        [Tooltip(
            "Method used to repeat or finish the scale animation:\n• Once: Stops after reaching end scale.\n• Loop: Resets to start scale and repeats.\n• PingPong: Alternates back and forth between start and end scale.\n• Pulse: Smoothly eases in and out between start and end scale.")]
        private RepeatMode _scaleRepeatMode = RepeatMode.Once;

        [SerializeField] [Tooltip("Enable scaling automatically when the scene starts.")]
        private bool _scaleOnStart = true;

        // Runtime Scaling State & Timer Tracking
        private bool _isScaling = true;
        private float _elapsedTime;

        /// <summary>
        /// Gets or sets the initial scale vector.
        /// Initial scale captured on initialization
        /// </summary>
        public Vector3 StartScale
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the target scale vector.
        /// </summary>
        public Vector3 TargetScale
        {
            get => _targetScale;
            set => _targetScale = value;
        }

        /// <summary>
        /// Gets or sets the transition duration in seconds. Minimum value is 0.
        /// </summary>
        public float ScaleDuration
        {
            get => _scaleDuration;
            set => _scaleDuration = Mathf.Max(0f, value);
        }

        /// <summary>
        /// Gets or sets the repetition mode for the scale animation.
        /// </summary>
        public RepeatMode ScaleRepeatMode
        {
            get => _scaleRepeatMode;
            set => _scaleRepeatMode = value;
        }

        #endregion
        
        #region Initialization & Activation

        // Awake is called once on initialization
        private void Awake()
        {
            // Re-assign fields through properties to enforce validation rules 
            StartScale = transform.localScale;
            TargetScale = _targetScale;
            ScaleDuration = _scaleDuration;
            ScaleRepeatMode = _scaleRepeatMode;

            Debug.Log(
                $"Initialized Values - Start Scale: {StartScale}, Target Scale: {TargetScale}, Scale Duration: {ScaleDuration}, Scale RepeatMode: {ScaleRepeatMode}");
        } //end Awake()

        // Start is called once before the first Update
        private void Start()
        {
            _isScaling = _scaleOnStart;
        } //end Start()

        #endregion

        #region Update Game Loop

        // Update is called once per frame
        private void Update()
        {
            if (_isScaling) Scale(); //end if(_isScaling)
        } //end Update()

        #endregion

        #region Feature Logic

        /// <summary>
        /// Evaluates elapsed time and routes execution to the active <see cref="RepeatMode" /> method.
        /// </summary>
        public void Scale()
        {
            // Enable scale state 
            _isScaling = true;

            _elapsedTime += Time.deltaTime;
            
            var travelTime = GetNormalizedTime();

            // Apply pulse easing curve to the normalized loop time
            if (ScaleRepeatMode == RepeatMode.Pulse)
                travelTime =
                    HALF_RANGE *
                    (COSINE_OFFSET - Mathf.Cos(FULL_CIRCULAR_RADIANS * travelTime)); //end (RepeatMode.Pulse)

            // Interpolate and assign new local scale based on calculated progress
            transform.localScale = Vector3.Lerp(StartScale, TargetScale, travelTime);

            // Stop scaling when the single execution mode completes
            if (ScaleRepeatMode == RepeatMode.Once &&
                _elapsedTime >= ScaleDuration) StopScale(); //end if(RepeatMode.Once)
        } //end Scale()

        /// <summary>
        /// Stops rotation by disabling the rotation state.
        /// </summary>
        public void StopScale()
        {
            _isScaling = false;
        } //end StopScale()

        #endregion

        #region Helpers

        /// <summary>
        /// Calculates normalized time [0, 1] based on the selected <see cref="RepeatMode" />.
        /// </summary>
        private float GetNormalizedTime()
        {
            switch (ScaleRepeatMode)
            {
                case RepeatMode.Once:
                    // Clamps progress between 0.0 and 1.0
                    return Mathf.Clamp01(_elapsedTime / ScaleDuration);

                case RepeatMode.Loop:
                case RepeatMode.Pulse:
                    // Wraps elapsed time within the cycle duration and normalizes it to a [0, 1] range for looping.
                    return _elapsedTime % ScaleDuration / ScaleDuration;

                case RepeatMode.PingPong:
                    // Oscillates back and forth normalized to [0, 1]
                    return Mathf.PingPong(_elapsedTime, ScaleDuration) / ScaleDuration;

                default:
                    return 0f;
            } //end switch(ScaleRepeatMode) 
        } //end GetNormalizedTIme()

        #endregion
        
    } //end ScaleOverTime
} //end namespace CSG.Transform.Scale