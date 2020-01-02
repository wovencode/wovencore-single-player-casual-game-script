// =======================================================================================
// Wovencore by Wovencode (c)
// =======================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Data;
using UnityEngine;
using UnityEngine.Events;
using Mono.Data.Sqlite;
using woco.core;

namespace woco.core
{
	
	// ===================================================================================
	// SaveManager
	// ===================================================================================
	[DisallowMultipleComponent]
	public partial class SaveManager : _BaseManager
	{
	
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		// PROPERTIES
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		
		[Header("[OPTIONS]")]
		[Range(5,99999)]
		public float updateInterval	= 5f;
		
		[Header("[DATABASE]")]
		public DatabaseData database;
		
		public UnityEvent					loadEvent;
		public UnityEvent					saveEvent;
		
		protected static string 			_dbPath 		= "";
		protected SqliteConnection 			_connection 	= null;
		protected SqliteCommand 			_command 		= null;
		protected SqliteDataReader 			_reader 		= null;
		protected string 					_sqlString	 	= "";
		
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		// MONO
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		
		// -------------------------------------------------------------------------------
		// Awake
		// -------------------------------------------------------------------------------
		protected override void Awake()
		{
			base.Awake();
			_dbPath = Tools.GetPath(database.name);
			InitDatabase();
		}
		
		// -------------------------------------------------------------------------------
		// OnDestroy
		// -------------------------------------------------------------------------------
		protected void OnDestroy()
		{
			CancelInvoke();
			//saveEvent.Invoke();
			CloseDatabase();
		}

		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		// QUERIES
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		
		// -------------------------------------------------------------------------------
		// ExecuteNonQuery
		// Execute a query that does not return anything
		// -------------------------------------------------------------------------------
		protected void ExecuteNonQuery(string sql, params SqliteParameter[] args)
		{
			using (SqliteCommand command = new SqliteCommand(sql, _connection))
			{
				foreach (SqliteParameter param in args)
					command.Parameters.Add(param);
				command.ExecuteNonQuery();
			}
		}
		
		// -------------------------------------------------------------------------------
		// ExecuteScalar
		// Executes a query that returns a single value
		// -------------------------------------------------------------------------------
		protected object ExecuteScalar(string sql, params SqliteParameter[] args)
		{
			using (SqliteCommand command = new SqliteCommand(sql, _connection))
			{
				foreach (SqliteParameter param in args)
					command.Parameters.Add(param);
				return command.ExecuteScalar();
			}
		}

		// -------------------------------------------------------------------------------
		// ExecuteReader
		// Return multiple values from the database
		// -------------------------------------------------------------------------------
		protected List< List<object> > ExecuteReader(string sql, params SqliteParameter[] args)
		{
			List< List<object> > result = new List< List<object> >();

			using (SqliteCommand command = new SqliteCommand(sql, _connection))
			{
				foreach (SqliteParameter param in args)
					command.Parameters.Add(param);

				using (SqliteDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						object[] buffer = new object[reader.FieldCount];
						reader.GetValues(buffer);
						result.Add(buffer.ToList());
					}
				}
			}

			return result;
		}
		
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		// PROTECTED FUNCTIONS
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
			
		// -------------------------------------------------------------------------------
		// InitDatabase
		// -------------------------------------------------------------------------------
		protected void InitDatabase()
		{
			
			if (!database.valid)
				return;
			
			if (File.Exists(_dbPath) && database.initDatabase) 
			{
				File.Delete(_dbPath);
			}
			else if (File.Exists(_dbPath))
			{
				if (Tools.GetChecksum(_dbPath) == false)
				{
					Debug.LogError("Database file is corrupted!");
					//File.Delete(_dbPath); // TODO: enable later on
				}
			}
        	else if (!File.Exists(_dbPath))
        	{
            	SqliteConnection.CreateFile(_dbPath);
			}
			
			_sqlString = "URI=file:" + _dbPath;
			
			_connection = new SqliteConnection(_sqlString);
			_command = _connection.CreateCommand();
			
			_connection.Open();

			// BEGIN Performance Improvements
			
			_command.CommandText = "PRAGMA journal_mode = WAL;";	// WAL = write ahead logging
			_command.ExecuteNonQuery();

			_command.CommandText = "PRAGMA journal_mode";
			_reader = _command.ExecuteReader();
			
			_reader.Close();

			_command.CommandText = "PRAGMA synchronous = OFF";
			_command.ExecuteNonQuery();

			_command.CommandText = "PRAGMA synchronous";
			_reader = _command.ExecuteReader();
			
			// END Performance Improvements
			
			_reader.Close();
			
			_sqlString = "";
						
			foreach (_BaseManager manager in GetComponents<_BaseManager>() )
				for (int i = 0; i < manager.tableData.Length; i++)
    				InitTables(manager.getName, manager.tableData[i]);
			
			Invoke(nameof(LoadDatabase), 2f);
			
			InvokeRepeating(nameof(UpdateDatabase), updateInterval, updateInterval);
			
		}

