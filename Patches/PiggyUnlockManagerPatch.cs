using System.Collections.Generic;
using HarmonyLib;
using PiggyBankBuster.Progression;

namespace BillsAP;

[HarmonyPatch(typeof(PiggyUnlockManager), nameof(PiggyUnlockManager.GrantUnlock))]
public static class GrantUnlockPatch
{
    static bool Prefix(string internalId)
    {   
        Plugin.BepInLog.LogInfo($"Attempting to unlock {internalId}");
        return false;
    }
}

[HarmonyPatch(typeof(PiggyUnlockManager), nameof(PiggyUnlockManager.CommitFill), [typeof(float)])]
public static class CommitFillPatch
{
    static bool Prefix(float amount)
    {   
        Plugin.BepInLog.LogInfo($"CommitFill {amount}");
        return false;
    }
}

[HarmonyPatch(typeof(PiggyUnlockManager), nameof(PiggyUnlockManager.CommitFill), [typeof(float), typeof(int)])]
public static class CommitFillPatch1
{
    static bool Prefix(ref PiggyUnlockManager __instance, float amount, int billsPaid)
    {
        Plugin.BepInLog.LogInfo($"CommitFill {amount} {billsPaid}");
        PiggyBankBuster.Config.PiggyFillUnlockConfig.PiggyUnlockEntry[] entries = __instance.Config.unlockSequence;

        foreach (PiggyBankBuster.Config.PiggyFillUnlockConfig.PiggyUnlockEntry entry in entries)
        {
            Plugin.BepInLog.LogInfo($"piggyType: {entry.piggyType} -- fillRequired: {entry.fillRequired}");
        }

        return false;
    }
}

[HarmonyPatch(typeof(PiggyUnlockManager), nameof(PiggyUnlockManager.CommitRunFill))]
public static class CommitRunFillPatch
{
    static bool Prefix(long coinsEarned, Dictionary<string, int> killsByType)
    {   
        Plugin.BepInLog.LogInfo($"CommitRunFill {coinsEarned} {killsByType}");
        return false;
    }
}