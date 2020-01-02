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
	// UIActorDetail
	// ===================================================================================
	public partial class PopupWindow : _BaseWindow
	{
		
		public Text 	title;
		public Text 	description;
		public Button	buttonConfirm;
		public Button	buttonCancel;
		
		public Slider	sliderAmount;
		
		protected Action	actionConfirm;
		protected Action	actionCancel;
		
		// ==================================== FUNCTIONS ================================
		
		// -------------------------------------------------------------------------------
		// 
        // -------------------------------------------------------------------------------
		public void Show(string _title, string _description, Action _actionConfirm, Action _actionCancel, int maxSliderValue = 1) {
			
			title.text 			= _title;
			description.text 	= _description;
			
			actionConfirm 		= _actionConfirm;
			actionCancel 		= _actionCancel;
			
			sliderAmount.maxValue = 1;
			sliderAmount.value = 1;
			
			if (maxSliderValue <= 1)
			{
				sliderAmount.gameObject.SetActive(false);
			}
			else
			{
				sliderAmount.maxValue = maxSliderValue;
				sliderAmount.gameObject.SetActive(true);
			}
			
			OnShow();
		}
		
		
		// -------------------------------------------------------------------------------
		// 
	    // -------------------------------------------------------------------------------
		public int GetSliderValue
		{
			get {
				return (int)sliderAmount.value;
			}
		}
		
		// ===================================== EVENTS ==================================
        
        // -------------------------------------------------------------------------------
        //
        // -------------------------------------------------------------------------------
        public void OnConfirm() {
        
        	if (actionConfirm != null)
        		actionConfirm();
        
        	OnHide();
        	
        }
		
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
       	public void OnCancel() {
        
        	if (actionCancel != null)
        		actionCancel();
        		
        	OnHide();
        
        }
		
		// -------------------------------------------------------------------------------
		// 
        // -------------------------------------------------------------------------------
		public override void OnDraw() {
		}

		// -------------------------------------------------------------------------------
		
	}
}
