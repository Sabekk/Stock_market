using Database;
using EventSystem;
using Gameplay.Companies;
using Gameplay.Managment;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerManager : GameplayManager<PlayerManager>
    {
        #region VARIABLES

        private int currentMoney;
        private Dictionary<int, int> sharesOfCompanies;

        #endregion

        #region PROPERTIES

        public int CurrentMoney => currentMoney;
        public Dictionary<int, int> SharesOfCompanies => sharesOfCompanies;

        #endregion

        #region METHODS

        public override void Initialzie()
        {
            base.Initialzie();
            SetMoney();
            ResetShares();
        }

        protected override void AttachEvents()
        {
            base.AttachEvents();
            Events.Gameplay.SharesEv.OnSharesChanging += HandleSharesChanging;
        }

        protected override void DetachEvents()
        {
            base.DetachEvents();
            Events.Gameplay.SharesEv.OnSharesChanging -= HandleSharesChanging;
        }

        public int GetShares(int companyId)
        {
            SharesOfCompanies.TryGetValue(companyId, out int shares);
            return shares;
        }

        private void ChangeShares(int companyId, int sharesDelta)
        {
            if (SharesOfCompanies.ContainsKey(companyId) == false)
            {
                SharesOfCompanies.Add(companyId, sharesDelta);
                return;
            }

            SharesOfCompanies[companyId] += sharesDelta;

            if (SharesOfCompanies[companyId] < 0)
            {
                SharesOfCompanies[companyId] = 0;
                Debug.LogError("Error setting shares. Check conditions");
            }
        }

        private void ChangeMoney(int delta)
        {
            currentMoney += delta;
        }

        private void SetMoney()
        {
            currentMoney = MainDatabases.Instance.GameplaySettings.AssumptionsSettings.PlayerMoney;
        }

        private void ResetShares()
        {
            if (SharesOfCompanies == null)
                sharesOfCompanies = new();

            SharesOfCompanies.Clear();
        }

        #region HANDLERS

        private void HandleSharesChanging(Company company, int sharesDelta)
        {
            ChangeShares(company.Id, sharesDelta);

            int totalCost = company.GetPrizeOfShares(sharesDelta);
            ChangeMoney(-totalCost);
        }

        #endregion

        #endregion
    }
}