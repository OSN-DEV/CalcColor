namespace CalcColor.Data {
    class MainWindowViewModel : BindableBase {
        /// <summary>
        /// Hex Color
        /// </summary>
        public string HexThin1 {
            get => _hexThin1;
            set {
                if (_hexThin1 != value) {
                    SetProperty(ref _hexThin1, value, () => OnPropertyChanged(nameof(HexThin1)));
                }
            }
        }
        private string _hexThin1 = "";

        /// <summary>
        /// Hex Color
        /// </summary>
        public string HexThin2 {
            get => _hexThin2;
            set {
                if (_hexThin2 != value) {
                    SetProperty(ref _hexThin2, value, () => OnPropertyChanged(nameof(HexThin2)));
                }
            }
        }
        private string _hexThin2 = "";

        /// <summary>
        /// Hex Color
        /// </summary>
        public string HexDark1{
            get => _hexDark1;
            set {
                if (_hexDark1 != value) {
                    SetProperty(ref _hexDark1, value, () => OnPropertyChanged(nameof(HexDark1)));
                }
            }
        }
        private string _hexDark1= "";

        /// <summary>
        /// Hex Color
        /// </summary>
        public string HexDark2 {
            get => _hexDark2;
            set {
                if (_hexDark2 != value) {
                    SetProperty(ref _hexDark2, value, () => OnPropertyChanged(nameof(HexDark2)));
                }
            }
        }
        private string _hexDark2 = "";

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
    }
}
