using Xamarin.Forms;

namespace App2
{
	public sealed class FastListView : ListView
	{
		public FastListView() : base(ListViewCachingStrategy.RecycleElement)
		{
		}
	}
}