		// -------------------------------------------------------------------------------
		// InitTables
		// Here we check if each table in the database already exists or not. If not, we
		// create it. We might also delete and re-create one or more tables if required.
		// -------------------------------------------------------------------------------
		protected void InitTables(string tableName, TableData table) {
		
			if (!table.valid)
				return;
			
			bool 			deleteTable 	= false;
			List<string> 	primaryKeys 	= new List<string>();
			
			_command.CommandText = "SELECT name FROM sqlite_master WHERE name='" + tableName + "'";
			_reader = _command.ExecuteReader();
			
			if (!_reader.Read())
				deleteTable = true;

			_reader.Close();

			if (deleteTable || table.deleteTable)
			{
				
				DeleteTable(tableName);
				
				_sqlString = "CREATE TABLE IF NOT EXISTS " + tableName + " (";
				
				foreach (ColumnData column in table.template.columns)
				{
					if (!column.valid)
					{
						Debug.Log("Column settings invalid!");
						continue;
					}
					
					_sqlString += column.name + " " + column.dataType.ToString();
					
					if (column.notNull)
						_sqlString += " NOT NULL";
					
					if (column.primaryKey)
						primaryKeys.Add(column.name);
						
					if (!column.Equals(table.template.columns[table.template.columns.GetUpperBound(0)] ))
						_sqlString += ", ";
					
				}
				
				if (primaryKeys.Count > 0)
				{
				
					_sqlString += ", PRIMARY KEY(";
					
					foreach (string key in primaryKeys)
					{
						
						_sqlString += key;
						
						if (!key.Equals(primaryKeys.Last()))
							_sqlString += ", ";
					
					}
				
					_sqlString += ")";
					
				}
				
				_sqlString += ")";

				_command.CommandText = _sqlString;
				_command.ExecuteNonQuery();
				
			}
				
		}
		
		// -------------------------------------------------------------------------------
		// UpdateDatabase
		// -------------------------------------------------------------------------------
		protected void UpdateDatabase() {
			saveEvent.Invoke();
			Tools.SetChecksum(_dbPath);
			Debug.Log("Updating database...");
		}

		// -------------------------------------------------------------------------------
		// LoadDatabase
		// -------------------------------------------------------------------------------
		protected void LoadDatabase() {
			loadEvent.Invoke();
		}
		
		// -------------------------------------------------------------------------------
		// DeleteTable
		// -------------------------------------------------------------------------------
		protected void DeleteTable(string sName)
		{
			_command.CommandText = "DROP TABLE IF EXISTS " + sName;
			_command.ExecuteNonQuery();
		}
		
		// -------------------------------------------------------------------------------
		// CloseDatabase
		// -------------------------------------------------------------------------------
		protected void CloseDatabase() {
			
			if (_reader != null && !_reader.IsClosed)
				_reader.Close();
			_reader = null;

			if (_command != null)
				_command.Dispose();
			_command = null;

			if (_connection != null && _connection.State != ConnectionState.Closed)
				_connection.Close();
			_connection = null;
			
			Tools.SetChecksum(_dbPath);
			
		}
		
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		// PUBLIC FUNCTIONS
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

		// -------------------------------------------------------------------------------
		// LoadData
		// -------------------------------------------------------------------------------
		public List< List<object> > LoadData(string tableName, string[] fields)
		{
			return ExecuteReader("SELECT "+Tools.StringArrayToString(fields)+" FROM "+tableName+" WHERE account=@account", new SqliteParameter("@account", Tools.GetUserId));
		}

		// -------------------------------------------------------------------------------
		// SaveData
		// -------------------------------------------------------------------------------
		public void SaveData(DataHolder dataHolder)
		{
			
			if (dataHolder == null) return;
			
			ExecuteNonQuery("DELETE FROM "+dataHolder.tableName+" WHERE account=@account", new SqliteParameter("@account", Tools.GetUserId));
			
			ExecuteNonQuery("BEGIN");
			
			foreach (DataFields data in dataHolder.fieldData)
			{
			
				SqliteParameter[] args = new SqliteParameter[data.length];
			
				for (int i = 0; i < data.fieldData.Count(); i++)
				{
					args[i] = new SqliteParameter("@" + dataHolder.fieldNames[i], data.fieldData[i]);
				}
			
				ExecuteNonQuery("INSERT INTO "+dataHolder.tableName+" VALUES ("+Tools.StringListToString(dataHolder.fieldNames, "@")+")", args);

			}

			ExecuteNonQuery("END");
			
			dataHolder = null;
			
		}
		
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		// TYPECASTING
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		
		// -------------------------------------------------------------------------------
		// CastToLong
		// -------------------------------------------------------------------------------
		public long CastToLong(object data)
		{
			return (long)data;
		}
		
		// -------------------------------------------------------------------------------
		// CastToInt
		// -------------------------------------------------------------------------------
		public int CastToInt(object data)
		{
			return Convert.ToInt32((long)data);
		}
		
		// -------------------------------------------------------------------------------
		// CastToString
		// -------------------------------------------------------------------------------
		public string CastToString(object data)
		{
			return (string)data;
		}
		
		// -------------------------------------------------------------------------------
		// CastToFloat
		// -------------------------------------------------------------------------------
		public float CastToFloat(object data)
		{
			return (float)data;
		}
		
		// -------------------------------------------------------------------------------
		// CastToTime
		// -------------------------------------------------------------------------------
		public float CastToTime(object data)
		{
			return (float)data + Time.time;
		}
		
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		// EVENTS
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

		// -------------------------------------------------------------------------------
		// OnReset
		// -------------------------------------------------------------------------------
		public override void OnReset() {}
		
		// -------------------------------------------------------------------------------
		// OnLoad
		// -------------------------------------------------------------------------------
		public override void OnLoad() {}
		
		// -------------------------------------------------------------------------------
		// OnSave
		// -------------------------------------------------------------------------------
		public override void OnSave() {}
		
		// -------------------------------------------------------------------------------
		// OnData
		// -------------------------------------------------------------------------------
		public override DataHolder OnData()
		{
			return null;
		}

		// -------------------------------------------------------------------------------

	}
	
	// ===================================================================================
	
}
