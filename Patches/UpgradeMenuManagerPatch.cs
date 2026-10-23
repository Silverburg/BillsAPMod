using System;
using HarmonyLib;
using PiggyBankBuster.Config.Shop;
using PiggyBankBuster.Progression;
using PiggyBankBuster.UI;
using UnityEngine;

namespace BillsAP;

[HarmonyPatch(typeof(UpgradeMenuManager), nameof(UpgradeMenuManager.ShowShop))]
public static class ShowShopPatch
{
    static void Prefix(ref UpgradeMenuManager __instance)
    {   
        Plugin.BepInLog.LogInfo("Opening Shop");
    }
}