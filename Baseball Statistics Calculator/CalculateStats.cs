using System;

using Data_Access_Library;

using Microsoft.Data.Sqlite;

namespace BaseballStatisticsCalculator
{
	/// <summary>
	/// This class holds the method that performs the calculation of the statistics and saves the results to the database
	/// </summary>
	internal class CalculateStats
	{
		/// <summary>
		/// Method that will perform the calculation of the baseball statistics
		/// </summary>
		/// <param name="player">
		/// The player's id number in the database
		/// </param>
		public static void CalculateStatistics (int player)
		{
			// Create the variables that will hold the player's statistics
			double dA, dBB, dCS, dCSP, dDER, dE, dFPCT, dH, dHBP, dHR, dINN, dO, dPA, dPO, dRF, dROE, dSBA, dSO, dTC, o1B, o2B, o3B, oAB, oAO, oAVG, oBABIP, oBB, oBBP, oCS, oGO, oGOAO, oH, oHBP, oHR, oHRP, oIBB, oISO, oOBP, oOPS, oPA, oPASO, oRC, oROE, oSB, oSBA, oSBP, oSF, oSH, oSLG, oSO, oSOP, oTB, pBB, pBBP, pER, pERA, pFB, pFBP, pGB, pGBP, pGS, pH, pIGS, pIP, pIR, pIRA, pIRAP, pKBB, pKP, pLD, pLDP, pNP, pPA, pPIP, pPU, pPUP, pSIERA, pSO, pSV, pSVO, pSVP, pWHIP, W, L, WPCT;

			SqliteDataReader stats = DataAccess.GetObserveredStats (player);

			// Assign each value to its proper variable
			dA = stats.GetInt32 (0);
			dBB = stats.GetInt32 (1);
			dCS = stats.GetInt32 (2);
			dE = stats.GetInt32 (3);
			dH = stats.GetInt32 (4);
			dHBP = stats.GetInt32 (5);
			dHR = stats.GetInt32 (6);
			dO = stats.GetInt32 (7);
			dPA = stats.GetInt32 (8);
			dPO = stats.GetInt32 (9);
			dROE = stats.GetInt32 (10);
			dSBA = stats.GetInt32 (11);
			dSO = stats.GetInt32 (12);
			o1B = stats.GetInt32 (13);
			o2B = stats.GetInt32 (14);
			o3B = stats.GetInt32 (15);
			oPA = stats.GetInt32 (16);
			oAO = stats.GetInt32 (17);
			oBB = stats.GetInt32 (18);
			oCS = stats.GetInt32 (19);
			oGO = stats.GetInt32 (20);
			oH = stats.GetInt32 (21);
			oHBP = stats.GetInt32 (22);
			oHR = stats.GetInt32 (23);
			oIBB = stats.GetInt32 (24);
			oROE = stats.GetInt32 (25);
			oSB = stats.GetInt32 (26);
			oSBA = stats.GetInt32 (27);
			oSF = stats.GetInt32 (28);
			oSH = stats.GetInt32 (29);
			oSO = stats.GetInt32 (30);
			pBB = stats.GetInt32 (31);
			pER = stats.GetInt32 (32);
			pFB = stats.GetInt32 (33);
			pGB = stats.GetInt32 (34);
			pGS = stats.GetInt32 (35);
			pH = stats.GetInt32 (36);
			pIP = stats.GetInt32 (37);
			pIR = stats.GetInt32 (38);
			pIRA = stats.GetInt32 (39);
			pLD = stats.GetInt32 (40);
			pNP = stats.GetInt32 (41);
			pPA = stats.GetInt32 (42);
			pPU = stats.GetInt32 (43);
			pSO = stats.GetInt32 (44);
			pSV = stats.GetInt32 (45);
			pSVO = stats.GetInt32 (46);
			W = stats.GetInt32 (47);
			L = stats.GetInt32 (48);

			// Perform defense calculations
			dTC = (dPO + dA + dE);

			dINN = (dO / 3);

			if (dSBA != 0)
			{
				dCSP = (dCS / dSBA);
			}
			else
			{
				dCSP = 0;
			}

			if ((dPA - dBB - dSO - dHBP - dHR) != 0)
			{
				dDER = (1 - ((dH + dROE - dHR) / (dPA - dBB - dSO - dHBP - dHR)));
			}
			else
			{
				dDER = 0;
			}

			if (dTC != 0)
			{
				dFPCT = ((dPO + dA) / dTC);
			}
			else
			{
				dFPCT = 0;
			}

			if (dINN != 0)
			{
				dRF = (((dPO + dA) * 9) / dINN);
			}
			else
			{
				dRF = 0;
			}

			// Perform offense calculations
			oAB = (oPA - oBB - oHBP - oSH - oSF - oROE);

			oTB = (o1B + (2 * o2B) + (3 * o3B) + (4 * oHR));

			if (oAB != 0)
			{
				oISO = ((o2B + (2 * o3B) + (3 * oHR) / oAB));
				oSLG = ((o1B + (2 * o2B) + (3 * o3B) + (4 * oHR)) / oAB);
				oAVG = (oH / oAB);
			}
			else
			{
				oISO = 0;
				oSLG = 0;
				oAVG = 0;
			}

			if ((oAB + oBB + oHBP + oSF) != 0)
			{
				oOBP = ((oH + oBB + oHBP) / (oAB + oBB + oHBP + oSF));
			}
			else
			{
				oOBP = 0;
			}

			if ((oAB - oSO - oHR + oSF) != 0)
			{
				oBABIP = ((oH - oHR) / (oAB - oSO - oHR + oSF));
			}
			else
			{
				oBABIP = 0;
			}

			if (oPA != 0)
			{
				oBBP = ((oBB + oIBB + oHBP) / oPA);
				oHRP = (oHR / oPA);
				oSOP = (oSO / oPA);
			}
			else
			{
				oBBP = 0;
				oHRP = 0;
				oSOP = 0;
			}

			if (oAO != 0)
			{
				oGOAO = (oGO / oAO);
			}
			else
			{
				oGOAO = 0;
			}

			if (oSO != 0)
			{
				oPASO = (oPA / oSO);
			}
			else
			{
				oPASO = 0;
			}

			if ((oAB + oBB) != 0)
			{
				oRC = ((oTB * (oH + oBB)) / (oAB + oBB));
			}
			else
			{
				oRC = 0;
			}

			if (oSBA != 0)
			{
				oSBP = (oSB / oSBA);
			}
			else
			{
				oSBP = 0;
			}

			oOPS = (oOBP + oSLG);

			// Perform pitching calculations
			if ((pFB + pGB + pLD + pPU) != 0)
			{
				pFBP = (pFB / (pFB + pGB + pLD + pPU));
				pGBP = (pGB / (pFB + pGB + pLD + pPU));
				pPUP = (pPU / (pFB + pGB + pLD + pPU));
				pLDP = (pLD / (pFB + pGB + pLD + pPU));
			}
			else
			{
				pFBP = 0;
				pGBP = 0;
				pPUP = 0;
				pLDP = 0;
			}

			if (pIP != 0)
			{
				pPIP = (pNP / pIP);
				pWHIP = ((pBB + pH) / pIP);
				pERA = ((9 * pER) / pIP);
			}
			else
			{
				pPIP = 0;
				pWHIP = 0;
				pERA = 0;
			}

			if (pPA != 0)
			{
				pKP = (pSO / pPA);
				if (((pGB - pFB - pPU) / pPA) > 0)
				{
					pSIERA = (6.145 - 16.986 * (pSO / pPA) + 11.434 * (pBB / pPA) - 1.858 * ((pGB - pFB - pPU) / pPA) + 7.653 * (Math.Pow ((pSO / pPA), 2)) - 6.664 * (Math.Pow (((pGB - pFB - pPU) / pPA), 2)) + 10.130 * (pSO / pPA) * ((pGB - pFB - pPU) / pPA) - 5.195 * (pBB / pPA) * ((pGB - pFB - pPU) / pPA));
				}
				else
				{
					pSIERA = (6.145 - 16.986 * (pSO / pPA) + 11.434 * (pBB / pPA) - 1.858 * ((pGB - pFB - pPU) / pPA) + 7.653 * (Math.Pow ((pSO / pPA), 2)) + 6.664 * (Math.Pow (((pGB - pFB - pPU) / pPA), 2)) + 10.130 * (pSO / pPA) * ((pGB - pFB - pPU) / pPA) - 5.195 * (pBB / pPA) * ((pGB - pFB - pPU) / pPA));
				}
			}
			else
			{
				pSIERA = 0;
				pKP = 0;
			}

			if (pPA != 0)
			{
				pBBP = (pBB / pPA);
			}
			else
			{
				pBBP = 0;
			}

			if (pGS != 0)
			{
				pIGS = (pIP / pGS);
			}
			else
			{
				pIGS = 0;
			}

			if (pIR != 0)
			{
				pIRAP = (pIRA / pIR);
			}
			else
			{
				pIRAP = 0;
			}

			if (pBB != 0)
			{
				pKBB = (pSO / pBB);
			}
			else
			{
				pKBB = 0;
			}

			if (pSVO != 0)
			{
				pSVP = (pSV / pSVO);
			}
			else
			{
				pSVP = 0;
			}

			// Calculate win percentage
			if (L != 0)
			{
				WPCT = (W / (W + L));
			}
			else
			{
				WPCT = 0;
			}

			// Insert calculated statistics into database
			DataAccess.InsertCalculatedStats (player, dCSP, dDER, dFPCT, dINN, dRF, dTC, oAB, oAVG, oBABIP, oBBP, oGOAO, oHRP, oISO, oOBP, oOPS, oPASO, oRC, oSBP, oSLG, oSOP, oTB, pBBP, pERA, pFBP, pGBP, pIGS, pIRAP, pKBB, pKP, pLDP, pPIP, pPUP, pSIERA, pSVP, pWHIP, WPCT);
		}
	}
}