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
	// _CurrencyTemplate
	// ===================================================================================
	public abstract partial class _CurrencyTemplate : _BaseTemplate
	{
	
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		// PROPERTIES
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		public long startAmount;
		
		public long baseMaxAmount;
		
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
		static Dictionary<int, _CurrencyTemplate> cache;
		public static Dictionary<int, _CurrencyTemplate> dict
		{
			get
			{
				if (cache == null)
				{
					_CurrencyTemplate[] templates = Resources.LoadAll<_CurrencyTemplate>("");
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