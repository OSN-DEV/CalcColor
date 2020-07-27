using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CalcColor.Component {
    /// <summary>
    /// RGB_Color.xaml の相互作用ロジック
    /// </summary>
    public partial class HexColor : UserControl {

        #region Declaration
        private string _oldValue = "";
        private Regex _numberRegEx = new Regex("[0-9a-fA-F]");
        
        public class ColorEventArgs :EventArgs {
            public string Hex { set; get; } = "";
            public string R { set; get; } = "";
            public string G { set; get; } = "";
            public string B { set; get; } = "";
        }
        public delegate void ColorEventHandler(ColorEventArgs e);
        public event ColorEventHandler ColorEvent = null;
        #endregion

        #region Public Property
        public static readonly DependencyProperty ValueProp =
            DependencyProperty.Register("Value",typeof(string),typeof(HexColor), 
                new FrameworkPropertyMetadata("", new PropertyChangedCallback(OnValueChanged)));

        public string Value {
            get { return (string)GetValue(ValueProp); }
            set { this._oldValue = value; SetValue(ValueProp, value); }
        }

        private static void OnValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
            var ctrl = obj as HexColor;
            if (ctrl != null) {
                ctrl.cValue.Text = ctrl.Value;
            }
        }

        public static readonly DependencyProperty ReadOnlyProp =
            DependencyProperty.Register("ReadOnly", typeof(bool), typeof(HexColor),
        new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnReadOnlyChanged)));

        public bool ReadOnly {
            get { return (bool)GetValue(ReadOnlyProp); }
            set { SetValue(ReadOnlyProp, value); }
        }

        private static void OnReadOnlyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
            var ctrl = obj as HexColor;
            if (ctrl != null) {
                ctrl.cValue.IsReadOnly = ctrl.ReadOnly;
            }
        }
        #endregion

        #region Constructor
        public HexColor() {
            InitializeComponent();
            this.cValue.TextChanged += (sender, e) => { 
                this.ShowColor();
                this.RaiseColorEvent();
            };
            this.cValue.PreviewTextInput += (sender, e) => { this.AllowNumber(e); };
            this.cCopy.Click += (sender, e) => { Clipboard.SetText(this.cValue.Text, TextDataFormat.Text); };

        }
        #endregion

        #region Private Method
        /// <summary>
        /// show color
        /// </summary>
        private void ShowColor() {
            var val = ConvertString(this.cValue.Text);
            if (val.Length != 6) {
                this.cColor.Background = Brushes.Transparent;
            } else {
                this.cColor.Background = new SolidColorBrush(
                    (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#" + val));
            }
        }

        /// <summary>
        /// raise color changed event
        /// </summary>
        private void RaiseColorEvent() {
            var val = ConvertString(this.cValue.Text);
            if (val.Length != 6) {
                return;
            }

            if (this._oldValue == val) {
                return;
            }
            this._oldValue = val;

            var args = new ColorEventArgs();
            var hex = val;
            args.Hex = hex;
            args.R = Convert.ToInt32(hex.Substring(0, 2), 16).ToString();
            args.G = Convert.ToInt32(hex.Substring(2, 2), 16).ToString();
            args.B = Convert.ToInt32(hex.Substring(4, 2), 16).ToString();
            this.ColorEvent?.Invoke(args);
        }

        /// <summary>
        /// if input value is not number, ignore input
        /// </summary>
        /// <param name="e"></param>
        private void AllowNumber(TextCompositionEventArgs e) {
            if (!this._numberRegEx.IsMatch(e.Text)) {
                e.Handled = true;
                return;
            }
        }


        Regex regex = new Regex("[0-9a-f]");
        /// <summary>
        /// convert hex string without #
        /// </summary>
        /// <param name="value">return blank if invalid string</param>
        /// <returns></returns>
        private string ConvertString(string value) { 
            if (value.Length != 6 && value.Length != 7) {
                return "";
            }
            var val = value;
            if (value.Length == 7) {
                val = value.Substring(1);
            }

            if (!regex.Match(val).Success || regex.Matches(val).Count != 6) {
                val = "";
            }
            return val;
        }
        #endregion

    }
}
