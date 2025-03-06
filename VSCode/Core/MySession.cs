
namespace TFModFortRisePickupArrowBomb
{
  public class MySession
  {
    public static int NbLaserBombPickupActivated { get; set; }

    internal static void Load()
    {
      On.TowerFall.Session.StartGame += StartGame_patch;
      On.TowerFall.Session.GotoNextRound += GotoNextRound_patch;
    }

    internal static void Unload()
    {
      On.TowerFall.Session.StartGame -= StartGame_patch;
      On.TowerFall.Session.GotoNextRound -= GotoNextRound_patch;
    }
    public MySession()
    {
    }

    public static void StartGame_patch(On.TowerFall.Session.orig_StartGame orig, global::TowerFall.Session self)
    {
      if (TFModFortRisePickupArrowBombModule.Settings.periodicity == TFModFortRisePickupArrowBombSettings.OncePerMatch)
      {
        NbLaserBombPickupActivated = 0;
      }
      orig(self);
    }

    public static void GotoNextRound_patch(On.TowerFall.Session.orig_GotoNextRound orig, global::TowerFall.Session self)
    {
      if (TFModFortRisePickupArrowBombModule.Settings.periodicity == TFModFortRisePickupArrowBombSettings.OncePerRound)
      {
        NbLaserBombPickupActivated = 0;
      }
      if (TFModFortRisePickupArrowBombModule.Settings.periodicity == TFModFortRisePickupArrowBombSettings.Test)
      {
        NbLaserBombPickupActivated = 0;
      }
      orig(self);
    }
  }
}
