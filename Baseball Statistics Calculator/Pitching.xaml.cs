using System;

using Data_Access_Library;

using Microsoft.Data.Sqlite;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BaseballStatisticsCalculator
{
	/// <summary>
	/// This page will be used to get, display, and update the stored pitching stats
	/// </summary>
	public sealed partial class Pitching : Page
	{
		private TextBox [] txtStats = new TextBox [33];

		/// <summary>
		/// Constructor
		/// </summary>
		public Pitching ()
		{
			InitializeComponent ();
			double txtWidth = 0;
			string [] stat = { "Walks", "Walk Percentage", "Earned Runs", "Earned Run Average", "Fly Ball", "Fly Ball Rate", "Ground Ball", "Ground Ball Percentage", "Games Started", "Hits", "Innings Per Start", "Innings Pitched", "Inherited Runners", "Inherited Runners Allowed", "Inherited Runners Allowed Percentage", "Strikeout to Walk Ratio", "Strikeout Rate", "Line Drives", "Line Drive Rate", "Pitch Count", "Plate Appearances", "Pitches per Inning Pitched", "Pop-ups", "Pop-up Rate", "Skill Interactive ERA", "Strikeouts", "Saves", "Save Opportunities", "Save Percentage", "Walks and Hits per Inning Pitched", "Wins", "Losses", "Win Percentage" };

			// Get list of players
			SqliteDataReader players = DataAccess.GetPlayers ();

			// Integer to count the number of players in the database
			int index = 0;
			foreach (SqliteDataReader p in players)
			{
				index++;
			}

			// List box items to hold the stored players
			ListBoxItem [] lstPlayer = new ListBoxItem [index];

			// Reset variable to use it to step through the list box item array
			index = 0;

			// Add each player name and ID number in the SQLite dataset to the current list box item then add it to the list box
			foreach (SqliteDataReader p in players)
			{
				lstPlayer [index] = new ListBoxItem
				{
					Content = p.GetString (1),
					Tag = p.GetInt32 (0)
				};
				lstPlayers.Items.Add (lstPlayer [index++]);
			}

			// Initialize and format text boxes
			for (int count = 0; count < 33; count++)
			{
				txtStats [count] = new TextBox
				{
					HorizontalAlignment = HorizontalAlignment.Left,
					VerticalAlignment = VerticalAlignment.Top,
					PlaceholderText = stat [count]
				};

				// Find the maximum width of text boxes
				if (txtStats [count].Width > txtWidth)
				{
					txtWidth = txtStats [count].Width;
				}
			}

			// Set calculated text boxes to read-only
			txtStats [1].IsReadOnly = true;
			txtStats [3].IsReadOnly = true;
			txtStats [5].IsReadOnly = true;
			txtStats [7].IsReadOnly = true;
			txtStats [10].IsReadOnly = true;
			txtStats [14].IsReadOnly = true;
			txtStats [15].IsReadOnly = true;
			txtStats [16].IsReadOnly = true;
			txtStats [18].IsReadOnly = true;
			txtStats [21].IsReadOnly = true;
			txtStats [23].IsReadOnly = true;
			txtStats [24].IsReadOnly = true;
			txtStats [28].IsReadOnly = true;
			txtStats [29].IsReadOnly = true;
			txtStats [32].IsReadOnly = true;

			// Set the width of the text boxes
			for (int count = 0; count < 33; count++)
			{
				txtStats [count].Width = txtWidth;
			}

			// Set the margin increment for the x axis
			int xInc = ((int) Math.Round (txtWidth + 5));

			// Set initial margin
			int x = 5;
			int y = 5;

			// Position text boxes
			for (int count = 0; count < 33; count++)
			{
				Thickness margin = new Thickness (x, y, 0, 0);
				txtStats [count].Margin = margin;

				// Increment margin
				x = (x + xInc);
				y = (y + 37);
			}

			// Create a list box item to hold the currently selected player
			ListBoxItem player = new ListBoxItem ();

			// Assign the selected player to the list box item
			player = (ListBoxItem) lstPlayers.SelectedItem;

			// Call get stats and pass the player's database ID
			GetStats ((int) player.Tag);

			// Add text boxes to the grid
			for (int count = 0; count < 33; count++)
			{
				grdStats.Children.Add (txtStats [count]);
			}
		}

		/// <summary>
		/// This method handles the click event of the update button
		/// </summary>
		/// <param name="sender">
		/// Object that called the method
		/// </param>
		/// <param name="e">
		/// Event arguments
		/// </param>
		private void CmdUpdate_Click (object sender, RoutedEventArgs e)
		{
			// Get the selected player's ID number
			ListBoxItem lstPlayer = (ListBoxItem) lstPlayers.SelectedItem;

			// Create variables to hold input statistics
			int pBB, pER, pFB, pGB, pGS, pH, pIP, pIR, pIRA, pLD, pNP, pPA, pPU, pSO, pSV, pSVO, W, L;

			// Assign input statistics to corresponding variable
			pBB = int.Parse (txtStats [0].Text);
			pER = int.Parse (txtStats [2].Text);
			pFB = int.Parse (txtStats [4].Text);
			pGB = int.Parse (txtStats [6].Text);
			pGS = int.Parse (txtStats [8].Text);
			pH = int.Parse (txtStats [9].Text);
			pIP = int.Parse (txtStats [11].Text);
			pIR = int.Parse (txtStats [12].Text);
			pIRA = int.Parse (txtStats [13].Text);
			pLD = int.Parse (txtStats [17].Text);
			pNP = int.Parse (txtStats [19].Text);
			pPA = int.Parse (txtStats [20].Text);
			pPU = int.Parse (txtStats [22].Text);
			pSO = int.Parse (txtStats [25].Text);
			pSV = int.Parse (txtStats [26].Text);
			pSVO = int.Parse (txtStats [27].Text);
			W = int.Parse (txtStats [30].Text);
			L = int.Parse (txtStats [31].Text);

			// Insert pitching statistics into database
			DataAccess.InsertPitchingStats ((int) lstPlayer.Tag, pBB, pER, pFB, pGB, pGS, pH, pIP, pIR, pIRA, pLD, pNP, pPA, pPU, pSO, pSV, pSVO, W, L);

			// Call the CalculateStatistics constructor to perform the calculations
			CalculateStats.CalculateStatistics ((int) lstPlayer.Tag);

			// Refresh statistics
			GetStats ((int) lstPlayer.Tag);
		}

		/// <summary>
		/// This method gets the statistics stored in the database and assigns them to the associated text box
		/// </summary>
		/// <param name="player">
		/// The selected player's database ID
		/// </param>
		private void GetStats (int player)
		{
			// Get the pitching statistics from the database
			SqliteDataReader stats = DataAccess.GetPitchingStats (player);

			// Assign and format the text in the text boxes
			txtStats [0].Text = string.Format ("{0:###0}", stats.GetString (0));
			txtStats [1].Text = string.Format ("{0:##0.0%}", stats.GetString (1));
			txtStats [2].Text = string.Format ("{0:###0}", stats.GetString (2));
			txtStats [3].Text = string.Format ("{0:###.00}", stats.GetString (3));
			txtStats [4].Text = string.Format ("{0:###0}", stats.GetString (4));
			txtStats [5].Text = string.Format ("{0:##0.0%}", stats.GetString (5));
			txtStats [6].Text = string.Format ("{0:###0}", stats.GetString (6));
			txtStats [7].Text = string.Format ("{0:##0.0%}", stats.GetString (7));
			txtStats [8].Text = string.Format ("{0:###0}", stats.GetString (8));
			txtStats [9].Text = string.Format ("{0:###0}", stats.GetString (9));
			txtStats [10].Text = string.Format ("{0:###.0}", stats.GetString (10));
			txtStats [11].Text = string.Format ("{0:###0}", stats.GetString (11));
			txtStats [12].Text = string.Format ("{0:###0}", stats.GetString (12));
			txtStats [13].Text = string.Format ("{0:###0}", stats.GetString (13));
			txtStats [14].Text = string.Format ("{0:##0.0%}", stats.GetString (14));
			txtStats [15].Text = string.Format ("{0:###.00}", stats.GetString (15));
			txtStats [16].Text = string.Format ("{0:##0.0%}", stats.GetString (16));
			txtStats [17].Text = string.Format ("{0:###0}", stats.GetString (17));
			txtStats [18].Text = string.Format ("{0:##0.0%}", stats.GetString (18));
			txtStats [19].Text = string.Format ("{0:###0}", stats.GetString (19));
			txtStats [20].Text = string.Format ("{0:###0}", stats.GetString (20));
			txtStats [21].Text = string.Format ("{0:###.0}", stats.GetString (21));
			txtStats [22].Text = string.Format ("{0:###0}", stats.GetString (22));
			txtStats [23].Text = string.Format ("{0:##0.0%}", stats.GetString (23));
			txtStats [24].Text = string.Format ("{0:###.00}", stats.GetString (24));
			txtStats [25].Text = string.Format ("{0:###0}", stats.GetString (25));
			txtStats [26].Text = string.Format ("{0:###0}", stats.GetString (26));
			txtStats [27].Text = string.Format ("{0:###0}", stats.GetString (27));
			txtStats [28].Text = string.Format ("{0:##0.0%}", stats.GetString (28));
			txtStats [29].Text = string.Format ("{0:###.000}", stats.GetString (29));
			txtStats [30].Text = string.Format ("{0:###0}", stats.GetString (30));
			txtStats [31].Text = string.Format ("{0:###0}", stats.GetString (31));
			txtStats [32].Text = string.Format ("{0:##0%}", stats.GetString (32));
		}

		/// <summary>
		/// This method gets the statistics for the new player when the selection is changed in the list box
		/// </summary>
		/// <param name="sender">
		/// Object that called this method
		/// </param>
		/// <param name="e">
		/// Event Arguments
		/// </param>
		private void LstPlayers_SelectionChanged (object sender, SelectionChangedEventArgs e)
		{
			// Create a list box item to hold the currently selected player
			ListBoxItem player = new ListBoxItem ();

			// Assign the selected player to the list box item
			player = (ListBoxItem) lstPlayers.SelectedItem;

			// Call get stats and pass the player's database ID
			GetStats ((int) player.Tag);
		}
	}
}