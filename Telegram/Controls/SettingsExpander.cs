﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Hosting;

namespace Telegram.Controls
{
    internal class SettingsExpander : ContentControl
    {
        private ToggleButton ActionButton;
        private Border PopupHost;
        private ContentPresenter PopupRoot;

        public SettingsExpander()
        {
            DefaultStyleKey = typeof(SettingsExpander);
        }

        protected override void OnApplyTemplate()
        {
            ActionButton = GetTemplateChild(nameof(ActionButton)) as ToggleButton;
            ActionButton.Click += OnClick;
            ActionButton.IsChecked = IsExpanded;

            PopupHost = GetTemplateChild(nameof(PopupHost)) as Border;
            PopupRoot = GetTemplateChild(nameof(PopupRoot)) as ContentPresenter;
            PopupRoot.SizeChanged += OnSizeChanged;

            ElementCompositionPreview.SetIsTranslationEnabled(PopupRoot, true);

            OnExpandedChanged(IsExpanded, IsExpanded);
            base.OnApplyTemplate();
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Logger.Debug();

            PopupRoot.Margin = new Thickness(0, 0, 0, IsExpanded ? 0 : -e.NewSize.Height);
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            IsExpanded = !IsExpanded;
        }

        #region IsExpanded

        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register("IsExpanded", typeof(bool), typeof(SettingsExpander), new PropertyMetadata(false, OnExpandedChanged));

        private static void OnExpandedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SettingsExpander)d).OnExpandedChanged((bool)e.NewValue, (bool)e.OldValue);
        }

        private void OnExpandedChanged(bool newValue, bool oldValue)
        {
            if (PopupHost == null)
            {
                return;
            }

            ActionButton.IsChecked = newValue;

            PopupHost.Height = newValue ? double.NaN : 0;
            PopupRoot.Margin = new Thickness(0, 0, 0, newValue ? 0 : -PopupRoot.ActualHeight);
            PopupRoot.Visibility = Visibility.Visible;

            var visual = ElementCompositionPreview.GetElementVisual(PopupRoot);
            visual.Clip = visual.Compositor.CreateInsetClip();

            var clip = visual.Compositor.CreateScalarKeyFrameAnimation();
            clip.InsertKeyFrame(newValue ? 0 : 1, 44);
            clip.InsertKeyFrame(newValue ? 1 : 0, 0);
            clip.Duration = Constants.FastAnimation;

            var offset = visual.Compositor.CreateScalarKeyFrameAnimation();
            offset.InsertKeyFrame(newValue ? 0 : 1, -44);
            offset.InsertKeyFrame(newValue ? 1 : 0, 0);
            offset.Duration = Constants.FastAnimation;

            var opacity = visual.Compositor.CreateScalarKeyFrameAnimation();
            opacity.InsertKeyFrame(newValue ? 0 : 1, 0);
            opacity.InsertKeyFrame(newValue ? 1 : 0, 1);
            opacity.Duration = Constants.FastAnimation;

            var batch = visual.Compositor.CreateScopedBatch(Windows.UI.Composition.CompositionBatchTypes.Animation);
            batch.Completed += (s, args) =>
            {
                PopupRoot.Visibility = newValue
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            };

            visual.Clip.StartAnimation("TopInset", clip);
            visual.StartAnimation("Translation.Y", offset);
            visual.StartAnimation("Opacity", opacity);

            batch.End();
        }

        #endregion

        #region Header

        public object Header
        {
            get { return (object)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(object), typeof(SettingsExpander), new PropertyMetadata(null));

        #endregion
    }
}
