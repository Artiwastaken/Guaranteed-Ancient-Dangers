using HarmonyLib;
using RimWorld;
using Verse;

namespace Shrinez
{
    // Ensures GenStep_ScatterShrines never skips on non-starting maps (forces shrine/temple generation attempt everywhere).
    [StaticConstructorOnStartup]
    public static class ShrineNoSkipPatch
    {
        static ShrineNoSkipPatch()
        {
            var harmony = new Harmony("com.arti.shrinez.noskip");
            var target = AccessTools.Method(typeof(GenStep_ScatterShrines), "ShouldSkipMap");
            harmony.Patch(target, prefix: new HarmonyMethod(typeof(ShrineNoSkipPatch), nameof(ShouldSkipMapPrefix)));
        }

        // Prefix completely overrides the original: always return false (do not skip).
        private static bool ShouldSkipMapPrefix(Map map, ref bool __result)
        {
            __result = false; // never skip
            return false;     // suppress original method
        }
    }
}
