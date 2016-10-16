﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GC.Helpers
{
   public class AutoCompleteBehavior
    {
        public static readonly TextChangedEventHandler onTextChanged = OnTextChanged;
        private static KeyEventHandler onKeyDown = OnPreviewKeyDown;
        private static bool _isSubscribed;

        /// <summary>
        /// The collection to search for matches from.
        /// </summary>
        public static readonly DependencyProperty AutoCompleteItemsSource =
            DependencyProperty.RegisterAttached
            (
                "AutoCompleteItemsSource",
                typeof(IEnumerable<String>),
                typeof(AutoCompleteBehavior),
                new UIPropertyMetadata(null, OnAutoCompleteItemsSource)
            );
        /// <summary>
        /// Whether or not to ignore case when searching for matches.
        /// </summary>
        public static readonly DependencyProperty AutoCompleteStringComparison =
            DependencyProperty.RegisterAttached
            (
                "AutoCompleteStringComparison",
                typeof(StringComparison),
                typeof(AutoCompleteBehavior),
                new UIPropertyMetadata(StringComparison.Ordinal)
            );

        #region Items Source
        public static IEnumerable<String> GetAutoCompleteItemsSource(DependencyObject obj)
        {
            object objRtn = obj.GetValue(AutoCompleteItemsSource);
            if (objRtn is IEnumerable<String>)
                return (objRtn as IEnumerable<String>);

            return null;
        }

        public static void SetAutoCompleteItemsSource(DependencyObject obj, IEnumerable<String> value)
        {
            obj.SetValue(AutoCompleteItemsSource, value);
        }

        private static void OnAutoCompleteItemsSource(object sender, DependencyPropertyChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (sender == null)
                return;

            //If we're being removed, remove the callbacks
            if (e.NewValue == null && _isSubscribed)
            {
                if (tb != null)
                {
                    tb.TextChanged -= onTextChanged;
                    tb.PreviewKeyDown -= onKeyDown;
                }
                _isSubscribed = false;
            }
            else if (!_isSubscribed)
            {
                //New source.  Add the callbacks
                if (tb != null)
                {
                    tb.TextChanged += onTextChanged;
                    tb.PreviewKeyDown += onKeyDown;
                }
                _isSubscribed = true;
            }
        }
        #endregion

        #region String Comparison
        public static StringComparison GetAutoCompleteStringComparison(DependencyObject obj)
        {
            return (StringComparison)obj.GetValue(AutoCompleteStringComparison);
        }
       public static void SetAutoCompleteStringComparison(DependencyObject obj, StringComparison value)
        {
            obj.SetValue(AutoCompleteStringComparison, value);
        }
        #endregion

        /// <summary>
        /// Used for moving the caret to the end of the suggested auto-completion text.
        /// </summary>
        /// <param name="sender"></param> <param name="e"></param>
        static void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
                return;

            TextBox tb = e.OriginalSource as TextBox;
            if (tb == null)
                return;

            //If we pressed enter and if the selected text goes all the way to the end, move our caret position to the end
            if (tb.SelectionLength > 0 && (tb.SelectionStart + tb.SelectionLength == tb.Text.Length))
            {
                tb.SelectionStart = tb.CaretIndex = tb.Text.Length;
                tb.SelectionLength = 0;
            }
        }

        /// <summary>
        /// Search for auto-completion suggestions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if
            (
                (from change in e.Changes where change.RemovedLength > 0 select change).Any() &&
                (from change in e.Changes where change.AddedLength > 0 select change).Any() == false
            )
                return;

            TextBox tb = e.OriginalSource as TextBox;
            if (sender == null)
                return;

            IEnumerable<String> values = GetAutoCompleteItemsSource(tb);
            //No reason to search if we don't have any values.
            if (values == null)
                return;

            //No reason to search if there's nothing there.
            if (tb != null && String.IsNullOrEmpty(tb.Text))
                return;

            if (tb != null)
            {
                Int32 textLength = tb.Text.Length;

                StringComparison comparer = GetAutoCompleteStringComparison(tb);
                //Do search and changes here.
                String match =
                    (
                        from
                            value
                            in
                            (
                                from subvalue
                                    in values
                                where subvalue.Length >= textLength
                                select subvalue
                                )
                        where value.Substring(0, textLength).Equals(tb.Text, comparer)
                        select value
                        ).FirstOrDefault();

                //Nothing.  Leave 'em alone
                if (String.IsNullOrEmpty(match))
                    return;

                tb.TextChanged -= onTextChanged;
                tb.Text = match;
                tb.CaretIndex = textLength;
                tb.SelectionStart = textLength;
                tb.SelectionLength = (match.Length - textLength);
            }
            if (tb != null) tb.TextChanged += onTextChanged;
        }
    }
}
