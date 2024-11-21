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


        public static void CreamSex(CommonStates girl, ref bool __result, CommonStates man, ref ManagersScript ___mn)
        {

            bool creamed = false;

            if ((!__result) || (girl.sex == CommonStates.SexState.GameOver))//If the game's own result is set as false we take over, this does have the side effect of adding a 2nd roll of the dice for native women/girls, next update I will further qualify this to avoid double dipping like that.
            {

                //System.Random random = new System.Random();
                //eggs = random.Next(8);
                //Debug.Log(egged);
                //Debug.Log(eggs);
                creamed = true; //Must have taken an action that gives the creampie state, for now we have given it that state through other means.
                Debug.Log(creamed + ": Creampied");

                //System.Random random = new System.Random();
                int isPreg = UnityEngine.Random.Range(0, 15); //Set a random range for preg chance
                Debug.Log(isPreg + ": Random int, must be > 11 for pregnancy");
                int pregStage = new int(); //eventually will become part of a mentstrual system, for now it's only used to hold an int for the game's preg system to receive.
                pregStage = 0;
                //pregStage++;

                if ((creamed == true) && (isPreg >= 11)) //Tests whether creampied and if the RNG allows it, for now. Later it will test creampied vs the mentstrual stage plus some RNG.
                {
                    pregStage = 12;
                    Debug.Log(pregStage + ": Staging, ignore, not needed yet");
                    __result = true; //The base game purposely sets the result as false for Yona et al. Here we force it to true, but this alone won't trigger pregnancy so we continue below.
                    Debug.Log(__result + ": Pregnant or not");
                    girl.pregnant[1] = pregStage; //Trigger the game's pregnancy system.
                    girl.pregnant[0] = man.friendID; //Pregnancy system requires the father be set.
                    Debug.Log(girl.pregnant[1] + ": Default pregnancy state");
                    Debug.Log(girl.pregnant[0] + ": Return ID of potential father");
                    ___mn.uiMN.FriendHealthCheck(girl); //Trigger a health check to update the UI panels.
                    
                    //mn.randChar.SetPregnantState(__girl, state: true);
                }


                if (girl.pregnant[1] == 12)
                {
                    ___mn.sound.GoSound(108, girl.transform.position, randomPitch: true); //play sound on successful impregnation.
                    if ((girl.anim.skeleton.FindSlot("Body_preg") == null) && (girl.pregnant[1] < 0))
                    {
                        CommonStates common1 = new CommonStates();
                        common1.npcID = 15;
                        Attachment slot1 = common1.anim.skeleton.GetAttachment("Body_preg", "Body_preg");
                        switch (girl.npcID)
                        {
                            case 0: //Yona
                                girl.anim.skeleton.SetAttachment("Body_preg", slot1.Name);
                                Debug.Log(girl.anim.skeleton.GetAttachment("Body_preg", "Body_preg"));
                                break;
                            case 5: //Reika
                                girl.anim.skeleton.SetAttachment("Body_preg", slot1.Name);
                                Debug.Log(girl.anim.skeleton.GetAttachment("Body_preg", "Body_preg"));

                                break;
                            case 6: //Nami
                                girl.anim.skeleton.SetAttachment("Body_preg", slot1.Name);
                                Debug.Log(girl.anim.skeleton.GetAttachment("Body_preg", "Body_preg"));
                                break;
                        }

                    }
                }
                pregStage = 0; //Reset used variables, just in case.
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

        }/*
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
            
        }*/
    }
}
