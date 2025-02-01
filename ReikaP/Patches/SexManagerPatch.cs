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

namespace ReikaP.Patches
{

    internal class SexManagerPatch
    {

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
                    case 113:
                    case 114:
                    case 115:
                    case 116:
                        __result = true;
                        break;
                }
            }
        }

        [HarmonyPatch(typeof(SexManager))]
        [HarmonyPatch("CommonSexNPC")]
        [HarmonyPrefix]

        public static void CommonSexNPCFix(SexManager __instance, CommonStates npcA, CommonStates npcB, SexPlace sexPlace)
        {
            Transform transform = sexPlace.transform.Find("pos");
            Vector3 pos = transform.position;
            GameObject tmpSex = null;
            switch (npcA.npcID)
            {
                case 0:
                    if ((npcB.npcID == 10) && (npcA.sex == CommonStates.SexState.Playing))
                    {
                        tmpSex = UnityEngine.Object.Instantiate(__instance.sexList[2].sexObj[4], pos, Quaternion.identity);
                    }
                    if ((npcB.npcID == 11) && (npcA.sex == CommonStates.SexState.Playing))
                    {
                        tmpSex = UnityEngine.Object.Instantiate(__instance.sexList[14].sexObj[0], pos, Quaternion.identity);
                    }
                    break;
            }
            switch (npcB.npcID)
            {
                case 0:
                    if ((npcA.npcID == 10) && (npcA.sex == CommonStates.SexState.Playing))
                    {
                        tmpSex = UnityEngine.Object.Instantiate(__instance.sexList[2].sexObj[4], pos, Quaternion.identity);
                    }
                    if ((npcA.npcID == 11) && (npcA.sex == CommonStates.SexState.Playing))
                    {
                        tmpSex = UnityEngine.Object.Instantiate(__instance.sexList[14].sexObj[0], pos, Quaternion.identity);
                    }
                    break;
                    
            }
        }
    }
}



