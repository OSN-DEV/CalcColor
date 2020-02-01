using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalcColor.Component {
    /// <summary>
    /// RGB_Color.xaml の相互作用ロジック
    /// </summary>
    public partial class RGBColor : UserControl {

        #region Declaration
        private Regex _numberRegEx = new Regex("[0-9]");
        #endregion

        #region PublicProperty
        public int Offset { set; get; } = 0;
        public int Value {
            set {
                if (255 < value + this.Offset) {
                    this.cValue.Text = "255";
                } else if (value + this.Offset < 0) {
                    this.cValue.Text = "0";
                } else {
                    this.cValue.Text = value.ToString();
                }
            }
            get {
                return (0 == this.cValue.Text.Length) ? 0 : int.Parse(this.cValue.Text);
            }
        }
        #endregion

        #region Constructor
        public RGBColor() {
            InitializeComponent();
            this.cValue.PreviewTextInput += (sender, e) => { this.AllowNumber(e); };
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
        #endregion

    }
}
