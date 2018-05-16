using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace App9
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

			MainPage = new TabbedPage();
		}

		protected override void OnStart ()
		{
            IPReceiver receiver = new IPReceiver();
            // Handle when your app starts
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
