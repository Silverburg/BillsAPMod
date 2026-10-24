using System;
using HarmonyLib;
using PiggyBankBuster.Config.Shop;
using PiggyBankBuster.Progression;
using PiggyBankBuster.Shop;
using PiggyBankBuster.UI;
using UnityEngine;

namespace BillsAP;

[HarmonyPatch(typeof(ShopItemDisplay), nameof(ShopItemDisplay.GetCost))]
public static class GetCostPatch
{
    static void Postfix(ref int __result)
    {   
        Plugin.BepInLog.LogInfo($"Cost is {__result}");
    }
}