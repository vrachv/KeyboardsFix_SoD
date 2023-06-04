using BepInEx.Unity.IL2CPP.UnityEngine;
using UnityEngine;
using KeyCode = BepInEx.Unity.IL2CPP.UnityEngine.KeyCode;

namespace KeyboardsFix;

internal static class Shared
{
    internal static float ElapsedTime = Time.time;
    internal static bool TimeDelayed => Time.time - ElapsedTime > 0.5f;
    internal static void RestartTime() => ElapsedTime = Time.time;
    
    internal static bool IsLookingAtPC => InteractionController._instance.currentLookAtTransform.name == "Screen";
    internal static bool IsUsingAndLookingAtPC => Player._instance.computerInteractable != null && IsLookingAtPC;
    internal static bool IsControlOrAltPressed => Input.GetKeyInt(KeyCode.LeftControl) || Input.GetKeyInt(KeyCode.LeftAlt);
    internal static bool IsDeskOrInventoryOpened => InterfaceController._instance.desktopMode || BioScreenController._instance.isOpen;
}