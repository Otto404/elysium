﻿using System;
using System.Diagnostics.Contracts;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

using Elysium.Controls.Primitives;

using JetBrains.Annotations;

namespace Elysium.Controls.Automation
{
    public class ProgressBarAutomationPeer : RangeBaseAutomationPeer, IRangeValueProvider
    {
        public ProgressBarAutomationPeer([NotNull] ProgressBarBase owner) : base(owner)
        {
        }

        [PublicAPI]
        public new ProgressBarBase Owner
        {
            get
            {
                Contract.Ensures(Contract.Result<ProgressBarBase>() != null);
                var result = (ProgressBarBase)base.Owner;
                Contract.Assume(result != null);
                return result;
            }
        }

        protected override string GetClassNameCore()
        {
            Contract.Ensures(Contract.Result<string>() == "ProgressBar");
            return "ProgressBar";
        }

        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            Contract.Ensures(Contract.Result<AutomationControlType>() == AutomationControlType.ProgressBar);
            return AutomationControlType.ProgressBar;
        }

        public override object GetPattern(PatternInterface patternInterface)
        {
            var state = Owner.State;
            if (patternInterface == PatternInterface.RangeValue && (state == ProgressBarState.Indeterminate || state == ProgressBarState.Busy))
            {
                return null;
            }
            return base.GetPattern(patternInterface);
        }

        void IRangeValueProvider.SetValue(double value)
        {
            Contract.Ensures(false);
            throw new InvalidOperationException("Progress bar is read-only");
        }

        public bool IsReadOnly
        {
            get
            {
                Contract.Ensures(Contract.Result<bool>());
                return true;
            }
        }

        public double LargeChange
        {
            get
            {
                Contract.Ensures(double.IsNaN(Contract.Result<double>()));
                return double.NaN;
            }
        }

        public double SmallChange
        {
            get
            {
                Contract.Ensures(double.IsNaN(Contract.Result<double>()));
                return double.NaN;
            }
        }
    }
} ;