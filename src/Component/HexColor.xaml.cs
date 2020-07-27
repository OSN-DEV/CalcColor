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
            if (this.cValue.Text.Length < 6 || !this.IsNumeric(this.cValue.Text)) {
                this.cColor.Background = Brushes.Transparent;
            } else {
                this.cColor.Background = new SolidColorBrush(
                    (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#" + this.cValue.Text));
            }
        }

        /// <summary>
        /// raise color changed event
        /// </summary>
        private void RaiseColorEvent() {
            if (this.cValue.Text.Length < 6 || !this.IsNumeric(this.cValue.Text)) {
                return;
            }

            if (this._oldValue == this.cValue.Text) {
                return;
            }
            this._oldValue = this.cValue.Text;

            var args = new ColorEventArgs();
            var hex = this.cValue.Text;
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

        /// <summary>
        /// check value is numric
        /// </summary>
        /// <param name="value">true:numeric, false:otherwise</param>
        /// <returns></returns>
        private bool IsNumeric(string value) {
            int n;
            return int.TryParse(value, out n);
        }
        #endregion

    }
}
