using UnityEngine;
using TMPro;
using Gameplay.Player;
using Gameplay.UI.BaseElements;
using EventSystem;

namespace Gameplay.HUD
{
    public class MainHUD : MonoBehaviour, IAttachableEvents
    {
        #region VARIABLES

        [SerializeField] private TextMeshProUGUI roundsCounter;
        [SerializeField] private ValueField moneyValue;
        [SerializeField] private ValueField sharesValue;

        #endregion

        #region PROPERTIES

        private PlayerManager Player => PlayerManager.Instance;

        #endregion

        #region UNITY_METHODS

        private void Awake()
        {
            Events.Gameplay.CoreEv.OnManagersInitialized += HandleManagersInitialized;
        }

        private void OnDestroy()
        {
            DetachEvents();
            Events.Gameplay.CoreEv.OnManagersInitialized -= HandleManagersInitialized;
        }

        #endregion

        #region METHODS

        public void AttachEvents()
        {
            
        }

        public void DetachEvents()
        {
            
        }

        private void Initialize()
        {
            AttachEvents();
            moneyValue.Initialize(Player.CurrentMoney);
            sharesValue.Initialize(Player.SharesValue);
        }

        #region UI_HANDLERS

        public void OnOpenJournalClick()
        {

        }

        public void OnOpenStockMarketClick()
        {

        }

        public void OnEndRoundClick()
        {

        }

        #endregion

        #region HANDLERS

        private void HandleManagersInitialized()
        {
            Initialize();
        }

        #endregion

        #endregion
    }
}