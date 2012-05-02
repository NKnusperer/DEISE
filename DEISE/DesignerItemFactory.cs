using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DEISE.Controls;
using System.Windows;

namespace DEISE
{
    public class DesignerItemFactory
    {
        public static DesignerItem CreateItem(ItemType type)
        {
            switch (type)
            {
                case ItemType.Command:
                    return createCommandItem();
                case ItemType.Trigger:
                    return createTriggerItem();
                default:
                    throw new Exception("Unknow ItemType");
            }
        }

        private static DesignerItem createCommandItem()
        {
            var item = new DesignerItem();
            item.Type = ItemType.Command;
            item.Width = 250;
            item.Height = 65;
            var control = new DesignerControl();
            control.Titel = "New Command";
            control.ContentGrid.Content = new CommandControl();
            item.Content = control;
            return item;
        }

        private static DesignerItem createTriggerItem()
        {
            var item = new DesignerItem();
            item.Type = ItemType.Trigger;
            item.Width = 250;
            item.Height = 250;
            var control = new DesignerControl();
            control.Titel = "New Trigger";
            control.ContentGrid.Content = new TriggerControl();
            item.Content = control;
            return item;
        }
    }
}
