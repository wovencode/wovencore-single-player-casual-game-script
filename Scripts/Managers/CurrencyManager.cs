// =======================================================================================
// Wovencore by Wovencode (c)
// =======================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using woco.core;

namespace woco.core
{
	
	// ===================================================================================
	// CurrencyManager
	// ===================================================================================
	[DisallowMultipleComponent]
	public partial class CurrencyManager : _BaseManager
	{
	
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		// PROPERTIES
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		
		protected List<CurrencyInstance> currencies = new List<CurrencyInstance>();
		
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		// PUBLIC FUNCTIONS
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		
		// -------------------------------------------------------------------------------
		// addCurrency
		// -------------------------------------------------------------------------------
		public void addCurrency(_CurrencyTemplate _template, long _amount)
		{
			CurrencyInstance instance = new CurrencyInstance(game, _template, _amount);
			currencies.Add(instance);
		}
		
		// -------------------------------------------------------------------------------
		// hasCurrency
		// -------------------------------------------------------------------------------
		public bool hasCurrency(_CurrencyTemplate _template)
		{
			return currencies.Exists(x => x.template == _template);
		}
		
		// -------------------------------------------------------------------------------
		// getCurrencyIndex
		// -------------------------------------------------------------------------------
		public int getCurrencyIndex(_CurrencyTemplate _template)
		{
			return currencies.FindIndex(x => x.template == _template);
		}
		
		// -------------------------------------------------------------------------------
		// getCurrencyAmount
		// -------------------------------------------------------------------------------
		public long getCurrencyAmount(_CurrencyTemplate _template)
		{
			return currencies.FirstOrDefault(x => x.template == _template).amount;
		}
		
		// -------------------------------------------------------------------------------
		// increaseCurrencyAmount
		// -------------------------------------------------------------------------------
		public void increaseCurrencyAmount(CurrencyAmount[] currencyAmount, int _amount = 1)
		{
			foreach (CurrencyAmount currency in currencyAmount)
			{
				int index = getCurrencyIndex(currency.template);
				
				if (index == -1)
				{
					addCurrency(currency.template, currency.amount * _amount);
				}
				else
				{
					currencies[index].amount += currency.amount * _amount;
				}
			}
		}
		
		// -------------------------------------------------------------------------------
		// decreaseCurrencyAmount
		// -------------------------------------------------------------------------------
		public void decreaseCurrencyAmount(CurrencyAmount[] currencyAmount, int _amount = 1)
		{
			foreach (CurrencyAmount currency in currencyAmount)
			{
				int index = getCurrencyIndex(currency.template);
				
				if (index != -1)
					currencies[index].amount -= currency.amount * _amount;
				
			}
		}
	
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		// EVENTS
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

		// -------------------------------------------------------------------------------
		// OnReset
		// -------------------------------------------------------------------------------
		public override void OnReset() {
			
			currencies.Clear();
			
			foreach (_CurrencyTemplate template in _CurrencyTemplate.dict.Values)
			{
				if (template.startAmount > 0)
				{
					addCurrency(template, template.startAmount);
				}
			}
		
		}
		
		// -------------------------------------------------------------------------------
		// OnLoad
		// -------------------------------------------------------------------------------
		public override void OnLoad() {

			List< List<object> > table = game.saveManager.LoadData(getName, getColumns());
			
			if (table.Count > 0) 
			{
				foreach (List<object> row in table)
				{
				
					string name 			= game.saveManager.CastToString(row[1]);
					long amount 			= game.saveManager.CastToLong(row[2]);
					
					_CurrencyTemplate template;
					
					if (_CurrencyTemplate.dict.TryGetValue(name.GetDeterministicHashCode(), out template))
					{
						addCurrency(template, amount);
					}
				}
				
			}
		
		}
		
		// -------------------------------------------------------------------------------
		// OnData
		// -------------------------------------------------------------------------------
		public override DataHolder OnData()
		{
			
			if (tableData.Length == 0) return null;
			
			int tableCount 	= 0;
			int recordCount = currencies.Count();
			
			DataHolder dataHolder = new DataHolder(getName, tableData[tableCount].tableColumns);
			
			for (int i = 0; i < recordCount; i++)
			{
			
				dataHolder.addData(Tools.GetUserId,
						currencies[i].getName,
						currencies[i].amount);
				
			}
			
			return dataHolder;
			
			
			
		}

		// -------------------------------------------------------------------------------

	}
	
	// ===================================================================================
	
}
