using FortRise;
using TowerFall;

namespace TFModFortRisePickupArrowBomb
{
  public class TFModFortRisePickupArrowBombSettings : ModuleSettings
  {

    [SettingsName("Pickup activated even \n\nwhen variant is not selected")]
    public bool activated = false;

    public const int OncePerMatch = 0;
    public const int OncePerRound = 1;
    public const int Test = 2;
    [SettingsOptions("OncePerMatch", "OncePerRound", "Test")]
    public int periodicity = 0;

    [SettingsName("Treasure Rate 1 chance on N, choose N")]
    [SettingsNumber(10, 100)]
    public int treasureRate = 100;

    [SettingsName("Number of Arrow")]
    [SettingsNumber(1, 100)]
    public int numberArrow = 15;

    public const int Normal = 0;
    public const int Bomb = 1;
    public const int SuperBomb = 2;
    public const int Laser = 3;
    public const int Bramble = 4;
    public const int Drill = 5;
    public const int Bolt = 6;
    public const int Toy = 7;
    public const int Feather = 8;
    public const int Trigger = 9;
    public const int Prism = 10;
    [SettingsName("Arrow Type")]
    [SettingsOptions("Normal", "Bomb", "SuperBomb", "Laser", "Bramble", "Drill", "Bolt", "Toy", "Feather", "Trigger", "Prism")]
    public int arrowType;
  }
}
