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

        public static void CommonSexNPCFix(SexManager __instance, CommonStates npcA, CommonStates npcB, SexPlace sexPlace, ref GameObject ___tmpSex, ref ManagersScript ___mn)
        {
            Transform transform = __instance.transform;
            SkeletonAnimation animation = transform.GetComponent<SkeletonAnimation>();
            Vector3 pos = transform.position;
            //GameObject tmpSex = null;
            switch (npcA.npcID)
            {
                case 0:
                    if (npcB.npcID == 10)
                    {
                        CommonSexNPC playerSex = new CommonSexNPC(npcA, npcB, sexPlace, SexCountState.Creampie);
                        controller.PlayOnceStep(playerSex, animation, "A_Contact_01", false);
                        controller.PlayTimedStep(playerSex, animation, "A_Loop_01", 20f);
                        controller.PlayTimedStep(playerSex, animation, "A_Loop_02", 20f);
                        controller.PlayOnceStep(playerSex, animation, "A_Finish", false);
                        controller.PlayTimedStep(playerSex, animation, "A_Finish_Idle", 10f);
                        playerSex.Run();
                    }
                    if (npcB.npcID == 11)
                    {
                        CommonSexNPC playerSex = new CommonSexNPC(npcA, npcB, sexPlace, SexCountState.Creampie);
                        controller.PlayOnceStep(playerSex, animation, "A_Contact_01", false);
                        controller.PlayTimedStep(playerSex, animation, "A_Loop_01", 20f);
                        controller.PlayTimedStep(playerSex, animation, "A_Loop_02", 20f);
                        controller.PlayOnceStep(playerSex, animation, "A_Finish", false);
                        controller.PlayTimedStep(playerSex, animation, "A_Finish_Idle", 10f);
                        playerSex.Run();
                    }
                    if (npcB.npcID == 25)
                    {
                        PlayerRaped playerRaped = new PlayerRaped(npcA, npcB);
                        controller.PlayOnceStep(playerRaped, animation, "A_AttackToSex", false);
                        controller.PlayTimedStep(playerRaped, animation, "A_Loop_01", 20f);
                        controller.PlayTimedStep(playerRaped, animation, "A_Loop_02", 20f);
                        controller.PlayOnceStep(playerRaped, animation, "A_Finish", false);
                        controller.PlayTimedStep(playerRaped, animation, "A_Finish_Idle", 10f);
                        playerRaped.Run();
                    }
                    if (npcB.npcID == 89)
                    {
                        ManRapes manRapes = new ManRapes(npcA, npcB);
                        controller.PlayOnceStep(manRapes, animation, "A_AttackToSex", false);
                        controller.PlayTimedStep(manRapes, animation, "A_Loop_01", 20f);
                        controller.PlayTimedStep(manRapes, animation, "A_Loop_02", 20f);
                        controller.PlayOnceStep(manRapes, animation, "A_Finish", false);
                        controller.PlayTimedStep(manRapes, animation, "A_Finish_Idle", 10f);
                        manRapes.Run();
                    }
                    break;
            }
            switch (npcB.npcID)
            {
                case 0:
                    switch (npcA.npcID)
                    {
                        case 10:
                            {
                                CommonSexNPC playerSex = new CommonSexNPC(npcB, npcA, sexPlace, SexCountState.Normal);
                                controller.PlayOnceStep(playerSex, animation, "A_Contact_01", false);
                                controller.PlayTimedStep(playerSex, animation, "A_Loop_01", 20f);
                                controller.PlayTimedStep(playerSex, animation, "A_Loop_02", 20f);
                                controller.PlayOnceStep(playerSex, animation, "A_Finish", false);
                                controller.PlayTimedStep(playerSex, animation, "A_Finish_Idle", 10f);
                                playerSex.Run();
                                break;
                            }
                        case 11:
                            {
                                CommonSexNPC playerSex = new CommonSexNPC(npcB, npcA, sexPlace, SexCountState.Normal);
                                controller.PlayOnceStep(playerSex, animation, "A_Contact_01", false);
                                controller.PlayTimedStep(playerSex, animation, "A_Loop_01", 20f);
                                controller.PlayTimedStep(playerSex, animation, "A_Loop_02", 20f);
                                controller.PlayOnceStep(playerSex, animation, "A_Finish", false);
                                controller.PlayTimedStep(playerSex, animation, "A_Finish_Idle", 10f);
                                playerSex.Run();
                                break;
                            }
                        case 25:
                            {
                                PlayerRaped playerRaped = new PlayerRaped(npcA, npcB);
                                controller.PlayOnceStep(playerRaped, animation, "A_AttackToSex", false);
                                controller.PlayTimedStep(playerRaped, animation, "A_Loop_01", 20f);
                                controller.PlayTimedStep(playerRaped, animation, "A_Loop_02", 20f);
                                controller.PlayOnceStep(playerRaped, animation, "A_Finish", false);
                                controller.PlayTimedStep(playerRaped, animation, "A_Finish_Idle", 10f);
                                playerRaped.Run();
                                break;
                            }
                        case 89:
                            {
                                ManRapes manRapes = new ManRapes(npcB, npcA);
                                controller.PlayOnceStep(manRapes, animation, "A_AttackToSex", false);
                                controller.PlayTimedStep(manRapes, animation, "A_Loop_01", 20f);
                                controller.PlayTimedStep(manRapes, animation, "A_Loop_02", 20f);
                                controller.PlayOnceStep(manRapes, animation, "A_Finish", false);
                                controller.PlayTimedStep(manRapes, animation, "A_Finish_Idle", 10f);
                                manRapes.Run();
                                break;
                            }
                            
                    }
                    break;
            }
        }

        [HarmonyPatch(typeof(SexManager))]
        [HarmonyPatch("CommonRapesNPC")]
        [HarmonyPrefix]

        public static void CommonRapesNPCFix(SexManager __instance, CommonStates npcA, CommonStates npcB, ref GameObject ___tmpSex, ref ManagersScript ___mn)
        {
            Transform transform = __instance.transform;
            SkeletonAnimation animation = transform.GetComponent<SkeletonAnimation>();
            //GameObject tmpSex = null;
            switch (npcA.npcID)
            {
                case 10:
                case 11:
                case 25:
                case 35:
                case 89:
                    switch (npcB.npcID)
                    {
                        case 0:
                        PlayerRaped playerRaped = new PlayerRaped(npcB, npcA);
                        controller.PlayOnceStep(playerRaped, animation, "A_AttackToSex", false);
                        controller.PlayTimedStep(playerRaped, animation, "A_Loop_01", 20f);
                        controller.PlayTimedStep(playerRaped, animation, "A_Loop_02", 20f);
                        controller.PlayOnceStep(playerRaped, animation, "A_Finish", false);
                            controller.PlayTimedStep(playerRaped, animation, "A_Finish_Idle", 10f);
                            playerRaped.Run();
                            break;
                    }
                    break;
            }
        }
    }
}



