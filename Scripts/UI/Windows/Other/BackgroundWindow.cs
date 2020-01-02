// =======================================================================================
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
	// BackgroundWindow
	// ===================================================================================
	public partial class BackgroundWindow : MonoBehaviour
	{
		
		public Animator animator;
		public string fadeInTriggerName = "fadeIn";
		public string fadeOutTriggerName = "fadeOut";

		public void FadeIn()
		{
			animator.SetTrigger(fadeInTriggerName);
		}

		public void FadeOut()
		{
			animator.SetTrigger(fadeOutTriggerName);
		}
		
		// -------------------------------------------------------------------------------
		
	}
}
