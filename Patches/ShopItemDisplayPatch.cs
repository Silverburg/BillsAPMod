using HarmonyLib;
using PiggyBankBuster.Shop;

namespace BillsAP;

[HarmonyPatch(typeof(ShopItemDisplay), nameof(ShopItemDisplay.GetCost))]
public static class GetCostPatch
{
    static void Postfix(ref int __result)
    {   
        //Plugin.BepInLog.LogInfo($"Cost is {__result}");
    }
}