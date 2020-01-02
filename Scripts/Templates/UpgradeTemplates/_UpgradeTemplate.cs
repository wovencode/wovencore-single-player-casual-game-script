// =======================================================================================
// Wovencore by Wovencode (c)
// =======================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using woco.core;

namespace woco.core
{
	
	// ===================================================================================
	// _UpgradeTemplate
	// ===================================================================================
	public abstract partial class _UpgradeTemplate : _BaseTemplate
	{
	
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		// PROPERTIES
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		[Header("[START]")]
		public int startLevel;
		
		[Header("[MAXES]")]
		public int baseMaxLevel;

		[Header("[PRICES]")]
		public CurrencyAmount[] buyPrice;
		public CurrencyAmount[] sellPrice;

		
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		// 
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
	
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------


		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		
		// -------------------------------------------------------------------------------
		// CACHE
		// -------------------------------------------------------------------------------
		static Dictionary<int, _UpgradeTemplate> cache;
		public static Dictionary<int, _UpgradeTemplate> dict
		{
			get
			{
				if (cache == null)
				{
					_UpgradeTemplate[] templates = Resources.LoadAll<_UpgradeTemplate>("");
					List<string> duplicates = templates.ToList().FindDuplicates(tmpl => tmpl.name);
					if (duplicates.Count == 0)
					{
						cache = templates.ToDictionary(tmpl => tmpl.name.GetDeterministicHashCode(), tmpl => tmpl);
					}
					else
					{
						foreach (string duplicate in duplicates)
							Debug.LogError("Resources folder contains multiple templates with the name: " + duplicate);
					}
				}
				return cache;
			}
		}

		// -------------------------------------------------------------------------------

	}
	
	// ===================================================================================
	
}