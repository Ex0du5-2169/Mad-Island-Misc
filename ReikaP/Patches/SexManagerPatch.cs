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

        [HarmonyPatch(typeof(CommonStates))]
        [HarmonyPatch("Employed")]
        [HarmonyPrefix]
        public static void NPCSettingsFix(CommonStates __instance)
        {

            switch (__instance.npcID)
            {
                case 0:
                    __instance.employ = CommonStates.Employ.Friend;
                    __instance.pregnant[0] = 0;
                    break;
                case 5:
                    __instance.employ = CommonStates.Employ.Friend;
                    __instance.pregnant[0] = 0;
                    break;
                case 6:
                    __instance.employ = CommonStates.Employ.Friend;
                    __instance.pregnant[0] = 0;
                    break;

            }

        }/*

        
        [HarmonyPatch(typeof(NPCMove))]
        [HarmonyPatch("PregnancyCheck")]
        [HarmonyPostfix]
        public static void PregFix(SexManager __instance, ref bool __result, CommonStates girl)
        {

            int npcID = girl.npcID;
            if (!__result)
            {
                switch (npcID)
                {
                    case 0:

                        __result = true;
                        break;
                }
            }
        }*/
        //The following is just a test, can remove.
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

                        __result = true;
                        break;
                }
            }
        }/*
        [HarmonyPatch(typeof(RandomCharacter))]
        [HarmonyPatch("SetPregnantState")]
        [HarmonyPrefix]

        public static void BodyFix(RandomCharacter __instance, CommonStates common)
        {
            CommonStates tempGirl = common;
            tempGirl.npcID = 15;
            
            if ((common.npcID != 15) && (common.npcID != 16))
            {
                switch (common.npcID)
                {
                    case 0:
                    case 5:
                    case 6:
                    case 113:
                    case 114:
                    case 115:
                        Attachment slot1 = tempGirl.anim.skeleton.GetAttachment("Body_preg", "Body_preg");
                        common.anim.skeleton.SetAttachment("Body_preg", slot1.Name);
                        break;
                }

            }


        }*/
    }
}



