using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;

namespace App2
{
    public class TransactionsViewModel
    {
	    public TransactionsViewModel()
	    {
		    var items = Enumerable.Range(1, 500).Select(i => new Item { Name = i.ToString() });

		    Items = new ObservableCollection<Item>(items);

		    RepeatCommand = new AsyncDelegateCommand<object>(Repeat, x => true);
		}

	    public ObservableCollection<Item> Items { get; }

	    public AsyncDelegateCommand<object> RepeatCommand { get; }

	    private Task Repeat(object item)
	    {
			return Task.CompletedTask;
	    }
	}

	public class Item
	{
		public string Name { get; set; }
	}

	public sealed class AsyncDelegateCommand<T> : ICommand
	{
		public event EventHandler CanExecuteChanged;

		private readonly Func<T, Task> _executeMethod;
		private readonly Func<T, bool> _canExecuteMethod;
		private bool _isInFlight;

#if XAMARIN
		private DateTime _lastExecuted = DateTime.MinValue;
#endif

		public AsyncDelegateCommand(Func<T, Task> executeMethod)
			: this(executeMethod, _ => true)
		{
		}

		public AsyncDelegateCommand(Func<T, Task> executeMethod, Func<T, bool> canExecuteMethod)
		{
			var genericTypeInfo = typeof(T).GetTypeInfo();

			// DelegateCommand allows object or Nullable<>.  
			// note: Nullable<> is a struct so we cannot use a class constraint.
			if (genericTypeInfo.IsValueType)
			{
				if (!genericTypeInfo.IsGenericType || !typeof(Nullable<>).GetTypeInfo().IsAssignableFrom(genericTypeInfo.GetGenericTypeDefinition().GetTypeInfo()))
					throw new InvalidCastException("T for DelegateCommand<T> is not an object nor Nullable.");
			}

			_executeMethod = executeMethod;
			_canExecuteMethod = canExecuteMethod;
		}

		internal async Task Execute(T parameter)
		{
			if (_isInFlight)
				return;

#if XAMARIN
			if (DateTime.UtcNow.Subtract(_lastExecuted).TotalMilliseconds < 200d)
				return;
#endif

			try
			{
				_isInFlight = true;

				await _executeMethod(parameter);
			}
			finally
			{
				_isInFlight = false;

#if XAMARIN
				_lastExecuted = DateTime.UtcNow;
#endif
			}
		}

		internal bool CanExecute(T parameter)
		{
			return _canExecuteMethod(parameter);
		}

		public void Execute(object parameter)
		{
			//Execute((T)parameter).NotWait();
		}

		public bool CanExecute(object parameter)
		{
			return CanExecute((T)parameter);
		}
	}
}