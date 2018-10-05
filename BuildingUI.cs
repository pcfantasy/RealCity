using System.Collections.Generic;
using ColossalFramework.UI;
using UnityEngine;
using ColossalFramework;
using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace RealCity
{
    public class BuildingUI : UIPanel
    {
        public static readonly string cacheName = "BuildingUI";

        private static readonly float SPACING = 15f;

        private static readonly float SPACING22 = 22f;

        private Dictionary<string, UILabel> _valuesControlContainer = new Dictionary<string, UILabel>(16);

        public ZonedBuildingWorldInfoPanel baseBuildingWindow;

        public static bool refesh_once = false;

        //1、citizen tax income
        private UILabel buildingmoney;
        private UILabel buildingincomebuffer;
        private UILabel buildingoutgoingbuffer;
        //private UILabel aliveworkcount;
        private UILabel employfee;
        private UILabel landrent;

        private UILabel buy_price;
        private UILabel sell_price;
        private UILabel comsuptiondivide;
        private UILabel sell_tax;
        private UILabel buy2sell_profit;

        public override void Update()
        {
            this.RefreshDisplayData();
            base.Update();
        }

        public override void Awake()
        {
            base.Awake();
            //DebugLog.LogToFileOnly("buildingUI start now");
            //DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "Go to UI now");
            this.DoOnStartup();
        }

        public override void Start()
        {
            base.Start();
            //DebugLog.LogToFileOnly("buildingUI start now");
            //DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "Go to UI now");
            base.backgroundSprite = "MenuPanel";
            this.canFocus = true;
            this.isInteractive = true;
            base.isVisible = true;
            //this.BringToFront();
            base.opacity = 1f;
            base.cachedName = cacheName;
            this.RefreshDisplayData();
            base.Hide();
        }

        private void DoOnStartup()
        {
            this.ShowOnGui();
            base.Hide();          
        }


        private void ShowOnGui()
        {
            this.buildingmoney = base.AddUIComponent<UILabel>();
            this.buildingmoney.text = language.BuildingUI[0];
            this.buildingmoney.tooltip = language.BuildingUI[1];
            this.buildingmoney.relativePosition = new Vector3(SPACING, 50f);
            this.buildingmoney.autoSize = true;
            this.buildingmoney.name = "Moreeconomic_Text_0";

            this.buildingincomebuffer = base.AddUIComponent<UILabel>();
            this.buildingincomebuffer.text = language.BuildingUI[2];
            this.buildingincomebuffer.tooltip = language.BuildingUI[3];
            this.buildingincomebuffer.relativePosition = new Vector3(SPACING, this.buildingmoney.relativePosition.y + SPACING22);
            this.buildingincomebuffer.autoSize = true;
            this.buildingincomebuffer.name = "Moreeconomic_Text_1";

            this.buildingoutgoingbuffer = base.AddUIComponent<UILabel>();
            this.buildingoutgoingbuffer.text = language.BuildingUI[4];
            this.buildingoutgoingbuffer.tooltip = language.BuildingUI[5];
            this.buildingoutgoingbuffer.relativePosition = new Vector3(SPACING, this.buildingincomebuffer.relativePosition.y + SPACING22);
            this.buildingoutgoingbuffer.autoSize = true;
            this.buildingoutgoingbuffer.name = "Moreeconomic_Text_2";

            /*this.aliveworkcount = base.AddUIComponent<UILabel>();
            this.aliveworkcount.text = language.BuildingUI[6];
            this.aliveworkcount.tooltip = language.BuildingUI[7];
            this.aliveworkcount.relativePosition = new Vector3(SPACING, this.buildingoutgoingbuffer.relativePosition.y + SPACING22);
            this.aliveworkcount.autoSize = true;
            this.aliveworkcount.name = "Moreeconomic_Text_3";*/

            this.employfee = base.AddUIComponent<UILabel>();
            this.employfee.text = language.BuildingUI[8];
            this.employfee.tooltip = language.BuildingUI[9];
            this.employfee.relativePosition = new Vector3(SPACING, this.buildingoutgoingbuffer.relativePosition.y + SPACING22);
            this.employfee.autoSize = true;
            this.employfee.name = "Moreeconomic_Text_4";

            this.landrent = base.AddUIComponent<UILabel>();
            this.landrent.text = language.BuildingUI[10];
            this.landrent.tooltip = language.BuildingUI[11];
            this.landrent.relativePosition = new Vector3(SPACING, this.employfee.relativePosition.y + SPACING22);
            this.landrent.autoSize = true;
            this.landrent.name = "Moreeconomic_Text_5";

            this.buy_price = base.AddUIComponent<UILabel>();
            this.buy_price.text = language.BuildingUI[18];
            this.buy_price.tooltip = language.BuildingUI[18];
            this.buy_price.relativePosition = new Vector3(SPACING, this.landrent.relativePosition.y + SPACING22);
            this.buy_price.autoSize = true;
            this.buy_price.name = "Moreeconomic_Text_5";

            this.sell_price = base.AddUIComponent<UILabel>();
            this.sell_price.text = language.BuildingUI[19];
            this.sell_price.tooltip = language.BuildingUI[19];
            this.sell_price.relativePosition = new Vector3(SPACING, this.buy_price.relativePosition.y + SPACING22);
            this.sell_price.autoSize = true;
            this.sell_price.name = "Moreeconomic_Text_5";

            this.comsuptiondivide = base.AddUIComponent<UILabel>();
            this.comsuptiondivide.text = language.BuildingUI[21];
            this.comsuptiondivide.tooltip = language.BuildingUI[21];
            this.comsuptiondivide.relativePosition = new Vector3(SPACING, this.sell_price.relativePosition.y + SPACING22);
            this.comsuptiondivide.autoSize = true;
            this.comsuptiondivide.name = "Moreeconomic_Text_5";

            this.sell_tax = base.AddUIComponent<UILabel>();
            this.sell_tax.text = language.BuildingUI[22];
            this.sell_tax.tooltip = language.BuildingUI[22];
            this.sell_tax.relativePosition = new Vector3(SPACING, this.comsuptiondivide.relativePosition.y + SPACING22);
            this.sell_tax.autoSize = true;
            this.sell_tax.name = "Moreeconomic_Text_5";

            this.buy2sell_profit = base.AddUIComponent<UILabel>();
            this.buy2sell_profit.text = language.BuildingUI[23];
            this.buy2sell_profit.tooltip = language.BuildingUI[23];
            this.buy2sell_profit.relativePosition = new Vector3(SPACING, this.sell_tax.relativePosition.y + SPACING22);
            this.buy2sell_profit.autoSize = true;
            this.buy2sell_profit.name = "Moreeconomic_Text_5";
        }

        private void RefreshDisplayData()
        {
            uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
            uint num2 = currentFrameIndex & 255u;


            if (refesh_once || (comm_data.last_buildingid != WorldInfoPanel.GetCurrentInstanceID().Building))
            {
                //DebugLog.LogToFileOnly("buildingUI try to refreshing");
                if (base.isVisible)
                {
                    comm_data.last_buildingid = WorldInfoPanel.GetCurrentInstanceID().Building;
                    Building buildingdata = Singleton<BuildingManager>.instance.m_buildings.m_buffer[comm_data.last_buildingid];
                    if (buildingdata.Info.m_class.m_service == ItemClass.Service.Residential)
                    {
                        base.Hide();
                    }
                    else
                    {
                        int aliveWorkerCount = 0;
                        int totalWorkerCount = 0;
                        float num = caculate_employee_outcome(buildingdata, comm_data.last_buildingid, out aliveWorkerCount, out totalWorkerCount);
                        int num1 = process_land_fee(buildingdata, comm_data.last_buildingid);
                        this.buildingmoney.text = string.Format(language.BuildingUI[0] + " [{0}]", comm_data.building_money[comm_data.last_buildingid]);
                        this.buildingincomebuffer.text = string.Format(language.BuildingUI[2] + " [{0}]", buildingdata.m_customBuffer1);
                        this.buildingoutgoingbuffer.text = string.Format(language.BuildingUI[4] + " [{0}]", buildingdata.m_customBuffer2);
                        //this.aliveworkcount.text = string.Format(language.BuildingUI[6] + " [{0}]", aliveWorkerCount);
                        this.employfee.text = language.BuildingUI[8] + " " + num.ToString() + " " + language.BuildingUI[16];                       
                        this.landrent.text = string.Format(language.BuildingUI[10] + " [{0:N2}]", (float)num1 / 100f);
                    }
                    //this.alivevisitcount.text = string.Format(language.BuildingUI[14] + " [{0}]", totalWorkerCount);
                    float price = 0f;
                    float price2 = 0f;
                    if (buildingdata.Info.m_buildingAI is IndustrialExtractorAI)
                    {
                        this.buy_price.text = string.Format(language.BuildingUI[18] + " N/A");
                    }
                    else
                    {
                        if (buildingdata.Info.m_class.m_subService == ItemClass.SubService.IndustrialGeneric)
                        {
                            price = pc_PrivateBuildingAI.pre_good_price / 4f;
                        }
                        else
                        {
                            if (buildingdata.Info.m_class.m_subService == ItemClass.SubService.IndustrialFarming)
                            {
                                price = pc_PrivateBuildingAI.food_price;
                            }

                            if (buildingdata.Info.m_class.m_subService == ItemClass.SubService.IndustrialForestry)
                            {
                                price = pc_PrivateBuildingAI.lumber_price;
                            }

                            if (buildingdata.Info.m_class.m_subService == ItemClass.SubService.IndustrialOil)
                            {
                                price = pc_PrivateBuildingAI.petrol_price;
                            }

                            if (buildingdata.Info.m_class.m_subService == ItemClass.SubService.IndustrialOre)
                            {
                                price = pc_PrivateBuildingAI.coal_price;
                            }
                        }
                        this.buy_price.text = string.Format(language.BuildingUI[18] + " [{0:N2}]", price);
                    }



                    if (buildingdata.Info.m_class.m_subService == ItemClass.SubService.IndustrialGeneric)
                    {
                        price2 = pc_PrivateBuildingAI.good_price;
                    }
                    else
                    {
                        if (buildingdata.Info.m_class.m_subService == ItemClass.SubService.IndustrialFarming)
                        {
                            price2 = pc_PrivateBuildingAI.grain_price;
                        }

                        if (buildingdata.Info.m_class.m_subService == ItemClass.SubService.IndustrialForestry)
                        {
                            price2 = pc_PrivateBuildingAI.log_price;
                        }

                        if (buildingdata.Info.m_class.m_subService == ItemClass.SubService.IndustrialOil)
                        {
                            price2 = pc_PrivateBuildingAI.oil_price;
                        }

                        if (buildingdata.Info.m_class.m_subService == ItemClass.SubService.IndustrialOre)
                        {
                            price2 = pc_PrivateBuildingAI.ore_price;
                        }
                    }

                    if (buildingdata.Info.m_class.m_service == ItemClass.Service.Commercial)
                    {
                        price2 = 1;
                        this.sell_price.text = string.Format(language.BuildingUI[19] + " [{0:N2}]", price2);
                    }
                    else
                    {
                        this.sell_price.text = string.Format(language.BuildingUI[19] + " [{0:N2}]", price2);
                    }

                    float ConsumptionDivider = 0f;
                    if (buildingdata.Info.m_class.m_service == ItemClass.Service.Commercial)
                    {
                        ConsumptionDivider = (float)pc_PrivateBuildingAI.get_comsumptiondivider(buildingdata, comm_data.last_buildingid);
                        this.comsuptiondivide.text = string.Format(language.BuildingUI[21] + " [1:{0:N2}]", ConsumptionDivider);
                    }
                    else if (buildingdata.Info.m_class.m_subService == ItemClass.SubService.IndustrialGeneric)
                    {
                        ConsumptionDivider = (float)pc_PrivateBuildingAI.get_comsumptiondivider(buildingdata, comm_data.last_buildingid);
                        this.comsuptiondivide.text = string.Format(language.BuildingUI[21] + " [1:{0:N2}]", ConsumptionDivider);
                    }
                    else
                    {
                        if (buildingdata.Info.m_buildingAI is IndustrialBuildingAI)
                        {
                            ConsumptionDivider = (float)pc_PrivateBuildingAI.get_comsumptiondivider(buildingdata, comm_data.last_buildingid);
                            this.comsuptiondivide.text = string.Format(language.BuildingUI[21] + " [1:{0:N2}]", ConsumptionDivider);
                        }
                        else
                        {
                            this.comsuptiondivide.text = string.Format(language.BuildingUI[21] + " N/A");
                        }
                    }

                    if (buildingdata.Info.m_class.m_subService == ItemClass.SubService.IndustrialGeneric)
                    {
                        ConsumptionDivider = ConsumptionDivider * 4f;
                    }

                    float sell_tax_1 = pc_PrivateBuildingAI.get_tax_rate(buildingdata, comm_data.last_buildingid);

                    this.sell_tax.text = string.Format(language.BuildingUI[22] + " [{0}%]", (int)(sell_tax_1 * 100f));


                    if (ConsumptionDivider == 0f)
                    {
                        this.buy2sell_profit.text = string.Format(language.BuildingUI[23] + " N/A");
                    }
                    else
                    {
                        float temp = (price2 * (1 - sell_tax_1) - (price / ConsumptionDivider)) / price2;
                        if (buildingdata.Info.m_class.m_service == ItemClass.Service.Commercial)
                        {
                            this.buy2sell_profit.text = string.Format(language.BuildingUI[23] + " [{0}%]" + language.BuildingUI[24], (int)(temp * 100f));
                        }
                        else
                        {
                            this.buy2sell_profit.text = string.Format(language.BuildingUI[23] + " [{0}%]", (int)(temp * 100f));
                        }
                    }

                    this.BringToFront();
                    BuildingUI.refesh_once = false;
                }
            }
        }

        public static float caculate_employee_outcome(Building building, ushort buildingID, out int aliveWorkerCount, out int totalWorkerCount)
        {
            float num1 = 0;
            Citizen.BehaviourData behaviour = default(Citizen.BehaviourData);
            aliveWorkerCount = 0;
            totalWorkerCount = 0;
            GetWorkBehaviour(buildingID, ref building, ref behaviour, ref aliveWorkerCount, ref totalWorkerCount);


            if (building.Info.m_class.m_service == ItemClass.Service.Commercial)
            {
                if (comm_data.building_money[buildingID] > (pc_PrivateBuildingAI.good_price * 2000))
                {
                    switch (building.Info.m_class.m_subService)
                    {
                        case ItemClass.SubService.CommercialLow:
                            if (building.Info.m_class.m_level == ItemClass.Level.Level1)
                            {
                                num1 = (int)((comm_data.building_money[buildingID] - (pc_PrivateBuildingAI.good_price * 2000)) * 0.1f / totalWorkerCount);
                            }
                            if (building.Info.m_class.m_level == ItemClass.Level.Level2)
                            {
                                num1 = (int)((comm_data.building_money[buildingID] - (pc_PrivateBuildingAI.good_price * 2000)) * 0.3f / totalWorkerCount);
                            }
                            if (building.Info.m_class.m_level == ItemClass.Level.Level3)
                            {
                                num1 = (int)((comm_data.building_money[buildingID] - (pc_PrivateBuildingAI.good_price * 2000)) * 0.6f / totalWorkerCount);
                            }
                            break;
                        case ItemClass.SubService.CommercialHigh:
                            if (building.Info.m_class.m_level == ItemClass.Level.Level1)
                            {
                                num1 = (int)((comm_data.building_money[buildingID] - (pc_PrivateBuildingAI.good_price * 2000)) * 0.2f / totalWorkerCount);
                            }
                            if (building.Info.m_class.m_level == ItemClass.Level.Level2)
                            {
                                num1 = (int)((comm_data.building_money[buildingID] - (pc_PrivateBuildingAI.good_price * 2000)) * 0.4f / totalWorkerCount);
                            }
                            if (building.Info.m_class.m_level == ItemClass.Level.Level3)
                            {
                                num1 = (int)((comm_data.building_money[buildingID] - (pc_PrivateBuildingAI.good_price * 2000)) * 0.7f / totalWorkerCount);
                            }
                            break;
                        case ItemClass.SubService.CommercialLeisure:
                            num1 = (int)((comm_data.building_money[buildingID] - (pc_PrivateBuildingAI.good_price * 2000)) * 0.7f / totalWorkerCount);
                            break;
                        case ItemClass.SubService.CommercialTourist:
                            num1 = (int)((comm_data.building_money[buildingID] - (pc_PrivateBuildingAI.good_price * 2000)) * 0.9f / totalWorkerCount);
                            break;
                        case ItemClass.SubService.CommercialEco:
                            num1 = (int)((comm_data.building_money[buildingID] - (pc_PrivateBuildingAI.good_price * 2000)) * 0.05f / totalWorkerCount);
                            break;
                    }
                }
                else
                {
                    num1 = 0;
                }
            }

            if (building.Info.m_class.m_subService == ItemClass.SubService.IndustrialGeneric)
            {
                if (comm_data.building_money[buildingID] > (pc_PrivateBuildingAI.pre_good_price * 2000))
                {
                    if (building.Info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        num1 = (int)((comm_data.building_money[buildingID] - (pc_PrivateBuildingAI.pre_good_price * 2000)) * 0.1f / totalWorkerCount);
                    }
                    if (building.Info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        num1 = (int)((comm_data.building_money[buildingID] - (pc_PrivateBuildingAI.pre_good_price * 2000)) * 0.3f / totalWorkerCount);
                    }
                    if (building.Info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        num1 = (int)((comm_data.building_money[buildingID] - (pc_PrivateBuildingAI.pre_good_price * 2000)) * 0.6f / totalWorkerCount);
                    }
                }
                else
                {
                    num1 = 0;
                }
            }


            if (building.Info.m_class.m_service == ItemClass.Service.Office)
            {
                if (comm_data.building_money[buildingID] > 0)
                {
                    num1 = (comm_data.building_money[buildingID] / totalWorkerCount);
                }
                else
                {
                    num1 = 0;
                }
            }

            if (building.Info.m_class.m_subService == ItemClass.SubService.IndustrialFarming)
            {
                if (building.Info.m_buildingAI is IndustrialExtractorAI)
                {
                    if (comm_data.building_money[buildingID] > 0)
                    {
                        num1 = (comm_data.building_money[buildingID] * 0.2f / totalWorkerCount);
                    }
                    else
                    {
                        num1 = 0;
                    }
                }
                else
                {
                    if (comm_data.building_money[buildingID] > pc_PrivateBuildingAI.grain_price * 2000)
                    {
                        num1 = (comm_data.building_money[buildingID] * 0.2f / totalWorkerCount);
                    }
                    else
                    {
                        num1 = 0;
                    }
                }
            }


            if (building.Info.m_class.m_subService == ItemClass.SubService.IndustrialForestry)
            {
                if (building.Info.m_buildingAI is IndustrialExtractorAI)
                {
                    if (comm_data.building_money[buildingID] > 0)
                    {
                        num1 = (comm_data.building_money[buildingID] * 0.2f / totalWorkerCount);
                    }
                    else
                    {
                        num1 = 0;
                    }
                }
                else
                {
                    if (comm_data.building_money[buildingID] > pc_PrivateBuildingAI.log_price * 2000)
                    {
                        num1 = (comm_data.building_money[buildingID] * 0.2f / totalWorkerCount);
                    }
                    else
                    {
                        num1 = 0;
                    }
                }
            }


            if (building.Info.m_class.m_subService == ItemClass.SubService.IndustrialOil)
            {
                if (building.Info.m_buildingAI is IndustrialExtractorAI)
                {
                    if (comm_data.building_money[buildingID] > 0)
                    {
                        num1 = (comm_data.building_money[buildingID] * 0.2f / totalWorkerCount);
                    }
                    else
                    {
                        num1 = 0;
                    }
                }
                else
                {
                    if (comm_data.building_money[buildingID] > pc_PrivateBuildingAI.oil_price * 2000)
                    {
                        num1 = (comm_data.building_money[buildingID] * 0.2f / totalWorkerCount);
                    }
                    else
                    {
                        num1 = 0;
                    }
                }
            }


            if (building.Info.m_class.m_subService == ItemClass.SubService.IndustrialOre)
            {
                if (building.Info.m_buildingAI is IndustrialExtractorAI)
                {
                    if (comm_data.building_money[buildingID] > 0)
                    {
                        num1 = (comm_data.building_money[buildingID] * 0.2f / totalWorkerCount);
                    }
                    else
                    {
                        num1 = 0;
                    }
                }
                else
                {
                    if (comm_data.building_money[buildingID] > pc_PrivateBuildingAI.ore_price * 2000)
                    {
                        num1 = (comm_data.building_money[buildingID] * 0.2f / totalWorkerCount);
                    }
                    else
                    {
                        num1 = 0;
                    }
                }
            }

            return num1;
        }


       

        public static void GetWorkBehaviour(ushort buildingID, ref Building buildingData, ref Citizen.BehaviourData behaviour, ref int aliveCount, ref int totalCount)
        {
            CitizenManager instance = Singleton<CitizenManager>.instance;
            uint num = buildingData.m_citizenUnits;
            int num2 = 0;
            while (num != 0u)
            {
                if ((ushort)(instance.m_units.m_buffer[(int)((UIntPtr)num)].m_flags & CitizenUnit.Flags.Work) != 0)
                {
                    instance.m_units.m_buffer[(int)((UIntPtr)num)].GetCitizenWorkBehaviour(ref behaviour, ref aliveCount, ref totalCount);
                }
                num = instance.m_units.m_buffer[(int)((UIntPtr)num)].m_nextUnit;
                if (++num2 > 524288)
                {
                    CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                    break;
                }
            }
        }

        public int process_land_fee(Building building, ushort buildingID)
        {
            DistrictManager instance = Singleton<DistrictManager>.instance;
            byte district = instance.GetDistrict(building.m_position);
            DistrictPolicies.Services servicePolicies = instance.m_districts.m_buffer[(int)district].m_servicePolicies;
            DistrictPolicies.Taxation taxationPolicies = instance.m_districts.m_buffer[(int)district].m_taxationPolicies;
            DistrictPolicies.CityPlanning cityPlanningPolicies = instance.m_districts.m_buffer[(int)district].m_cityPlanningPolicies;

            int num = 0;
            GetLandRent(building, buildingID, out num);
            int num2;
            num2 = Singleton<EconomyManager>.instance.GetTaxRate(building.Info.m_class, taxationPolicies);
            if (((taxationPolicies & DistrictPolicies.Taxation.DontTaxLeisure) != DistrictPolicies.Taxation.None) && (building.Info.m_class.m_subService == ItemClass.SubService.CommercialLeisure))
            {
                num = 0;
            }

            if (comm_data.building_money[buildingID] < 0)
            {
                num = 0;
            }
            return num*num2;
        }


        public void GetLandRent(Building building, ushort buildingID, out int incomeAccumulation)
        {
            ItemClass @class = building.Info.m_class;
            incomeAccumulation = 0;
            ItemClass.SubService subService = @class.m_subService;
            switch (subService)
            {
                case ItemClass.SubService.IndustrialFarming:
                    incomeAccumulation = comm_data.indu_farm;
                    break;
                case ItemClass.SubService.IndustrialForestry:
                    incomeAccumulation = comm_data.indu_forest;
                    break;
                case ItemClass.SubService.IndustrialOil:
                    incomeAccumulation = comm_data.indu_oil;
                    break;
                case ItemClass.SubService.IndustrialOre:
                    incomeAccumulation = comm_data.indu_ore;
                    break;
                case ItemClass.SubService.IndustrialGeneric:
                    if (building.Info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        incomeAccumulation = comm_data.indu_gen_level1;
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        incomeAccumulation = comm_data.indu_gen_level2;
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        incomeAccumulation = comm_data.indu_gen_level3;
                    }
                    break;
                case ItemClass.SubService.CommercialHigh:
                    if (building.Info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        incomeAccumulation = comm_data.comm_high_level1;
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        incomeAccumulation = comm_data.comm_high_level2;
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        incomeAccumulation = comm_data.comm_high_level3;
                    }
                    break;
                case ItemClass.SubService.CommercialLow:
                    if (building.Info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        incomeAccumulation = comm_data.comm_low_level1;
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        incomeAccumulation = comm_data.comm_low_level2;
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        incomeAccumulation = comm_data.comm_low_level3;
                    }
                    break;
                case ItemClass.SubService.CommercialLeisure:
                    incomeAccumulation = comm_data.comm_leisure;
                    break;
                case ItemClass.SubService.CommercialTourist:
                    incomeAccumulation = comm_data.comm_tourist;
                    break;
                case ItemClass.SubService.CommercialEco:
                    incomeAccumulation = comm_data.comm_eco;
                    break;
                default: break;
            }
        }
    }
}
