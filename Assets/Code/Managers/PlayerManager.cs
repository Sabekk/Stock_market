using Database;
using EventSystem;
using Gameplay.Companies;
using Gameplay.Managment;
using Gameplay.Values;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerManager : GameplayManager<PlayerManager>
    {
        #region VARIABLES

        private ModifiableValue currentMoney;
        private ModifiableValue sharesValue;

        private Dictionary<Company, int> sharesOfCompanies;

        #endregion

        #region PROPERTIES

        public ModifiableValue CurrentMoney => currentMoney;
        public ModifiableValue SharesValue => sharesValue;
        public Dictionary<Company, int> SharesOfCompanies => sharesOfCompanies;

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

        public int GetShares(Company company)
        {
            SharesOfCompanies.TryGetValue(company, out int shares);
            return shares;
        }

        private void ChangeShares(Company company, int sharesDelta)
        {
            if (SharesOfCompanies.ContainsKey(company) == false)
            {
                SharesOfCompanies.Add(company, sharesDelta);
                return;
            }

            SharesOfCompanies[company] += sharesDelta;

            if (SharesOfCompanies[company] < 0)
            {
                SharesOfCompanies[company] = 0;
                Debug.LogError("Error setting shares. Check conditions");
            }
        }

        private void ChangeMoney(int delta)
        {
            currentMoney.AddToRawValue(delta);
        }

        private void SetMoney()
        {
            currentMoney = new ModifiableValue(MainDatabases.Instance.GameplaySettings.AssumptionsSettings.PlayerMoney, ValueType.OVERALL);
        }

        private void ResetShares()
        {
            if (sharesValue == null)
                sharesValue = new ModifiableValue(0, ValueType.OVERALL);
            else
                sharesValue.CleanUp();

            if (SharesOfCompanies == null)
                sharesOfCompanies = new();

            SharesOfCompanies.Clear();
            RecalculateSharesValue();
        }

        private void RecalculateSharesValue()
        {
            SharesValue.CleanUp();

            foreach (var sharesOfCompany in SharesOfCompanies)
                SharesValue.AddToRawValue(sharesOfCompany.Key.StockPrize.CurrentValue * (float)GetShares(sharesOfCompany.Key));
        }

        #region HANDLERS

        private void HandleSharesChanging(Company company, int sharesDelta)
        {
            ChangeShares(company, sharesDelta);

            int totalCost = company.GetPrizeOfShares(sharesDelta);
            ChangeMoney(-totalCost);

            RecalculateSharesValue();
        }

        #endregion

        #endregion
    }
}