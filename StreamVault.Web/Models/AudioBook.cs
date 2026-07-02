using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Text;

namespace StreamVault.Web.Models;

public class AudioBook:BaseProperties
{
    [Required]
    public string Author { get; set; }

    [Required]
    public string Narrator { get; set; }

    [Range(1, int.MaxValue)]
    public int DurationMinutes { get; set; }
    public override ContentType Type => ContentType.AudioBook;
    public override string Details => $"{DurationMinutes} min · {Author}, narrated by {Narrator}";
}
