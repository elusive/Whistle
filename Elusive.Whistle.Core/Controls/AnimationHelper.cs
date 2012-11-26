// -----------------------------------------------------------------------
// <copyright file="AnimationHelper.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Elusive.Whistle.Core.Controls
{
    /// <summary>
    /// Helper class to create animation storyboards used by the card and deck controls.
    /// </summary>
    public class AnimationHelper
    {
        /// <summary>
        /// Creates and returns a storyboard containing a two frame animation to begin a flip of a card.
        /// </summary>
        /// <param name="targetName">Name of the target.</param>
        /// <returns>Storyboard instance.</returns>
        public static Storyboard FlipStartBoard(string targetName)
        {
            var scaleXAnim = new DoubleAnimationUsingKeyFrames
                           {
                               BeginTime = new TimeSpan(0, 0, 0),
                           };
            scaleXAnim.KeyFrames.Add(new SplineDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(2))));
            scaleXAnim.SetValue(Storyboard.TargetNameProperty, targetName);
            scaleXAnim.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));

            var scaleYAnim = new DoubleAnimationUsingKeyFrames
            {
                BeginTime = new TimeSpan(0, 0, 0),
            };
            scaleYAnim.KeyFrames.Add(new SplineDoubleKeyFrame(1.1, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(2))));
            scaleYAnim.SetValue(Storyboard.TargetNameProperty, targetName);
            scaleYAnim.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)"));

            var sb = new Storyboard();
            sb.Children.Add(scaleXAnim);
            sb.Children.Add(scaleYAnim);
            return sb;
        }

        /// <summary>
        /// Creates and returns a storyboard containing several frames to end a flip of a card.
        /// </summary>
        /// <param name="targetName">Name of the target.</param>
        /// <returns>Storyboard instance.</returns>
        public static Storyboard FlipEndBoard(string targetName)
        {
            var scaleXAnim = new DoubleAnimationUsingKeyFrames
            {
                BeginTime = new TimeSpan(0, 0, 0),
            };
            scaleXAnim.KeyFrames.Add(new SplineDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(2))));
            scaleXAnim.SetValue(Storyboard.TargetNameProperty, targetName);
            scaleXAnim.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));

            var scaleYAnim = new DoubleAnimationUsingKeyFrames
            {
                BeginTime = new TimeSpan(0, 0, 0),
            };
            scaleYAnim.KeyFrames.Add(new SplineDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(2))));
            scaleYAnim.SetValue(Storyboard.TargetNameProperty, targetName);
            scaleYAnim.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)"));

            var translateXAnim = new DoubleAnimationUsingKeyFrames
            {
                BeginTime = new TimeSpan(0, 0, 0),
            };
            translateXAnim.KeyFrames.Add(new SplineDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(2))));
            translateXAnim.SetValue(Storyboard.TargetNameProperty, targetName);
            translateXAnim.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)"));

            var translateYAnim = new DoubleAnimationUsingKeyFrames
            {
                BeginTime = new TimeSpan(0, 0, 0),
            };
            translateYAnim.KeyFrames.Add(new SplineDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(2))));
            translateYAnim.SetValue(Storyboard.TargetNameProperty, targetName);
            translateYAnim.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)"));

            var sb = new Storyboard();
            sb.Children.Add(scaleXAnim);
            sb.Children.Add(scaleYAnim);
            sb.Children.Add(translateXAnim);
            sb.Children.Add(translateYAnim);
            return sb;
        }

        /// <summary>
        /// Create and return a storyboard for a 360 rotation.
        /// </summary>
        /// <param name="targetName">Name of the target.</param>
        /// <returns>Storyboard instance.</returns>
        public static Storyboard RotateBoard(string targetName)
        {
            var rotateAnim = new DoubleAnimationUsingKeyFrames
            {
                BeginTime = new TimeSpan(0, 0, 0),
            };
            rotateAnim.KeyFrames.Add(new SplineDoubleKeyFrame(360, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(3))));
            rotateAnim.SetValue(Storyboard.TargetNameProperty, targetName);
            rotateAnim.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[2].(RotateTransform.Angle)"));

            var sb = new Storyboard();
            sb.Children.Add(rotateAnim);
            return sb;
        }
    }
}
