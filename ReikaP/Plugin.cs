﻿using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReikaP.Patches;

namespace ReikaP
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class ReikaBase : BaseUnityPlugin
    {
        private const string modGUID = "Ex.ReikaP";
        private const string modName = "Ex Reika Pregnancy Enabler";
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

            mls.LogInfo("Reika Pregnancy Enabler");
            string location = ((BaseUnityPlugin)Instance).Info.Location;
            string text = "ReikaP.dll";
            string text2 = location.TrimEnd(text.ToCharArray());

            harmony.PatchAll(typeof(ReikaBase));
            harmony.PatchAll(typeof(SexManagerPatch));
            harmony.PatchAll(typeof(AltPatch));
            mls.LogInfo("Fill them up.");
        }
    }
}
