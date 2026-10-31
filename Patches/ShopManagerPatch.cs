using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HarmonyLib;
using PiggyBankBuster.Config.Shop;
using PiggyBankBuster.Progression;

namespace BillsAP;

[HarmonyPatch(typeof(ShopManager), nameof(ShopManager.IsHammerOwned))]
public static class IsHammerOwnedPatch
{
    static void Prefix(ref ShopManager __instance, HammerId hammerId)
    {   
        // foreach(HammerId hammer in __instance._unlockedHammers)
        // {
        //     Plugin.BepInLog.LogInfo($"Owned hammer: {hammer}");
        // }
        
        //     else
        //     {
        //         //Plugin.BepInLog.LogInfo($"{hammerId} already unlocked.");
        //     }
        // }
        ShopManagerUpdates.unlockHammer(__instance, hammerId);
        Plugin.giveMoney(1000);
    }
}

[HarmonyPatch(typeof(ShopManager), nameof(ShopManager.TryEquipHammer))]
public static class TryEquipHammerPatch
{
    static bool Prefix(ref ShopManager __instance, HammerId hammerId)
    {
        //__instance.Config._defaultHammer = __instance.Config.GetHammer(hammerId);
        if(ShopManagerUpdates.hammerCount+1 >= (int)hammerId)
        {
            return true;
        }
        else return false;
    }
}

[HarmonyPatch(typeof(ShopManager), nameof(ShopManager.TryPurchaseHammer))]
public static class TryPurchaseHammerPatch
{
    static bool Prefix(ref ShopManager __instance, HammerId hammerId)
    {
        Plugin.BepInLog.LogInfo($"Trying to purchase {hammerId} at id {(int)hammerId}");
        if (__instance._moneyManager.TotalMoney >= __instance.Config.GetHammer(hammerId).cost)
        {
            Plugin.BepInLog.LogInfo("Enough money, trying to purchase.");
            if(!Plugin.ArchipelagoClient.HasLocationBeenChecked((int)hammerId + ShopManagerUpdates.hammerIdOffset))
            {
                __instance._moneyManager.DeductMoney(__instance.Config.GetHammer(hammerId).cost);
                Plugin.ArchipelagoClient.TrySendLocationsByIds([(int)hammerId + ShopManagerUpdates.hammerIdOffset]);    
            }
            
            if(ShopManagerUpdates.hammerCount+1 >= (int)hammerId)
            {
                ShopManagerUpdates.unlockHammer(__instance, hammerId);
                return false;
            }
            else
            {
                //Put code here to update text to need progressive hammer.
                return false;
            }
        }
        Plugin.BepInLog.LogInfo("Not enough money.");
        return false;
    }
}

public static class ShopManagerUpdates
{
    public static int hammerCount = 0;
    public const int hammerIdOffset = 34;

    public static void unlockHammer(ShopManager instance, HammerId hammerId)
    {
        List<HammerId> hammerUnlockOrder = [HammerId.ToyHammer, HammerId.UltraCritHammer, HammerId.EfficientHammer, HammerId.BerserkerHammer, HammerId.SpikesHammer, HammerId.GoldenHammer];
        ShopSaveData saveData = instance.GetSaveData();

        Plugin.BepInLog.LogInfo($"Unlock check {hammerId}, HammerCount {hammerCount}");
        if (hammerCount > hammerUnlockOrder.IndexOf(hammerId) && Plugin.ArchipelagoClient.HasLocationBeenChecked((int)hammerId + hammerIdOffset))
        {
            if (!saveData.unlockedHammers.Contains(hammerId))
            {
                Plugin.BepInLog.LogInfo($"Attempting to unlock {hammerId}");
                saveData.unlockedHammers.Add(hammerId);
                instance._unlockedHammers.Add(hammerId);
                instance.LoadSaveData(saveData);
            }
        }
    }
}