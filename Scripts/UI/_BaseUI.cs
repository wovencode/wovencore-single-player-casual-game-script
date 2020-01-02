// =======================================================================================
// Wovencore by Wovencode (c)
// =======================================================================================
using System;
using UnityEngine;
using UnityEngine.Events;
using woco.core;

namespace woco.core
{
	
	// ===================================================================================
	// _BaseUI
	// ===================================================================================
	public abstract partial class _BaseUI : MonoBehaviour
	{
	
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		// PROPERTIES
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		
		public UnityEvent onAwake;
		public UnityEvent onUpdate;
		
		protected GameManager 	_gameManager;
		protected float 		_fUpdateTimer 		= 0;
		protected float 		_fUpdateInterval 	= 1.0f;
		
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		// MONO
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		
		// -------------------------------------------------------------------------------
		// Awake
		// -------------------------------------------------------------------------------
		protected void Awake()
		{
			_gameManager = FindObjectOfType<GameManager>();
			_fUpdateTimer = Time.time + _fUpdateInterval;
			onAwake.Invoke();
		}
		
		// -------------------------------------------------------------------------------
		// Update
		// -------------------------------------------------------------------------------
		protected void Update()
		{
			if (Time.time > _fUpdateTimer)
			{
				SlowUpdate();
				onUpdate.Invoke();
				_fUpdateTimer = Time.time + _fUpdateInterval;
			}
		}
		
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		// PUBLIC FUNCTIONS
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		
		public GameManager game { get { return _gameManager; } }
		
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		// PROTECTED FUNCTIONS
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		
		// -------------------------------------------------------------------------------
		// SlowUpdate
		// -------------------------------------------------------------------------------
		protected abstract void SlowUpdate();

		// -------------------------------------------------------------------------------

	}
	
	// ===================================================================================
	
}
