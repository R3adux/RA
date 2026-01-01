// License: MIT
// Copyright (c) 2026 Readux
// KCT-Resizer: A mod to adjust GUI heights in Kerbal Construction Time

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using RP0;
using UnityEngine;

namespace KCT_Resizer
{
    [KSPAddon(KSPAddon.Startup.MainMenu, false)]
    public class KCT_Resizer_Loader : MonoBehaviour
    {
        void Start()
        {
            try
            {
                var harmony = new Harmony("com.readux.kctresizer");
                harmony.PatchAll();

                UnityEngine.Debug.Log("<color=green>ReaduxAddition KCT-Resizer -> READY!</color>");
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError("<color=red>ReaduxAddition KCT-Resizer -> !NOT!</color>");
                UnityEngine.Debug.LogError("<color=red>Details: " + ex.Message + "</color>");
            }
        }
    }

    [HarmonyPatch(typeof(KCT_GUI), "RenderCombinedList")]
    public static class Patch_RenderCombinedList
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);

            for (int i = codes.Count - 1; i >= 0; i--)
            {
                if (codes[i].opcode == OpCodes.Ldc_R4 && codes[i].operand is float f5 && f5 == 5f)
                {
                    codes[i].operand = 15f;
                    break;
                }
            }
            return codes.AsEnumerable();
        }
    }

    [HarmonyPatch(typeof(KCT_GUI), "RenderBuildList")]
    public static class Patch_RenderBuildList
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);
            for (int i = 0; i < codes.Count; i++)
            {
                // set the Integration-Window to 550f
                if (codes[i].opcode == OpCodes.Ldc_R4 && codes[i].operand is float f375 && f375 == 375f)
                {
                    codes[i].operand = 550f;
                }
            }
            return codes.AsEnumerable();
        }
    }
}