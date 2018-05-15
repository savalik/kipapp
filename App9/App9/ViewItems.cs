using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using KipLib;

namespace App9
{
    public class ViewItems
    {
        public Color color { get; set; }
        public string text { get; set; }
        public string repairSimbol { get; set; }

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
    }
}
