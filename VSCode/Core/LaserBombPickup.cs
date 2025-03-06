using FortRise;
using Microsoft.Xna.Framework;
using Monocle;
using TowerFall;
using System;
using MonoMod.Utils;

namespace TFModFortRisePickupArrowBomb
{
  [CustomPickup("LaserBombPickup", "0.0")]
  public class LaserBombPickup : BombPickup
  {
    public LaserBombPickup(Vector2 position, Vector2 targetPosition, int playerIndex)
        : base(position, targetPosition, playerIndex)
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

      for (int i = 0; i < TFModFortRisePickupArrowBombModule.Settings.numberArrow; i++)
      {
        float angle = i * (MathHelper.TwoPi / TFModFortRisePickupArrowBombModule.Settings.numberArrow);
        Arrow arrow = Arrow.Create((ArrowTypes)TFModFortRisePickupArrowBombModule.Settings.arrowType, this, Position, angle, null, null);
        Level.Add(arrow);
      }

      var dynData = DynamicData.For(this);
      Explosion.Spawn(Level, Position, (int)dynData.Get("playerIndex"), false, false, true);
      dynData.Dispose();
      RemoveSelf();
    }
  }
}