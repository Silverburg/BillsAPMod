using HarmonyLib;
using Il2CppSystem;
using PiggyBankBuster.Progression;

namespace BillsAP;

[HarmonyPatch(typeof(PiggyLevelManager), nameof(PiggyLevelManager.AddXP))]
    public static class AddXPPatch
    {
        static bool Prefix(string internalId, int amount)
        {   
            Plugin.BepInLog.LogInfo($"Attempting to add {amount} xp to {internalId}");
            return true;
        }
    }

[HarmonyPatch(typeof(PiggyLevelManager), nameof(PiggyLevelManager.add_OnPiggyXPGained))]
    public static class add_OnPiggyXPGainedPatch
    {
        static bool Prefix(Action<PiggyBankBuster.Config.PiggyTypeConfig, int> value)
        {
            Plugin.BepInLog.LogInfo($"Attempting to add {value}");
            return false;    
        }
    }