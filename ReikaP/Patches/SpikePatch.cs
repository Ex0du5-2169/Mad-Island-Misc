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


        [HarmonyPatch(typeof(StoryManager))]
        [HarmonyPatch("BossSpider")]
        [HarmonyPrefix]


        public static void SpikeEggs(StoryManager __instance, CommonStates pCommon, ref int __bossBattleState)
        {

            bool egged = false;
            /*int eggs = new int();
            eggs = 0;


            
            if (eggs > 0)
            {
                egged = true;

            }
           */
            //if ((pCommon.nMove.actType == NPCMove.ActType.Wait) && (pCommon.sex == CommonStates.SexState.GameOver))
            if ((__instance.bossBattleState == 2) && (pCommon.sex == CommonStates.SexState.GameOver))
            {

                //System.Random random = new System.Random();
                //eggs = random.Next(8);
                //Debug.Log(egged);
                //Debug.Log(eggs);
                egged = true;
            }
            if (egged)
            {
                pCommon.pregnant[0] = 1;
            }



        }
        [HarmonyPatch(typeof(StoryManager))]
        public static void EggPregnancy(CommonStates girl)
        {
            int eggStage = new int();
            eggStage = 0;

            eggStage++;

            if (girl.pregnant[1] <= 0 && girl.anim.skeleton.FindSlot("Body_preg") == null)
            {
                Attachment slot1 = girl.anim.skeleton.GetAttachment("Body_preg", "Body_preg");
                girl.anim.skeleton.SetAttachment("Body_preg", slot1.Name);
            }
        }

        // The following is just a list of birth animations and their possible replacements. I will need to check them out in the gallery before I make a final decision. The code below is likely wrong, it's placeholder for now.
        [HarmonyPatch(typeof(SexManager))]
        [HarmonyPatch("Delivery")]
        [HarmonyPrefix]
        public static void DeliveryPatch(SexManager __instance, CommonStates girl)
        {
            /*if (girl.anim.skeleton.FindSlot("A_delivery_idle") == null)
            {
                girl.anim.state.SetAnimation(0, "B_dogeza_idle", loop: true);
            }
            if (girl.anim.skeleton.FindSlot("A_delivery_loop") == null)
            {
                girl.anim.state.SetAnimation(0, "B_dogeza_idle", loop: true);
            }
            if (girl.anim.skeleton.FindSlot("A_delivery_end") == null)
            {
                girl.anim.state.SetAnimation(0, "B_dogezaToBack", loop: true);
            }*/
            if (girl.anim.name.Equals("A_delivery_idle"))
            {
                girl.anim.state.SetAnimation(0, "B_dogeza_idle", loop: true);
            }
            // Note to self: The following looks to be how I can address and swap animations simply
            if (girl.anim != null && girl.anim.skeleton.Data.FindAnimation("animName") != null)
            {
                girl.anim.state.SetAnimation(0, "animName", loop: true);
            }
        }

    }
}
