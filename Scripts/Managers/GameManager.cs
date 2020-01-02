// =======================================================================================
// Wovencore by Wovencode (c)
// =======================================================================================
using System;
using System.Collections.Generic;
using UnityEngine;
using woco.core;

namespace woco.core
{
	
	// ===================================================================================
	// GameManager
	// ===================================================================================
	[DisallowMultipleComponent]
	[RequireComponent(typeof(UnitManager))]
	[RequireComponent(typeof(SaveManager))]
	[RequireComponent(typeof(CurrencyManager))]
	
	
	/*
	[RequireComponent(typeof(AbilityManager))]
	[RequireComponent(typeof(AccountManager))]
	[RequireComponent(typeof(CampaignManager))]
	[RequireComponent(typeof(ConditionManager))]
	[RequireComponent(typeof(ConfigManager))]
	
	[RequireComponent(typeof(EnemyManager))]
	[RequireComponent(typeof(EquipmentManager))]
	[RequireComponent(typeof(HeroManager))]
	[RequireComponent(typeof(InventoryManager))]
	
	[RequireComponent(typeof(TownManager))]
	[RequireComponent(typeof(UpgradeManager))]
	[RequireComponent(typeof(UIManager))]
	*/
	public partial class GameManager : _BaseManager
	{
	
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		// PROPERTIES
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		public UnitManager			_unitManager;
		public SaveManager			_saveManager;
		public CurrencyManager		_currencyManager;
		
		/*
		public AbilityManager		_abilityManager;
		public AccountManager		_accountManager;
		public CampaignManager		_campaignManager;
		public ConditionManager		_conditionManager;
		public ConfigManager		_configManager;
		
		public EnemyManager			_enemyManager;
		public EquipmentManager		_equipmentManager;
		public HeroManager			_heroManager;
		public InventoryManager		_inventoryManager;
		
		public TownManager			_townManager;
		public UpgradeManager		_upgradeManager;
		public UIManager			_uiManager;
		*/
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		// PUBLIC FUNCTIONS
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		public UnitManager			unitManager 		{ get { return _unitManager; } }
		public SaveManager			saveManager			{ get { return _saveManager; } }
		public CurrencyManager		currencyManager		{ get { return _currencyManager; } }
		
		
		/*
		public AbilityManager		abilityManager 		{ get { return _abilityManager; } }
		public AccountManager		accountManager		{ get { return _accountManager; } }
		public CampaignManager		campaignManager		{ get { return _campaignManager; } }
		public ConditionManager		conditionManager	{ get { return _conditionManager; } }
		public ConfigManager		configManager		{ get { return _configManager; } }
		
		public EnemyManager			enemyManager		{ get { return _enemyManager; } }
		public EquipmentManager		equipmentManager	{ get { return _equipmentManager; } }
		public HeroManager			heroManager			{ get { return _heroManager; } }
		public InventoryManager		inventoryManager	{ get { return _inventoryManager; } }
		
		public TownManager			townManager			{ get { return _townManager; } }
		public UpgradeManager		upgradeManager		{ get { return _upgradeManager; } }
		public UIManager			uiManager			{ get { return _uiManager; } }
		*/
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		// EVENTS
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

		// -------------------------------------------------------------------------------
		// OnReset
		// -------------------------------------------------------------------------------
		public override void OnReset() {}
		
		// -------------------------------------------------------------------------------
		// OnLoad
		// -------------------------------------------------------------------------------
		public override void OnLoad() {
			_unitManager.OnLoad();
			_saveManager.OnLoad();
			_currencyManager.OnLoad();
			
			
			
			/*
			_abilityManager.OnLoad();
			_accountManager.OnLoad();
			_campaignManager.OnLoad();
			_conditionManager.OnLoad();
			_configManager.OnLoad();
			
			_enemyManager.OnLoad();
			_equipmentManager.OnLoad();
			_heroManager.OnLoad();
			_inventoryManager.OnLoad();
			
			_townManager.OnLoad();
			_upgradeManager.OnLoad();
			*/
		}
		
		// -------------------------------------------------------------------------------
		// OnSave
		// -------------------------------------------------------------------------------
		public override void OnSave() {
			_unitManager.OnSave();
			_saveManager.OnSave();
			_currencyManager.OnSave();
			
			
			/*
			_abilityManager.OnSave();
			_accountManager.OnSave();
			_campaignManager.OnSave();
			_conditionManager.OnSave();
			_configManager.OnSave();
			
			_enemyManager.OnSave();
			_equipmentManager.OnSave();
			_heroManager.OnSave();
			_inventoryManager.OnSave();
			
			_townManager.OnSave();
			_upgradeManager.OnSave();
			*/
		}
		
		// -------------------------------------------------------------------------------
		// OnData
		// -------------------------------------------------------------------------------
		public override DataHolder OnData()
		{
			return null;
		}
		
		// -------------------------------------------------------------------------------
	
	}
	
	// ===================================================================================
	
}
