// =======================================================================================
// Wovencore by Wovencode (c)
// =======================================================================================
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.SqliteClient;
using System.IO;
using woco.core;

namespace woco.core
{

	// ===================================================================================
	// CURRENCY CLASSES
	// ===================================================================================
	// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
	// LanguageString
	// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
	[System.Serializable]
	public class CurrencyAmount
	{
		
		public CurrencyTemplate template;
		public long amount;
		
		// -------------------------------------------------------------------------------
		// valid
		// -------------------------------------------------------------------------------
		public bool valid {
			get { return (template != null && amount > 0); }
		}
		
	}


	// ===================================================================================
	// LANGUAGE CLASSES
	// ===================================================================================
	
	// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
	// LanguageString
	// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
	[System.Serializable]	
	public class LanguageString
	{
	
		public string[] text;
		
		// -------------------------------------------------------------------------------
		// get
		// -------------------------------------------------------------------------------
		public string get(int index=0)
		{
			return text[index];
		}
		
	}
	
	// ===================================================================================
	// DATA CLASSES
	// ===================================================================================

	// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
	// DatabaseData
	// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
	[System.Serializable]
	public class DatabaseData
	{
		
		public string name = "Database.sqlite";
		public bool initDatabase 	= false;
		
		// -------------------------------------------------------------------------------
		// valid
		// -------------------------------------------------------------------------------
		public bool valid
		{
			get {
				return !string.IsNullOrWhiteSpace(name);
			}
		}
		
	}
	
	// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
	// TableData
	// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
	[System.Serializable]
	public class TableData
	{
		
		public bool deleteTable = false;
		public TableTemplate template;
		
		// -------------------------------------------------------------------------------
		// valid
		// -------------------------------------------------------------------------------
		public bool valid
		{
			get {
				return 
						template != null &&
						template.valid;
			}
		}
		
		// -------------------------------------------------------------------------------
		// tableColumns
		// -------------------------------------------------------------------------------
		public string[] tableColumns
		{
			get {
				
				string[] columns = new string[template.columns.Length];
				
				for (int i = 0; i < template.columns.Length; i++)
				{
					columns[i] = template.columns[i].name;
				}
				
				return columns;
			
			}
		}
		
		// -------------------------------------------------------------------------------
		
	}
	
	// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
	// ColumnData
	// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
	[System.Serializable]
	public class ColumnData
	{
		
		public string name;
		public DataType dataType;
		public bool primaryKey = false;
		public bool notNull = true;
		
		// -------------------------------------------------------------------------------
		// valid
		// -------------------------------------------------------------------------------
		public bool valid
		{
			get {
				return !string.IsNullOrWhiteSpace(name);
			}
		}
		
	}

	// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
	// DataHolder
	// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
	public class DataHolder
	{
		public string tableName;
		public List<string> 	fieldNames	= new List<string>();
		public List<DataFields> fieldData	= new List<DataFields>();
		
		// -------------------------------------------------------------------------------
		// Constructor
		// -------------------------------------------------------------------------------
		public DataHolder(string _tableName, string[] _fieldNames)
		{
			tableName = _tableName;
			fieldNames.AddRange(_fieldNames);
		}
		
		// -------------------------------------------------------------------------------
		// length
		// -------------------------------------------------------------------------------
		public int length
		{
			get
			{
				return fieldNames.Count;
			}
		}
		
		// -------------------------------------------------------------------------------
		// addData
		// -------------------------------------------------------------------------------
		public void addData(params object[] args)
		{
			DataFields fields = new DataFields();
			
			foreach (object obj in args)
				fields.fieldData.Add(obj);
			
			fieldData.Add(fields);
		}
		
		// -------------------------------------------------------------------------------
		
	}
	
	// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
	// DataFields
	// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
	public class DataFields
	{
		
		public List<object> fieldData = new List<object>();
		
		// -------------------------------------------------------------------------------
		// length
		// -------------------------------------------------------------------------------
		public int length
		{
			get
			{
				return fieldData.Count;
			}
		}
		
	}

	// ===================================================================================
		
}
