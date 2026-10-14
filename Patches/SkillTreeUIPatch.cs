using HarmonyLib;
using PiggyBankBuster.Config;
using PiggyBankBuster.Progression;
using PiggyBankBuster.UI;
using UnityEngine;

namespace BillsAP;

[HarmonyPatch(typeof(SkillTreeUI), nameof(SkillTreeUI.Show))]
    public static class GetAllNodesPatch
    {
    
        static void Postfix()
        {
            GameObject obj = GameObject.Find("SkillTreeManager");
            SkillTreeManager manager = obj.GetComponent<SkillTreeManager>();
            int index = 0;
            
            foreach (SkillNodeConfig config in manager.GetAllNodes())
            {
                Plugin.BepInLog.LogInfo($"ID: {config.nodeId} -- Name: {config.name} -- Position: {config.treePosition} -- Costs: {config.levelCosts} -- MaxLevel: {config.maxLevel}");
                GameObject UIobj = GameObject.Find("SkillTreeUI");
                SkillTreeUI UI = UIobj.GetComponent<SkillTreeUI>();

                SkillNodeUI nodeUI = UI.FindNodeUI(index);
                nodeUI.SetVisibility(NodeVisibility.Visible);

                index++;
            }
        }
    }