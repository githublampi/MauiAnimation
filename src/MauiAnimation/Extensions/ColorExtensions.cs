﻿// https://developer.xamarin.com/samples/xamarin-forms/userinterface/animation/custom/
namespace MauiAnimation
{
    using System;
    using System.Threading.Tasks;

    public static class ColorExtensions
    {
        public static Task<bool> ColorTo(this VisualElement self, Color fromColor, Color toColor, Action<Color> callback, uint length = 250, Easing easing = null)
        {
            Color transform(double t) =>
                Color.FromRgba(fromColor.Red + t * (toColor.Red - fromColor.Red),
                               fromColor.Green + t * (toColor.Green - fromColor.Green),
                               fromColor.Blue + t * (toColor.Blue - fromColor.Blue),
                               fromColor.Alpha + t * (toColor.Alpha - fromColor.Alpha));

            return ColorAnimation(self, "ColorTo", transform, callback, length, easing);
        }

        public static void CancelAnimation(this VisualElement self)
        {
            self.AbortAnimation("ColorTo");
        }

        static Task<bool> ColorAnimation(VisualElement element, string name, Func<double, Color> transform, Action<Color> callback, uint length, Easing easing)
        {
            easing = easing ?? Easing.Linear;
            var taskCompletionSource = new TaskCompletionSource<bool>();

            element.Animate<Color>(name, transform, callback, 16, length, easing, (v, c) => taskCompletionSource.SetResult(c));

            return taskCompletionSource.Task;
        }
    }
}
