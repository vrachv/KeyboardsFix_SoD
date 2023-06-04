using HarmonyLib;

namespace KeyboardsFix;

public class FixDatabaseKeyboard
{
    [HarmonyPatch(typeof(DatabaseApp), nameof(DatabaseApp.KeyboardButton))]
    [HarmonyPrefix]
    public static bool KeyboardButton(DatabaseApp __instance, string charStr)
    {
        if (Shared.IsDeskOrInventoryOpened)
        {
            return false;
        }
            
        if (Shared.IsControlOrAltPressed)
        {
            return false;
        }
            
        return Shared.IsLookingAtPC && Shared.TimeDelayed;
    }

    [HarmonyPatch(typeof(InterfaceController), nameof(InterfaceController.SetDesktopMode))]
    [HarmonyPrefix]
    public static bool SetDesktopMode(InterfaceController __instance, bool val, bool showPanels)
    {
        if (val == false)
        {
            Shared.RestartTime();
            return true;
        }
            
        if (Shared.IsControlOrAltPressed)
        {
            return true;
        }

        return !Shared.IsUsingAndLookingAtPC;
    }
        
    [HarmonyPatch(typeof(BioScreenController), nameof(BioScreenController.SetInventoryOpen))]
    [HarmonyPrefix]
    public static bool SetInventoryOpen(BioScreenController __instance, bool val, bool forceUpdate, bool resumeGame)
    {
        if (val == false)
        {
            Shared.RestartTime();
            return true;
        }
            
        if (Shared.IsControlOrAltPressed)
        {
            return true;
        }
            
        return !Shared.IsUsingAndLookingAtPC;
    }
        
    [HarmonyPatch(typeof(SessionData), nameof(SessionData.PauseGame))]
    [HarmonyPrefix]
    public static bool PauseGame(SessionData __instance, bool showPauseText, bool delayOverride, bool openDesktopMode)
    {
        if (Shared.IsControlOrAltPressed)
        {
            return true;
        }

        return !Shared.IsUsingAndLookingAtPC;
    }
}