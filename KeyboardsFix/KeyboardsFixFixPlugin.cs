using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;

namespace KeyboardsFix;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class DatabaseKeyboardFixPlugin : BasePlugin
{
    public override void Load()
    {
        Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        
        var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
        harmony.PatchAll(typeof(FixDatabaseKeyboard));
        harmony.PatchAll(typeof(FixPasswordKeyboard));
    }
}