using System;
using UnityEngine;

namespace Client.UI
{
    [Serializable]
    /// <summary>
    /// Structure storing details related to navigation.
    /// </summary>
    public struct BrandoUINavigation : IEquatable<BrandoUINavigation>
    {
        /*
         * This looks like it's not flags, but it is flags,
         * the reason is that Automatic is considered horizontal
         * and verical mode combined
         */
        [Flags]
        /// <summary>
        /// Navigation mode enumeration.
        /// </summary>
        /// <remarks>
        /// This looks like it's not flags, but it is flags, the reason is that Automatic is considered horizontal and vertical mode combined
        /// </remarks>
        /// <example>
        /// <code>
        /// using UnityEngine;
        /// using System.Collections;
        /// using UnityEngine.UI; // Required when Using UI elements.
        ///
        /// public class ExampleClass : MonoBehaviour
        /// {
        ///     public Button button;
        ///
        ///     void Start()
        ///     {
        ///         //Set the navigation to the mode "Vertical".
        ///         if (button.navigation.mode == Navigation.Mode.Vertical)
        ///         {
        ///             Debug.Log("The button's navigation mode is Vertical");
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public enum Mode
        {
            /// <summary>
            /// No navigation is allowed from this object.
            /// </summary>
            None        = 0,

            /// <summary>
            /// Horizontal Navigation.
            /// </summary>
            /// <remarks>
            /// Navigation should only be allowed when left / right move events happen.
            /// </remarks>
            Horizontal  = 1,

            /// <summary>
            /// Vertical navigation.
            /// </summary>
            /// <remarks>
            /// Navigation should only be allowed when up / down move events happen.
            /// </remarks>
            Vertical    = 2,

            /// <summary>
            /// Automatic navigation.
            /// </summary>
            /// <remarks>
            /// Attempt to find the 'best' next object to select. This should be based on a sensible heuristic.
            /// </remarks>
            Automatic   = 3,

            /// <summary>
            /// Explicit navigation.
            /// </summary>
            /// <remarks>
            /// User should explicitly specify what is selected by each move event.
            /// </remarks>
            Explicit    = 4,
        }

        // Which method of navigation will be used.
        [SerializeField]
        private Mode m_Mode;

        // Game object selected when the joystick moves up. Used when navigation is set to "Explicit".
        [SerializeField]
        private BrandoUISelectable m_SelectOnUp;

        // Game object selected when the joystick moves down. Used when navigation is set to "Explicit".
        [SerializeField]
        private BrandoUISelectable m_SelectOnDown;

        // Game object selected when the joystick moves left. Used when navigation is set to "Explicit".
        [SerializeField]
        private BrandoUISelectable m_SelectOnLeft;

        // Game object selected when the joystick moves right. Used when navigation is set to "Explicit".
        [SerializeField]
        private BrandoUISelectable m_SelectOnRight;

        /// <summary>
        /// Navigation mode.
        /// </summary>
        public Mode       mode           { get { return m_Mode; } set { m_Mode = value; } }

        /// <summary>
        /// Specify a Selectable UI GameObject to highlight when the Up arrow key is pressed.
        /// </summary>
        /// <example>
        /// <code>
        /// using UnityEngine;
        /// using System.Collections;
        /// using UnityEngine.UI;  // Required when Using UI elements.
        ///
        /// public class HighlightOnKey : MonoBehaviour
        /// {
        ///     public Button btnSave;
        ///     public Button btnLoad;
        ///
        ///     public void Start()
        ///     {
        ///         // get the Navigation data
        ///         Navigation navigation = btnLoad.navigation;
        ///
        ///         // switch mode to Explicit to allow for custom assigned behavior
        ///         navigation.mode = Navigation.Mode.Explicit;
        ///
        ///         // highlight the Save button if the up arrow key is pressed
        ///         navigation.selectOnUp = btnSave;
        ///
        ///         // reassign the struct data to the button
        ///         btnLoad.navigation = navigation;
        ///     }
        /// }
        /// </code>
        /// </example>
        public BrandoUISelectable selectOnUp     { get { return m_SelectOnUp; } set { m_SelectOnUp = value; } }

