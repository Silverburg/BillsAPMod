using HarmonyLib;
using PiggyBankBuster.Progression;

namespace BillsAP;

[HarmonyPatch(typeof(PiggyUnlockManager), nameof(PiggyUnlockManager.GrantUnlock))]
    public static class GrantUnlockPatch
    {
    
        static bool Prefix(string internalId)
        {   
            Plugin.Instance.Log.LogInfo($"Attempting to unlock {internalId}");
            return false;
        }
    }