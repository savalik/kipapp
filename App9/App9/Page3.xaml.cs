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
        static StackLayout GetLayout = null;

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

            GetLayout = layout;

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
                    bool defect = false;
                    defect = await DisplayAlert("Замена оборудования", "Замененное оборудование вышло из строя?", "Да", "Нет");
                    Replace.CreateReplace(blockIn, blockOut, defect);
                    var str = Replace.GetReplaces.Last().GetText();
                    var strings = str.Split(';');
                    Label.Text = strings[0].TrimEnd(' ') + "\n" + strings[1].TrimStart(' ').TrimEnd(' ');

                    var childs = new List<View>();
                    foreach (View v in GetLayout.Children)
                        childs.Add(v);
                    GetLayout.Children.Clear();

                    var RemoveReplace = new TapGestureRecognizer();
                    RemoveReplace.Tapped += (s, e) =>
                    {
                        Replace.GetReplaces.Remove(Replace.GetReplaces.Last());
                        PageReload();
                    };

                    var ApplyReplace = new TapGestureRecognizer();
                    ApplyReplace.Tapped += (s, e) =>
                    {
                        PageReload();
                    };

                    var ApplyLabel = new Label()
                    {
                        Text = "Нажмите сюда чтобы запомнить замену",
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        HeightRequest = 70,
                        FontSize = 18,
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                        BackgroundColor = Color.Teal,
                    };
                    ApplyLabel.GestureRecognizers.Add(ApplyReplace);

                    var RemoveLabel = new Label()
                    {
                        Text = "Нажмите сюда чтобы сбросить замену",
                        VerticalOptions = LayoutOptions.End,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        HeightRequest = 70,
                        FontSize = 18,
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                        BackgroundColor = Color.DarkRed
                    };
                    RemoveLabel.GestureRecognizers.Add(RemoveReplace);

                    GetLayout.Children.Add(ApplyLabel);
                    foreach (View v in childs)
                        GetLayout.Children.Add(v);
                    GetLayout.Children.Add(RemoveLabel);
                }
            }
            else scanner.Cancel();
        }

        private static void PageReload()
        {
            Page4.GetReplacesView.ItemsSource = ViewItems.GetReplacesList(Replace.GetReplaces);

            blockIn = null;
            blockOut = null;

            PageAddInBlock.Text = "Сканировать установленный блок";
            PageAddOutBlock.Text = "Сканировать снятый блок";
            Label.Text = "Замена";

            PageAddInBlock.IsEnabled = true;
            PageAddOutBlock.IsEnabled = true;

            var r_childs = new List<View>() { GetLayout.Children[1], GetLayout.Children[2], GetLayout.Children[3] };
            GetLayout.Children.Clear();
            foreach (View r in r_childs)
                GetLayout.Children.Add(r);
        }
    }
}