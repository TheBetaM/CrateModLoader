using System;

namespace CrateModLoader
{

    public class EventValueArgs<T> : EventArgs
    {
        public T Value;

        public EventValueArgs(T value)
        {
            Value = value;
        }
    }

    public class EventGameDetails : EventArgs
    {
        public Game Game;
        public string Console;
        public string Region;

        public EventGameDetails(Game g, string cons, string reg)
        {
            Game = g;
            Console = cons;
            Region = reg;
        }
    }

}
