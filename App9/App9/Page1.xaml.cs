using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App9
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page1 : ContentPage
	{
        public static ListView GetListView;

		public Page1 ()
		{
            ListView listView = new ListView
            {
                //ItemsSource = 
            };
            GetListView = listView;

            var itemsTemplate = new DataTemplate(() =>
            {
                var stackLayout = new StackLayout() {Orientation = StackOrientation.Horizontal};
                var repairLabel = new Label() { VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.End, Margin = new Thickness(10,10) };
                var textLabel = new Label() { VerticalTextAlignment = TextAlignment.Center };

                repairLabel.SetBinding(Label.TextProperty, "repairSimbol");
                repairLabel.SetBinding(BackgroundColorProperty, "color");
                textLabel.SetBinding(Label.TextProperty, "text");

                stackLayout.Children.Add(textLabel);
                stackLayout.Children.Add(repairLabel);

                return new ViewCell { View = stackLayout };
            });
            listView.ItemTemplate = itemsTemplate;

            var PrintDocs = new Button { Text = "Печатать контрольные карты и дефектные ведомости" };
            PrintDocs.Clicked += OnPrintDocsButtonClicked;

            StackLayout layout = new StackLayout();
            layout.Children.Add(listView);
            layout.Children.Add(PrintDocs);

            Content = layout;
            InitializeComponent ();
		}

        private void OnPrintDocsButtonClicked(object sender, EventArgs e)
        {
            Thread thread = new Thread(Sender.PrepareAndSend);
            thread.Start();
        }
    }
}