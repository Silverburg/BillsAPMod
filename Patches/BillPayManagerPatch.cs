using HarmonyLib;
using PiggyBankBuster.Config;
using PiggyBankBuster.Economy;
using PiggyBankBuster.UI;
using UnityEngine;

namespace BillsAP;

[HarmonyPatch(typeof(BillManager), nameof(BillManager.CreateBill))]
    public static class CreateBillPatch
    {
    
        static void Postfix(BillData __result)
        {
            Plugin.BepInLog.LogInfo($"Created bill {__result.displayName}.");
        }
    }

[HarmonyPatch(typeof(BillManager), nameof(BillManager.TryPayBill))]
    public static class TryPayBillPatch
    {
    
        static void Postfix(BillManager __instance, bool __result)
        {
            if (__result)
            {
                Plugin.ArchipelagoClient.TrySendLocationsByIds([__instance.BillsPaidThisCycle+16]);
            }
            foreach(BillData bill in __instance.GetSaveData().bills)
            {
                Plugin.BepInLog.LogInfo($"BillTypeId: {bill.billTypeId} -- displayName: {bill.displayName} -- baseAmount: {bill.baseAmount} -- isPaid: {bill.isPaid}");
            }

            Plugin.BepInLog.LogInfo(__instance.BillsPaidThisCycle);
        }
    }