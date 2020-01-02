// =======================================================================================
// Wovencore by Wovencode (c)
// =======================================================================================
using System;
using woco.core;

namespace woco.core
{
	
	// ===================================================================================
	// CostModule
	// ===================================================================================
	public partial class CostModule : _BaseModule
	{
	
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		// PROPERTIES
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		public CurrencyAmount[] currencyAmount;
		
		protected UnitInstance instance;
		
		// -------------------------------------------------------------------------------
		// Constructor
		// -------------------------------------------------------------------------------
		public CostModule(UnitInstance _instance)
		{
			instance = _instance;
		}

		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		// PROTECTED FUNCTIONS
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		
		// -------------------------------------------------------------------------------
		// Calculate
		// -------------------------------------------------------------------------------
		protected void Calculate()
		{
		
		}

		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		// PUBLIC FUNCTIONS
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

		// -------------------------------------------------------------------------------
		// buy
		// -------------------------------------------------------------------------------
		public void buyInstance(int _amount = 1) {
		
			instance.game.currencyManager.decreaseCurrencyAmount(instance.buyPrice, _amount);
			
			/*if (!canAddInstance(_amount)) return;
			addInstance(_amount);
			instance.game.currencyManager.sellInstance(simpleTemplate.buyPrice, _amount);
			*/
		}
		
		// -------------------------------------------------------------------------------
		// sell
		// -------------------------------------------------------------------------------
		public void sellInstance(int _amount = 1) {
			instance.game.currencyManager.increaseCurrencyAmount(instance.sellPrice, _amount);
			instance.removeInstance(_amount);
		}
		
		// -------------------------------------------------------------------------------
		// getBuyPriceString
		// -------------------------------------------------------------------------------
		public string getBuyPriceString {
			get {
				string s = "";
				foreach (CurrencyAmount currency in instance.buyPrice)
				{
					if (currency.valid)
					{
						s += currency.amount.ToString() + " " + currency.template.title.get(0) + " ";
					}
				}
				return s;
			}
		}
		
		// -------------------------------------------------------------------------------
		// getSellPriceString
		// -------------------------------------------------------------------------------
		public string getSellPriceString {
			get {
				string s = "";
				foreach (CurrencyAmount currency in instance.sellPrice)
				{
					if (currency.valid)
					{
						s += currency.amount.ToString() + " " + currency.template.title.get(0) + " ";
					}
				}
				return s;
			}
		}
		
		
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------


		// -------------------------------------------------------------------------------

	}
	
	// ===================================================================================
	
}
