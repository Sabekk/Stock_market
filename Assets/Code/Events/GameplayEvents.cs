using Gameplay.Companies;
using UnityEngine;

namespace EventSystem.Gameplay
{
    public class GameplayEvents
    {
        public SharesEvents SharesEv { get; private set; } = new SharesEvents();


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
