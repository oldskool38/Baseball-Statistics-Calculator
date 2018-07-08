using System;

using Data_Access_Library;

using Microsoft.Data.Sqlite;

using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BaseballStatisticsCalculator
{
	/// <summary>
	/// This page will show the user all the players saved in the database and allow them to either delete specific players or delete all players from
	/// the database
	/// </summary>
	public sealed partial class DeletePlayers : Page
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public DeletePlayers ()
		{
			InitializeComponent ();

			// Get list of players
			SqliteDataReader players = DataAccess.GetPlayers ();

			// Integer to count the number of players in the database
			int count = 0;
			foreach (SqliteDataReader player in players)
			{
				count++;
			}

			// List box items to hold the stored players
			ListBoxItem [] lstPlayer = new ListBoxItem [count];

			// Reset variable to use it to step through the list box item array
			count = 0;

			// Add each player name and ID number in the SQLite dataset to the current list box item then add it to the list box
			foreach (SqliteDataReader player in players)
			{
				lstPlayer [count] = new ListBoxItem
				{
					Content = player.GetString (1),
					Tag = player.GetInt32 (0)
				};
				lstPlayers.Items.Add (lstPlayer [count++]);
			}
		}

		/// <summary>
		/// Delete button click handler
		/// </summary>
		/// <param name="sender">
		/// Object that called the handler
		/// </param>
		/// <param name="e">
		/// Event arguments
		/// </param>
		private async void CmdDelete_Click (object sender, RoutedEventArgs e)
		{
			// String to hold the selected player's name
			string player = lstPlayers.SelectedValue.ToString ();

			// Create a pop up to confirm the delete action
			MessageDialog confirmDelete = new MessageDialog ("Are you sure you want to delete " + player + "?", "Confirm Delete");
			confirmDelete.Commands.Add (new UICommand ("Delete", new UICommandInvokedHandler (DeletePlayer)));
			confirmDelete.Commands.Add (new UICommand ("Cancel"));
			confirmDelete.DefaultCommandIndex = 1;
			confirmDelete.CancelCommandIndex = 1;

			// Show the pop up window
			await confirmDelete.ShowAsync ();
		}

		/// <summary>
		/// Delete all button click handler
		/// </summary>
		/// <param name="sender">
		/// Object that called the handler
		/// </param>
		/// <param name="e">
		/// Event arguments
		/// </param>
		private async void CmdDeleteAll_Click (object sender, RoutedEventArgs e)
		{
			// Create a pop up to confirm the delete action
			MessageDialog confirmDelete = new MessageDialog ("Are you sure you want to delete ALL players?", "Confirm Delete All");
			confirmDelete.Commands.Add (new UICommand ("Delete All", new UICommandInvokedHandler (DeletePlayer)));
			confirmDelete.Commands.Add (new UICommand ("Cancel"));
			confirmDelete.DefaultCommandIndex = 1;
			confirmDelete.CancelCommandIndex = 1;

			// Show the pop up window
			await confirmDelete.ShowAsync ();
		}

		/// <summary>
		/// Handler method to remove one or all rows in the database table
		/// </summary>
		/// <param name="command">
		/// The command object that called the handler
		/// </param>
		private void DeletePlayer (IUICommand command)
		{
			// Determine if deleting one player or all players
			if (command.Label == "Delete" && command.Label != "Delete All")
			{
				// Get the selected player's ID number
				ListBoxItem player = (ListBoxItem) lstPlayers.SelectedItem;

				// Delete the selected player
				DataAccess.DeletePlayer ((int) player.Tag);
			}
			else
			{
				// Delete all players
				DataAccess.DeleteAll ();
			}
		}
	}
}