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

namespace ReikaP.Patches
{

    internal class SpikePatch
    {


        [HarmonyPatch(typeof(SexManager))]
        [HarmonyPatch("PregnancyCheck")]
        [HarmonyPrefix]


        public static void CreamSex(CommonStates __girl, CommonStates __instance, ref bool __result, SexManager.SexCountState sexState)
        {

            bool creamed = false;

            if ((__instance.sex == CommonStates.SexState.GameOver) || (sexState == SexManager.SexCountState.Creampie) || (sexState == SexManager.SexCountState.Normal))
            {

                //System.Random random = new System.Random();
                //eggs = random.Next(8);
                //Debug.Log(egged);
                //Debug.Log(eggs);
                creamed = true;
                Debug.Log(creamed);
            }

            System.Random random = new System.Random();
            int isPreg = random.Next(9);
            Debug.Log(isPreg);
            int pregStage = new int();
            pregStage = 0;
            Debug.Log(pregStage);
            //pregStage++;

            if ((creamed) && (isPreg > 5) && (!__result))
            {
                pregStage = 1;
                __result = true;
                Debug.Log(__instance.pregnant);
                //mn.randChar.SetPregnantState(__girl, state: true);
            }

            if ((__instance.anim.skeleton.FindSlot("Body_preg") == null) && (pregStage >= 1))
            {
                Attachment slot1 = __girl.anim.skeleton.GetAttachment("Body_preg", "Body_preg");
                switch (__instance.npcID)
                {
                    case 0:
                        __instance.anim.skeleton.SetAttachment("Body_preg", slot1.Name);
                        Debug.Log(__instance.anim.skeleton.GetAttachment("Body_preg", "Body_preg"));
                        break;
                    case 5:
                        __instance.anim.skeleton.SetAttachment("Body_preg", slot1.Name);
                        Debug.Log(__instance.anim.skeleton.GetAttachment("Body_preg", "Body_preg"));
                        break;
                    case 6:
                        __instance.anim.skeleton.SetAttachment("Body_preg", slot1.Name);
                        Debug.Log(__instance.anim.skeleton.GetAttachment("Body_preg", "Body_preg"));
                        break;
                }
                pregStage = 0;
                creamed = false;
                isPreg = 0;
            }
        }

        
        [HarmonyPatch(typeof(SexManager))]
        [HarmonyPatch("Delivery")]
        [HarmonyPrefix]
        public static void DeliveryPatch(CommonStates __instance, SexManager.SexCountState sexState)
        {
            switch (__instance.npcID)
            {
                case 0: //Yona
                    
                    // Note to self: The following looks to be how I can address and swap animations simply
                    if ((__instance.anim.skeleton.Data.FindAnimation("A_delivery_idle") == null) && (sexState == SexManager.SexCountState.Delivery))
                    {
                        __instance.anim.state.SetAnimation(0, "B_dogeza_idle", loop: true);
                    }
                    if ((__instance.anim.skeleton.Data.FindAnimation("A_delivery_loop") == null) && (sexState == SexManager.SexCountState.Delivery))
                    {
                        __instance.anim.state.SetAnimation(0, "B_dogeza_idle", loop: true);
                    }
                    if ((__instance.anim.skeleton.Data.FindAnimation("A_delivery_end") == null) && (sexState == SexManager.SexCountState.Delivery))
                    {
                        __instance.anim.state.SetAnimation(0, "B_dogezaToDown", loop: false);
                    }
                    break;
                case 5: //Reika
                    if ((__instance.anim.skeleton.Data.FindAnimation("A_delivery_idle") == null) && (sexState == SexManager.SexCountState.Delivery))
                    {
                        __instance.anim.state.SetAnimation(0, "B_idle", loop: true);
                    }
                    if ((__instance.anim.skeleton.Data.FindAnimation("A_delivery_loop") == null) && (sexState == SexManager.SexCountState.Delivery))
                    {
                        __instance.anim.state.SetAnimation(0, "A_down_drug_idle", loop: true);
                    }
                    if ((__instance.anim.skeleton.Data.FindAnimation("A_delivery_end") == null) && (sexState == SexManager.SexCountState.Delivery))
                    {
                        __instance.anim.state.SetAnimation(0, "A_down_raped", loop: false);
                    }
                    break;
                case 6: //Nami
                    if ((__instance.anim.skeleton.Data.FindAnimation("A_delivery_idle") == null) && (sexState == SexManager.SexCountState.Delivery))
                    {
                        __instance.anim.state.SetAnimation(0, "B_idle_weak", loop: true);
                    }
                    if ((__instance.anim.skeleton.Data.FindAnimation("A_delivery_loop") == null) && (sexState == SexManager.SexCountState.Delivery))
                    {
                        __instance.anim.state.SetAnimation(0, "B_idle_damage", loop: true);
                    }
                    if ((__instance.anim.skeleton.Data.FindAnimation("A_delivery_end") == null) && (sexState == SexManager.SexCountState.Delivery))
                    {
                        __instance.anim.state.SetAnimation(0, "B_idle_damagetoweak", loop: false);
                    }
                    break;
            }

        }

    }
}
