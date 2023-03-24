using System;
using ColossalFramework.UI;

using RealCity.CustomData;
using System.Text;
using RealCity.Util;
using UnityEngine;
using ColossalFramework.Math;
using RealCity.CustomAI;
using ColossalFramework;

namespace RealCity.UI
{
    class ResidentalBuildingUI
    {
        private UILabel buildingMoney;

        private static readonly float SPACING = 15f;

        public ResidentalBuildingUI(UIPanel parent)
        {
            buildingMoney = parent.AddUIComponent<UILabel>();

            buildingMoney.text = Localization.Get("SALARY_PER_FAMILY");
            buildingMoney.relativePosition = new Vector3(SPACING, 50f);
            buildingMoney.autoSize = true;
        }


        public void Hide()
        {
            buildingMoney.Hide();
        }

        public void Show()
        {
            buildingMoney.Show();
        }

        public void Update(Building buildingData)
        {
            long money = RealCityPrivateBuildingAI.GetResidentialBuildingAverageMoney(BuildingData.lastBuildingID, ref buildingData);

            buildingMoney.text = string.Format(Localization.Get("AVERAGE_FAMILY_MONEY") + " [{0}]", money);
        }
    }
}
