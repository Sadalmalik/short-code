using System.Collections;
using System.Collections.Generic;

public class CoroutineContainer
{
	private readonly Stack<IEnumerator> _coroutine;
	public bool Expire => _coroutine.Count == 0;

	public IEnumerator Root { get; }

	public CoroutineContainer(IEnumerator coroutine)
	{
		Root       = coroutine;
		_coroutine = new Stack<IEnumerator>();
		_coroutine.Push(coroutine);
	}

	public void Tick()
	{
		var top = _coroutine.Peek();
		if (top.MoveNext())
		{
			var value = top.Current;
			if (value is IEnumerator inner)
			{
				_coroutine.Push(inner);
			}
		}
		else
		{
			_coroutine.Pop();
		}
	}
}

public class CoroutinesManager
{
	private readonly List<IEnumerator> _expired;
	private readonly Dictionary<IEnumerator, CoroutineContainer> _coroutines;

	public CoroutinesManager()
	{
		_expired    = new List<IEnumerator>();
		_coroutines = new Dictionary<IEnumerator, CoroutineContainer>();
	}

	public void Start(IEnumerator coroutine)
	{
		lock (_coroutines)
		{
			_coroutines.Add(coroutine, new CoroutineContainer(coroutine));
		}
	}

	public void Interupt(IEnumerator coroutine)
	{
		lock (_coroutines)
		{
			_coroutines.Remove(coroutine);
		}
	}
	
	public void InteruptAll()
	{
		lock (_coroutines)
		{
			_coroutines.Clear();
		}
	}

	public void Tick()
	{
		lock (_coroutines)
		{
			foreach (var cor in _coroutines.Values)
			{
				cor.Tick();
				if (cor.Expire)
					_expired.Add(cor.Root);
			}

			foreach (var exp in _expired)
			{
				_coroutines.Remove(exp);
			}
		}

		_expired.Clear();
	}
}