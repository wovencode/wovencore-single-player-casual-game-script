// =======================================================================================
// Wovencore by Wovencode (c)
// =======================================================================================
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.SqliteClient;
using System.IO;
using woco.core;

namespace woco.core
{

	// ===================================================================================
	// CURRENCY CLASSES
	// ===================================================================================
	
	// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
	// CurrencyCost
	// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
	[System.Serializable]
	public class CurrencyCost
	{
		[Tooltip("Can be bought only once")]
		public bool limitedPurchase;
		public CurrencyAmount[] buyPrice;
		public CurrencyAmount[] sellPrice;
	}

	// ===================================================================================
		
}
