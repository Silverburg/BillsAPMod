using HarmonyLib;
using PiggyBankBuster.UI;

namespace BillsAP;

[HarmonyPatch(typeof(UpgradeMenuManager), nameof(UpgradeMenuManager.ShowShop))]
public static class ShowShopPatch
{
    static void Prefix(ref UpgradeMenuManager __instance)
    {   
        Plugin.BepInLog.LogInfo("Opening Shop");
    }
}