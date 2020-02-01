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

namespace CalcColor {
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window {

        #region Declartion
        private MainWindowViewModel _model = new MainWindowViewModel();
        #endregion

        #region Constructor
        public MainWindow() {
            InitializeComponent();
            this.DataContext = this._model;
        }
        #endregion

        #region Event
        private void ColorEditor_ColorEvent(Component.ColorEditor.ColorEventArgs e) {
            //this._model.HexThin1 = this.ToHex(e, -30);
            //this._model.HexThin2 = this.ToHex(e, -60);
            //this._model.HexDark1 = this.ToHex(e, 30);
            //this._model.HexDark2 = this.ToHex(e, 60);

            // データモデルがうまいこと機能しないのでとりあえず直接設定。。
            this.cThins1.Hex = this.ToHex(e, -30);
            this.cThins2.Hex = this.ToHex(e, -60);
            this.cDark1.Hex = this.ToHex(e, 30);
            this.cDark2.Hex = this.ToHex(e, 60);
        }
        #endregion

        #region Private Method
        /// <summary>
        /// convert int to hex with offset
        /// </summary>
        /// <param name="e">event arguement</param>
        /// <param name="offSet">color offset</param>
        /// <returns>hex color</returns>
        private string ToHex(Component.ColorEditor.ColorEventArgs e, int offSet) {
            string toHexSub (int value) {
                var val = value + offSet;
                if (val <0) {
                    val = 0;
                } else if (255 < val) {
                    val = 255;
                }
                var result = Convert.ToString(val, 16);
                if (1 == result.Length) {
                    result = "0" + result;
                }
                return result;
            };
            return toHexSub(e.R) + toHexSub(e.G) + toHexSub(e.B);
        }
        #endregion
    }
}
