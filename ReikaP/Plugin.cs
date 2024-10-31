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


namespace ReikaP
{
    [BepInPlugin(modGUID, modName, modVersion)]
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
            string text2 = location.TrimEnd(text.ToCharArray());
            string text3 = text2 + "MIUPEAssets";
            AssetBundle val = AssetBundle.LoadFromFile(text3);
            if (val == null)
            {
                mls.LogError("Failed to load assets!");
                return;
            }



            harmony.PatchAll(typeof(ReikaBase));
            harmony.PatchAll(typeof(SpikePatch));
            harmony.PatchAll(typeof(AltPatch));
            mls.LogInfo("Fill them up.");
        }
    }
}
