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
using static SexManager;

namespace ReikaP.Patches
{

    internal class SpikePatch
    {
        public static bool raped1 = false;
        public static bool creamed = false;

        [HarmonyPatch(typeof(SexManager))]
        [HarmonyPatch("CommonRapesNPC")]
        [HarmonyPrefix]
        public static void NPCRaped(CommonStates npcA, CommonStates npcB, SexManager __instance)
        {
            __instance.PregnancyCheck(npcB, npcA);
            raped1 = true;
        }

        [HarmonyPatch(typeof(SexManager))]
        [HarmonyPatch("PlayerRaped")]
        [HarmonyPrefix]

        public static void PRaped(CommonStates to, CommonStates from, SexManager __instance, ref ManagersScript ___mn)
        {
            switch (to.npcID)
            {
                case 0:
                    __instance.PregnancyCheck(to, from);
                    raped1 = true;
                    break;
                case 1:
                    break;
            }
            //This section attaempts to trigger a pregnancy check upon the player being raped.
        }

        [HarmonyPatch(typeof(SexManager))]
        [HarmonyPatch("PregnancyCheck")]
        [HarmonyPrefix]


        public static void CreamSex(CommonStates girl, ref bool __result, CommonStates man, ManagersScript ___mn, SexManager __instance)
        {


            bool canGet = false;
            CommonStates getsIt = girl;
            CommonStates givesIt = man;


            switch (getsIt.npcID)
            {
                case 0:
                case 5:
                case 6:
                case 17:
                case 90:
                case 113:
                case 114:
                case 115:
                case 116:
                    canGet = true;
                    break;
                case 1:
                case 10:
                case 11:
                case 12:
                case 14:
                case 89:
                case 91:
                    canGet = false;
                    __result = false;
                    //__instance.Pregnancy(girl, man, state: false);

                    break;
            }
            switch (givesIt.npcID)
            {
                case 0:
                case 5:
                case 6:
                case 17:
                case 90:
                case 113:
                case 114:
                case 115:
                case 116:
                    canGet = true;
                    break;
                case 1:
                case 10:
                case 11:
                case 12:
                case 14:
                case 89:
                case 91:
                    canGet = false;
                    __result = false;
                    //__instance.Pregnancy(girl, man, state: false);

                    break;
            }

            if ((!__result) || (raped1))//If the game's own result is set as false we take over, this does have the side effect of adding a 2nd roll of the dice for native women/girls
                    {


                        creamed = true; //Must have taken an action that gives the creampie state, for now we have given it that state through other means.
                        Debug.Log(creamed + ": Creampied");

                        int isPreg = UnityEngine.Random.Range(0, 15); //Set a random range for preg chance
                        Debug.Log(isPreg + ": Random int, must be > 11 for pregnancy");
                        int pregStage = new int(); //eventually will become part of a mentstrual system, for now it's only used to hold an int for the game's preg system to receive.
                        pregStage = 0;
                        //pregStage++;

                        if ((creamed == true) && (isPreg >= 11) && (canGet == true)) //Tests whether creampied and if the RNG allows it, for now. Later it will test creampied vs the mentstrual stage plus some RNG.
                        {
                            pregStage = 12;
                            Debug.Log(pregStage + ": Staging, ignore, not needed yet");
                            __result = true; //The base game purposely sets the result as false for Yona et al. Here we force it to true, but this alone won't trigger pregnancy so we continue below.
                            Debug.Log(__result + ": Pregnant or not");
                            girl.pregnant[1] = pregStage; //Trigger the game's pregnancy system.
                            girl.pregnant[0] = man.friendID; //Pregnancy system requires the father be set.
                            Debug.Log(girl.pregnant[1] + ": Default pregnancy state");
                            Debug.Log(girl.pregnant[0] + ": Return ID of sperm donor");
                            ___mn.uiMN.FriendHealthCheck(girl); //Trigger a health check to update the UI panels.

                    

                        if (givesIt.npcID == 0)
                                {
                                __instance.Pregnancy(givesIt, getsIt, state: true); //This section checks for Yona initiating sex and changes her to receiver for pregnancy purposes.
                                }
                        else
                                {
                                __instance.Pregnancy(girl, man, state: true);
                                }
                   
                        }


                        if (girl.pregnant[1] == 12)
                        {
                            ___mn.sound.GoSound(108, girl.transform.position, randomPitch: true); //play sound on successful impregnation.
                        }
                        pregStage = 0; //Reset used variables, just in case.
                        creamed = false;
                        raped1 = false;
                        isPreg = 0;
            }

        }


        [HarmonyPatch(typeof(SexManager))]
        [HarmonyPatch("Delivery")]
        [HarmonyPrefix]
        public static void DeliveryPatch(SexManager __instance, CommonStates common, ManagersScript ___mn)
        {
            //Ignore the following, work in-progress.

            Transform transform = __instance.transform;
            SkeletonAnimation animationEnd = transform.Find("B_idle").GetComponent<SkeletonAnimation>();
            SkeletonAnimation animation = transform.Find("A_delivery_idle").GetComponent<SkeletonAnimation>();
            SkeletonAnimation animation2 = transform.Find("A_delivery_loop").GetComponent<SkeletonAnimation>();
            SkeletonAnimation animation3 = transform.Find("A_delivery_end").GetComponent<SkeletonAnimation>();

            Spine.AnimationState sourceState = animationEnd.AnimationState;
            TrackEntry entry = sourceState.SetAnimation(0, "B_idle", false);
            TrackEntry target = animation.AnimationState.SetAnimation(0, entry.Animation, false);
            animation.AnimationName = "A_delivery_idle";
            TrackEntry target2 = animation2.AnimationState.SetAnimation(0, entry.Animation, false);
            animation2.AnimationName = "A_delivery_loop";
            TrackEntry target3 = animation3.AnimationState.SetAnimation(0, entry.Animation, false);
            animation3.AnimationName = "A_delivery_end";

                 /*   if (common.pregnant[0] == 1)
                    {
                        yield return ___mn.npcMN.GenSpawn(24, common.gameObject, null, 0f, null, tmpUse: true);
                    }*/
                    
            }
        }


        /*[HarmonyPatch(typeof(RandomCharacter))]
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
