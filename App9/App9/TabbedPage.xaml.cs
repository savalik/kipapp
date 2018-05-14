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
    public partial class TabbedPage
    {
        public TabbedPage ()
        {
            InitializeComponent();

            var page1 = new Page1 {Title = "Список ремонта/проверки"};
            var page2 = new Page2 { Title = "Добавить блок" };
            var page3 = new Page3 { Title = "Замена" };
            var page4 = new Page4 { Title = "Память" };

            Children.Add(page1);
            Children.Add(page2);
            Children.Add(page3);
            Children.Add(page4);
        }
    }
}