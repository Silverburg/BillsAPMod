using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using UnityEngine;
using Archipelago.MultiClient.Net.Helpers;
using System.Collections.ObjectModel;

namespace BillsAP;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInProcess("Bills Must Be Paid.exe")]
public class Plugin : BasePlugin
{
    public static ManualLogSource BepInLog;

    public const string ModDisplayInfo = $"{MyPluginInfo.PLUGIN_NAME} v{MyPluginInfo.PLUGIN_VERSION}";
    public static ArchipelagoClient ArchipelagoClient;
    public static GameObject ModObject;

    public override void Load()
    {

        // Plugin startup logic
        BepInLog = Log;

        ArchipelagoClient = new ArchipelagoClient();
        connectAP("localhost", "38281", "TestBill");
        // ArchipelagoConsole.Awake();
        // ArchipelagoConsole.LogMessage($"{ModDisplayInfo} loaded!");

        // ModObject = new GameObject();
        // GameObject.DontDestroyOnLoad(ModObject);
        
        // BepInLog.LogInfo($"PreAddComponent");
        // ModObject.AddComponent<UIController>();
        // BepInLog.LogInfo($"PostAddComponent");

        var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
        harmony.PatchAll();

        ArchipelagoClient.TrySendLocations([1,3]);

        BepInLog.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");

    }

    public void connectAP(string host, string port, string slot)
    {

        ArchipelagoClient.ServerData.Uri = host+":"+port;
        ArchipelagoClient.ServerData.SlotName = slot;

        ArchipelagoClient.Connect();
    }
}

public class UIController : MonoBehaviour
{
    private void Awake()
    {
        Plugin.BepInLog.LogInfo("Awake fired.");
    }

    // private const string APDisplayInfo = $"Archipelago v{ArchipelagoClient.APVersion}";

    // private void OnGUI()
    // {
    //     // show the mod is currently loaded in the corner
    //     GUI.Label(new Rect(16, 16, 300, 20), Plugin.ModDisplayInfo);
    //     ArchipelagoConsole.OnGUI();

    //     string statusMessage;
    //     // show the Archipelago Version and whether we're connected or not
    //     if (ArchipelagoClient.Authenticated)
    //     {
    //         // if your game doesn't usually show the cursor this line may be necessary
    //         // Cursor.visible = false;

    //         statusMessage = " Status: Connected";
    //         GUI.Label(new Rect(16, 50, 300, 20), APDisplayInfo + statusMessage);
    //     }
    //     else
    //     {
    //         // if your game doesn't usually show the cursor this line may be necessary
    //         // Cursor.visible = true;

    //         statusMessage = " Status: Disconnected";
    //         GUI.Label(new Rect(16, 50, 300, 20), APDisplayInfo + statusMessage);
    //         GUI.Label(new Rect(16, 70, 150, 20), "Host: ");
    //         GUI.Label(new Rect(16, 90, 150, 20), "Player Name: ");
    //         GUI.Label(new Rect(16, 110, 150, 20), "Password: ");

    //         ArchipelagoClient.ServerData.Uri = GUI.TextField(new Rect(150, 70, 150, 20),
    //             ArchipelagoClient.ServerData.Uri);
    //         ArchipelagoClient.ServerData.SlotName = GUI.TextField(new Rect(150, 90, 150, 20),
    //             ArchipelagoClient.ServerData.SlotName);
    //         ArchipelagoClient.ServerData.Password = GUI.TextField(new Rect(150, 110, 150, 20),
    //             ArchipelagoClient.ServerData.Password);

    //         // requires that the player at least puts *something* in the slot name
    //         if (GUI.Button(new Rect(16, 130, 100, 20), "Connect") &&
    //             !ArchipelagoClient.ServerData.SlotName.IsNullOrWhiteSpace())
    //         {
    //             Plugin.ArchipelagoClient.Connect();
    //         }
    //     }
    //     // this is a good place to create and add a bunch of debug buttons
    // }
}