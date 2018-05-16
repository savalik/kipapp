using System;
using System.Collections.Generic;
using System.Text;
using KipLib;
using Xamarin.Forms;

namespace App9
{
    public class ViewItems
    {
        public Color color { get; set; }
        public string text { get; set; }
        public string repairSimbol { get; set; }

        public static DataTemplate itemsTemplate = new DataTemplate(() =>
        {
            var stackLayout = new StackLayout() { Orientation = StackOrientation.Horizontal };
            var repairLabel = new Label() { VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.End, Margin = new Thickness(10, 10), HorizontalOptions = LayoutOptions.End };
            var textLabel = new Label() { VerticalTextAlignment = TextAlignment.Center, HorizontalOptions = LayoutOptions.FillAndExpand };

            repairLabel.SetBinding(Label.TextProperty, "repairSimbol");
            repairLabel.SetBinding(Label.BackgroundColorProperty, "color");
            textLabel.SetBinding(Label.TextProperty, "text");

            stackLayout.Children.Add(textLabel);
            stackLayout.Children.Add(repairLabel);

            return new ViewCell { View = stackLayout };
        });

        public static List<ViewItems> GetViewItemsList(List<Items> items)
        {
            var ViewItemsList = new List<ViewItems>();
            var itemColor = new Color();
            string simbol;
            foreach (var item in items)
            {
                if (item.RepairOrCheck)
                {
                    itemColor = Color.Orange;
                    simbol = "РЕМОНТ";
                }
                else
                {
                    itemColor = Color.Yellow;
                    simbol = "ПРОВЕРКА";
                }
                ViewItemsList.Add(new ViewItems() { color = itemColor, text = item.GetText().TrimEnd(' '), repairSimbol = simbol });
            }
            return ViewItemsList;
        }

        public static List<ViewItems> GetReplacesList(List<Replace> items)
        {
            var ViewItemsList = new List<ViewItems>();
            var itemColor = new Color();
            string simbol;
            foreach (var item in items)
            {
                if (item.defect)
                {
                    itemColor = Color.Orange;
                    simbol = "Неиспр.";
                }
                else
                {
                    itemColor = Color.Yellow;
                    simbol = "";
                }
                ViewItemsList.Add(new ViewItems() { color = itemColor, text = item.GetText().TrimEnd(' '), repairSimbol = simbol });
            }
            return ViewItemsList;
        }
    }
}
