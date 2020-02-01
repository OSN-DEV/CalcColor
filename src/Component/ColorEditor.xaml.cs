using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using CalcColor.Data;

namespace CalcColor.Component {
    /// <summary>
    /// ColorEditor.xaml の相互作用ロジック
    /// </summary>
    public partial class ColorEditor : UserControl {

        #region Declaration
        private ColorEditorDataModel _model = new ColorEditorDataModel();

        public class ColorEventArgs : EventArgs {
            public int R { set; get; }
            public int G { set; get; }
            public int B { set; get; }
        }
        public delegate void ColorEventHandler(ColorEventArgs e);
        public event ColorEventHandler ColorEvent = null;
        #endregion

        #region Public Property
        public void SetReadonly(bool value) {
            this._model.ReadOnly = value;
        }
        public static readonly DependencyProperty ReadOnlyProp =
            DependencyProperty.Register("ReadOnly", typeof(bool), typeof(ColorEditor),
        new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnReadOnlyChanged)));

        public bool ReadOnly {
            get { return (bool)GetValue(ReadOnlyProp); }
            set { SetValue(ReadOnlyProp, value); }
        }

        private static void OnReadOnlyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
            var ctrl = obj as ColorEditor;
            if (ctrl != null) {
                ctrl.SetReadonly(ctrl.ReadOnly);
            }
        }


        public void SetHex(string hex) {
            this._model.Hex = hex;
        }
        public static readonly DependencyProperty HexProp =
            DependencyProperty.Register("Hex", typeof(string), typeof(ColorEditor),
        new FrameworkPropertyMetadata("", new PropertyChangedCallback(OnHexChanged)));

        public string Hex {
            get { return (string)GetValue(HexProp); }
            set { SetValue(HexProp, value); }
        }

        private static void OnHexChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
            var ctrl = obj as ColorEditor;
            if (ctrl != null) {
                ctrl.SetHex(ctrl.Hex);
            }
        }
        #endregion

        #region Constructor
        public ColorEditor() {
            InitializeComponent();
            this._model.ReadOnly = this.ReadOnly;
            this.DataContext = this._model;

        }
        #endregion

        #region Event
        /// <summary>
        /// hex color changed
        /// </summary>
        /// <param name="e"></param>
        private void HexColor_ColorEvent(HexColor.ColorEventArgs e) {
            this._model.R = e.R;
            this._model.G = e.G;
            this._model.B = e.B;

            var args = new ColorEventArgs() {
                R = int.Parse(e.R),
                G = int.Parse(e.G),
                B = int.Parse(e.B)
            };
            this.ColorEvent?.Invoke(args);
        }

        /// <summary>
        /// rgb color changed
        /// </summary>
        /// <param name="e"></param>
        private void RGBColor_ColorEvent(RGBColor.ColorEventArgs e) {
            var r = this._model.IntR;
            var g = this._model.IntG;
            var b = this._model.IntB;
            this._model.Hex = this.ToHex(r) + this.ToHex(g) + this.ToHex(b);

        }

        /// <summary>
        /// convert int to hex
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ToHex(int value) {
            var result = Convert.ToString(value, 16);
            if (1 == result.Length) {
                result = "0" + result;
            }
            return result;
        }
        #endregion

    }
}
