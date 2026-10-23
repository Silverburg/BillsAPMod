using System;
using HarmonyLib;
using PiggyBankBuster.Config.Shop;
using PiggyBankBuster.Progression;
using PiggyBankBuster.UI;
using UnityEngine;

namespace BillsAP;

[HarmonyPatch(typeof(ShopActionPanelUI), nameof(ShopActionPanelUI.HandlePurchase))]
public static class OnPurchaseClickedPatch
{
    static bool Prefix(ref ShopActionPanelUI __instance)
    {   
        Plugin.BepInLog.LogInfo("Attempting to purchase.");
        return false;
    }
}