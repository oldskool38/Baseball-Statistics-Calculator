using System;
using Microsoft.Data.Sqlite;

namespace Data_Access_Library
{
	public static class DataAccess
	{
		public static void AddPlayers (string [] players)
		{
			// Create SQLite connection
			using (SqliteConnection db = new SqliteConnection ("Filename=BaseballStats.db"))
			{
				// Open the database connection
				db.Open ();

				// Create the add command
				SqliteCommand addCommand = new SqliteCommand
				{
					// Add database connection to add command
					Connection = db
				};

				// Variable for multiple uses
				int index = 0;

				// Determine how many names have been entered
				for (int count = 0; count < 33; count++)
				{
					if (players [count] != null)
					{
						index++;
					}
				}

				// Create a string array for the new names
				string [] names = new string [index];

				// Reset variable
				index = 0;

				// Add the new names to the names array
				for (int count = 0; count < 33; count++)
				{
					if (players [count] != null)
					{
						names [index++] = players [count];
					}
				}

				// Build the command text
				string addCmdTxt = "INSERT INTO BaseballStats (playerName) VALUES ((@p0))";
				addCommand.Parameters.Add ("@p0", SqliteType.Text).Value = names [0];
				for (int count = 1; count < names.Length; count++)
				{
					addCmdTxt += ", ((@p" + count + "))";
					addCommand.Parameters.Add ("@p" + count, SqliteType.Text).Value = names [count];
				}
				addCmdTxt += ";";

				// Add command text to the add command
				addCommand.CommandText = addCmdTxt;

				// Add players to database
				addCommand.ExecuteReader ();
			}
		}

		public static void DeleteAll ()
		{
			// Create SQLite connection
			using (SqliteConnection db = new SqliteConnection ("Filename=BaseballStats.db"))
			{
				// Open the database connection
				db.Open ();

				// Create delete all command text
				string delComTxt = "DELETE FROM BaseballStats;";

				// Create and execute the delete command
				SqliteCommand delCom = new SqliteCommand (delComTxt, db);
				delCom.ExecuteReader ();
			}
		}

		public static void DeletePlayer (int player)
		{
			// Create SQLite connection
			using (SqliteConnection db = new SqliteConnection ("Filename=BaseballStats.db"))
			{
				// Open the database connection
				db.Open ();

				// Create delete player command text
				string delComTxt = "DELETE FROM BaseballStats WHERE (id = " + player + ");";

				// Create and execute the delete command
				SqliteCommand delCom = new SqliteCommand (delComTxt, db);
				delCom.ExecuteReader ();
			}
		}

		public static SqliteDataReader GetDefenseStats (int player)
		{
			// Create the database connection
			using (SqliteConnection db = new SqliteConnection ("Filename=BaseballStats.db"))
			{
				// Open the connection to the database
				db.Open ();

				// Create the SQLite command string to get stored statistics
				string getStatsTxt = "SELECT dA, dBB, dCS, dCSP, dDER, dE, dFPCT, dH, dHBP, dHR, dINN, dO, dPA, dPO, dRF, dROE, dSBA, dSO, dTC, W, L, WPCT FROM BaseballStats WHERE (id = " + player + ");";

				// Create the SQLite command
				SqliteCommand getStats = new SqliteCommand (getStatsTxt, db);

				// Return the results
				return getStats.ExecuteReader ();
			}
		}

		public static SqliteDataReader GetObserveredStats (int player)
		{
			// Create the database connection
			using (SqliteConnection db = new SqliteConnection ("Filename=BaseballStats.db"))
			{
				// Open the connection to the database
				db.Open ();

				// Create the SQL command string to get observed statistics from the database
				string tableCommand = "SELECT dA, dBB, dCS, dE, dH, dHBP, dHR, dO, dPA, dPO, dROE, dSBA, dSO, o1B, o2B, o3B, oPA, oAO, oBB, oCS, oGO, oH, oHBP, oHR, oIBB, oROE, oSB, oSBA, oSF, oSH, oSO, pBB, pER, pFB, pGB, pGS, pH, pIP, pIR, pIRA, pLD, pNP, pPA, pPU, pSO, pSV, pSVO, W, L WHERE (id = " + player + ");";

				// Create the SQLite command to get the required statistics
				SqliteCommand getStats = new SqliteCommand (tableCommand, db);

				// Return the results
				return getStats.ExecuteReader ();
			}
		}

