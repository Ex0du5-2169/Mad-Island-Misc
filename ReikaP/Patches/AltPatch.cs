using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReikaP.Patches
{
    internal class AltPatch
    {
        [HarmonyPatch(typeof(NPCManager))]
        [HarmonyPatch("IsPerfumeNPC")]
        [HarmonyPostfix]

        public static void PerfumeFix(NPCManager __instance, ref bool __result, CommonStates common)
        {
            //Should be fairly obvious, allows use of the perfume item on any NPC added to this list.
            if (!__result)
            {
                switch (common.npcID)
                {
                    case 0:
                    case 5:
                    case 6:
                    case 11:
                    case 12:
                    case 14:
                    case 113:
                    case 114:
                    case 115:
                    case 116:
                    case 89:
                    case 90:
                    case 91:
                        __result = true;
                        break;
                }

            }



        }
        /*[HarmonyPatch(typeof(NPCManager))]
        [HarmonyPatch("AssaultTargetableCheck")]
        [HarmonyPostfix]

        public static void AssaultTargetFix(NPCManager __instance, ref bool __result, CommonStates common)
        {

            if (!__result)
            {
                switch (common.npcID)
                {
                    case 0:
                    case 5:
                    case 6:
                    case 113:
                    case 114:
                    case 115:
                        __result = true;
                        break;
                }

            }



        }*/
    }
}
