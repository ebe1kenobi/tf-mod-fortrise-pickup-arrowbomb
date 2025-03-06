using System;
using FortRise;
using Monocle;
using TowerFall;

namespace TFModFortRisePickupArrowBomb
{
  [Fort("com.ebe1.kenobi.TFModFortRisePickupArrowBomb", "TFModFortRisePickupArrowBomb")]
  public class TFModFortRisePickupArrowBombModule : FortModule
  {
    public static TFModFortRisePickupArrowBombModule Instance;
    public Atlas Atlas;
    public override Type SettingsType => typeof(TFModFortRisePickupArrowBombSettings);
    public static TFModFortRisePickupArrowBombSettings Settings => (TFModFortRisePickupArrowBombSettings)Instance.InternalSettings;
    public TFModFortRisePickupArrowBombModule()
    {
      Instance = this;
      //Logger.Init("TFModFortRisePickupArrowBombLog");
    }

    public override void Load()
    {
      MyPickup.Load();
      MySession.Load();
      MyTreasureSpawner.Load();
    }

    public override void Unload()
    {
      MyPickup.Unload();
      MySession.Unload();
      MyTreasureSpawner.Unload();
      Instance = null;
    }

    public override void LoadContent()
    {
      Atlas = Content.LoadAtlas("Atlas/atlas.xml", "Atlas/atlas.png"); //TODO change the image
    }

    public static bool activated() {
      return VariantManager.GetCustomVariant("ArrowBomb") || TFModFortRisePickupArrowBombModule.Settings.activated;
    }

    public override void OnVariantsRegister(VariantManager manager, bool noPerPlayer = false)
    {
      var icon = new CustomVariantInfo(
          "ArrowBomb", VariantManager.GetVariantIconFromName("ArrowBomb", Atlas), 
          CustomVariantFlags.None
          );
      manager.AddVariant(icon);
    }
  }
}
