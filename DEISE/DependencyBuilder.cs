using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DEISE.Controls;
using Delta.InputSystem;

namespace DEISE
{
    public static class DependencyBuilder
    {
        public static List<Command> GetGraph(DesignerCanvas context)
        {
            var graph = new List<Command>();
            var commands = getBaseCommands(context);

            foreach (var item in commands)
            {
                graph.Add(getCommand(item));
            }

            return graph;

        }

        private static Command getCommand(DesignerItem designer)
        {
            var cmd = new Command();
            cmd.Name = ((designer.Content as DesignerControl).ContentGrid.Content as CommandControl).tbxTitel.Text;

            foreach (var item in designer.ConnectedDesignerItems)
            {
                cmd.Triggers.Add(getTrigger(item));
            }

            return cmd;
        }

        private static Trigger getTrigger(DesignerItem designer)
        {
            var trigger = new Trigger();

            var control = (designer.Content as DesignerControl).ContentGrid.Content as TriggerControl;
            trigger.Name = control.tbxTitel.Text;


            if (control.cbxTriggerButton.SelectedItem != null)
            {
                trigger.Button = ParseEnum<InputButton>(control.cbxTriggerButton.SelectedItem);
            }
            else
            {
                trigger.Button = null;
            }

            if (control.cbxState.SelectedItem != null)
            {
                trigger.State = ParseEnum<InputState>(control.cbxState.SelectedItem);
            }
            else
            {
                trigger.State = null;
            }
            

            foreach (var item in control.lvModifierButtons.Items)
            {
                var modifier = item as Modifier;
                trigger.Modifiers.Add((InputButton)Enum.Parse(typeof(InputButton), modifier.Selected.ToString()));
            }

            return trigger;
        }


        private static List<DesignerItem> getBaseCommands(DesignerCanvas context)
        {
            var commands = new List<DesignerItem>();

            foreach (var item in context.Children)
            {
                if (item is DesignerItem)
                {
                    var di = item as DesignerItem;

                    if (di.Type == ItemType.Command)
                    {
                        commands.Add(di);
                    }
                }
            }

            return commands;
        }

        public static T ParseEnum<T>(object o)
        {
            T e = (T)Enum.Parse(typeof(T), o.ToString());
            return e;
        }
    }
}
