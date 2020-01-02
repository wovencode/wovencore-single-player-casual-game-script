// =======================================================================================
//
//
// =======================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using woco.core;

namespace woco.core
{
	
	// ===================================================================================
	// 
	// ===================================================================================
	[CreateAssetMenu(fileName = "New Table", menuName = "Editor/New Table", order = 999)]
	public partial class TableTemplate : ScriptableObject
	{
		
		public ColumnData[] columns;
		
		// -------------------------------------------------------------------------------
		// valid
		// -------------------------------------------------------------------------------
		public bool valid
		{
			get {
				return columns.Length > 0;
			}
		}
		
		// -------------------------------------------------------------------------------
		
	}
}
