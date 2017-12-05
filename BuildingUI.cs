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

        private UILabel m_HeaderDataText;

        public static bool refesh_once = false;

        //1、citizen tax income
        private UILabel buildingmoney;
        private UILabel buildingincomebuffer;
        private UILabel buildingoutgoingbuffer;
        private UILabel aliveworkcount;
        private UILabel employfee;
        private UILabel landrent;
        private UILabel net_asset;
        //private UILabel alivevisitcount;

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
        }

        private void DoOnStartup()
        {
            this.ShowOnGui();            
        }


        private void ShowOnGui()
        {
            this.m_HeaderDataText = base.AddUIComponent<UILabel>();
            this.m_HeaderDataText.textScale = 0.825f;
            this.m_HeaderDataText.text = string.Concat(new string[]
            {
                "Object Type    [data]"
            });
            this.m_HeaderDataText.tooltip = "N/A";
            this.m_HeaderDataText.relativePosition = new Vector3(SPACING, 50f);
            this.m_HeaderDataText.autoSize = true;

            this.buildingmoney = base.AddUIComponent<UILabel>();
            this.buildingmoney.text = "Building Money [000000000000000]";
            this.buildingmoney.tooltip = language.BuildingUI[1];
            this.buildingmoney.relativePosition = new Vector3(SPACING, this.m_HeaderDataText.relativePosition.y + SPACING22);
            this.buildingmoney.autoSize = true;
            this.buildingmoney.name = "Moreeconomic_Text_0";

            this.buildingincomebuffer = base.AddUIComponent<UILabel>();
            this.buildingincomebuffer.text = "buildingincomebuffer [000000000000000]";
            this.buildingincomebuffer.tooltip = language.BuildingUI[3];
            this.buildingincomebuffer.relativePosition = new Vector3(SPACING, this.buildingmoney.relativePosition.y + SPACING22);
            this.buildingincomebuffer.autoSize = true;
            this.buildingincomebuffer.name = "Moreeconomic_Text_1";

            this.buildingoutgoingbuffer = base.AddUIComponent<UILabel>();
            this.buildingoutgoingbuffer.text = "buildingoutgoingbuffer [000000000000000]";
            this.buildingoutgoingbuffer.tooltip = language.BuildingUI[5];
            this.buildingoutgoingbuffer.relativePosition = new Vector3(SPACING, this.buildingincomebuffer.relativePosition.y + SPACING22);
            this.buildingoutgoingbuffer.autoSize = true;
            this.buildingoutgoingbuffer.name = "Moreeconomic_Text_2";

            this.aliveworkcount = base.AddUIComponent<UILabel>();
            this.aliveworkcount.text = "aliveworkcont [000000000000000]";
            this.aliveworkcount.tooltip = language.BuildingUI[7];
            this.aliveworkcount.relativePosition = new Vector3(SPACING, this.buildingoutgoingbuffer.relativePosition.y + SPACING22);
            this.aliveworkcount.autoSize = true;
            this.aliveworkcount.name = "Moreeconomic_Text_3";

            this.employfee = base.AddUIComponent<UILabel>();
            this.employfee.text = "employfee [000000000000000]";
            this.employfee.tooltip = language.BuildingUI[9];
            this.employfee.relativePosition = new Vector3(SPACING, this.aliveworkcount.relativePosition.y + SPACING22);
            this.employfee.autoSize = true;
            this.employfee.name = "Moreeconomic_Text_4";

            this.landrent = base.AddUIComponent<UILabel>();
            this.landrent.text = "landrent [000000000000000]";
            this.landrent.tooltip = language.BuildingUI[11];
            this.landrent.relativePosition = new Vector3(SPACING, this.employfee.relativePosition.y + SPACING22);
            this.landrent.autoSize = true;
            this.landrent.name = "Moreeconomic_Text_5";

            this.net_asset = base.AddUIComponent<UILabel>();
            this.net_asset.text = "net_asset [000000000000000]";
            this.net_asset.tooltip = language.BuildingUI[13];
            this.net_asset.relativePosition = new Vector3(SPACING, this.landrent.relativePosition.y + SPACING22);
            this.net_asset.autoSize = true;
            this.net_asset.name = "Moreeconomic_Text_5";

            /*this.alivevisitcount = base.AddUIComponent<UILabel>();
            this.alivevisitcount.text = "alivevisitcount [000000000000000]";
            this.alivevisitcount.tooltip = language.BuildingUI[15];
            this.alivevisitcount.relativePosition = new Vector3(SPACING, this.net_asset.relativePosition.y + SPACING22);
            this.alivevisitcount.autoSize = true;
            this.alivevisitcount.name = "Moreeconomic_Text_3";*/
        }

        private void RefreshDisplayData()
        {
            uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
            uint num2 = currentFrameIndex & 255u;
     
            if (((num2 == 255u) && (comm_data.current_time != comm_data.prev_time)) || BuildingUI.refesh_once || (comm_data.last_buildingid != WorldInfoPanel.GetCurrentInstanceID().Building))
            {
                //DebugLog.LogToFileOnly("buildingUI try to refreshing");
                comm_data.last_buildingid = WorldInfoPanel.GetCurrentInstanceID().Building;
                Building buildingdata = Singleton<BuildingManager>.instance.m_buildings.m_buffer[comm_data.last_buildingid];
                int aliveWorkerCount = 0;
                int totalWorkerCount = 0;
                int num = caculate_employee_outcome(buildingdata, comm_data.last_buildingid, out aliveWorkerCount, out totalWorkerCount);
                int num1 = process_land_fee(buildingdata, comm_data.last_buildingid);
                int asset = pc_PrivateBuildingAI.process_building_asset(comm_data.last_buildingid, ref buildingdata);
                //int alivecisitCount = 0;
                //int totalvisitCount = 0;
                //Citizen.BehaviourData behaviour = default(Citizen.BehaviourData);
                //GetVisitBehaviour(comm_data.current_buildingid, ref buildingdata, ref behaviour, ref alivecisitCount, ref totalvisitCount);               
                this.buildingmoney.text = string.Format(language.BuildingUI[0] + " [{0}]", comm_data.building_money[comm_data.last_buildingid]);
                this.buildingincomebuffer.text = string.Format(language.BuildingUI[2] + " [{0}]", buildingdata.m_customBuffer1);
                this.buildingoutgoingbuffer.text = string.Format(language.BuildingUI[4] + " [{0}]", buildingdata.m_customBuffer2);
                this.aliveworkcount.text = string.Format(language.BuildingUI[6] + " [{0}]", aliveWorkerCount);
                if (buildingdata.Info.m_class.m_service == ItemClass.Service.Office || buildingdata.Info.m_class.m_service == ItemClass.Service.Commercial)
                {
                    this.employfee.text = language.BuildingUI[8] + " " + language.BuildingUI[16];
                }
                else if (buildingdata.Info.m_class.m_subService == ItemClass.SubService.IndustrialFarming)
                {
                    if (buildingdata.Info.m_buildingAI is IndustrialExtractorAI)
                    {
                        this.employfee.text = language.BuildingUI[8] + " " + language.BuildingUI[16];
                    }
                    else
                    {
                        this.employfee.text = string.Format(language.BuildingUI[8] + " [{0}]", num);
                    }
                }
                else
                {
                    this.employfee.text = string.Format(language.BuildingUI[8] + " [{0}]", num);
                }
                this.landrent.text = string.Format(language.BuildingUI[10] + " [{0:N2}]", (float)num1/100f);
                this.net_asset.text = string.Format(language.BuildingUI[12] + " [{0}]", comm_data.building_money[comm_data.last_buildingid] + asset);
                //this.alivevisitcount.text = string.Format(language.BuildingUI[14] + " [{0}]", totalWorkerCount);
                BuildingUI.refesh_once = false;
            }

        }

        public int caculate_employee_outcome(Building building, ushort buildingID, out int aliveWorkerCount, out int totalWorkerCount)
        {
            int num1 = 0;
            Citizen.BehaviourData behaviour = default(Citizen.BehaviourData);
            aliveWorkerCount = 0;
            totalWorkerCount = 0;
            GetWorkBehaviour(buildingID, ref building, ref behaviour, ref aliveWorkerCount, ref totalWorkerCount);
            switch (building.Info.m_class.m_subService)
            {
                case ItemClass.SubService.IndustrialFarming:
                    num1 = (int)(behaviour.m_educated0Count * comm_data.indus_far_education0 + behaviour.m_educated1Count * comm_data.indus_far_education1 + behaviour.m_educated2Count * comm_data.indus_far_education2 + behaviour.m_educated3Count * comm_data.indus_far_education3);
                    break;
                case ItemClass.SubService.IndustrialForestry:
                    num1 = (int)(behaviour.m_educated0Count * comm_data.indus_for_education0 + behaviour.m_educated1Count * comm_data.indus_for_education1 + behaviour.m_educated2Count * comm_data.indus_for_education2 + behaviour.m_educated3Count * comm_data.indus_for_education3);
                    break;
                case ItemClass.SubService.IndustrialOil:
                    num1 = (int)(behaviour.m_educated0Count * comm_data.indus_oil_education0 + behaviour.m_educated1Count * comm_data.indus_oil_education1 + behaviour.m_educated2Count * comm_data.indus_oil_education2 + behaviour.m_educated3Count * comm_data.indus_oil_education3);
                    break;
                case ItemClass.SubService.IndustrialOre:
                    num1 = (int)(behaviour.m_educated0Count * comm_data.indus_ore_education0 + behaviour.m_educated1Count * comm_data.indus_ore_education1 + behaviour.m_educated2Count * comm_data.indus_ore_education2 + behaviour.m_educated3Count * comm_data.indus_ore_education3);
                    break;
                case ItemClass.SubService.IndustrialGeneric:
                    if (building.Info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        num1 = (int)(behaviour.m_educated0Count * comm_data.indus_gen_level1_education0 + behaviour.m_educated1Count * comm_data.indus_gen_level1_education1 + behaviour.m_educated2Count * comm_data.indus_gen_level1_education2 + behaviour.m_educated3Count * comm_data.indus_gen_level1_education3);
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        num1 = (int)(behaviour.m_educated0Count * comm_data.indus_gen_level2_education0 + behaviour.m_educated1Count * comm_data.indus_gen_level2_education1 + behaviour.m_educated2Count * comm_data.indus_gen_level2_education2 + behaviour.m_educated3Count * comm_data.indus_gen_level2_education3);
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        num1 = (int)(behaviour.m_educated0Count * comm_data.indus_gen_level3_education0 + behaviour.m_educated1Count * comm_data.indus_gen_level3_education1 + behaviour.m_educated2Count * comm_data.indus_gen_level3_education2 + behaviour.m_educated3Count * comm_data.indus_gen_level3_education3);
                    }
                    break;
                case ItemClass.SubService.CommercialHigh:
                    if (building.Info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        num1 = (int)(behaviour.m_educated0Count * comm_data.comm_high_level1_education0 + behaviour.m_educated1Count * comm_data.comm_high_level1_education1 + behaviour.m_educated2Count * comm_data.comm_high_level1_education2 + behaviour.m_educated3Count * comm_data.comm_high_level1_education3);
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        num1 = (int)(behaviour.m_educated0Count * comm_data.comm_high_level2_education0 + behaviour.m_educated1Count * comm_data.comm_high_level2_education1 + behaviour.m_educated2Count * comm_data.comm_high_level2_education2 + behaviour.m_educated3Count * comm_data.comm_high_level2_education3);
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        num1 = (int)(behaviour.m_educated0Count * comm_data.comm_high_level3_education0 + behaviour.m_educated1Count * comm_data.comm_high_level3_education1 + behaviour.m_educated2Count * comm_data.comm_high_level3_education2 + behaviour.m_educated3Count * comm_data.comm_high_level3_education3);
                    }
                    break;
                case ItemClass.SubService.CommercialLow:
                    if (building.Info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        num1 = (int)(behaviour.m_educated0Count * comm_data.comm_low_level1_education0 + behaviour.m_educated1Count * comm_data.comm_low_level1_education1 + behaviour.m_educated2Count * comm_data.comm_low_level1_education2 + behaviour.m_educated3Count * comm_data.comm_low_level1_education3);
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        num1 = (int)(behaviour.m_educated0Count * comm_data.comm_low_level2_education0 + behaviour.m_educated1Count * comm_data.comm_low_level2_education1 + behaviour.m_educated2Count * comm_data.comm_low_level2_education2 + behaviour.m_educated3Count * comm_data.comm_low_level2_education3);
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        num1 = (int)(behaviour.m_educated0Count * comm_data.comm_low_level3_education0 + behaviour.m_educated1Count * comm_data.comm_low_level3_education1 + behaviour.m_educated2Count * comm_data.comm_low_level3_education2 + behaviour.m_educated3Count * comm_data.comm_low_level3_education3);
                    }
                    break;
                case ItemClass.SubService.CommercialLeisure:
                    num1 = (int)(behaviour.m_educated0Count * comm_data.comm_lei_education0 + behaviour.m_educated1Count * comm_data.comm_lei_education1 + behaviour.m_educated2Count * comm_data.comm_lei_education2 + behaviour.m_educated3Count * comm_data.comm_lei_education3);
                    break;
                case ItemClass.SubService.CommercialTourist:
                    num1 = (int)(behaviour.m_educated0Count * comm_data.comm_tou_education0 + behaviour.m_educated1Count * comm_data.comm_tou_education1 + behaviour.m_educated2Count * comm_data.comm_tou_education2 + behaviour.m_educated3Count * comm_data.comm_tou_education3);
                    break;
                case ItemClass.SubService.CommercialEco:
                    num1 = (int)(behaviour.m_educated0Count * comm_data.comm_eco_education0 + behaviour.m_educated1Count * comm_data.comm_eco_education1 + behaviour.m_educated2Count * comm_data.comm_eco_education2 + behaviour.m_educated3Count * comm_data.comm_eco_education3);
                    break;
                default: break;
            }
            System.Random rand = new System.Random();

            //add random value to match citizen salary
            if (num1 != 0)
            {
                num1 += (behaviour.m_educated0Count * rand.Next(1) + behaviour.m_educated0Count * rand.Next(2) + behaviour.m_educated0Count * rand.Next(3) + behaviour.m_educated0Count * rand.Next(4));
            }

            if (building.Info.m_class.m_service == ItemClass.Service.Commercial || (building.Info.m_class.m_service == ItemClass.Service.Industrial))
            {
                if (building.Width * building.Length <= 12)
                {
                    num1 = (int)(num1 / 1.1f);
                }

                if (building.Width * building.Length <= 9)
                {
                    num1 = (int)(num1 / 1.1f);
                }

                if (building.Width * building.Length <= 6)
                {
                    num1 = (int)(num1 / 1.1f);
                }

                if (building.Width * building.Length <= 4)
                {
                    num1 = (int)(num1 / 1.1f);
                }

                if (building.Width * building.Length <= 2)
                {
                    num1 = (int)(num1 / 1.1f);
                }
            }

            if (building.Info.m_buildingAI is IndustrialExtractorAI)
            {
                //num1 = num1 * 2;
            } else
            {
                num1 = num1 / 2;
            }

            float local_salary_idex = 0.5f;
            float final_salary_idex = 0.5f;
            DistrictManager instance2 = Singleton<DistrictManager>.instance;
            byte district = 0;
            if ((building.Info.m_class.m_service == ItemClass.Service.Commercial) || (building.Info.m_class.m_service == ItemClass.Service.Industrial))
            {
                district = instance2.GetDistrict(building.m_position);
                local_salary_idex = (Singleton<DistrictManager>.instance.m_districts.m_buffer[district].GetLandValue() + 50f) / 120f;
                final_salary_idex = (local_salary_idex * 4f + comm_data.salary_idex) / 5f;
            }

            //money < 0, salary/3f
            if ((building.Info.m_class.m_service == ItemClass.Service.Commercial) || (building.Info.m_class.m_service == ItemClass.Service.Industrial))
            {
                if (comm_data.building_money[buildingID] < 0)
                {
                    num1 =  (int)((float)num1 * final_salary_idex / 48f);
                }
                else if (comm_data.building_money[buildingID] > 200 * totalWorkerCount)
                {
                    num1 = (int)((float)num1 * final_salary_idex / 12f);
                } else
                {
                    num1 = (int)((float)num1 * final_salary_idex / 16f);
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

        protected void GetVisitBehaviour(ushort buildingID, ref Building buildingData, ref Citizen.BehaviourData behaviour, ref int aliveCount, ref int totalCount)
        {
            CitizenManager instance = Singleton<CitizenManager>.instance;
            uint num = buildingData.m_citizenUnits;
            int num2 = 0;
            while (num != 0u)
            {
                if ((ushort)(instance.m_units.m_buffer[(int)((UIntPtr)num)].m_flags & CitizenUnit.Flags.Visit) != 0)
                {
                    instance.m_units.m_buffer[(int)((UIntPtr)num)].GetCitizenVisitBehaviour(ref behaviour, ref aliveCount, ref totalCount);
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
