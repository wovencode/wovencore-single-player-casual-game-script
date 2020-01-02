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
	// _ItemTemplate
	// ===================================================================================
	public abstract partial class _ItemTemplate : _BaseTemplate
	{
	
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		// PROPERTIES
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		[Header("Start")]
		public int startAmount;
		public int startLevel;
		
		[Header("Costs")]
		public CurrencyCost cost;
		
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
		static Dictionary<int, _ItemTemplate> cache;
		public static Dictionary<int, _ItemTemplate> dict
		{
			get
			{
				if (cache == null)
				{
					_ItemTemplate[] templates = Resources.LoadAll<_ItemTemplate>("");
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
