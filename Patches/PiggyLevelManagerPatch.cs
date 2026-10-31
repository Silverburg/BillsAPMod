using HarmonyLib;
using PiggyBankBuster.Progression;

namespace BillsAP;

[HarmonyPatch(typeof(PiggyLevelManager), nameof(PiggyLevelManager.AddXP))]
    public static class AddXPPatch
    {
        static bool Prefix(string internalId, int amount)
        {   
            //Plugin.BepInLog.LogInfo($"Attempting to add {amount} xp to {internalId}");
            return true;
        }
    }