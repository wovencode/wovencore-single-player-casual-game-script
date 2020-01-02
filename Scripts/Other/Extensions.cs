// =======================================================================================
// Wovencore by Wovencode (c)
// =======================================================================================

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using woco.core;

namespace woco.core
{

	// ===================================================================================
	// EXTENSIONS
	// ===================================================================================
	public static class Extensions
	{


		// -------------------------------------------------------------------------------
		// GetDeterministicHashCode
		// -------------------------------------------------------------------------------
		public static int GetDeterministicHashCode(this string text)
		{
			int hash1 = (5381 << 16) + 5381;
			int hash2 = hash1;

			for (int i = 0; i < text.Length; i += 2)
			{
				hash1 = ((hash1 << 5) + hash1) ^ text[i];
				if (i == text.Length - 1)
					break;
				hash2 = ((hash2 << 5) + hash2) ^ text[i + 1];
			}

			return hash1 + (hash2 * 1566083941);
		}

		// -------------------------------------------------------------------------------
		// HasDuplicates
		// -------------------------------------------------------------------------------
		public static bool HasDuplicates<T>(this List<T> list)
		{
			return list.Count != list.Distinct().Count();
		}
		
		// -------------------------------------------------------------------------------
		// FindDuplicates
		// -------------------------------------------------------------------------------
		public static List<U> FindDuplicates<T, U>(this List<T> list, Func<T, U> keySelector)
		{
			return list.GroupBy(keySelector)
					   .Where(group => group.Count() > 1)
					   .Select(group => group.Key).ToList();
		}
		
		// -------------------------------------------------------------------------------
		
	}
		
}