		public static SqliteDataReader GetOffenseStats (int player)
		{
			// Create the database connection
			using (SqliteConnection db = new SqliteConnection ("Filename=BaseballStats.db"))
			{
				// Open the connection to the database
				db.Open ();

				// Create the SQLite command string to get stored statistics
				string getStatsTxt = "SELECT o1B, o2B, o3B, oAB, oAO, oAVG, oBABIP, oBB, oBBP, oCS, oGO, oGOAO, oH, oHBP, oHR, oHRP, oIBB, oISO, oOBP, oOPS, oPA, oPASO, oRC, oROE, oSB, oSBA, oSBP, oSF, oSH, oSLG, oSO, oSOP, oTB, W, L, WPCT FROM BaseballStats WHERE (id = " + player + ");";

				// Create the SQLite command
				SqliteCommand getStats = new SqliteCommand (getStatsTxt, db);

				// Return the results
				return getStats.ExecuteReader ();
			}
		}

		public static SqliteDataReader GetPitchingStats (int player)
		{
			// Create the database connection
			using (SqliteConnection db = new SqliteConnection ("Filename=BaseballStats.db"))
			{
				// Open the connection to the database
				db.Open ();

				// Create the SQLite command string to get stored statistics
				string getStatsTxt = "SELECT pBB, pBBP, pER, pERA, pFB, pFBP, pGB, pGBP, pGS, pH, pIGS, pIP, pIR, pIRA, pIRAP, pKBB, pKP, pLD, pLDP, pNP, pPA, pPIP, pPU, pPUP, pSIERA, pSO, pSV, pSVO, pSVP, pWHIP, W, L, WPCT FROM BaseballStats WHERE (id = " + player + ");";

				// Create the SQLite command
				SqliteCommand getStats = new SqliteCommand (getStatsTxt, db);

				// Return the results
				return getStats.ExecuteReader ();
			}
		}

		public static SqliteDataReader GetPlayers ()
		{
			// Create the database connection
			using (SqliteConnection db = new SqliteConnection ("Filename=BaseballStats.db"))
			{
				// Open the connection to the database
				db.Open ();

				// Create the SQLite command to get all stored player names
				string getCommand = "SELECT id, playerName FROM BaseballStats ORDER BY playerName;";
				SqliteCommand getPlayers = new SqliteCommand (getCommand, db);

				// Return the results
				return getPlayers.ExecuteReader ();
			}
		}

