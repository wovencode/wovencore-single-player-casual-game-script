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
	// UnitInstance
	// ===================================================================================
	public partial class UnitInstance : _BaseInstance
	{
	
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		// PROPERTIES
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		
		public int hash;
		public string parent;
		public string id;
		
		public int level;
		public int grade;
		
		public int health;
		public int energy;
		
		public GameManager game;
		
		public CostModule cost;
		//CategoryModule
		
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		// PUBLIC FUNCTIONS
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		
		// -------------------------------------------------------------------------------
		// Constructor
		// -------------------------------------------------------------------------------
		public UnitInstance(GameManager _game, UnitTemplate _template) : base(_game)
		{
			game = _game;
			hash = template.name.GetDeterministicHashCode();
						
			cost = new CostModule(this);
			
		}
		
		// -------------------------------------------------------------------------------
		// getTemplate
		// -------------------------------------------------------------------------------
		public UnitTemplate template
		{
			get
			{
				if (!UnitTemplate.dict.ContainsKey(hash))
					throw new KeyNotFoundException("There is no template with hash: "+hash);
				return UnitTemplate.dict[hash];
			}
		}
		
		// -------------------------------------------------------------------------------
		// Wrappers for easier access
		// -------------------------------------------------------------------------------
		public string getName { get { return template.name; } }
		public CurrencyAmount[] buyPrice { get { return template.buyPrice; } }
		public CurrencyAmount[] sellPrice { get { return template.sellPrice; } }
		
		// -------------------------------------------------------------------------------
		// addInstance
		// -------------------------------------------------------------------------------
		public void addInstance(int _amount)
		{
		
		}
		
		// -------------------------------------------------------------------------------
		// removeInstance
		// -------------------------------------------------------------------------------
		public void removeInstance(int _amount)
		{
		
		}
		
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------


		// -------------------------------------------------------------------------------

	}
	
	// ===================================================================================
	
}
