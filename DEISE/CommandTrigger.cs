using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Delta.InputSystem;

namespace DEISE
{
    public class Command
    {
        public Command()
        {
            Triggers = new List<Trigger>();
        }

        public string Name { get; set; }
        public List<Trigger> Triggers { get; set; }
    }

    public class Trigger
    {
        public Trigger()
        {
            Modifiers = new List<InputButton>();
        }

        public string Name { get; set; }
        public InputButton? Button { get; set; }
        public InputState? State { get; set; }
        public List<InputButton> Modifiers { get; set; }
    }
}