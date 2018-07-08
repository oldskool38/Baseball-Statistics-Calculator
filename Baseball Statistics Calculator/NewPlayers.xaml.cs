using System;
using Data_Access_Library;

using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BaseballStatisticsCalculator
{
	/// <summary>
	/// The new players page
	/// </summary>
	public sealed partial class NewPlayers : Page
	{
		private TextBox [] txtName = new TextBox [33];

		/// <summary>
		/// Constructor
		/// </summary>
		public NewPlayers ()
		{
			InitializeComponent ();

			// Variable for stepping through the text box array
			int count = 0;

			// Initialize the text boxes
			for (int index = 0; index < 33; index++)
			{
				txtName [index] = new TextBox ();
			}

			// Determine if the user's device is in landscape mode
			if (grdMain.ActualWidth > 640)
			{
				// Increments the left margin
				for (int x = 10; x <= 320; x = x + 155)
				{
					// Increments the top margin
					for (int y = 10; y <= 380; y = y + 37)
					{
						// Set the properties of the text box and add it to the page
						Thickness margin = new Thickness (x, y, 0, 0);
						txtName [count].Text = "";
						txtName [count].HorizontalAlignment = HorizontalAlignment.Left;
						txtName [count].VerticalAlignment = VerticalAlignment.Top;
						txtName [count].Margin = margin;
						txtName [count].PlaceholderText = "Name";
						txtName [count].IsSpellCheckEnabled = true;
						txtName [count].MaxLength = 50;
						txtName [count].Width = 150;
						grdMain.Children.Add (txtName [count++]);
					}
				}
			}
			else
			{
				// Increments the top margin
				for (int y = 10; y <= 713; y = y + 37)
				{
					// Set the properties of the text box and add it to the page
					Thickness margin = new Thickness (10, y, 0, 0);
					txtName [count].Text = "";
					txtName [count].HorizontalAlignment = HorizontalAlignment.Left;
					txtName [count].VerticalAlignment = VerticalAlignment.Top;
					txtName [count].Margin = margin;
					txtName [count].PlaceholderText = "Name";
					txtName [count].IsSpellCheckEnabled = true;
					txtName [count].MaxLength = 50;
					txtName [count].Width = 150;
					grdMain.Children.Add (txtName [count++]);
				}
			}
		}

		/// <summary>
		/// Method for handling the add players button click event
		/// </summary>
		/// <param name="sender">
		/// Object that called the method
		/// </param>
		/// <param name="e">
		/// Event Arguments
		/// </param>
		private void CmdAdd_Click (object sender, RoutedEventArgs e)
		{
			if (txtName [0].Text.Length != 0)
			{
				// Create an array to hold the new player names
				string [] players = new string [33];

				// Variable to step through the string array
				int index = 0;
				for (int count = 0; count < 33; count++)
				{
					players [count] = null;
					if (txtName [count].Text.Length != 0)
					{
						players [index++] = txtName [count].Text;
					}
				}

				// Add players to database
				DataAccess.AddPlayers (players);

				// Clear the text boxes
				for (int count = 0; count < 33; count++)
				{
					txtName [count].Text = null;
				}
			}
			else
			{
				MessageDialog blankField = new MessageDialog ("You must put the first name in the top left text box.", "Error");
				blankField.Commands.Add (new UICommand ("OK"));
			}
		}

		/// <summary>
		/// Method that rearranges the text boxes on the page if the user changes the orientation of their device
		/// </summary>
		/// <param name="sender">
		/// The object that called the method
		/// </param>
		/// <param name="e">
		/// The event arguments
		/// </param>
		private void GrdMain_SizeChanged (object sender, SizeChangedEventArgs e)
		{
			// Variable for stepping through the text box array
			int count = 0;

			// Determine if the user's device is in landscape mode
			if (grdMain.ActualWidth > 640)
			{
				// Increments the left margin
				for (int x = 10; x <= 320; x = x + 155)
				{
					// Increments the top margin
					for (int y = 10; y <= 380; y = y + 37)
					{
						// Set the properties of the text box and add it to the page
						Thickness margin = new Thickness (x, y, 0, 0);
						txtName [count].HorizontalAlignment = HorizontalAlignment.Left;
						txtName [count].VerticalAlignment = VerticalAlignment.Top;
						txtName [count].Margin = margin;
						txtName [count].PlaceholderText = "Name";
						txtName [count].IsSpellCheckEnabled = true;
						txtName [count].MaxLength = 50;
						txtName [count].Width = 150;

						// Determine if the text box is already on the page and if not, add it to the page
						if (grdMain.Children.Contains (txtName [count]))
						{
							count++;
						}
						else
						{
							grdMain.Children.Add (txtName [count++]);
						}
					}
				}
			}
			else
			{
				// Clear the text field and remove text boxes 21-33 since they won't fit in this orientation
				for (int index = 20; index < 33; index++)
				{
					txtName [index].Text = "";
					grdMain.Children.Remove (txtName [index]);
				}

				// Increments the top margin
				for (int y = 10; y <= 713; y = y + 37)
				{
					// Set the properties of the text box and add it to the page
					Thickness margin = new Thickness (10, y, 0, 0);
					txtName [count].HorizontalAlignment = HorizontalAlignment.Left;
					txtName [count].VerticalAlignment = VerticalAlignment.Top;
					txtName [count].Margin = margin;
					txtName [count].PlaceholderText = "Name";
					txtName [count].IsSpellCheckEnabled = true;
					txtName [count].MaxLength = 50;
					txtName [count].Width = 150;

					// Determine if the text box is already on the page and if not, add it to the page
					if (grdMain.Children.Contains (txtName [count]))
					{
						count++;
					}
					else
					{
						grdMain.Children.Add (txtName [count++]);
					}
				}
			}
		}
	}
}