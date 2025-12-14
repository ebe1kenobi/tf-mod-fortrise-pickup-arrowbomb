using FortRise;
using Microsoft.Xna.Framework;
using Monocle;
using TowerFall;
using System;
using MonoMod.Utils;

namespace TFModFortRisePickupArrowBomb
{
  public class LaserBombPickup : BombPickup, IRegisterable
  {
    public static IPickupEntry ArrowBombMeta = null!;

    public static void Register(IModContent content, IModRegistry registry)
    {
      ArrowBombMeta = registry.Pickups.RegisterPickups("LaserBombPickup", new()
      {
        Name = "LaserBombPickup",
        PickupType = typeof(LaserBombPickup)
      });
    }

    public LaserBombPickup(Vector2 position, Vector2 targetPosition) //todo add playerindex!! but not supported by fortrise 5 for now
        : base(position, targetPosition, -1)
    {
      var dynData = DynamicData.For(this);
      Sprite<int> image = (Sprite<int>)dynData.Get("image");
      image.Color = Color.Green;
      dynData.Dispose();

    }

    public override void FinishUnpack(Tween tt)
    {
      this.Collidable = false;
      Tween tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.CubeIn, 40, true);
      tween.OnUpdate = delegate (Tween t)
      {
        var dynData = DynamicData.For(this);
        Sprite<int> image = (Sprite<int>)dynData.Get("image");
        image.Scale = Vector2.One * MathHelper.Lerp(1f, 3f, t.Eased);
        image.Rate = MathHelper.Lerp(1f, 4f, t.Eased);
        image.Rotation = MathHelper.Lerp(0f, 6.2831855f, t.Eased);
        //todo  grossir la bombe bcp
        dynData.Dispose();
      };
      tween.OnComplete = new Action<Tween>(this.ExplodeArrow);
      base.Add(tween);
      BombPickup.SFXNewest = this;
      Sounds.sfx_bombChestLoop.Play(base.X, 1f);
    }

    protected void ExplodeArrow(Tween t = null)
    {
      Sounds.pu_bombArrowExplode.Play(X, 1f);

      ArrowTypes arrowType = 0;
      switch (TFModFortRisePickupArrowBombModule.Settings.arrowType) {
        case "Bomb":
          arrowType = ArrowTypes.Bomb;
          break;
        case "SuperBomb":
          arrowType = ArrowTypes.SuperBomb;
          break;
        case "Laser":
          arrowType = ArrowTypes.Laser;
          break;
        case "Bramble":
          arrowType = ArrowTypes.Bramble;
          break;
        case "Drill":
          arrowType = ArrowTypes.Drill;
          break;
        case "Bolt":
          arrowType = ArrowTypes.Bolt;
          break;
        case "Toy":
          arrowType = ArrowTypes.Toy;
          break;
        case "Feather":
          arrowType = ArrowTypes.Feather;
          break;
        case "Trigger":
          arrowType = ArrowTypes.Trigger;
          break;
        case "Prism":
          arrowType = ArrowTypes.Prism;
          break;
        case "Normal":
        default:
          arrowType = ArrowTypes.Feather;
          break;
      }
      for (int i = 0; i < TFModFortRisePickupArrowBombModule.Settings.numberArrow; i++)
      {
        float angle = i * (MathHelper.TwoPi / TFModFortRisePickupArrowBombModule.Settings.numberArrow);
        Arrow arrow = Arrow.Create(arrowType, this, Position, angle, null, null);
        Level.Add(arrow);
      }

      var dynData = DynamicData.For(this);
      Explosion.Spawn(Level, Position, (int)dynData.Get("playerIndex"), false, false, true);
      dynData.Dispose();
      RemoveSelf();
    }
  }
}