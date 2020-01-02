// =======================================================================================
// Wovencore by Wovencode (c)
// =======================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using woco.core;

namespace woco.core
{
	
	// ===================================================================================
	// UnitManager
	// ===================================================================================
	[DisallowMultipleComponent]
	public partial class UnitManager : _BaseManager
	{
	
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		// PROPERTIES
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		
		public List<UnitInstance> units = new List<UnitInstance>();
		
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		// PUBLIC FUNCTIONS
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		
		// -------------------------------------------------------------------------------
		// addUnit
		// -------------------------------------------------------------------------------
		public void addUnit(UnitTemplate _template)
		{
			UnitInstance instance	= new UnitInstance(game, _template);
			units.Add(instance);
		}
		
		// -------------------------------------------------------------------------------
		// removeUnit
		// -------------------------------------------------------------------------------
		public void removeUnit(string _id, int _amount)
		{
			if (!hasUnit(_id)) return;
			int index = getUnitIndex(_id);
			units.RemoveAt(index);
			
		}
		
		// -------------------------------------------------------------------------------
		// hasUnit
		// -------------------------------------------------------------------------------
		public bool hasUnit(string _id)
		{
			return units.Exists(x => x.id == _id);
		}
		
		// -------------------------------------------------------------------------------
		// getUnitIndex
		// -------------------------------------------------------------------------------
		public int getUnitIndex(string _id)
		{
			return units.FindIndex(x => x.id == _id);
		}
		
		
		

		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		// EVENTS
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

		// -------------------------------------------------------------------------------
		// OnReset
		// -------------------------------------------------------------------------------
		public override void OnReset() {
		
			units.Clear();
			
			foreach (UnitTemplate template in UnitTemplate.dict.Values)
			{
				
				if (template.startAmount <= 0) continue;
				
				for (int i = 0; i < template.startAmount; i++)
				{
					addUnit(template);
				}
			}
			
		}
		
		// -------------------------------------------------------------------------------
		// OnLoad
		// -------------------------------------------------------------------------------
		public override void OnLoad() {
		
			List< List<object> > table = game.saveManager.LoadData(getName, getColumns());
			
			if (table.Count > 0) 
			{
				foreach (List<object> row in table)
				{
				
					string 	name 			= game.saveManager.CastToString(row[1]);
					string 	parent 			= game.saveManager.CastToString(row[2]);
					string	id				= game.saveManager.CastToString(row[3]);
					int 	level	 		= game.saveManager.CastToInt(row[4]);
					int 	grade	 		= game.saveManager.CastToInt(row[5]);
					int 	health	 		= game.saveManager.CastToInt(row[6]);
					int 	energy	 		= game.saveManager.CastToInt(row[7]);
					
					UnitTemplate template;
					
					if (UnitTemplate.dict.TryGetValue(name.GetDeterministicHashCode(), out template))
					{
						addUnit(template);
					}
				}
				
			}
			
		}
		
		// -------------------------------------------------------------------------------
		// OnData
		// -------------------------------------------------------------------------------
		public override DataHolder OnData()
		{
			if (tableData.Length == 0) return null;
			
			int tableCount 	= 0;
			int unitCount = units.Count();
			
			DataHolder dataHolder = new DataHolder(getName, tableData[tableCount].tableColumns);
			
			for (int i = 0; i < unitCount; i++)
			{
			
				dataHolder.addData(Tools.GetUserId,
						units[i].getName,
						units[i].parent,
						units[i].id,
						units[i].level,
						units[i].grade,
						units[i].health,
						units[i].energy);
				
			}
			
			return dataHolder;
			
		}

		// -------------------------------------------------------------------------------

	}
	
	// ===================================================================================
	
}
