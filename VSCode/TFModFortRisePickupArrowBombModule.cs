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
        typeof(Variants)

    ];
    internal Type[] Hookables = [
        //typeof(MyPickup),
        //typeof(MySession),
        typeof(MyTreasureSpawner),
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
      TFModFortRisePickupArrowBomb.Logger.Init("ArrowBomb");

      
      foreach (var hookable in Hookables)
      {
        hookable.GetMethod(nameof(IHookable.Load))!.Invoke(null, [context.Harmony]);
      }

      foreach (var registerable in Registerables)
      {
        registerable.GetMethod(nameof(IRegisterable.Register))!.Invoke(null, [content, context.Registry]);
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
