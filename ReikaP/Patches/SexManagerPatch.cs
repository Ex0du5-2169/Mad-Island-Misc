using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using System.CodeDom;
using System.Reflection;
using UnityEngine;
using JetBrains.Annotations;
using System.Collections;
using System.Diagnostics.Eventing.Reader;
using UnityEngine.Assertions.Must;
using System.ComponentModel;
using UnityEngine.Rendering;
using UnityEngine.Animations;
using Spine.Unity;
using Spine;
using static SexManager;
using ExtendedHSystem.Patches;
using ExtendedHSystem.Scenes;
using ExtendedHSystem;

namespace ReikaP.Patches
{

    internal class SexManagerPatch
    {
        public static DefaultSceneController controller = new DefaultSceneController();
        //The following is a test, meant to allow various NPCs to engage in sex automatically. Only half-finished currently, likely going to remove it in favour of a hook into Yotan's framework.
        [HarmonyPatch(typeof(NPCMove))]
        [HarmonyPatch("SexableNPC")]
        [HarmonyPostfix]
        public static void SexableNPCFix(NPCMove __instance, ref bool __result)
        {
            if (!__result)
            {
                switch (__instance.common.npcID)
                {
                    case 0:
                    case 1:
                    case 5:
                    case 6:
                    case 25:
                    case 35:
                    case 113:
                    case 114:
                    case 115:
                    case 116:
                        __result = true;
                        break;
                }
            }
        }

        
    }
}



