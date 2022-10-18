﻿namespace MauiAnimation
{
    using System;
    using System.Threading.Tasks;

    public class ColorAnimation : AnimationBase
    {
        public static readonly BindableProperty ToColorProperty =
            BindableProperty.Create(nameof(ToColor), typeof(Color), typeof(ColorAnimation), Colors.Transparent,
                BindingMode.TwoWay, null);

        public Color ToColor
        {
            get { return (Color)GetValue(ToColorProperty); }
            set { SetValue(ToColorProperty, value); }
        }

        protected override Task BeginAnimation()
        {
            if (Target == null)
            {
                throw new NullReferenceException("Null Target property.");
            }

            var fromColor = Target.BackgroundColor;

            return Task.Run(() =>
            {
                Target.Dispatcher.Dispatch(async () =>
                {
                    await Target.ColorTo(fromColor, ToColor, c => Target.BackgroundColor = c, Convert.ToUInt32(Duration));
                });
            });
        }
    }
}