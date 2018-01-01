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
        private UILabel net_asset;

        private UILabel buy_price;
        private UILabel sell_price;
        private UILabel comsuptiondivide;
        private UILabel sell_tax;
        private UILabel buy2sell_profit;
        //private UILabel alivevisitcount;

        private UILabel building_type;
        private UIButton acquire;
        private UIButton update_building;

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

            this.net_asset = base.AddUIComponent<UILabel>();
            this.net_asset.text = "net_asset [000000000000000]";
            this.net_asset.tooltip = language.BuildingUI[13];
            this.net_asset.relativePosition = new Vector3(SPACING, this.landrent.relativePosition.y + SPACING22);
            this.net_asset.autoSize = true;
            this.net_asset.name = "Moreeconomic_Text_5";

            this.buy_price = base.AddUIComponent<UILabel>();
            this.buy_price.text = language.BuildingUI[18];
            this.buy_price.tooltip = language.BuildingUI[18];
            this.buy_price.relativePosition = new Vector3(SPACING, this.net_asset.relativePosition.y + SPACING22);
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

            this.acquire = base.AddUIComponent<UIButton>();
            this.acquire.size = new Vector2(160f, 24f);
            this.acquire.text = language.BuildingUI[26];
            this.acquire.tooltip = language.BuildingUI[26];
            this.acquire.textScale = 0.875f;
            this.acquire.normalBgSprite = "ButtonMenu";
            this.acquire.hoveredBgSprite = "ButtonMenuHovered";
            this.acquire.pressedBgSprite = "ButtonMenuPressed";
            this.acquire.disabledBgSprite = "ButtonMenuDisabled";
            this.acquire.relativePosition = new Vector3(SPACING, this.buy2sell_profit.relativePosition.y + SPACING22 + 5f);
            this.acquire.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                //comm_data.last_buildingid = WorldInfoPanel.GetCurrentInstanceID().Building;
                if (!comm_data.building_flag[comm_data.last_buildingid])
                {
                    Building buildingdata = Singleton<BuildingManager>.instance.m_buildings.m_buffer[comm_data.last_buildingid];
                    int acquire_money = 0;
                    if (buildingdata.Info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        acquire_money = 200000;
                    }

                    if (buildingdata.Info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        acquire_money = 600000;
                    }

                    if (buildingdata.Info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        acquire_money = 1800000;
                    }
                    comm_data.building_flag[comm_data.last_buildingid] = true;
                    if (comm_data.building_money[comm_data.last_buildingid] > 0)
                    {
                        comm_data.city_bank -= comm_data.building_money[comm_data.last_buildingid];
                    }
                    comm_data.building_money[comm_data.last_buildingid] = 0;
                    refesh_once = true;
                    Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.PolicyCost, (int)acquire_money, ItemClass.Service.Beautification, ItemClass.SubService.None, ItemClass.Level.Level1);
                }
            };

            this.building_type = base.AddUIComponent<UILabel>();
            this.building_type.text = language.BuildingUI[27];
            this.building_type.tooltip = language.BuildingUI[27];
            this.building_type.relativePosition = new Vector3(this.acquire.relativePosition.x + this.acquire.width + SPACING + 40f, this.acquire.relativePosition.y);
            this.building_type.autoSize = true;
            this.building_type.name = "Moreeconomic_Text_10";

            this.update_building = base.AddUIComponent<UIButton>();
            this.update_building.size = new Vector2(160f, 24f);
            this.update_building.text = language.BuildingUI[32];
            this.update_building.tooltip = language.BuildingUI[32];
            this.update_building.textScale = 0.875f;
            this.update_building.normalBgSprite = "ButtonMenu";
            this.update_building.hoveredBgSprite = "ButtonMenuHovered";
            this.update_building.pressedBgSprite = "ButtonMenuPressed";
            this.update_building.disabledBgSprite = "ButtonMenuDisabled";
            this.update_building.relativePosition = new Vector3(SPACING, this.building_type.relativePosition.y + SPACING22 + 5f);
            this.update_building.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                //comm_data.last_buildingid = WorldInfoPanel.GetCurrentInstanceID().Building;
                Building buildingdata = Singleton<BuildingManager>.instance.m_buildings.m_buffer[comm_data.last_buildingid];
                int update_money = 0;
                if (buildingdata.Info.m_class.m_level == ItemClass.Level.Level1)
                {
                    update_money = 500000;
                }

                if (buildingdata.Info.m_class.m_level == ItemClass.Level.Level2)
                {
                    update_money = 1000000;
                }
                refesh_once = true;
                comm_data.update_building = comm_data.last_buildingid;
                Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.PolicyCost, (int)update_money, ItemClass.Service.Beautification, ItemClass.SubService.None, ItemClass.Level.Level1);
            };
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
                        int asset = pc_PrivateBuildingAI.process_building_asset(comm_data.last_buildingid, ref buildingdata);
                        this.buildingmoney.text = string.Format(language.BuildingUI[0] + " [{0}]", comm_data.building_money[comm_data.last_buildingid]);
                        this.buildingincomebuffer.text = string.Format(language.BuildingUI[2] + " [{0}]", buildingdata.m_customBuffer1);
                        this.buildingoutgoingbuffer.text = string.Format(language.BuildingUI[4] + " [{0}]", buildingdata.m_customBuffer2);
                        //this.aliveworkcount.text = string.Format(language.BuildingUI[6] + " [{0}]", aliveWorkerCount);
                        if (buildingdata.Info.m_class.m_service == ItemClass.Service.Office || buildingdata.Info.m_class.m_service == ItemClass.Service.Commercial)
                        {
                            this.employfee.text = language.BuildingUI[8] + " " + num.ToString() + " " + language.BuildingUI[16];
                        }
                        else if (buildingdata.Info.m_class.m_subService == ItemClass.SubService.IndustrialFarming)
                        {
                            if (buildingdata.Info.m_buildingAI is IndustrialExtractorAI)
                            {
                                this.employfee.text = language.BuildingUI[8] + " " + num.ToString() + " " + language.BuildingUI[16];
                            }
                            else
                            {
                                this.employfee.text = string.Format(language.BuildingUI[8] + " [{0:N2}]", (int)num);
                            }
                        }
                        else
                        {
                            this.employfee.text = string.Format(language.BuildingUI[8] + " [{0:N2}]", (int)num);
                        }
                        this.landrent.text = string.Format(language.BuildingUI[10] + " [{0:N2}]", (float)num1 / 100f);
                        this.net_asset.text = string.Format(language.BuildingUI[12] + " [{0}]", comm_data.building_money[comm_data.last_buildingid] + asset);
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
                            price += pc_PrivateBuildingAI.get_price(false, comm_data.last_buildingid, buildingdata, TransferManager.TransferReason.Coal);
                            price += pc_PrivateBuildingAI.get_price(false, comm_data.last_buildingid, buildingdata, TransferManager.TransferReason.Lumber);
                            price += pc_PrivateBuildingAI.get_price(false, comm_data.last_buildingid, buildingdata, TransferManager.TransferReason.Petrol);
                            price += pc_PrivateBuildingAI.get_price(false, comm_data.last_buildingid, buildingdata, TransferManager.TransferReason.Food);
                            price = price / 4f;
                        }
                        else
                        {
                            price = pc_PrivateBuildingAI.get_price(false, comm_data.last_buildingid, buildingdata, TransferManager.TransferReason.None);
                        }
                        this.buy_price.text = string.Format(language.BuildingUI[18] + " [{0:N2}]", price);
                    }

                    price2 = pc_PrivateBuildingAI.get_price(true, comm_data.last_buildingid, buildingdata, TransferManager.TransferReason.None);

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
                        ConsumptionDivider = (float)comm_data.Commerical_price * pc_PrivateBuildingAI.get_comsumptiondivider(buildingdata, comm_data.last_buildingid);
                        this.comsuptiondivide.text = string.Format(language.BuildingUI[21] + " [1:{0:N2}]", ConsumptionDivider);
                    }
                    else if (buildingdata.Info.m_class.m_subService == ItemClass.SubService.IndustrialGeneric)
                    {
                        ConsumptionDivider = (float)comm_data.ConsumptionDivider * pc_PrivateBuildingAI.get_comsumptiondivider(buildingdata, comm_data.last_buildingid);
                        this.comsuptiondivide.text = string.Format(language.BuildingUI[21] + " [1:{0:N2}]", ConsumptionDivider);
                    }
                    else
                    {
                        if (buildingdata.Info.m_buildingAI is IndustrialBuildingAI)
                        {
                            ConsumptionDivider = (float)comm_data.ConsumptionDivider1 * pc_PrivateBuildingAI.get_comsumptiondivider(buildingdata, comm_data.last_buildingid);
                            this.comsuptiondivide.text = string.Format(language.BuildingUI[21] + " [1:{0:N2}]", ConsumptionDivider);
                        }
                        else
                        {
                            this.comsuptiondivide.text = string.Format(language.BuildingUI[21] + " N/A");
                        }
                    }

                    float sell_tax_1 = pc_PrivateBuildingAI.get_tax_rate(buildingdata, comm_data.last_buildingid);
                    if (comm_data.have_tax_department)
                    {
                        this.sell_tax.text = string.Format(language.BuildingUI[22] + " [{0}%]", (int)(sell_tax_1 * 100f));
                    } else
                    {
                        this.sell_tax.text = string.Format(language.BuildingUI[22] + " " + language.BuildingUI[30]);
                    }

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


                    if (buildingdata.Info.m_buildingAI is IndustrialBuildingAI)
                    {
                        int acquire_money = 0;
                        if (buildingdata.Info.m_class.m_level == ItemClass.Level.Level1)
                        {
                            acquire_money = 200000;
                        }

                        if (buildingdata.Info.m_class.m_level == ItemClass.Level.Level2)
                        {
                            acquire_money = 600000;
                        }

                        if (buildingdata.Info.m_class.m_level == ItemClass.Level.Level3)
                        {
                            acquire_money = 1800000;
                        }
                        FieldInfo cashAmount;
                        cashAmount = typeof(EconomyManager).GetField("m_cashAmount", BindingFlags.NonPublic | BindingFlags.Instance);
                        long _cashAmount = (long)cashAmount.GetValue(Singleton<EconomyManager>.instance);

                        if (_cashAmount > acquire_money)
                        {
                            acquire.isEnabled = true;
                        } else
                        {
                            acquire.isEnabled = false;
                        }
                        acquire.text = string.Format(language.BuildingUI[26] + ":" + acquire_money.ToString());
                    } else
                    {
                        acquire.text = string.Format(language.BuildingUI[26]);
                        acquire.isEnabled = false;
                    }


                    if (comm_data.building_flag[comm_data.last_buildingid])
                    {
                        building_type.text = string.Format(language.BuildingUI[28]);
                        acquire.isEnabled = false;
                    }
                    else
                    {
                        building_type.text = string.Format(language.BuildingUI[27]);
                    }

                    this.BringToFront();
                    BuildingUI.refesh_once = false;


                    if (buildingdata.Info.m_class.m_subService == ItemClass.SubService.CommercialHigh  || buildingdata.Info.m_class.m_subService == ItemClass.SubService.CommercialLow || buildingdata.Info.m_class.m_subService == ItemClass.SubService.IndustrialGeneric)
                    {
                        int update_money = 0;
                        if (buildingdata.Info.m_class.m_level == ItemClass.Level.Level1)
                        {
                            update_money = 300000;
                        }

                        if (buildingdata.Info.m_class.m_level == ItemClass.Level.Level2)
                        {
                            update_money = 900000;
                        }

                        FieldInfo cashAmount;
                        cashAmount = typeof(EconomyManager).GetField("m_cashAmount", BindingFlags.NonPublic | BindingFlags.Instance);
                        long _cashAmount = (long)cashAmount.GetValue(Singleton<EconomyManager>.instance);

                        if (_cashAmount > update_money)
                        {
                            update_building.isEnabled = true;
                        }
                        else
                        {
                            update_building.isEnabled = false;
                        }
                        update_building.text = string.Format(language.BuildingUI[32] + ":" + update_money.ToString());
                    } else
                    {
                        update_building.text = string.Format(language.BuildingUI[32]);
                        update_building.isEnabled = false;
                    }

                    if ((buildingdata.Info.m_class.m_level == ItemClass.Level.Level3) || (comm_data.update_building != 0))
                    {
                        update_building.text = string.Format(language.BuildingUI[32]);
                        update_building.isEnabled = false;
                    }
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
                default: break;
            }
            System.Random rand = new System.Random();

            //add random value to match citizen salary
            if (num1 != 0)
            {
                num1 += (behaviour.m_educated0Count * rand.Next(1) + behaviour.m_educated1Count * rand.Next(2) + behaviour.m_educated2Count * rand.Next(3) + behaviour.m_educated3Count * rand.Next(4));
            }

            if (building.Info.m_class.m_service == ItemClass.Service.Industrial)
            {
                num1 = (float)(num1 * (float)((float)building.m_width * (float)building.m_length / 9f));
                //DebugLog.LogToFileOnly("num1 = " + num1.ToString() + " " + building.m_width.ToString() + " " + building.m_length.ToString());
            }

            if (building.Info.m_buildingAI is IndustrialExtractorAI)
            {
                //num1 = num1 * 2;
            }
            else
            {
                if (building.Info.m_class.m_subService != ItemClass.SubService.IndustrialGeneric)
                {
                    num1 = num1 / 2f;
                }
            }

            float local_salary_idex = 0.5f;
            float final_salary_idex = 0.5f;
            DistrictManager instance2 = Singleton<DistrictManager>.instance;
            byte district = 0;
            if (building.Info.m_class.m_service == ItemClass.Service.Industrial)
            {
                district = instance2.GetDistrict(building.m_position);
                local_salary_idex = (Singleton<DistrictManager>.instance.m_districts.m_buffer[district].GetLandValue() + 50f) / 120f;
                final_salary_idex = (local_salary_idex * 2f + comm_data.salary_idex) / 3f;
            }


            if (totalWorkerCount > 0)
            {
                //money < 0, salary/3f
                if (building.Info.m_class.m_service == ItemClass.Service.Industrial)
                {
                    if (comm_data.building_money[buildingID] < 0)
                    {
                        //num1 =  (float)((float)num1 * final_salary_idex / 48f);
                        num1 = (float)((float)num1 * final_salary_idex / (3f * totalWorkerCount));
                    }
                    else if (!comm_data.building_flag[buildingID])
                    {
                        //num1 = (float)((float)num1 * final_salary_idex / 16f);
                        num1 = (float)((float)num1 * final_salary_idex / totalWorkerCount);
                    } else
                    {
                        num1 = (float)((float)num1 / totalWorkerCount);
                    }
                }





                if (building.Info.m_class.m_service == ItemClass.Service.Commercial)
                {
                    if (comm_data.building_money[buildingID] > (pc_PrivateBuildingAI.good_import_price * 2000))
                    {
                        switch (building.Info.m_class.m_subService)
                        {
                            case ItemClass.SubService.CommercialLow:
                                if (building.Info.m_class.m_level == ItemClass.Level.Level1)
                                {
                                    num1 = (int)((comm_data.building_money[buildingID] - (pc_PrivateBuildingAI.good_import_price * 2000)) * 0.1f / totalWorkerCount);
                                }
                                if (building.Info.m_class.m_level == ItemClass.Level.Level2)
                                {
                                    num1 = (int)((comm_data.building_money[buildingID] - (pc_PrivateBuildingAI.good_import_price * 2000)) * 0.3f / totalWorkerCount);
                                }
                                if (building.Info.m_class.m_level == ItemClass.Level.Level3)
                                {
                                    num1 = (int)((comm_data.building_money[buildingID] - (pc_PrivateBuildingAI.good_import_price * 2000)) * 0.6f / totalWorkerCount);
                                }
                                break;
                            case ItemClass.SubService.CommercialHigh:
                                if (building.Info.m_class.m_level == ItemClass.Level.Level1)
                                {
                                    num1 = (int)((comm_data.building_money[buildingID] - (pc_PrivateBuildingAI.good_import_price * 2000)) * 0.2f / totalWorkerCount);
                                }
                                if (building.Info.m_class.m_level == ItemClass.Level.Level2)
                                {
                                    num1 = (int)((comm_data.building_money[buildingID] - (pc_PrivateBuildingAI.good_import_price * 2000)) * 0.4f / totalWorkerCount);
                                }
                                if (building.Info.m_class.m_level == ItemClass.Level.Level3)
                                {
                                    num1 = (int)((comm_data.building_money[buildingID] - (pc_PrivateBuildingAI.good_import_price * 2000)) * 0.7f / totalWorkerCount);
                                }
                                break;
                            case ItemClass.SubService.CommercialLeisure:
                                num1 = (int)((comm_data.building_money[buildingID] - (pc_PrivateBuildingAI.good_import_price * 2000)) * 0.7f / totalWorkerCount);
                                break;
                            case ItemClass.SubService.CommercialTourist:
                                num1 = (int)((comm_data.building_money[buildingID] - (pc_PrivateBuildingAI.good_import_price * 2000)) * 0.9f / totalWorkerCount);
                                break;
                            case ItemClass.SubService.CommercialEco:
                                num1 = (int)((comm_data.building_money[buildingID] - (pc_PrivateBuildingAI.good_import_price * 2000)) * 0.05f / totalWorkerCount);
                                break;
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
                }


            }
            else
            {
                num1 = 0;
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

            if (comm_data.citizen_count < 500)
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