		/// <summary>
		/// Creates the database and table that will hold the statistics if they don't exist
		/// </summary>
		public static void InitializeDatabase ()
		{
			// Create SQLite connection
			using (SqliteConnection db = new SqliteConnection ("Filename=BaseballStats.db"))
			{
				// Open the database connection
				db.Open ();

				// Create the SQL command string to create the table BaseballStats if it doesn't exist
				string tableCommand = "CREATE TABLE IF NOT EXISTS BaseballStats (id INTEGER PRIMARY KEY ASC, playerName VARCHAR(50) NOT NULL, dA  DEFAULT 0, dBB  DEFAULT 0, dCS  DEFAULT 0, dCSP  DEFAULT 0, dDER  DEFAULT 0, dE  DEFAULT 0, dFPCT  DEFAULT 0, dH  DEFAULT 0, dHBP  DEFAULT 0, dHR  DEFAULT 0, dINN  DEFAULT 0, dO  DEFAULT 0, dPA  DEFAULT 0, dPO  DEFAULT 0, dRF  DEFAULT 0, dROE  DEFAULT 0, dSBA  DEFAULT 0, dSO  DEFAULT 0, dTC  DEFAULT 0, o1B  DEFAULT 0, o2B  DEFAULT 0, o3B  DEFAULT 0, oAB  DEFAULT 0, oAO  DEFAULT 0, oAVG  DEFAULT 0, oBABIP  DEFAULT 0, oBB  DEFAULT 0, oBBP  DEFAULT 0, oCS  DEFAULT 0, oGO  DEFAULT 0, oGOAO  DEFAULT 0, oH  DEFAULT 0, oHBP  DEFAULT 0, oHR  DEFAULT 0, oHRP  DEFAULT 0, oIBB  DEFAULT 0, oISO  DEFAULT 0, oOBP  DEFAULT 0, oOPS  DEFAULT 0, oPA  DEFAULT 0, oPASO  DEFAULT 0, oRC  DEFAULT 0, oROE  DEFAULT 0, oSB  DEFAULT 0, oSBA  DEFAULT 0, oSBP  DEFAULT 0, oSF  DEFAULT 0, oSH  DEFAULT 0, oSLG  DEFAULT 0, oSO  DEFAULT 0, oSOP  DEFAULT 0, oTB  DEFAULT 0, pBB  DEFAULT 0, pBBP  DEFAULT 0, pER  DEFAULT 0, pERA  DEFAULT 0, pFB  DEFAULT 0, pFBP  DEFAULT 0, pGB  DEFAULT 0, pGBP  DEFAULT 0, pGS  DEFAULT 0, pH  DEFAULT 0, pIGS  DEFAULT 0, pIP  DEFAULT 0, pIR  DEFAULT 0, pIRA  DEFAULT 0, pIRAP  DEFAULT 0, pKBB  DEFAULT 0, pKP  DEFAULT 0, pLD  DEFAULT 0, pLDP  DEFAULT 0, pNP  DEFAULT 0, pPA  DEFAULT 0, pPIP  DEFAULT 0, pPU  DEFAULT 0, pPUP  DEFAULT 0, pSIERA  DEFAULT 0, pSO  DEFAULT 0, pSV  DEFAULT 0, pSVO  DEFAULT 0, pSVP  DEFAULT 0, pWHIP  DEFAULT 0, W  DEFAULT 0, L  DEFAULT 0, WPCT  DEFAULT 0);";

				// Create the SQLite command
				SqliteCommand createTable = new SqliteCommand (tableCommand, db);

				// Execute the SQLite command
				createTable.ExecuteReader ();
			}
		}

		public static void InsertCalculatedStats (int player, double dCSP, double dDER, double dFPCT, double dINN, double dRF, double dTC, double oAB, double oAVG, double oBABIP, double oBBP, double oGOAO, double oHRP, double oISO, double oOBP, double oOPS, double oPASO, double oRC, double oSBP, double oSLG, double oSOP, double oTB, double pBBP, double pERA, double pFBP, double pGBP, double pIGS, double pIRAP, double pKBB, double pKP, double pLDP, double pPIP, double pPUP, double pSIERA, double pSVP, double pWHIP, double WPCT)
		{
			// Create the database connection
			using (SqliteConnection db = new SqliteConnection ("Filename=BaseballStats.db"))
			{
				// Open database connection
				db.Open ();

				// Create the SQL command string to update the calculated statistics
				string updateCommand = "UPDATE BaseballStats SET dCSP = " + dCSP + ", dDER = " + dDER + ", dFPCT = " + dFPCT + ", dINN = " + dINN + ", dRF = " + dRF + ", dTC = " + dTC + ", oAB = " + oAB + ", oAVG = " + oAVG + ", oBABIP = " + oBABIP + ", oBBP = " + oBBP + ", oGOAO = " + oGOAO + ", oHRP = " + oHRP + ", oISO = " + oISO + ", oOBP = " + oOBP + ", oOPS = " + oOPS + ", oPASO = " + oPASO + ", oRC = " + oRC + ", oSBP = " + oSBP + ", oSLG = " + oSLG + ", oSOP = " + oSOP + ", oTB = " + oTB + ", pBBP = " + pBBP + ", pERA = " + pERA + ", pFBP = " + pFBP + ", pGBP = " + pGBP + ", pIGS = " + pIGS + ", pIRAP = " + pIRAP + ", pKBB = " + pKBB + ", pKP = " + pKP + ", pLDP = " + pLDP + ", pPIP = " + pPIP + ", pPUP = " + pPUP + ", pSIERA = " + pSIERA + ", pSVP = " + pSVP + ", pWHIP = " + pWHIP + ", WPCT = " + WPCT + " WHERE (id = " + player + ");";

				// Create the SQLite command to update the statistics
				SqliteCommand updateStats = new SqliteCommand (updateCommand, db);

				// Execute the SQLite command
				updateStats.ExecuteReader ();
			}
		}

