using System;
using System.Collections.Generic;

namespace HoleInOneControlAPI.Models;

public partial class Handicap
{
    public int IdHandicap { get; set; }

    public int? IdUser { get; set; }

    public int? HoleOne { get; set; }

    public int? HoleTwo { get; set; }

    public int? HoleFour { get; set; }

    public int? HoleFive { get; set; }

    public int? HoleSix { get; set; }

    public int? HoleSeven { get; set; }

    public int? HoleEight { get; set; }

    public int? HoleNine { get; set; }

    public int? HoleTen { get; set; }

    public int? HoleEleven { get; set; }

    public int? HoleTwelve { get; set; }

    public int? HoleThirteen { get; set; }

    public int? HoleFourteen { get; set; }

    public int? HoleFifteen { get; set; }

    public int? HoleSixteen { get; set; }

    public int? HoleSeventeen { get; set; }

    public int? HoleEighteen { get; set; }

    public int? HoleNineteen { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
