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
	public partial class UnitDetailWindow : _BaseWindow
	{
		
		public Image 	largeIcon;
		public Text 	title;
		
		public PopupWindow popupWindow;
		
		public string titleSell = "SELL HERO";
		public string textSell = "Sell this hero for {0} ?";
		
		protected UnitInstance instance;
		
		// ==================================== FUNCTIONS ================================
		
		// -------------------------------------------------------------------------------
		// 
        // -------------------------------------------------------------------------------
		public void Show(UnitInstance _instance) {
		
			instance = _instance;

			OnDraw();
			OnShow();
			
		}
		
		// ===================================== EVENTS ==================================
        
        // -------------------------------------------------------------------------------
        // 
        // -------------------------------------------------------------------------------
        public override void OnDraw() {
        	if (instance == null) return;
			largeIcon.sprite 	= instance.template.largeIcon;
			title.text 		= instance.template.title.get(0);
        }
        
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public void OnSell() {
        	string s =  string.Format(textSell, instance.cost.getSellPriceString);
        	popupWindow.Show(titleSell, s, OnSellConfirm, null);
        }
        
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		public void OnSellConfirm() {
			int amount = popupWindow.GetSliderValue;
			instance.cost.sellInstance(amount);
			OnHide();
		}
		
		// -------------------------------------------------------------------------------
		
	}
}
