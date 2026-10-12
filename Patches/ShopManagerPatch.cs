using System;
using System.Collections.Generic;
using HarmonyLib;
using PiggyBankBuster.Config.Shop;
using PiggyBankBuster.Persistence;
using PiggyBankBuster.Progression;
using UnityEngine;

namespace BillsAP;

[HarmonyPatch(typeof(ShopManager), nameof(ShopManager.IsHammerOwned))]
public static class IsHammerOwnedPatch
{
    static void Postfix(ref bool __result, ref ShopManager __instance)
    {   
        Plugin.Instance.Log.LogInfo($"Is hammer hammerId owned?  {__result}");
        ShopSaveData saveData = __instance.GetSaveData();
        saveData.unlockedHammers.Clear();
        __instance.LoadSaveData(saveData);
        // foreach (HammerId hammerId in Enum.GetValues<HammerId>())
        // {
        //     if (!saveData.unlockedHammers.Contains(hammerId))
        //     {
        //         saveData.unlockedHammers.Add(hammerId);
        //     }
        //     __instance.LoadSaveData(saveData);
        // }
    }
}

[HarmonyPatch(typeof(ShopManager), nameof(ShopManager.TryEquipHammer))]
public static class TryEquipHammerPatch
{
    static void Postfix(ref bool __result)
    {
        Plugin.Instance.Log.LogInfo($"Did we equip the hammer?  {__result}");
    }
}