using EventSystem;
using Gameplay.Companies;
using Gameplay.Managment;
using Gameplay.Player;
using UnityEngine;

namespace Gameplay.StockMarket
{
    public class StockMarketManager : GameplayManager<StockMarketManager>
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public void TryBuySharesOfCompany(int companyId, int sharesToBuy)
        {
            Company company = CompaniesManager.Instance.TryGetCompanyById(companyId);

            if (CanBuyShares(company, sharesToBuy) == false)
                return;

            Events.Gameplay.SharesEv.OnSharesChanging?.Invoke(company, sharesToBuy);
        }

        public void SellShares(int companyId, int sharesToSell)
        {
            Company company = CompaniesManager.Instance.TryGetCompanyById(companyId);

            Events.Gameplay.SharesEv.OnSharesChanging?.Invoke(company, -sharesToSell);
        }

        public bool CanBuyShares(int companyId, int sharesToBuy)
        {
            Company company = CompaniesManager.Instance.TryGetCompanyById(companyId);
            if (company == null)
                return false;

            return CanBuyShares(company, sharesToBuy);
        }

        public bool CanBuyShares(Company company, int sharesToBuy)
        {
            if (company.CurrentSharesCount < sharesToBuy)
                return false;

            int totalCostOfShares = company.GetPrizeOfShares(sharesToBuy);
            if (totalCostOfShares > PlayerManager.Instance.CurrentMoney.CurrentValue)
                return false;

            return true;
        }

        #endregion
    }
}
