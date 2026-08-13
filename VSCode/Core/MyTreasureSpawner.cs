using System;
using FortRise;
using HarmonyLib;
using TowerFall;

namespace TFModFortRisePickupArrowBomb
{
  /// <summary>
  /// L'ecriture directe dans les taux du TreasureSpawner, gardee POUR LES ESSAIS.
  ///
  /// C'est l'ancienne facon de faire, et elle a un defaut et une qualite. Le defaut :
  /// elle court-circuite tout ce qui vient apres le constructeur - exclusions de
  /// variantes, jeu d'objets de la tour, melange de fleches - puisqu'elle ecrit la
  /// derniere. La qualite, qui est exactement ce qu'on veut pour essayer un pickup :
  /// elle le fait tomber a coup sur, sans dependre d'un tirage.
  ///
  /// Elle ne tourne donc plus qu'en mode TEST. En mode normal c'est TreasureRates qui
  /// decide, par l'API de FortRise, et ce postfix ne touche a rien.
  /// </summary>
  public class MyTreasureSpawner : IHookable
  {
    /// <summary>
    /// Poids donne a la bombe en mode TEST.
    ///
    /// Vingt et non un : les taux vanilla d'une tour totalisent plusieurs dizaines,
    /// et un poids de 1 ne sortirait qu'un coffre sur trente. Il ne s'agit pas ici de
    /// doser mais de voir le pickup.
    /// </summary>
    private const float TestRate = 20f;

    public static void Load(IHarmony harmony)
    {
      harmony.Patch(
          AccessTools.DeclaredConstructor(typeof(TreasureSpawner), [typeof(Session), typeof(VersusTowerData)]),
          prefix: new HarmonyMethod(TreasureSpawner_ctor_Prefix),
          postfix: new HarmonyMethod(TreasureSpawner_ctor_Postfix)
      );
    }

    /// <summary>
    /// Donne a la tour la table de taux qu'il faut, avant que le spawner ne la lise.
    ///
    /// Les tours des MODS se fabriquent une copie de DefaultTreasureChances au
    /// chargement... et ce chargement a lieu AVANT que FortRise n'y ajoute les
    /// pickups des mods (RiseCore.Initialize precede ExtendTreasures). Leur copie
    /// s'arrete donc avant notre case, et le taux de notre pickup y vaut zero : il ne
    /// tombait jamais sur une tour de mod, quel que soit le masque - le bug ne se
    /// voyait pas sur les tours du jeu, qui partagent la table commune.
    ///
    /// En prefix et non en postfix : le constructeur lit la table pour calculer les
    /// taux, il faut donc l'avoir corrigee avant qu'il ne commence.
    /// </summary>
    public static void TreasureSpawner_ctor_Prefix(VersusTowerData versusTowerData)
    {
      try
      {
        if (versusTowerData == null)
        {
          return;
        }

        float[] canonical = TreasureSpawner.DefaultTreasureChances;
        float[] chances = versusTowerData.TreasureChances;

        // Une tour du jeu n'a pas de table a elle : elle retombe sur la table commune,
        // que ExtendTreasures a deja renseignee. Rien a faire.
        if (chances == null || ReferenceEquals(chances, canonical))
        {
          return;
        }

        if (chances.Length < canonical.Length)
        {
          Array.Resize(ref chances, canonical.Length);
          versusTowerData.TreasureChances = chances;
        }

        chances[(int)LaserBombPickup.ArrowBombMeta.Pickups] = Rarity.Unit;
      }
      catch (Exception e)
      {
        Logger.Error("MyTreasureSpawner.TreasureSpawner_ctor_Prefix: " + e);
      }
    }

    public static void TreasureSpawner_ctor_Postfix(TreasureSpawner __instance)
    {
      try
      {
        if (TFModFortRisePickupArrowBombModule.Settings.periodicity != "Test")
        {
          return;
        }

        Pickups pickup = LaserBombPickup.ArrowBombMeta.Pickups;

        // Meme en mode TEST, la variante decide : sans quoi on ne pourrait plus
        // essayer une partie SANS bombe.
        float rate = TFModFortRisePickupArrowBombModule.activated() ? TestRate : 0f;
        __instance.TreasureRates[(int)pickup] = rate;

        Logger.Info($"[Test] bombe forcee a {rate}");
      }
      catch (Exception e)
      {
        Logger.Error("MyTreasureSpawner.TreasureSpawner_ctor_Postfix: " + e);
      }
    }
  }
}
