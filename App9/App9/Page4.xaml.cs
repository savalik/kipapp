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
		public Page4 ()
		{
			InitializeComponent ();

            StackLayout layout = new StackLayout();
            Label IPlabel = new Label()
            {
                Text = "Скоро здесь будет IP адрес",
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
                
            IPReceiver receiver = new IPReceiver(IPlabel);

            layout.Children.Add(IPlabel);

            Content = layout;
		}
	}
}