		public static void InsertDefenseStats (int player, int dA, int dBB, int dCS, int dE, int dH, int dHBP, int dHR, int dO, int dPA, int dPO, int dROE, int dSBA, int dSO, int W, int L)
		{
			// Create the database connection
			using (SqliteConnection db = new SqliteConnection ("Filename=BaseballStats.db"))
			{
				// Open database connection
				db.Open ();

				// Create the update command text
				string updateCmdTxt = "UPDATE BaseballStats SET dA = " + dA + ", dBB = " + dBB + ", dCS = " + dCS + ", dE = " + dE + ", dH = " + dH + ", dHBP = " + dHBP + ", dHR = " + dHR + ", dO = " + dO + ", dPA = " + dPA + ", dPO = " + dPO + ", dROE = " + dROE + ", dSBA = " + dSBA + ", dSO = " + dSO + ", W = " + W + ", L = " + L + " WHERE (id = " + player + ");";

				// Create the SQLite update command
				SqliteCommand updateCmd = new SqliteCommand (updateCmdTxt, db);

				// Execute the update command
				updateCmd.ExecuteReader ();
			}
		}

		public static void InsertOffenseStats (int player, int o1B, int o2B, int o3B, int oAO, int oBB, int oCS, int oGO, int oH, int oHBP, int oHR, int oIBB, int oPA, int oROE, int oSB, int oSBA, int oSF, int oSH, int oSO, int W, int L)
		{
			// Create the database connection
			using (SqliteConnection db = new SqliteConnection ("Filename=BaseballStats.db"))
			{
				// Open database connection
				db.Open ();

				// Create the update command text
				string updateCmdTxt = "UPDATE BaseballStats SET o1B = " + o1B + ", o2B = " + o2B + ", o3B = " + o3B + ", oAO = " + oAO + ", oBB = " + oBB + ", oCS = " + oCS + ", oGO = " + oGO + ", oH = " + oH + ", oHBP = " + oHBP + ", oHR = " + oHR + ", oIBB = " + oIBB + ", oPA = " + oPA + ", oROE = " + oROE + ", oSB = " + oSB + ", oSBA = " + oSBA + ", oSF = " + oSF + ", oSH = " + oSH + ", oSO = " + oSO + ", W = " + W + ", L = " + L + " WHERE (id = " + player + ");";

				// Create the SQLite update command
				SqliteCommand updateCmd = new SqliteCommand (updateCmdTxt, db);

				// Execute the update command
				updateCmd.ExecuteReader ();
			}
		}

		public static void InsertPitchingStats (int player, int pBB, int pER, int pFB, int pGB, int pGS, int pH, int pIP, int pIR, int pIRA, int pLD, int pNP, int pPA, int pPU, int pSO, int pSV, int pSVO, int W, int L)
		{
			// Create the database connection
			using (SqliteConnection db = new SqliteConnection ("Filename=BaseballStats.db"))
			{
				// Open the connection to the database
				db.Open ();

				// Create the update command text
				string updateCmdTxt = "UPDATE BaseballStats SET pBB = " + pBB + ", pER = " + pER + ", pFB = " + pFB + ", pGB = " + pGB + ", pGS = " + pGS + ", pH = " + pH + ", pIP = " + pIP + ", pIR = " + pIR + ", pIRA = " + pIRA + ", pLD = " + pLD + ", pNP = " + pNP + ", pPA = " + pPA + ", pPU = " + pPU + ", pSO = " + pSO + ", pSV = " + pSV + ", pSVO = " + pSVO + ", W = " + W + ", L = " + L + " WHERE (id = " + player + ");";

				// Create the SQLite update command
				SqliteCommand updateCmd = new SqliteCommand (updateCmdTxt, db);

				// Execute the update command
				updateCmd.ExecuteReader ();
			}
		}
	}
}