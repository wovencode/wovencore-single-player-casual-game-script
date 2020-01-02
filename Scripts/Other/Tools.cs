// =======================================================================================
// Wovencore by Wovencode (c)
// =======================================================================================
using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using UnityEngine;
using woco.core;

namespace woco.core
{
	
	// ===================================================================================
	// TOOLS
	// ===================================================================================
	public partial class Tools
	{
		
		private const char CONST_DELIMITER = ',';
		
		protected static string sOldChecksum, sNewChecksum	= "";
		
		// ============================ PATH & DIRECTORIES ===============================
		
		// -------------------------------------------------------------------------------
		// SetPath
		// -------------------------------------------------------------------------------
		public static string GetPath(string fileName) {
#if UNITY_EDITOR
        	return Path.Combine(Directory.GetParent(Application.dataPath).FullName, fileName);
#elif UNITY_ANDROID
        	return Path.Combine(Application.persistentDataPath, fileName);
#elif UNITY_IOS
        	return Path.Combine(Application.persistentDataPath, fileName);
#else
        	return Path.Combine(Application.dataPath, fileName);
#endif			
		}
		
		// ================================= SECURITY ====================================
		
		// -------------------------------------------------------------------------------
		// GetChecksum
		// -------------------------------------------------------------------------------
		public static bool GetChecksum(string filepath)
		{
			
			sNewChecksum = CalculateMD5(filepath);
		
			sOldChecksum = PlayerPrefs.GetString("CS", "");
			
			if (string.IsNullOrWhiteSpace(sOldChecksum))
				SetChecksum(filepath);
			
			return (sOldChecksum == sNewChecksum);
			
		}
		
		// -------------------------------------------------------------------------------
		// SetChecksum
		// -------------------------------------------------------------------------------
		public static void SetChecksum(string filepath)
		{
			sNewChecksum = CalculateMD5(filepath);
			PlayerPrefs.SetString("CS", sNewChecksum);
		}
		
		// -------------------------------------------------------------------------------
		// CalculateMD5
		// -------------------------------------------------------------------------------
		public static string CalculateMD5(string filepath)
		{
			using (var md5 = MD5.Create())
			{
				using (var stream = File.OpenRead(filepath))
				{
					var hash = md5.ComputeHash(stream);
					return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
				}
			}
		}
		
		// ================================= OTHER =======================================
		
		
		
		
		// -------------------------------------------------------------------------------
		// ConvertToUnixTimestamp
		// -------------------------------------------------------------------------------
		public static double ConvertToUnixTimestamp(DateTime date)
		{
			DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			TimeSpan diff = date.ToUniversalTime() - origin;
			return Math.Floor(diff.TotalSeconds);
		}

		// -------------------------------------------------------------------------------
		// GetUserId
		// -------------------------------------------------------------------------------
		public static string GetUserId
		{
			get
			{
				return SystemInfo.deviceUniqueIdentifier.ToString();
			}
		}
		
		// -------------------------------------------------------------------------------
		// IntArrayToString
		// -------------------------------------------------------------------------------
		public static string IntArrayToString(int[] array, string prefix = "")
		{
			if (array == null || array.Length == 0) return null;
			string arrayString = "";
			for (int i = 0; i < array.Length; i++)
			{
				arrayString += prefix + array[i].ToString();
				if (i < array.Length - 1)
					arrayString += CONST_DELIMITER;
			}
			return arrayString;
		}
		
		// -------------------------------------------------------------------------------
		// StringArrayToString
		// -------------------------------------------------------------------------------
		public static string StringArrayToString(string[] array, string prefix = "")
		{
			if (array == null || array.Length == 0) return null;
			string arrayString = "";
			for (int i = 0; i < array.Length; i++)
			{
				arrayString += prefix + array[i].ToString();
				if (i < array.Length - 1)
					arrayString += CONST_DELIMITER;
			}
			return arrayString;
		}
		
		// -------------------------------------------------------------------------------
		// StringListToString
		// -------------------------------------------------------------------------------
		public static string StringListToString(List<string> list, string prefix = "")
		{
			if (list == null || list.Count == 0) return null;
			string arrayString = "";
			for (int i = 0; i < list.Count; i++)
			{
				arrayString += prefix + list[i].ToString();
				if (i < list.Count - 1)
					arrayString += CONST_DELIMITER;
			}
			return arrayString;
		}
		
		// -------------------------------------------------------------------------------
		// IntStringToArray
		// -------------------------------------------------------------------------------
		public static int[] IntStringToArray(string array)
		{
			if (string.IsNullOrWhiteSpace(array)) return null;
			string[] tokens = array.Split(CONST_DELIMITER);
			int[] arrayInt = Array.ConvertAll<string, int>(tokens, int.Parse);
			return arrayInt;
		}

		// -------------------------------------------------------------------------------
		// ArrayContains
		// -------------------------------------------------------------------------------
		public static bool ArrayContains(int[] defines, int define)
		{
			foreach (int def in defines)
			{
				if (def == define)
					return true;
			}
			return false;
		}

		// -------------------------------------------------------------------------------
		// ArrayContains
		// -------------------------------------------------------------------------------
		public static bool ArrayContains(string[] defines, string define)
		{
			foreach (string def in defines)
			{
				if (def == define)
					return true;
			}
			return false;
		}

		// -------------------------------------------------------------------------------
		// RemoveFromArray
		// -------------------------------------------------------------------------------
		public static string[] RemoveFromArray(string[] defines, string define)
		{
			return defines.Where(x => x != define).ToArray();
		}

		// -------------------------------------------------------------------------------
	
	}
	
	// ===================================================================================
	
}
