using Android.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KipLib;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Mobile;



namespace App9
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page2 : ContentPage
	{
		public Page2 ()
		{
			InitializeComponent ();

            var layout = new StackLayout();

            var label = new Label
            {
                Text = "Нажмите на одну из кнопок внизу, чтобы добавить блок в список путем сканирования QR-кода",
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = 24
            };

            var innerlayout = new Grid();
            innerlayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(400) });
            innerlayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1,GridUnitType.Star) });
            innerlayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1,GridUnitType.Star) });

            var AddRepairBlock = new Button { Text = "Ремонт" };
            var AddCheckBlock = new Button { Text = "Проверка" };
            AddRepairBlock.Clicked += OnRepairButtonClicked;
            AddCheckBlock.Clicked += OnCheckButtonClicked;

            innerlayout.Children.Add(AddRepairBlock, 0,0);
            innerlayout.Children.Add(AddCheckBlock, 1,0);
            
            layout.Children.Add(label);
            layout.Children.Add(innerlayout);
            layout.Spacing = 0;

            Content = layout;
        }

        private void OnRepairButtonClicked(object sender, System.EventArgs e)
        {
            GetQRCodeAsync(true);
        }

        private void OnCheckButtonClicked(object sender, System.EventArgs e)
        {
            GetQRCodeAsync(false);
        }

        public async Task GetQRCodeAsync(bool RepairOrCheck)
        {
            var app = Forms.Context as Activity;
            
            MobileBarcodeScanner.Initialize(app.Application);

            var scanner = new MobileBarcodeScanner();

            var result = await scanner.Scan();

            if (result != null)
            {
                string[] block = result.Text.Split(',');
                Items.CreateItem(block[1], block[2], block[0], RepairOrCheck);
                Console.WriteLine("Scanned Barcode: " + result.Text);
                Page1.GetListView.ItemsSource = ViewItems.GetViewItemsList(Items.GetItems);

            }
        }

    }
}