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
	// UnitTemplate
	// ===================================================================================
	public abstract partial class UnitTemplate : ScriptableObject
	{
	
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		// PROPERTIES
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		
		[Header("Description")]
		public LanguageString title;
		public LanguageString description;
		
		[Header("Icons")]
		public Sprite smallIcon;
		public Sprite largeIcon;
		
		[Header("Start")]
		public int startAmount;
		public int startLevel;
		
		[Header("Maximums")]
		public int baseMaxLevel;

		[Header("Prices")]
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
		static Dictionary<int, UnitTemplate> cache;
		public static Dictionary<int, UnitTemplate> dict
		{
			get
			{
				if (cache == null)
				{
					UnitTemplate[] templates = Resources.LoadAll<UnitTemplate>("");
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
