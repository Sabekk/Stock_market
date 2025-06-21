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

            int totalCostOfShares = company.GetPrizeOfShares(company.CurrentSharesCount);

            company.ChangeShares(-sharesToBuy);
            PlayerManager.Instance.ChangeShares(companyId, sharesToBuy);
            PlayerManager.Instance.ChangeMoney(-totalCostOfShares);
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

            int totalCostOfShares = company.GetPrizeOfShares(company.CurrentSharesCount);
            if (totalCostOfShares > PlayerManager.Instance.CurrentMoney)
                return false;

            return true;
        }

        #endregion
    }
}
