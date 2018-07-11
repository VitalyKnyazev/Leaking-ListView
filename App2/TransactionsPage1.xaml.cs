using System;
using System.Linq;
using Xamarin.Forms;

namespace App2
{
	public partial class TransactionsPage1
	{
		private readonly TransactionsViewModel _viewModel = new TransactionsViewModel();

		public TransactionsPage1()
		{
			InitializeComponent();

			BindingContext = _viewModel;
		}

		private void Button_OnClicked(object sender, EventArgs e)
		{
			var item = _viewModel.Items.Skip(250).First();
			transactionsListView.ScrollTo(item, ScrollToPosition.MakeVisible, false);
		}

		private void TransactionsPage_OnDisappearing(object sender, EventArgs e)
		{
			BindingContext = null; // IMPORTANT!!! Prism.Forms does this under the hood
		}
	}
}