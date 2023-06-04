using HarmonyLib;

namespace KeyboardsFix;

public class FixPasswordKeyboard
{
    [HarmonyPatch(typeof(ComputerLogin), nameof(ComputerLogin.PressNumberButton))]
    [HarmonyPrefix]
    public static bool PressNumberButton(ComputerLogin __instance, int newInt)
    {
        if (Shared.IsDeskOrInventoryOpened)
        {
            return false;
        }
            
        if (Shared.IsControlOrAltPressed)
        {
            return false;
        }

        return Shared.IsUsingAndLookingAtPC && Shared.TimeDelayed;
    }

    [HarmonyPatch(typeof(FirstPersonItemController), nameof(FirstPersonItemController.SetFirstPersonItem))]
    [HarmonyPrefix]
    public static bool SetFirstPersonItem(FirstPersonItemController __instance, FirstPersonItem newItem, bool forceSwitch, ref bool __state)
    {
        var itemSelected = __instance.currentItem != null && __instance.currentItem.name != "nothing";
            
        if (itemSelected)
        {
            Shared.RestartTime();
        }
            
        if (Shared.IsControlOrAltPressed || itemSelected)
        {
            return true;
        }
            
        var usingAndLookAtPC = Shared.IsUsingAndLookingAtPC;

        __state = usingAndLookAtPC && __instance.selectedWhenForceHolstered != null;
            
        return !usingAndLookAtPC;
    }
        
    [HarmonyPatch(typeof(FirstPersonItemController), nameof(FirstPersonItemController.SetFirstPersonItem))]
    [HarmonyPostfix]
    public static void SetFirstPersonItemPostfix(FirstPersonItemController __instance, FirstPersonItem newItem, bool forceSwitch, bool __state)
    {
        if (__state)
        {
            __instance.ForceHolster();
        }
    }
}