using FortRise;

namespace TFModFortRisePickupArrowBomb
{
  public class Variants : IRegisterable
  {
    /// <summary>
    /// Le libelle de la case, et le seul repere pour la reconnaitre dans l'ecran des
    /// variantes : VariantToggle n'expose pas l'identifiant du mod.
    /// </summary>
    public const string TITLE = "ArrowBomb";

    public static IVariantEntry ArrowBomb = null!;

    public static void Register(IModContent content, IModRegistry registry)
    {
      ArrowBomb = registry.Variants.RegisterVariant("ArrowBomb", new()
      {
        // Header commun a tous mes mods : sans lui FortRise retombe sur le nom du
        // mod et chacun cree sa propre colonne dans l'ecran des variantes.
        Header = "EBE1 MODS",
        Title = TITLE,
        Flags = CustomVariantFlags.None,
        Icon = TextureRegistry.ArrowBomb
      });
    }
  }
}
