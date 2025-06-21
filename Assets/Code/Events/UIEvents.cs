
using Gameplay.UI.Windows;

namespace EventSystem.UI
{
    public class UIEvents
    {
        public WindowsEvents WindowsEv { get; private set; } = new WindowsEvents();


        /// <summary>
        /// Events of ui windows
        /// </summary>
        public class WindowsEvents
        {
            /// <summary>
            /// Called when window opened
            /// </summary>
            public Events.Event<UIWindowBase> OnWindowOpened = new Events.Event<UIWindowBase>();

            /// <summary>
            /// Called when window closed
            /// </summary>
            public Events.Event<UIWindowBase> OnWindowClosed = new Events.Event<UIWindowBase>();
        }
    }
}
