using System;
using ClassLibrary1;

namespace App2
{
	public partial class AccountsPage
	{
		public AccountsPage()
		{
			InitializeComponent();
		}

		private void TransactionsLeaked_OnClicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new TransactionsPage1());
		}

		private void TransactionsNormal_OnClicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new TransactionsPage2());
		}

		private void Button_OnClicked1(object sender, EventArgs e)
		{
			GC.GetTotalMemory(true);
			GC.GetTotalMemory(true);
			GC.GetTotalMemory(true);
		}

		private void AccountsPage_OnAppearing(object sender, EventArgs e)
		{
			leakedMenuItemSubscriptionCountLabel.Text = "Leaked MenuItem.PropertyChanged subscription count: " + Leaks.MenuItemPropertyChangedSubscriptionCount;
			leakedMenuItemCountLabel.Text = "Leaked MenuItem count: " + Leaks.MenuItemCount;
		}
	}
}