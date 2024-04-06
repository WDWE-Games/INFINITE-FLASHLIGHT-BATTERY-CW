using System;
using HarmonyLib;
using BepInEx;
using UnityEngine;
using BepInEx.Logging;
using System.Reflection;

namespace InfiniteFlashLightBattery
{
    [BepInPlugin("WDWE.InfiniteFlashLightBattery", "infinite flashlight battery", "1.0.0")]


    public class Plugin : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony("infinite flashlight battery");
        internal ManualLogSource mls;
        void Awake()
        {
            mls = BepInEx.Logging.Logger.CreateLogSource("infinite flashlight battery");
            mls.LogWarning("this is a warning bro and it's working goodd");
            harmony.PatchAll(typeof(Plugin));
            harmony.PatchAll(typeof(CurrentFlashLightBatteryPatch));
        }

    }
    [HarmonyPatch(typeof(Flashlight))]
    internal class CurrentFlashLightBatteryPatch
    {
        [HarmonyPatch("Update")]
        [HarmonyPrefix]
        static void infiniteFlashLight(ref Flashlight __instance)
        {
            FieldInfo batteryEntryField = typeof(Flashlight).GetField("m_batteryEntry", BindingFlags.NonPublic | BindingFlags.Instance);
            BatteryEntry batteryEntry = (BatteryEntry)batteryEntryField.GetValue(__instance);


            // Imposta la stamina del giocatore su un valore desiderato (es. 10f)
            batteryEntry.m_charge = batteryEntry.m_maxCharge;
       
        }

    }
}