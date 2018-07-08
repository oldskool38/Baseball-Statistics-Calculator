using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BaseballStatisticsCalculator
{
	/// <summary>
	/// The main page of the application that holds the navigation menu and the frame for each page
	/// </summary>
	public sealed partial class MainPage : Page
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public MainPage ()
		{
			InitializeComponent ();

			// Determine screen size
			if (grdMain.ActualWidth > 640)
			{
				svMain.DisplayMode = SplitViewDisplayMode.CompactInline;
				svMain.IsPaneOpen = true;
				svMain.OpenPaneLength = (grdMain.ActualWidth / 6);
			}
			else
			{
				svMain.DisplayMode = SplitViewDisplayMode.CompactOverlay;
				svMain.IsPaneOpen = false;
				svMain.OpenPaneLength = grdMain.ActualWidth;
				svMain.CompactPaneLength = 100;
			}

			// Display the new players page
			frmContent.Navigate (typeof (NewPlayers));
		}

		/// <summary>
		/// Menu toggle button click handler
		/// </summary>
		/// <param name="sender">
		/// Object that called the handler
		/// </param>
		/// <param name="e">
		/// Event arguments
		/// </param>
		private void CmdToggle_Click (object sender, RoutedEventArgs e)
		{
			if (svMain.IsPaneOpen)
			{
				svMain.IsPaneOpen = false;
			}
			else
			{
				svMain.IsPaneOpen = true;
			}
		}

		/// <summary>
		/// Screen size changed handler
		/// </summary>
		/// <param name="sender">
		/// Object that called the handler
		/// </param>
		/// <param name="e">
		/// Event arguments
		/// </param>
		private void GrdMain_SizeChanged (object sender, SizeChangedEventArgs e)
		{
			if (grdMain.ActualWidth > 640)
			{
				svMain.DisplayMode = SplitViewDisplayMode.CompactInline;
				svMain.IsPaneOpen = true;
				svMain.OpenPaneLength = (grdMain.ActualWidth / 6);
			}
			else
			{
				svMain.DisplayMode = SplitViewDisplayMode.CompactOverlay;
				svMain.IsPaneOpen = false;
				svMain.OpenPaneLength = grdMain.ActualWidth;
			}
		}

		/// <summary>
		/// Navigation handler
		/// </summary>
		/// <param name="sender">
		/// Object that called the handler
		/// </param>
		/// <param name="e">
		/// Event arguments
		/// </param>
		private void LstNav_SelectionChanged (object sender, SelectionChangedEventArgs e)
		{
			switch (lstNav.SelectedIndex)
			{
				case 0:
					frmContent.Navigate (typeof (NewPlayers));
					break;

				case 1:
					frmContent.Navigate (typeof (DeletePlayers));
					break;

				case 2:
					frmContent.Navigate (typeof (Defense));
					break;

				case 3:
					frmContent.Navigate (typeof (Offense));
					break;

				case 4:

					frmContent.Navigate (typeof (Pitching));
					break;
			}
		}
	}
}