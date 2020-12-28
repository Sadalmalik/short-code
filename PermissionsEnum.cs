using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public enum Permissions
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

public static class PermissionsExtensions
{
#region Methods

	public static bool IsAllowed(this Permissions target, params Permissions[] permissions)
	{
		foreach (var perm in permissions)
			if ((target & perm) != perm)
				return false;
		return true;
	}

	public static void Grant(this ref Permissions target, Permissions permissions)
	{
		target |= permissions;
	}

	public static void Revoke(this ref Permissions target, Permissions permissions)
	{
		target &= ~permissions;
	}

	public static void GrantAll(this ref Permissions target, params Permissions[] permissions)
	{
		foreach (var perm in permissions)
			target |= perm;
	}

	public static void RevokeAll(this ref Permissions target, params Permissions[] permissions)
	{
		foreach (var perm in permissions)
			target &= ~perm;
	}

#endregion

#region Unity test
#if UNITY_EDITOR
	[MenuItem("TEST/permission")]
	public static void Test()
	{
		Permissions perm = Permissions.None;
		
		Debug.Log(perm);
		
		perm.Grant(Permissions.GetInput);
		
		Debug.Log(perm);
		
		perm.GrantAll(
			Permissions.Move,
			Permissions.Fight,
			Permissions.Talk);
		
		Debug.Log(perm);
		
		Debug.Log(perm.IsAllowed(Permissions.Fight));
		Debug.Log(perm.IsAllowed(Permissions.GetInput));
		Debug.Log(perm.IsAllowed(Permissions.BeInteracted));
	}
#endif
#endregion
}
