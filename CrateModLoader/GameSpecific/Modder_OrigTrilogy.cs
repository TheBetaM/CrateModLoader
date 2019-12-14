//Original trilogy games API by chekwob and ManDude

namespace CrateModLoader
{
    /*
     * This class should be used for modding options that are common
     * to all games of the original trilogy.
     * For example, a "random sound effect" mod can be done by
     * randomly swapping around sound entries, and this behavior
     * isn't tied to one specific game.
     */
    class Modder_OrigTrilogy
    {
        public string gameName = "Crash Trilogy (Original)";
        public string apiCredit = "API by chekwob and ManDude";
        public System.Drawing.Image gameIcon = null;
        public string[] modOptions = { "Randomize sound effects" };
    }
}
