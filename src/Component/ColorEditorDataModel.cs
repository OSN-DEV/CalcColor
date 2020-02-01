using CalcColor.Data;

namespace CalcColor.Component {
    class ColorEditorDataModel : BindableBase {
        /// <summary>
        /// Hex Color
        /// </summary>
        public string Hex {
            get => _hex;
            set {
                if (_hex != value) {
                    SetProperty(ref _hex, value, () => OnPropertyChanged(nameof(Hex)));
                }
            }
        }
        private string _hex = "";

        /// <summary>
        /// Red Color
        /// </summary>
        public string R {
            get => _r;
            set {
                if (_r != value) {
                    SetProperty(ref _r, value, () => OnPropertyChanged(nameof(R)));
                }
            }
        }
        public int IntR {
            get => (0 == _r.Length)?0:int.Parse(_r);
        }
        private string _r = "";

        /// <summary>
        /// Green Color
        /// </summary>
        public string G {
            get => _g;
            set {
                if (_g != value) {
                    SetProperty(ref _g, value, () => OnPropertyChanged(nameof(G)));
                }
            }
        }
        public int IntG {
            get => (0 == _g.Length) ? 0 : int.Parse(_g);
        }
        private string _g = "";

        /// <summary>
        /// Blue Color
        /// </summary>
        public string B {
            get => _b;
            set {
                if (_b != value) {
                    SetProperty(ref _b, value, () => OnPropertyChanged(nameof(B)));
                }
            }
        }
        public int IntB {
            get => (0 == _b.Length) ? 0 : int.Parse(_b);
        }
        private string _b = "";

        public bool ReadOnly {
            get => _readOnly;
            set => SetProperty(ref _readOnly, value, () => OnPropertyChanged(nameof(ReadOnly)));
        }
        private bool _readOnly = false;
    }
}
