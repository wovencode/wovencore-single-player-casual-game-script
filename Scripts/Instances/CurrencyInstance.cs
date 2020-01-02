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
	// CurrencyInstance
	// ===================================================================================
	public partial class CurrencyInstance : _BaseInstance
	{
	
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		// PROPERTIES
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		public long amount;
		
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		// PUBLIC FUNCTIONS
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		
		// -------------------------------------------------------------------------------
		// Constructor
		// -------------------------------------------------------------------------------
		public CurrencyInstance(GameManager _game, _CurrencyTemplate template, long _amount) : base(_game)
		{
			hash = template.name.GetDeterministicHashCode();
			amount = _amount;
		}
		
		// -------------------------------------------------------------------------------
		// getTemplate
		// -------------------------------------------------------------------------------
		public _CurrencyTemplate template
		{
			get
			{
				if (!_CurrencyTemplate.dict.ContainsKey(hash))
					throw new KeyNotFoundException("There is no template with hash: "+hash);
				return _CurrencyTemplate.dict[hash];
			}
		}
		
		// -------------------------------------------------------------------------------
		// Wrappers for easier access
		// -------------------------------------------------------------------------------
		public string getName { get { return template.name; } }
		
		
		
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------


		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------


		// -------------------------------------------------------------------------------

	}
	
	// ===================================================================================
	
}
