using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public enum ExamplePermissions
{
	None             = 0,
	Move             = 0x00000001,
	DoRoutines       = 0x00000010,
	BeInteracted     = 0x00000100,
	Fight            = 0x00001000,
	GetInput         = 0x00010000,
	Talk             = 0x00100000,

	All              = 0b00111111
}

public static class FlagsExtensions
{
	public static bool ContainsFlag(this ExamplePermissions target, ExamplePermissions flag)
	{
		return (target & flag) == flag;
	}
	
	public static bool ContainsAllFlag(this ExamplePermissions target, params ExamplePermissions[] flags)
	{
		foreach (ExamplePermissions flag in flags)
			if ((target & flag) != flag)
				return false;
		return true;
	}
	
	public static void SetFlag(this ref ExamplePermissions target, ExamplePermissions flag)
	{
		target |= flag;
	}
	
	public static void ClearFlag(this ref ExamplePermissions target, ExamplePermissions flag)
	{
		target &= ~flag;
	}
	
	public static void SetAllFlags(this ref ExamplePermissions target, params ExamplePermissions[] flags)
	{
		foreach (ExamplePermissions flag in flags)
			target |= flag;
	}
	
	public static void ClearAllFlags(this ref ExamplePermissions target, params ExamplePermissions[] flags)
	{
		foreach (ExamplePermissions flag in flags)
			target &= ~flag;
	}

#region Unity test
#if UNITY_EDITOR
	[MenuItem("[TEST]/Flag set test")]
	public static void Test()
	{
		ExamplePermissions perm = ExamplePermissions.None;
		
		Debug.Log(perm);
		
		perm.SetFlag(ExamplePermissions.GetInput);
		
		Debug.Log(perm);
		
		perm.SetAllFlags(
			ExamplePermissions.Move,
			ExamplePermissions.Fight,
			ExamplePermissions.Talk);
		
		Debug.Log(perm);
		
		Debug.Log(perm.Contains(ExamplePermissions.Fight));
		Debug.Log(perm.Contains(ExamplePermissions.GetInput));
		Debug.Log(perm.Contains(ExamplePermissions.BeInteracted));
	}
#endif
#endregion
}
