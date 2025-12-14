using FortRise;

namespace TFModFortRisePickupArrowBomb
{
  public class Variants : IRegisterable
  {
    public static IVariantEntry ArrowBomb = null!;

    public static void Register(IModContent content, IModRegistry registry)
    {
      ArrowBomb = registry.Variants.RegisterVariant("ArrowBomb", new()
      {
        Title = "ArrowBomb",
        Flags = CustomVariantFlags.None,
        Icon = TextureRegistry.ArrowBomb
      });
    }
  }
}
