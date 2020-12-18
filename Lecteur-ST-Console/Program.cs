using System;
using System.Threading.Tasks;

namespace Lecteur_ST_Console
{
    class Program
    {
        public static async Task Main()
        {
            Srt srt = new Srt();
            srt.SelectFile();
            srt.RecoverySrt();
            Task t = srt.ReadSrt();
            await t;
        }
    }
}
