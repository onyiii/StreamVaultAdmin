
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mime;
using System.Text;

namespace StreamVault.Web.Models;

    public abstract class BaseProperties
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Description { get; set; }

        [Required]
        public DateOnly ReleaseDate { get; set; }

        public AgeRating AgeRating { get; set; }

        [Required]
        [StringLength(100)]
        public string Genre { get; set; } = string.Empty;

        [NotMapped] public abstract ContentType Type { get; }
        [NotMapped] public abstract string Details { get; }

        
    }

