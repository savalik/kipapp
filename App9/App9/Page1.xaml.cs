﻿using System;
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
            InitializeComponent();

            ListView listView = new ListView { ItemTemplate = ViewItems.itemsTemplate };
            GetListView = listView;

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
            Thread thread = new Thread(new ParameterizedThreadStart(Sender.PrepareAndSend));
            thread.Start((byte)0x00);
        }
    }
}