using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using HarmonyLib;
using PiggyBankBuster.Progression;
using UnityEngine.Pool;

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
    static bool Prefix(ref PiggyUnlockManager __instance, ref float amount, int billsPaid)
    {
        Plugin.BepInLog.LogInfo($"CommitFill {amount} {billsPaid} {__instance.UnlockedPiggyCount}");
        // PiggyBankBuster.Config.PiggyFillUnlockConfig.PiggyUnlockEntry[] entries = __instance.Config.unlockSequence;

        // foreach (PiggyBankBuster.Config.PiggyFillUnlockConfig.PiggyUnlockEntry entry in entries)
        // {
        //     Plugin.BepInLog.LogInfo($"piggyType: {entry.piggyType.typeName} -- fillRequired: {entry.fillRequired}");
        // }

        if (Plugin.ArchipelagoClient.getRecievedItemAmount() == __instance.UnlockedPiggyCount)
        {
            Plugin.BepInLog.LogInfo(__instance.FillRequired);
            float amountLeft = __instance.FillRequired - __instance.CurrentFill;
            if(amount > amountLeft)
            {
                amount = amountLeft;
            }
            return true;
        }
        else if(Plugin.ArchipelagoClient.getRecievedItemAmount() > __instance.UnlockedPiggyCount)
        {
            return true;
        }
        return false;
    }

    static void Postfix(PiggyUnlockManager __instance)
    {
        if(__instance.UnlockedPiggyCount > Plugin.piggyCount)
        {
            Plugin.piggyCount++;
            Plugin.ArchipelagoClient.TrySendLocationsByIds([Plugin.piggyCount]);
        }
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