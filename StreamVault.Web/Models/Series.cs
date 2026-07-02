using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StreamVault.Web.Models;

public class Series:BaseProperties
{
    [Range(1, int.MaxValue)]
    public int NumberOfSeasons { get; set; }

    [Range(1, int.MaxValue)]
    public int TotalEpisodes { get; set; }
    public override ContentType Type => ContentType.Series;
    public override string Details => $"{NumberOfSeasons} season(s) · {TotalEpisodes} episodes";
}
