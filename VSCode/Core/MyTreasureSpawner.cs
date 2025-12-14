using System;
using System.Collections.Generic;
using FortRise;
using HarmonyLib;
using Microsoft.Xna.Framework;
using MonoMod.Utils;
using TowerFall;

namespace TFModFortRisePickupArrowBomb
{

  public class MyTreasureSpawner : IHookable
  {
    public static void Load(IHarmony harmony)
    {
      harmony.Patch(
          AccessTools.DeclaredConstructor(typeof(TreasureSpawner), [typeof(Session), typeof(VersusTowerData)]),
          postfix: new HarmonyMethod(TreasureSpawner_ctor_Postfix)
      );
      //harmony.Patch(
      //    AccessTools.DeclaredMethod(typeof(TreasureSpawner), nameof(TreasureSpawner.GetChestSpawnsForLevel)),
      //    postfix: new HarmonyMethod(GetChestSpawnsForLevel_patch)
      //);
    }

    public static void TreasureSpawner_ctor_Postfix(TreasureSpawner __instance)
    {
      Logger.Info("TreasureSpawner_ctor_Postfix");
      var ArrowBomb = LaserBombPickup.ArrowBombMeta.Pickups;
      Logger.Info($"ArrowBomb != null {LaserBombPickup.ArrowBombMeta.Name} {(int)ArrowBomb}");

      if (!TFModFortRisePickupArrowBombModule.activated()) {
        __instance.TreasureRates[(int)ArrowBomb] = 0f;
        return;
      }

      Random rnd = new Random();
      int draw;
      if (TFModFortRisePickupArrowBombModule.Settings.periodicity == "Test")
      {
        draw = 1;
      }
      else
      {
        draw = rnd.Next(0, TFModFortRisePickupArrowBombModule.Settings.treasureRate);
      }
      Logger.Info($"draw = {draw}");

      if (draw == 1)
      {
        __instance.TreasureRates[(int)ArrowBomb] = 1f;
      }
    }

    //public static void GetChestSpawnsForLevel_patch(
    //    TreasureSpawner __instance,
    //    List<Vector2> chestPositions,
    //    List<Vector2> bigChestPositions,
    //    List<TreasureChest> __result
    //    )
    //{

      //  //if (chestSpawnsForLevel.Count == 0) {
      //  if (__result.Count == 0) {
      //    return;
      //  }

      //  if (!TFModFortRisePickupArrowBombModule.activated()) return;

      //  if (MySession.NbLaserBombPickupActivated == 0) {
      //    Random rnd = new Random();
      //    int draw;
      //    if (TFModFortRisePickupArrowBombModule.Settings.periodicity == "Test") {
      //      draw = 1;
      //    } else {
      //      draw = rnd.Next(0, TFModFortRisePickupArrowBombModule.Settings.treasureRate); 
      //    }
      //    if (draw == 1) {
      //      var dynData = DynamicData.For(__result[0]);
      //      List<Pickups> pickups = (List<Pickups>)dynData.Get("pickups");
      //      pickups[0] = ArrowBomb;
      //      MySession.NbLaserBombPickupActivated++;
      //      dynData.Dispose();
      //    }
      //  }
      //}
  }
}