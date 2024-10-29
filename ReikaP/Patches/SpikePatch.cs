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


        public static void SpikeEggs(CommonStates girl, CommonStates pCommon, CommonStates __instance)
        {

            bool creamed = false;

            if (__instance.sex == CommonStates.SexState.GameOver)
            {

                //System.Random random = new System.Random();
                //eggs = random.Next(8);
                //Debug.Log(egged);
                //Debug.Log(eggs);
                creamed = true;
                Debug.Log(creamed);
            }
            if (creamed)
            {
                __instance.pregnant[0] = 1;
                Debug.Log(pCommon.pregnant);
            }


            System.Random random = new System.Random();
            int isPreg = random.Next(9);
            Debug.Log(isPreg);
            int pregStage = new int();
            pregStage = 0;
            Debug.Log(pregStage);
            //pregStage++;

            if (__instance.anim.skeleton.FindSlot("Body_preg") == null)
            {
                Attachment slot1 = girl.anim.skeleton.GetAttachment("Body_preg", "Body_preg");
                __instance.anim.skeleton.SetAttachment("Body_preg", slot1.Name);
                Debug.Log(girl.anim.skeleton.GetAttachment("Body_preg", "Body_preg"));
            }
        }

        // The following is just a list of birth animations and their possible replacements. I will need to check them out in the gallery before I make a final decision. The code below is likely wrong, it's placeholder for now.
        [HarmonyPatch(typeof(SexManager))]
        [HarmonyPatch("Delivery")]
        [HarmonyPrefix]
        public static void DeliveryPatch(SexManager __instance, CommonStates girl)
        {
          
            // Note to self: The following looks to be how I can address and swap animations simply
            if (girl.anim.skeleton.Data.FindAnimation("A_delivery_idle") == null)
            {
                girl.anim.state.SetAnimation(0, "B_dogeza_idle", loop: true);
            }
            if (girl.anim.skeleton.Data.FindAnimation("A_delivery_loop") == null)
            {
                girl.anim.state.SetAnimation(0, "B_dogeza_idle", loop: true);
            }
            if (girl.anim.skeleton.Data.FindAnimation("A_delivery_end") == null)
            {
                girl.anim.state.SetAnimation(0, "B_dogezaToDown", loop: true);
            }
        }

    }
}
