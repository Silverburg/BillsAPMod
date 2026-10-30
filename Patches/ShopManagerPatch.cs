using System;
using HarmonyLib;
using PiggyBankBuster.Config.Shop;
using PiggyBankBuster.Progression;
using UnityEngine;

namespace BillsAP;

[HarmonyPatch(typeof(ShopManager), nameof(ShopManager.IsHammerOwned))]
public static class IsHammerOwnedPatch
{
    static void Prefix(ref ShopManager __instance, HammerId hammerId)
    {   
        foreach(HammerId hammer in __instance._unlockedHammers)
        {
            Plugin.BepInLog.LogInfo($"Owned hammer: {hammer}");
        }
        ShopSaveData saveData = __instance.GetSaveData();
        
        //Plugin.BepInLog.LogInfo($"Unlock check {hammerId}");
        if(ArchipelagoClient.ServerData.HammerCount+1 >= (int)hammerId && ArchipelagoClient.ServerData.CheckedLocations.Contains((int)hammerId+36)){
            if(!saveData.unlockedHammers.Contains(hammerId))
            {
                Plugin.BepInLog.LogInfo($"Attempting to unlock {hammerId}");
                saveData.unlockedHammers.Add(hammerId);
                __instance._unlockedHammers.Add(hammerId);
                __instance.LoadSaveData(saveData);
            }
            else
            {
                //Plugin.BepInLog.LogInfo($"{hammerId} already unlocked.");
            }
        }
        Plugin.giveMoney(1000);
    }
}

// [HarmonyPatch(typeof(ShopManager))]
// public static class OnHammerPurchasedPatch
// {
//     [HarmonyPatch("Awake")]
//     [HarmonyPostfix]
//     public static void AwakePostFix(ShopManager __instance)
//     {
//         if(__instance != null)
//         {
//             __instance.add_OnHammerPurchased += OnHammerPurchasedHandler;
//         }
//     }

//     private static void OnHammerPurchasedHandler()
//     {
        
//     }
// }

// [HarmonyPatch(typeof(ShopManager), nameof(ShopManager.TryEquipHammer))]
// public static class TryEquipHammerPatch
// {
//     static bool Prefix(ref ShopManager __instance, HammerId hammerId)
//     {
//         //__instance.Config._defaultHammer = __instance.Config.GetHammer(hammerId);
//         if(ArchipelagoClient.ServerData.HammerCount+1 >= (int)hammerId)
//         {
//             return true;
//         }
//         else return false;
//     }
// }

// [HarmonyPatch(typeof(ShopManager), nameof(ShopManager.TryPurchaseHammer))]
// public static class TryPurchaseHammerPatch
// {
//     static bool Prefix(ref ShopManager __instance, HammerId hammerId)
//     {
//         if (!ArchipelagoClient.ServerData.CheckedLocations.Contains((int)hammerId + 36))
//         {
            
//         }
//         Plugin.BepInLog.LogInfo($"Trying to purchase {hammerId}");
//         return false;
//     }
// }