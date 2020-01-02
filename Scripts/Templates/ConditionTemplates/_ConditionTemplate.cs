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
	// 
	// ===================================================================================
	public partial class _ConditionTemplate : _BaseTemplate
	{
	
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		// PROPERTIES
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		
		
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
		static Dictionary<int, _ConditionTemplate> cache;
		public static Dictionary<int, _ConditionTemplate> dict
		{
			get
			{
				if (cache == null)
				{
					_ConditionTemplate[] templates = Resources.LoadAll<_ConditionTemplate>("");
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
