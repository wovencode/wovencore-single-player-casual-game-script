// =======================================================================================
//
//
// =======================================================================================

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using woco.core;

namespace woco.core
{
	
	// ===================================================================================
	public partial class UnitListWindow : _BaseWindow
	{
		
		public Transform content;
		public UnitWidget widgetPrefab;
		public UnitDetailWindow windowPrefab;
		
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		// PROTECTED FUNCTIONS
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		
		// -------------------------------------------------------------------------------
		// SlowUpdate
		// -------------------------------------------------------------------------------
		protected override void SlowUpdate(){
			OnDraw();
		}

		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		// EVENTS
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		
        // -------------------------------------------------------------------------------
        // OnDraw
        // -------------------------------------------------------------------------------
    	public override void OnDraw() {
        
        	for (int i = 0; i < content.childCount; ++i)
            	Destroy(content.GetChild(i).gameObject);
            
			foreach (UnitInstance unit in game.unitManager.units)
			{
			
				GameObject go = GameObject.Instantiate(widgetPrefab.gameObject);
				go.transform.SetParent(content, false);
				
				go.GetComponent<UnitWidget>().OnInit(unit, windowPrefab.gameObject);
				go.SetActive(true);
			}
			
        }
		
		// -------------------------------------------------------------------------------
		
	}
}
