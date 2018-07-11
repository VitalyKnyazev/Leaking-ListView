using System;
using Xamarin.Forms;

namespace App2
{
	public partial class MainPage : NavigationPage
	{
		public MainPage()
		{
			InitializeComponent();

			PushAsync(new AccountsPage());
		}
	}
}