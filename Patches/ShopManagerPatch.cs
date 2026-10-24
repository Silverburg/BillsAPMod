using HarmonyLib;
using PiggyBankBuster.Config.Shop;
using PiggyBankBuster.Progression;

namespace BillsAP;

[HarmonyPatch(typeof(ShopManager), nameof(ShopManager.IsHammerOwned))]
public static class IsHammerOwnedPatch
{
    static void Prefix(ref ShopManager __instance, HammerId hammerId)
    {   
        ShopSaveData saveData = __instance.GetSaveData();
        saveData.unlockedHammers.Clear();
        
        Plugin.BepInLog.LogInfo($"Unlock check {hammerId}");
        if(ArchipelagoClient.ServerData.HammerCount+1 >= (int)hammerId){
            if(!saveData.unlockedHammers.Contains(hammerId))
            {
                Plugin.BepInLog.LogInfo($"Attempting to unlock {hammerId}");
                saveData.unlockedHammers.Add(hammerId);
            }
            else
            {
                Plugin.BepInLog.LogInfo($"{hammerId} already unlocked.");
            }
        }
        __instance.LoadSaveData(saveData);

        Plugin.giveMoney(1000);
    }
}

[HarmonyPatch(typeof(ShopManager), nameof(ShopManager.TryEquipHammer))]
public static class TryEquipHammerPatch
{
    static void Postfix(ref bool __result)
    {
        Plugin.BepInLog.LogInfo($"Did we equip the hammer?  {__result}");
    }
}