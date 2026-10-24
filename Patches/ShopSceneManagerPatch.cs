using System;
using HarmonyLib;
using PiggyBankBuster.Config.Shop;
using PiggyBankBuster.Progression;
using PiggyBankBuster.Shop;
using PiggyBankBuster.UI;
using UnityEngine;

namespace BillsAP;

[HarmonyPatch(typeof(ShopSceneManager), nameof(ShopSceneManager.HandleHammerPurchased))]
public static class HandleHammerPurchasedPatch
{
    static bool Prefix(ref ShopSceneManager __instance, HammerId hammerId, ref int cost)
    {   
        cost = 0;
        Plugin.BepInLog.LogInfo($"Attempting to purchase {hammerId} at {cost}.");
        return false;
    }
}