﻿using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Windows;
using System.Windows.Automation.Peers;

using Elysium.Theme.Controls.Automation;
using Elysium.Theme.Controls.Primitives;
using Elysium.Theme.Extensions;

using JetBrains.Annotations;

namespace Elysium.Theme.Controls
{
    [PublicAPI]
    [DefaultEvent("Checked")]
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
    public class ToggleCommandButton : CommandButtonBase
// ReSharper restore ClassWithVirtualMembersNeverInherited.Global
    {
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static ToggleCommandButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleCommandButton), new FrameworkPropertyMetadata(typeof(ToggleCommandButton)));
        }

        [PublicAPI]
        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool?), typeof(ToggleCommandButton),
                                        new FrameworkPropertyMetadata(BooleanBoxingHelper.FalseBox,
                                                                      FrameworkPropertyMetadataOptions.BindsTwoWayByDefault |
                                                                      FrameworkPropertyMetadataOptions.Journal, OnIsCheckedChanged));

        [PublicAPI]
        [Category("Appearance")]
        [Description("Indicates whether the button is checked.")]
        [TypeConverter(typeof(NullableBoolConverter))]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        public bool? IsChecked
        {
            get { return NullableBooleanBoxingHelper.Unbox(GetValue(IsCheckedProperty)); }
            set { SetValue(IsCheckedProperty, NullableBooleanBoxingHelper.Box(value)); }
        }

        private static void OnIsCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null)
            {
                throw new ArgumentNullException("d");
            }
            Contract.EndContractBlock();
            var instance = (ToggleCommandButton)d;
            instance.OnIsCheckedChanged(NullableBooleanBoxingHelper.Unbox(e.OldValue), NullableBooleanBoxingHelper.Unbox(e.NewValue));
        }

        private void OnIsCheckedChanged(bool? oldIsChecked, bool? newIsChecked)
        {
            var peer = UIElementAutomationPeer.FromElement(this) as ToggleCommandButtonAutomationPeer;
            if (peer != null)
            {
                peer.RaiseToggleStatePropertyChangedEvent(oldIsChecked, newIsChecked);
            }

            switch (newIsChecked)
            {
                case true:
                    OnChecked(new RoutedEventArgs(CheckedEvent));
                    VisualStateManager.GoToState(this, "Checked", true);
                    break;
                case false:
                    OnUnchecked(new RoutedEventArgs(UncheckedEvent));
                    VisualStateManager.GoToState(this, "Normal", true);
                    break;
                case null:
                    OnIndeterminate(new RoutedEventArgs(IndeterminateEvent));
                    VisualStateManager.GoToState(this, "Indeterminate", true);
                    break;
            }
        }

        [PublicAPI]
        public static readonly RoutedEvent CheckedEvent = EventManager.RegisterRoutedEvent("Checked", RoutingStrategy.Bubble,
                                                                                           typeof(RoutedEventHandler), typeof(ToggleCommandButton));

        [PublicAPI]
        [Category("Behavior")]
        [Description("Occurs when a button is checked.")]
        public event RoutedEventHandler Checked
        {
            add { AddHandler(CheckedEvent, value); }

            remove { RemoveHandler(CheckedEvent, value); }
        }

        [PublicAPI]
        protected virtual void OnChecked(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        [PublicAPI]
        public static readonly RoutedEvent UncheckedEvent = EventManager.RegisterRoutedEvent("Unchecked", RoutingStrategy.Bubble,
                                                                                             typeof(RoutedEventHandler), typeof(ToggleCommandButton));

        [PublicAPI]
        [Category("Behavior")]
        [Description("Occurs when a button is unchecked.")]
        public event RoutedEventHandler Unchecked
        {
            add { AddHandler(UncheckedEvent, value); }

            remove { RemoveHandler(UncheckedEvent, value); }
        }

        [PublicAPI]
        protected virtual void OnUnchecked(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        [PublicAPI]
        public static readonly RoutedEvent IndeterminateEvent = EventManager.RegisterRoutedEvent("Indeterminate", RoutingStrategy.Bubble,
                                                                                                 typeof(RoutedEventHandler), typeof(ToggleCommandButton));

        [PublicAPI]
        [Category("Behavior")]
        [Description("Occurs when a button state is indeterminate.")]
        public event RoutedEventHandler Indeterminate
        {
            add { AddHandler(IndeterminateEvent, value); }

            remove { RemoveHandler(IndeterminateEvent, value); }
        }

        [PublicAPI]
        protected virtual void OnIndeterminate(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        [PublicAPI]
        public static readonly DependencyProperty IsThreeStateProperty =
            DependencyProperty.Register("IsThreeState", typeof(bool), typeof(ToggleCommandButton), new FrameworkPropertyMetadata(BooleanBoxingHelper.FalseBox));

        [PublicAPI]
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Determines whether the control supports two or three states.")]
        public bool IsThreeState
        {
            get { return BooleanBoxingHelper.Unbox(GetValue(IsThreeStateProperty)); }
            set { SetValue(IsThreeStateProperty, BooleanBoxingHelper.Box(value)); }
        }

        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ToggleCommandButtonAutomationPeer(this);
        }

        protected override void OnClick()
        {
            OnToggle();
            base.OnClick();
        }

        [PublicAPI]
        protected internal virtual void OnToggle()
        {
            bool? isChecked;
            if (IsChecked == true)
            {
                isChecked = IsThreeState ? (bool?)null : false;
            }
            else
            {
                isChecked = IsChecked.HasValue;
            }
            IsChecked = isChecked;
        }
    }
} ;