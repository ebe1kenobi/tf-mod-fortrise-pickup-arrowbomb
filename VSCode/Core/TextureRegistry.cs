using FortRise;
using Monocle;
using TowerFall;

namespace TFModFortRisePickupArrowBomb;

// Optional way to use textures
public class TextureRegistry : IRegisterable
{
    // Variants
    public static ISubtextureEntry ArrowBomb { get; private set; } = null!;

    public static void Register(IModContent content, IModRegistry registry)
    {
      ArrowBomb = registry.Subtextures.RegisterTexture(
                content.Root.GetRelativePath("Content/Atlas/arrowbomb.png")
            );
    }
}