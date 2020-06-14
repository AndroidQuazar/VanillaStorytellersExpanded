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

    public class VanillaStorytellersExpanded : Mod
    {
        public VanillaStorytellersExpanded(ModContentPack content) : base(content)
        {
            harmonyInstance = new Harmony("OskarPotocki.VanillaStorytellersExpanded");

            // Parsing
            ParseHelper.Parsers<TechLevelRange>.Register(s => TechLevelRange.FromString(s));
        }

        public static Harmony harmonyInstance;

    }

}
