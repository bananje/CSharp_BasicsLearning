using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    //при возврате значения оно
    //может автоматически сопоставляться с несколькими строками, разделенными
    //запятыми, вместо того чтобы возвращать значение int.
    [System.Flags]
    public enum WondersOfTheAncientWorld : byte
    {
        GreatPyramidOfGiza = 0b_0000_0001,
        HangingGardensOfBabylon = 0b_0000_0010,
        StatueOfZeusAtOlympia = 0b_0000_0100,
        TempleOfArtemisAtEphesus = 0b_0000_1000,
        MausoleumAtHalicarnassus = 0b_0001_0000,
        ColossusOfRhodes = 0b_0010_0000,
        LighthouseOfAlexandria = 0b_0100_0000
    }
}
