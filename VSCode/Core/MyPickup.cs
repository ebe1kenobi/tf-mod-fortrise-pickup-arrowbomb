//using System;
//using FortRise;
//using HarmonyLib;
//using Microsoft.Xna.Framework;
//using TowerFall;

//namespace TFModFortRisePickupArrowBomb
//{
//  public class MyPickup : IHookable
//  {
//    public static void Load(IHarmony harmony)
//    {
//      harmony.Patch(
//          AccessTools.DeclaredMethod(typeof(Pickup), nameof(Pickup.CreatePickup)),
//          postfix: new HarmonyMethod(CreatePickup_patch)
//      );
//    }

//    public static Pickup CreatePickup_patch(
//        Pickup __instance, Vector2 position, Vector2 targetPosition, Pickups type, int playerIndex)
//    {
//      if (type == ModRegisters.PickupType<LaserBombPickup>()) // ID personnalisé pour notre LaserBomb
//      {
//        return new LaserBombPickup(position, targetPosition, playerIndex);
//      }
//      return orig(position, targetPosition, type, playerIndex);
//    }
//  }
//}