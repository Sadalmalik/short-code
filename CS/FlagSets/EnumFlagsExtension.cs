using System;

public static class EnumFlagsExtension
{
	public static bool HasFlag<T>(this T target, T flag) where T : Enum
	{
		long flags = Convert.ToInt64(target);
		long value = Convert.ToInt64(flag);
		return (flags & value) == value;
	}

	public static void SetFlag<T>(this ref T target, T permissions) where T : struct, Enum
	{
		long flags = Convert.ToInt64(target);
		flags  |= Convert.ToInt64(permissions);
		target =  (T) Enum.ToObject(typeof(T), flags);
	}

	public static void ClearFlag<T>(this ref T target, T permissions) where T : struct, Enum
	{
		long flags = Convert.ToInt64(target);
		flags  &= ~Convert.ToInt64(permissions);
		target =  (T) Enum.ToObject(typeof(T), flags);
	}

	public static void SetFlags<T>(this ref T target, params T[] permissions) where T : struct, Enum
	{
		long flags = Convert.ToInt64(target);
		foreach (var perm in permissions)
			flags |= Convert.ToInt64(perm);
		target = (T) Enum.ToObject(typeof(T), flags);
	}

	public static void ClearFlags<T>(this ref T target, params T[] permissions) where T : struct, Enum
	{
		long flags = Convert.ToInt64(target);
		foreach (var perm in permissions)
			flags &= ~Convert.ToInt64(perm);
		target = (T) Enum.ToObject(typeof(T), flags);
	}
}
