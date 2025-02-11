using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReikaP.Patches;
using Spine.Unity;
using UnityEngine;
using Spine;
//Set a reference for nm088's framework when it's ready


namespace ReikaP
{
    [BepInPlugin(modGUID, modName, modVersion)]
    [BepInDependency("HFramework", "1.0.0")]
    public class ReikaBase : BaseUnityPlugin
    {
        private const string modGUID = "Ex.MadIslandUPE";
        private const string modName = "Ex Universal Pregnancy Enabler";
        private const string modVersion = "1.0.0";


        private readonly Harmony harmony = new Harmony(modGUID);

        private static ReikaBase Instance;

        internal ManualLogSource mls;



        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            
            mls.LogInfo("Mad Island Universal Pregnancy Enabler");
            string location = ((BaseUnityPlugin)Instance).Info.Location;
            string text = "ReikaP.dll";

            if (text == null)
            {
                mls.LogError("Failed to load mod!");
                return;
            }

            harmony.PatchAll(typeof(ReikaBase));
            //harmony.PatchAll(typeof(AtlasPatch)); //Set patch for any eventual hooks into nm088's code.
            harmony.PatchAll(typeof(SexManager));
            harmony.PatchAll(typeof(SpikePatch));
            harmony.PatchAll(typeof(AltPatch));
            mls.LogInfo("Fill them up.");
        }
    }
}