        /// <summary>
        /// Specify a Selectable UI GameObject to highlight when the down arrow key is pressed.
        /// </summary>
        /// <example>
        /// <code>
        /// using UnityEngine;
        /// using System.Collections;
        /// using UnityEngine.UI;  // Required when Using UI elements.
        ///
        /// public class HighlightOnKey : MonoBehaviour
        /// {
        ///     public Button btnSave;
        ///     public Button btnLoad;
        ///
        ///     public void Start()
        ///     {
        ///         // get the Navigation data
        ///         Navigation navigation = btnLoad.navigation;
        ///
        ///         // switch mode to Explicit to allow for custom assigned behavior
        ///         navigation.mode = Navigation.Mode.Explicit;
        ///
        ///         // highlight the Save button if the down arrow key is pressed
        ///         navigation.selectOnDown = btnSave;
        ///
        ///         // reassign the struct data to the button
        ///         btnLoad.navigation = navigation;
        ///     }
        /// }
        /// </code>
        /// </example>
        public BrandoUISelectable selectOnDown   { get { return m_SelectOnDown; } set { m_SelectOnDown = value; } }

        /// <summary>
        /// Specify a Selectable UI GameObject to highlight when the left arrow key is pressed.
        /// </summary>
        /// <example>
        /// <code>
        /// using UnityEngine;
        /// using System.Collections;
        /// using UnityEngine.UI;  // Required when Using UI elements.
        ///
        /// public class HighlightOnKey : MonoBehaviour
        /// {
        ///     public Button btnSave;
        ///     public Button btnLoad;
        ///
        ///     public void Start()
        ///     {
        ///         // get the Navigation data
        ///         Navigation navigation = btnLoad.navigation;
        ///
        ///         // switch mode to Explicit to allow for custom assigned behavior
        ///         navigation.mode = Navigation.Mode.Explicit;
        ///
        ///         // highlight the Save button if the left arrow key is pressed
        ///         navigation.selectOnLeft = btnSave;
        ///
        ///         // reassign the struct data to the button
        ///         btnLoad.navigation = navigation;
        ///     }
        /// }
        /// </code>
        /// </example>
        public BrandoUISelectable selectOnLeft   { get { return m_SelectOnLeft; } set { m_SelectOnLeft = value; } }

        /// <summary>
        /// Specify a Selectable UI GameObject to highlight when the right arrow key is pressed.
        /// </summary>
        /// <example>
        /// <code>
        /// using UnityEngine;
        /// using System.Collections;
        /// using UnityEngine.UI;  // Required when Using UI elements.
        ///
        /// public class HighlightOnKey : MonoBehaviour
        /// {
        ///     public Button btnSave;
        ///     public Button btnLoad;
        ///
        ///     public void Start()
        ///     {
        ///         // get the Navigation data
        ///         Navigation navigation = btnLoad.navigation;
        ///
        ///         // switch mode to Explicit to allow for custom assigned behavior
        ///         navigation.mode = Navigation.Mode.Explicit;
        ///
        ///         // highlight the Save button if the right arrow key is pressed
        ///         navigation.selectOnRight = btnSave;
        ///
        ///         // reassign the struct data to the button
        ///         btnLoad.navigation = navigation;
        ///     }
        /// }
        /// </code>
        /// </example>
        public BrandoUISelectable selectOnRight  { get { return m_SelectOnRight; } set { m_SelectOnRight = value; } }

        /// <summary>
        /// Return a Navigation with sensible default values.
        /// </summary>
        /// <example>
        /// <code>
        /// using UnityEngine;
        /// using System.Collections;
        /// using UnityEngine.UI; // Required when Using UI elements.
        ///
        /// public class ExampleClass : MonoBehaviour
        /// {
        ///     public Button button;
        ///
        ///     void Start()
        ///     {
        ///         //Set the navigation to the default value. ("Automatic" is the default value).
        ///         button.navigation = Navigation.defaultNavigation;
        ///     }
        /// }
        /// </code>
        /// </example>
        static public BrandoUINavigation defaultNavigation
        {
            get
            {
                var defaultNav = new BrandoUINavigation();
                defaultNav.m_Mode = Mode.Automatic;
                return defaultNav;
            }
        }

        public bool Equals(BrandoUINavigation other)
        {
            return mode == other.mode &&
                selectOnUp == other.selectOnUp &&
                selectOnDown == other.selectOnDown &&
                selectOnLeft == other.selectOnLeft &&
                selectOnRight == other.selectOnRight;
        }
    }
}
