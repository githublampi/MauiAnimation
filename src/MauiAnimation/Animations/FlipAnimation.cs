﻿namespace MauiAnimation
{
    using System;
    using System.Threading.Tasks;

    public class FlipAnimation : AnimationBase
    {
        public enum FlipDirection
        {
            Left,
            Right
        }

        public static readonly BindableProperty DirectionProperty =
          BindableProperty.Create(nameof(Direction), typeof(FlipDirection), typeof(FlipAnimation), FlipDirection.Right,
              BindingMode.TwoWay, null);

        public FlipDirection Direction
        {
            get { return (FlipDirection)GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
        }

        protected override Task BeginAnimation()
        {
            if (Target == null)
            {
                throw new NullReferenceException("Null Target property.");
            }

            return Task.Run(() =>
            {
                Target.Dispatcher.Dispatch(() =>
                {
                    Target.Animate("Flip", Flip(), 16, Convert.ToUInt32(Duration));
                });
            });
        }

        internal Animation Flip()
        {
            var animation = new Animation();

            animation.WithConcurrent((f) => Target.Opacity = f, 0.5, 1);
            animation.WithConcurrent((f) => Target.RotationY = f, (Direction == FlipDirection.Left) ? 90 : -90, 0, Microsoft.Maui.Easing.Linear);

            return animation;
        }
    }
}