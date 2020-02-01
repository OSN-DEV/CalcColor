using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CalcColor.Component {
    /// <summary>
    /// RGB_Color.xaml の相互作用ロジック
    /// </summary>
    public partial class RGBColor : UserControl {

        #region Declaration
        private string _oldValue = "";
        private Regex _numberRegEx = new Regex("[0-9]");
        public enum ColorType {
            Red,
            Green,
            Blue
        }
        public ColorType RGB { set; get; }
        public class ColorEventArgs : EventArgs {
            public ColorType ColorType { set; get; }
            public int Value { set; get; }
        }
        public delegate void ColorEventHandler(ColorEventArgs e);
        public event ColorEventHandler ColorEvent = null;
        #endregion

        #region Public Property
        public int IntValue {
            get {
                return (0 == this.cValue.Text.Length) ? 0 : int.Parse(this.cValue.Text);
            }
        }
        public static readonly DependencyProperty ValueProp =
            DependencyProperty.Register("Value", typeof(string), typeof(RGBColor),
                new FrameworkPropertyMetadata("", new PropertyChangedCallback(OnValueChanged)));

        public string Value {
            get { return (string)GetValue(ValueProp); }
            set { this._oldValue = value; SetValue(ValueProp, value); }
        }

        private static void OnValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
            var ctrl = obj as RGBColor;
            if (ctrl != null) {
                ctrl.cValue.Text = ctrl.Value;
            }
        }

        public static readonly DependencyProperty ReadOnlyProp =
            DependencyProperty.Register("ReadOnly", typeof(bool), typeof(RGBColor),
        new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnReadOnlyChanged)));

        public bool ReadOnly {
            get { return (bool)GetValue(ReadOnlyProp); }
            set { SetValue(ReadOnlyProp, value);  }
        }

        private static void OnReadOnlyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
            var ctrl = obj as RGBColor;
            if (ctrl != null) {
                ctrl.cValue.IsReadOnly = ctrl.ReadOnly;
            }
        }
        #endregion

        #region Constructor
        public RGBColor() {
            InitializeComponent();
            this.cValue.PreviewTextInput += (sender, e) => { this.AllowNumber(e); };
            this.cValue.TextChanged += (sender, e) => {
                this.RaiseColorEvent();
            };
            this.cCopy.Click += (sender, e) => { Clipboard.SetText(this.cValue.Text, TextDataFormat.Text); };

        }
        #endregion

        #region Private Method
        /// <summary>
        /// if input value is not number, ignore input
        /// </summary>
        /// <param name="e"></param>
        private void AllowNumber(TextCompositionEventArgs e) {
            if (!this._numberRegEx.IsMatch(e.Text)) {
                e.Handled = true;
                return;
            }

            string tmp;
            var len = this.cValue.SelectionLength;
            var pos = this.cValue.SelectionStart;
            if (0 < len) {
                if (0 == pos) {
                    tmp = e.Text;
                } else {
                    tmp = this.cValue.Text.Substring(0, pos) + e.Text;
                }
                tmp += this.cValue.Text.Substring(pos + len);
            } else {
                if (0 == pos) {
                    tmp = e.Text + this.cValue.Text;
                } else {
                    tmp = this.cValue.Text.Substring(0, pos) + e.Text;
                    if (pos + 1 <= this.cValue.Text.Length) {
                        tmp += this.cValue.Text.Substring(pos);
                    }
                }
            }
            if (255 < int.Parse(tmp)) {
                e.Handled = true;
            }
        }

        /// <summary>
        /// raise color event
        /// </summary>
        private void RaiseColorEvent() {
            this.Value = this.cValue.Text;
            if (this._oldValue != this.cValue.Text) {
                return;
            }
            this._oldValue = this.cValue.Text;
            var args = new ColorEventArgs();
            args.ColorType = this.RGB;
            this.ColorEvent?.Invoke(args);
        }
        #endregion

    }
}
