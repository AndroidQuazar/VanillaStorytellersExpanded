using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;
using RimWorld;
using HarmonyLib;

namespace VanillaStorytellersExpanded
{

    public static class Patch_ResearchProjectDef
    {

        [HarmonyPatch(typeof(ResearchProjectDef), nameof(ResearchProjectDef.CanStartNow), MethodType.Getter)]
        public static class get_CanStartNow
        {

            public static void Postfix(ResearchProjectDef __instance, ref bool __result)
            {
                if (__result && !CustomStorytellerUtility.TechLevelAllowed(__instance.techLevel))
                {
                    __result = false;
                }
            }

        }

    }

}
