using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using Verse;
using RimWorld;
using HarmonyLib;

namespace VanillaStorytellersExpanded
{

    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {

        static HarmonyPatches()
        {
            #if DEBUG
                Harmony.DEBUG = true;
            #endif

            VanillaStorytellersExpanded.harmonyInstance.PatchAll();

            // Research Tree
            if (ModCompatibilityCheck.ResearchTree)
            {
                var researchNode = GenTypes.GetTypeInAnyAssembly("FluffyResearchTree.ResearchNode", "FluffyResearchTree");
                if (researchNode != null)
                {
                    Patch_FluffyResearchTree_ResearchNode.instanceType = researchNode;
                    VanillaStorytellersExpanded.harmonyInstance.Patch(AccessTools.Property(researchNode, "Available").GetGetMethod(), postfix: new HarmonyMethod(typeof(Patch_FluffyResearchTree_ResearchNode.manual_get_Available), "Postfix"));
                    VanillaStorytellersExpanded.harmonyInstance.Patch(AccessTools.Method(researchNode, "Draw"), transpiler: new HarmonyMethod(typeof(Patch_FluffyResearchTree_ResearchNode.manual_Draw), "Transpiler"));
                }
                else
                    Log.Error("Could not find type FluffyResearchTree.ResearchNode in Research Tree");
            }

            // ResearchPal (pretty much identical code-wise to research tree as far as we are concerned, other than namespace)
            if (ModCompatibilityCheck.ResearchPal)
            {
                var researchNode = GenTypes.GetTypeInAnyAssembly("ResearchPal.ResearchNode", "ResearchPal");
                if (researchNode != null)
                {
                    Patch_FluffyResearchTree_ResearchNode.instanceType = researchNode;
                    VanillaStorytellersExpanded.harmonyInstance.Patch(AccessTools.Property(researchNode, "Available").GetGetMethod(), postfix: new HarmonyMethod(typeof(Patch_FluffyResearchTree_ResearchNode.manual_get_Available), "Postfix"));
                    VanillaStorytellersExpanded.harmonyInstance.Patch(AccessTools.Method(researchNode, "Draw"), transpiler: new HarmonyMethod(typeof(Patch_FluffyResearchTree_ResearchNode.manual_Draw), "Transpiler"));
                }
                else
                    Log.Error("Could not find type ResearchPal.ResearchNode in ResearchPal");
            }

            // RimCities
            if (ModCompatibilityCheck.RimCities)
            {
                var worldGenStepCities = GenTypes.GetTypeInAnyAssembly("Cities.WorldGenStep_Cities", "Cities");
                if (worldGenStepCities != null)
                    VanillaStorytellersExpanded.harmonyInstance.Patch(AccessTools.Method(worldGenStepCities, "GenerateFresh"), new HarmonyMethod(typeof(Patch_Cities_WorldGenStep_Cities.manual_GenerateFresh), "Prefix"));
            }
        }

    }

}
