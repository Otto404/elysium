﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;

using Elysium.Extensions;

using JetBrains.Annotations;

namespace Elysium.Controls
{
    [PublicAPI]
    [TemplatePart(Name = LayoutRootName, Type = typeof(Panel))]
    [TemplatePart(Name = CaptionName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = TitleName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = ProgressBarName, Type = typeof(LinearProgressBar))]
    [TemplatePart(Name = MinimizeName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = MaximizeRestoreName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = CloseName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = ApplicationBarHost, Type = typeof(Decorator))]
    [TemplatePart(Name = GripName, Type = typeof(ResizeGrip))]
    public class Window : System.Windows.Window
    {
        private const string LayoutRootName = "PART_LayoutRoot";
        private const string TitleName = "PART_Title";
        private const string CaptionName = "PART_Caption";
        private const string ProgressBarName = "PART_ProgressBar";
        private const string MinimizeName = "PART_Minimize";
        private const string MaximizeRestoreName = "PART_MaximizeRestore";
        private const string CloseName = "PART_Close";
        private const string ApplicationBarHost = "PART_ApplicationBarHost";
        private const string GripName = "PART_Grip";

        private FrameworkElement _caption;
        private Decorator _applicationBarHost;

        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static Window()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata(typeof(Window)));
        }

        public Window()
        {
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, (sender, e) => Close()));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (Template != null)
            {
                _applicationBarHost = Template.FindName(ApplicationBarHost, this) as Decorator;
                if (_applicationBarHost == null)
                {
                    Trace.TraceWarning(ApplicationBarHost + " not found.");
                }
                else
                {
                    var applicationBar = GetApplicationBar(this);
                    if (applicationBar != null)
                    {
                        _applicationBarHost.Child = applicationBar;
                    }
                }

                // NOTE: Lack of contracts: FindName is pure method
                Contract.Assume(Template != null);
                _caption = Template.FindName(CaptionName, this) as FrameworkElement;
                if (_caption == null)
                {
                    Trace.TraceError(CaptionName + " not found.");
                }
                else
                {
                    _caption.SizeChanged += (sender, e) =>
                    {
                        NonClientWidth = e.NewSize.Width;
                        NonClientHeight = e.NewSize.Height;
                    };
                }
            }
        }

        [PublicAPI]
        public static readonly DependencyProperty HasDropShadowProperty =
            DependencyProperty.Register("HasDropShadow", typeof(bool), typeof(Window),
                                        new FrameworkPropertyMetadata(BooleanBoxingHelper.FalseBox, null, CoerceHasDropShadow));

        [PublicAPI]
        public bool HasDropShadow
        {
            get { return BooleanBoxingHelper.Unbox(GetValue(HasDropShadowProperty)); }
            set { SetValue(HasDropShadowProperty, BooleanBoxingHelper.Box(value)); }
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private static object CoerceHasDropShadow(DependencyObject obj, object basevalue)
        {
            ValidationHelper.NotNull(obj, () => obj);
            try
            {
                // NOTE: Ignore Code Contracts warnings
                return BooleanBoxingHelper.Unbox(basevalue) && Environment.OSVersion.Version.Major >= 6;
            }
            catch
            {
                return basevalue;
            }
        }

        [PublicAPI]
        public static readonly DependencyProperty ProgressPercentProperty =
            DependencyProperty.Register("ProgressPercent", typeof(double), typeof(Window),
                                        new FrameworkPropertyMetadata(100d, FrameworkPropertyMetadataOptions.None));

        [PublicAPI]
        [Bindable(true)]
        [Category("Appearance")]
        [Description("The current magnitude of the progress bar that placed in the top of window.")]
        public double ProgressPercent
        {
            get { return BoxingHelper<double>.Unbox(GetValue(ProgressPercentProperty)); }
            set { SetValue(ProgressPercentProperty, value); }
        }

        [PublicAPI]
        public static readonly DependencyProperty NonClientWidthProperty =
            DependencyProperty.Register("NonClientWidth", typeof(double), typeof(Window),
                                        new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        [Bindable(true)]
        public double NonClientWidth
        {
            get { return BoxingHelper<double>.Unbox(GetValue(NonClientWidthProperty)); }
            set { SetValue(NonClientWidthProperty, value); }
        }

        [PublicAPI]
        public static readonly DependencyProperty NonClientHeightProperty =
            DependencyProperty.Register("NonClientHeight", typeof(double), typeof(Window),
                                        new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        [Bindable(true)]
        public double NonClientHeight
        {
            get { return BoxingHelper<double>.Unbox(GetValue(NonClientHeightProperty)); }
            set { SetValue(NonClientHeightProperty, value); }
        }

        [PublicAPI]
        public static readonly DependencyProperty IsMainWindowProperty =
            DependencyProperty.RegisterAttached("IsMainWindow", typeof(bool), typeof(Window), new FrameworkPropertyMetadata(false, OnIsMainWindowChanged));

        [PublicAPI]
        [JetBrains.Annotations.Pure]
        [System.Diagnostics.Contracts.Pure]
        [AttachedPropertyBrowsableForType(typeof(System.Windows.Window))]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static bool GetIsMainWindow([NotNull] System.Windows.Window obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            return BooleanBoxingHelper.Unbox(obj.GetValue(IsMainWindowProperty));
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetIsMainWindow([NotNull] System.Windows.Window obj, bool value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(IsMainWindowProperty, BooleanBoxingHelper.Box(value));
        }

        private static void OnIsMainWindowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = (System.Windows.Window)d;
            if (instance != null && e.NewValue == BooleanBoxingHelper.TrueBox)
            {
                Action setMainWindow =
                    () =>
                    {
                        foreach (var window in Application.Current.Windows.AsParallel().Cast<Window>().Where(window => window != instance))
                        {
                            SetIsMainWindow(window, false);
                        }
                    };
                Application.Current.Dispatcher.BeginInvoke(setMainWindow, DispatcherPriority.Render);
            }
        }

        [PublicAPI]
        public static readonly DependencyProperty ApplicationBarProperty =
            DependencyProperty.RegisterAttached("ApplicationBar", typeof(ApplicationBar), typeof(Window),
                                                new FrameworkPropertyMetadata(null, (OnApplicationBarChanged)));

        [PublicAPI]
        [JetBrains.Annotations.Pure]
        [System.Diagnostics.Contracts.Pure]
        [AttachedPropertyBrowsableForType(typeof(System.Windows.Window))]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static ApplicationBar GetApplicationBar([NotNull] System.Windows.Window obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            return (ApplicationBar)obj.GetValue(ApplicationBarProperty);
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetApplicationBar([NotNull] System.Windows.Window obj, ApplicationBar value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(ApplicationBarProperty, value);
        }

        private static void OnApplicationBarChanged([NotNull] DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ValidationHelper.NotNull(obj, () => obj);
            var instance = obj as System.Windows.Window;
            if (instance != null && e.OldValue == null)
            {
                instance.MouseRightButtonUp += OnApplicationBarOpening;
            }
            var window = obj as Window;
            if (window != null && window._applicationBarHost != null)
            {
                var newApplicationBar = (ApplicationBar)e.NewValue;
                window._applicationBarHost.Child = newApplicationBar;
            }
        }

        private static void OnApplicationBarOpening(object sender, MouseButtonEventArgs e)
        {
            var window = sender as System.Windows.Window;
            var source = e.OriginalSource as UIElement;
            if (window != null && source != null && !ApplicationBar.GetPreventsOpen(source))
            {
                var applicationBar = GetApplicationBar(window);
                if (applicationBar != null)
                {
                    applicationBar.IsOpen = true;
                }
            }
        }
    }
} ;