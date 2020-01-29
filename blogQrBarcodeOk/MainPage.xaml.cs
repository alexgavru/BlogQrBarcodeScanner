using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace blogQrBarcodeOk
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public bool IsScanning { get; set; } = false;
        public bool IsScanningAvailable { get; set; } = true;

        private ICommand scanResultCommand;
        public ICommand ScanResultCommand => scanResultCommand ?? (scanResultCommand = new Command<ZXing.Result>((result)=>
        {
            Device.BeginInvokeOnMainThread(()=> { 
                ScannedText = result.Text;
            });
        }));

        private string scannedText = "Scanned text will appear here...";
        public string ScannedText
        {
            get => scannedText;
            set
            {
                scannedText = value;
                OnPropertyChanged(nameof(ScannedText));
            }
        }

        private ICommand startScanCommand;
        public ICommand StartScanCommand => startScanCommand ?? (startScanCommand = new Command(()=>
        {
            Device.BeginInvokeOnMainThread(() => {
                IsScanning = true;
                IsScanningAvailable = false;
                OnPropertyChanged(nameof(IsScanning));
                OnPropertyChanged(nameof(IsScanningAvailable));
            });
        }));
    }
}
