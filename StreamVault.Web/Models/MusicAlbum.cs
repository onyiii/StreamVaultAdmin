using StreamVault.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StreamVault.Web.Models;

public class MusicAlbum : BaseProperties
{
    [Required]
    public string Artist { get; set; }

    public int TrackCount { get; set; }

    public string? RecordLabel { get; set; }
    public override ContentType Type => ContentType.MusicAlbum;
    public override string Details => $"{TrackCount} tracks · {Artist} · {RecordLabel}";
    public override void UpdateSpecificFields(BasePropertiesForm form)
    {
        Artist = form.Artist!.Trim(); TrackCount = form.TrackCount!.Value; RecordLabel = form.RecordLabel!.Trim();
    }
}

