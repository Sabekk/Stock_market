using Gameplay.Companies;
using UnityEngine;

namespace EventSystem.Gameplay
{
    public class GameplayEvents
    {
        public CoreEvents CoreEv { get; private set; } = new CoreEvents();
        public SharesEvents SharesEv { get; private set; } = new SharesEvents();


        /// <summary>
        /// Core events
        /// </summary>
        public class CoreEvents
        {
            /// <summary>
            /// Called when all managers was initialized
            /// </summary>
            public Events.Event OnManagersInitialized = new Events.Event();
        }

        /// <summary>
        /// Events of company shares
        /// </summary>
        public class SharesEvents
        {
            /// <summary>
            /// Called when shares changing. Company | shares delta
            /// </summary>
            public Events.Event<Company, int> OnSharesChanging = new Events.Event<Company, int>();
        }
    }
}
