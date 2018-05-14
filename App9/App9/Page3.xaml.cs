using Android.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Mobile;
using KipLib;

namespace App9
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page3 : ContentPage
	{
        static Block blockIn = null;
        static Block blockOut = null;
        static Button PageAddInBlock = null;
        static Button PageAddOutBlock = null;
        static Label Label = null;

        public Page3 ()
		{
			InitializeComponent ();

            StackLayout layout = new StackLayout();
            Label mainLabel = new Label()
            {
                Text = "Замена",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                HeightRequest = 100,
                FontSize = 24,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };
            Label = mainLabel;

            var AddInBlock = new Button { Text = "Сканировать установленный блок", VerticalOptions = LayoutOptions.FillAndExpand, FontSize = 18};
            var AddOutBlock = new Button { Text = "Сканировать снятый блок", VerticalOptions = LayoutOptions.FillAndExpand, FontSize = 18 };

            PageAddInBlock = AddInBlock;
            PageAddOutBlock = AddOutBlock;

            AddInBlock.Clicked += OnAddInBlockButtonClicked;
            AddOutBlock.Clicked += OnAddOutBlockButtonClicked;

            layout.Children.Add(AddInBlock);
            layout.Children.Add(mainLabel);
            layout.Children.Add(AddOutBlock);

            Content = layout;

        }

        private void OnAddInBlockButtonClicked(object sender, EventArgs e)
        {
            GetQRCodeAsync(true);
        }

        private void OnAddOutBlockButtonClicked(object sender, EventArgs e)
        {
            GetQRCodeAsync(false);
        }

        public async Task GetQRCodeAsync(bool InOrOut)
        {
            var app = Forms.Context as Activity;

            MobileBarcodeScanner.Initialize(app.Application);

            var scanner = new MobileBarcodeScanner();

            var result = await scanner.Scan();

            if (result != null)
            {
                string[] block = result.Text.Split(',');
                
                if (InOrOut)
                {
                    blockIn = new Block(block[1], block[2], block[0]);
                    PageAddInBlock.IsEnabled = false;
                    PageAddInBlock.Text = "Установлен: " + blockIn.GetText();
                }
                else
                {
                    blockOut = new Block(block[1], block[2], block[0]);
                    PageAddOutBlock.IsEnabled = false;
                    PageAddOutBlock.Text = "Снят: " + blockOut.GetText();
                }

                Console.WriteLine("Scanned Barcode: " + result.Text);

                if ((blockIn != null) && (blockOut != null))
                {
                    Console.WriteLine("Собрана замена: ");
                    Replace.CreateReplace(blockIn, blockOut);
                    var str = Replace.GetReplaces.Last().GetText();
                    var strings = str.Split(';');
                    Label.Text = strings[0].TrimEnd(' ') + "\n" + strings[1].TrimStart(' ').TrimEnd(' ');
                }
            }
        }
    }
}