using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StreamVault.Web.Models;

    public class Movie:BaseProperties
    {
        [Range(1, int.MaxValue)]
        public int DurationMinutes { get; set; }

        [Required]
        public string Director { get; set; }
        public override ContentType Type => ContentType.Movie;
        public override string Details => $"{DurationMinutes} min · Directed by {Director}";
    }

