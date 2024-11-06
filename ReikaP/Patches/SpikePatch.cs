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
using ReikaP;

namespace ReikaP.Patches
{

    internal class SpikePatch
    {


        [HarmonyPatch(typeof(SexManager))]
        [HarmonyPatch("PregnancyCheck")]
        [HarmonyPrefix]


        public static void CreamSex(CommonStates girl, ref bool __result)
        {

            bool creamed = false;


            if (!__result)
            {

                //System.Random random = new System.Random();
                //eggs = random.Next(8);
                //Debug.Log(egged);
                //Debug.Log(eggs);
                creamed = true;
                Debug.Log(creamed + ": Creampied");

                //System.Random random = new System.Random();
                int isPreg = UnityEngine.Random.Range(0, 9);
                Debug.Log(isPreg + ": Random int, must be > 5 for pregnancy");
                int pregStage = new int();
                pregStage = 0;
                //pregStage++;

                if ((creamed == true) && (isPreg >= 5))
                {
                    pregStage = 1;
                    Debug.Log(pregStage + ": Stage, not needed yet");
                    __result = true;
                    Debug.Log(__result + ": Pregnant or not");
                    
                    //mn.randChar.SetPregnantState(__girl, state: true);
                }

                Debug.Log(girl.pregnant[1] + ": Default pregnancy state");
                pregStage = 0;
                creamed = false;
                isPreg = 0;
            }
        }


        [HarmonyPatch(typeof(SexManager))]
        [HarmonyPatch("Delivery")]
        [HarmonyPrefix]
        public static void DeliveryPatch(CommonStates __instance)
        {
            NPCMove aMove = __instance.nMove;
            switch (__instance.npcID)
            {
                case 0: //Yona

                    // Note to self: The following looks to be how I can address and swap animations simply
                    if ((__instance.anim.skeleton.Data.FindAnimation("A_delivery_idle") == null) && (aMove.actType == NPCMove.ActType.Wait) && (__instance.pregnant[0] != -1))
                    {
                        __instance.anim.state.SetAnimation(0, "B_dogeza_idle", loop: true);
                    }
                    if ((__instance.anim.skeleton.Data.FindAnimation("A_delivery_loop") == null) && (aMove.actType == NPCMove.ActType.Wait) && (__instance.pregnant[0] != -1))
                    {
                        __instance.anim.state.SetAnimation(0, "B_dogeza_idle", loop: true);
                    }
                    if ((__instance.anim.skeleton.Data.FindAnimation("A_delivery_end") == null) && (aMove.actType == NPCMove.ActType.Wait) && (__instance.pregnant[0] != -1))
                    {
                        __instance.anim.state.SetAnimation(0, "B_dogezaToDown", loop: false);
                    }
                    break;
                case 5: //Reika
                    if ((__instance.anim.skeleton.Data.FindAnimation("A_delivery_idle") == null) && (aMove.actType == NPCMove.ActType.Wait) && (__instance.pregnant[0] != -1))
                    {
                        __instance.anim.state.SetAnimation(0, "B_idle", loop: true);
                    }
                    if ((__instance.anim.skeleton.Data.FindAnimation("A_delivery_loop") == null) && (aMove.actType == NPCMove.ActType.Wait) && (__instance.pregnant[0] != -1))
                    {
                        __instance.anim.state.SetAnimation(0, "A_down_drug_idle", loop: true);
                    }
                    if ((__instance.anim.skeleton.Data.FindAnimation("A_delivery_end") == null) && (aMove.actType == NPCMove.ActType.Wait) && (__instance.pregnant[0] != -1))
                    {
                        __instance.anim.state.SetAnimation(0, "A_down_raped", loop: false);
                    }
                    break;
                case 6: //Nami
                    if ((__instance.anim.skeleton.Data.FindAnimation("A_delivery_idle") == null) && (aMove.actType == NPCMove.ActType.Wait) && (__instance.pregnant[0] != -1))
                    {
                        __instance.anim.state.SetAnimation(0, "B_idle_weak", loop: true);
                    }
                    if ((__instance.anim.skeleton.Data.FindAnimation("A_delivery_loop") == null) && (aMove.actType == NPCMove.ActType.Wait) && (__instance.pregnant[0] != -1))
                    {
                        __instance.anim.state.SetAnimation(0, "B_idle_damage", loop: true);
                    }
                    if ((__instance.anim.skeleton.Data.FindAnimation("A_delivery_end") == null) && (aMove.actType == NPCMove.ActType.Wait) && (__instance.pregnant[0] != -1))
                    {
                        __instance.anim.state.SetAnimation(0, "B_idle_damagetoweak", loop: false);
                    }
                    break;
            }

        }
        [HarmonyPatch(typeof(RandomCharacter))]
        [HarmonyPatch("SetPregnantState")]
        [HarmonyPrefix]

        public static void PregStatePatch(CommonStates common)
        {

                if ((common.anim.skeleton.FindSlot("Body_preg") == null) && (common.pregnant[1] < 0))
                {
                CommonStates common1 = new CommonStates();
                common1.npcID = 15;
                Attachment slot1 = common1.anim.skeleton.GetAttachment("Body_preg", "Body_preg");
                switch (common.npcID)
                    {
                        case 0: //Yona
                            common.anim.skeleton.SetAttachment("Body_preg", slot1.Name);
                            Debug.Log(common.anim.skeleton.GetAttachment("Body_preg", "Body_preg"));
                            break;
                        case 5: //Reika
                            common.anim.skeleton.SetAttachment("Body_preg", slot1.Name);
                            Debug.Log(common.anim.skeleton.GetAttachment("Body_preg", "Body_preg"));
                            
                            break;
                        case 6: //Nami
                            common.anim.skeleton.SetAttachment("Body_preg", slot1.Name);
                            Debug.Log(common.anim.skeleton.GetAttachment("Body_preg", "Body_preg"));
                            break;
                    }

                }
            
        }
    }
}
