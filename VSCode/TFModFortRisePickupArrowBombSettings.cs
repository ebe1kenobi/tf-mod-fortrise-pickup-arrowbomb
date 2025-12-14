using FortRise;
using TowerFall;

namespace TFModFortRisePickupArrowBomb
{
  public class TFModFortRisePickupArrowBombSettings : ModuleSettings
  {

    public override void Create(ISettingsCreate settings)
    {
      settings.CreateOnOff("Pickup activated even \n\nwhen variant is not selected", activated, (x) => activated = x);
      settings.CreateOptions("Periodicity", periodicity, ["Normal", "Test"], (x) => periodicity = x.Item1);
      settings.CreateNumber("Treasure Rate 1 chance on N, choose N", treasureRate, (x) => treasureRate = x, 10, 100);
      settings.CreateNumber("Number of Arrow", numberArrow, (x) => numberArrow = x, 1, 100);
      settings.CreateOptions("Arrow Type", arrowType, ["Normal", "Bomb", "SuperBomb", "Laser", "Bramble", "Drill", "Bolt", "Toy", "Feather", "Trigger", "Prism"], (x) => arrowType = x.Item1);

    }

    //[SettingsName("Pickup activated even \n\nwhen variant is not selected")]
    public bool activated { get; set; } = false;

    //public const int OncePerMatch = 0;
    //public const int OncePerRound = 1;
    //public const int Test = 2;
    //[SettingsOptions("OncePerMatch", "OncePerRound", "Test")]
    public string periodicity { get; set; } = "OncePerMatch";

    //[SettingsName("Treasure Rate 1 chance on N, choose N")]
    //[SettingsNumber(10, 100)]
    public int treasureRate { get; set; } = 100;

    //[SettingsName("Number of Arrow")]
    //[SettingsNumber(1, 100)]
    public int numberArrow { get; set; } = 15;

    //public const int Normal = 0;
    //public const int Bomb = 1;
    //public const int SuperBomb = 2;
    //public const int Laser = 3;
    //public const int Bramble = 4;
    //public const int Drill = 5;
    //public const int Bolt = 6;
    //public const int Toy = 7;
    //public const int Feather = 8;
    //public const int Trigger = 9;
    //public const int Prism = 10;
    //[SettingsName("Arrow Type")]
    //[SettingsOptions("Normal", "Bomb", "SuperBomb", "Laser", "Bramble", "Drill", "Bolt", "Toy", "Feather", "Trigger", "Prism")]
    public string arrowType { get; set; } = "Normal";
  }
}
