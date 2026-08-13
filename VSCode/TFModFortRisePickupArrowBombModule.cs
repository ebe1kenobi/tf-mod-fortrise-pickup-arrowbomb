using System;
using System.Diagnostics;
using FortRise;
using Microsoft.Extensions.Logging;
using Monocle;
using TowerFall;

//Instance.Context.Interop.GetMod

namespace TFModFortRisePickupArrowBomb
{
  public class TFModFortRisePickupArrowBombModule : Mod
  {
    public static TFModFortRisePickupArrowBombModule Instance;

    private static Type[] Registerables = [
        typeof(LaserBombPickup),
        typeof(TextureRegistry),
        typeof(Variants),
        // Apres le pickup : le hook de tour cite sa valeur Pickups, qui n'existe
        // qu'une fois l'enregistrement fait.
        typeof(TreasureRates)
    ];
    internal Type[] Hookables = [
        //typeof(MyPickup),
        //typeof(MySession),
        typeof(MyTreasureSpawner),
        typeof(MyVariantToggle),
    ];
    public static TFModFortRisePickupArrowBombSettings Settings => Instance.GetSettings<TFModFortRisePickupArrowBombSettings>()!;
    public Atlas Atlas;
    //public override Type SettingsType => typeof(TFModFortRisePickupArrowBombSettings);
    //public static TFModFortRisePickupArrowBombSettings Settings => (TFModFortRisePickupArrowBombSettings)Instance.InternalSettings;
    public TFModFortRisePickupArrowBombModule(IModContent content, IModuleContext context, ILogger logger) : base(content, context, logger)
    {
      if (!Debugger.IsAttached)
      {
        //Debugger.Launch(); // Proposera d’attacher Visual Studio
      }
      Instance = this;
      TFModFortRisePickupArrowBomb.Logger.Init(logger);

      
      foreach (var hookable in Hookables)
      {
        hookable.GetMethod(nameof(IHookable.Load))!.Invoke(null, [context.Harmony]);
      }

      foreach (var registerable in Registerables)
      {
        registerable.GetMethod(nameof(IRegisterable.Register))!.Invoke(null, [content, context.Registry]);
      }
    }

    /// <summary>
    /// Ecrit les reglages sur le disque tout de suite.
    ///
    /// FortRise ne les enregistre qu'en sortant de SON ecran d'options : un reglage
    /// change depuis la fenetre de la variante ne vivrait qu'en memoire et serait
    /// perdu en quittant. SaveSettings est internal cote FortRise, d'ou la reflexion.
    /// </summary>
    public static void SaveSettingsNow()
    {
      if (Instance == null)
      {
        return;
      }

      try
      {
        var method = typeof(Mod).GetMethod("SaveSettings",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        method?.Invoke(Instance, null);
      }
      catch (Exception e)
      {
        TFModFortRisePickupArrowBomb.Logger.Info($"[Settings] sauvegarde immediate impossible : {e.Message}");
      }
    }

    public override ModuleSettings CreateSettings()
    {
      return new TFModFortRisePickupArrowBombSettings();
    }

    //public override void LoadContent()
    //{
    //  Atlas = Content.LoadAtlas("Atlas/atlas.xml", "Atlas/atlas.png"); //TODO change the image
    //}

    public static bool activated() {
      return Variants.ArrowBomb.IsActive() || TFModFortRisePickupArrowBombModule.Settings.activated;
    }


  }
}
