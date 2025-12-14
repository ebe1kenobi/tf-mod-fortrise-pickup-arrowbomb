
//using FortRise;
//using HarmonyLib;
//using TowerFall;

//namespace TFModFortRisePickupArrowBomb
//{
//  public class MySession : IHookable
//  {
//    public static int NbLaserBombPickupActivated { get; set; }

//    public static void Load(IHarmony harmony)
//    {
//      harmony.Patch(
//          AccessTools.DeclaredMethod(typeof(Session), nameof(Session.StartGame)),
//          prefix: new HarmonyMethod(StartGame_patch)
//      );
//      harmony.Patch(
//          AccessTools.DeclaredMethod(typeof(Session), nameof(Session.GotoNextRound)),
//          prefix: new HarmonyMethod(GotoNextRound_patch)
//      );
//    }

//    public static void StartGame_patch(Session __instance)
//    {
//      if (TFModFortRisePickupArrowBombModule.Settings.periodicity == "OncePerMatch")
//      {
//        NbLaserBombPickupActivated = 0;
//      }
//    }

//    public static void GotoNextRound_patch(Session __instance)
//    {
//      if (TFModFortRisePickupArrowBombModule.Settings.periodicity == "OncePerRound")
//      {
//        NbLaserBombPickupActivated = 0;
//      }
//      if (TFModFortRisePickupArrowBombModule.Settings.periodicity == "Test")
//      {
//        NbLaserBombPickupActivated = 0;
//      }
//    }
//  }
//}
