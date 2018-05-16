using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App9
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page4 : ContentPage
	{
        public static ListView GetListView;
        public static ListView GetReplacesView;

        public Page4 ()
		{
            InitializeComponent();

            ListView listView = new ListView { ItemTemplate = ViewItems.itemsTemplate };
            GetListView = listView;

            var SendList = new Button { Text = "Передать данные на сервер" };

            StackLayout TopLayout = new StackLayout();
            TopLayout.Children.Add(listView);
            TopLayout.Children.Add(SendList);

            //ToDo поменять темплейт
            ListView ReplacesView = new ListView { ItemTemplate = ViewItems.itemsTemplate };
            GetReplacesView = ReplacesView;

            var SendReplace = new Button { Text = "Передать данные на сервер" };

            StackLayout BottomLayout = new StackLayout();
            TopLayout.Children.Add(ReplacesView);
            TopLayout.Children.Add(SendReplace);

            StackLayout layout = new StackLayout();
            layout.Children.Add(TopLayout);
            layout.Children.Add(BottomLayout);

            Content = layout;
        }
    }
}