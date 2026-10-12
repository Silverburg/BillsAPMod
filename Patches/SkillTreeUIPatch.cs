using System.Collections.Generic;
using System.Data.Common;
using HarmonyLib;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppSystem;
using PiggyBankBuster.Config;
using PiggyBankBuster.Config.Shop;
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
            Il2CppReferenceArray<SkillNodeConfig> nodes = manager.GetAllNodes();
            int index = 0;
            
            foreach (SkillNodeConfig config in nodes)
            {
                Plugin.Instance.Log.LogInfo($"ID: {config.nodeId} -- Name: {config.name} -- Position: {config.treePosition}");
                GameObject UIobj = GameObject.Find("SkillTreeUI");
                SkillTreeUI UI = UIobj.GetComponent<SkillTreeUI>();

                SkillNodeUI nodeUI = UI.FindNodeUI(index);
                nodeUI.SetVisibility(NodeVisibility.Visible);

                index++;
            }
        }
    }