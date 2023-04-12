using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DOOR.EF.Models
{
    [Table("KEYS")]
    [Index("Use", Name = "IX_KEYS_USE")]
    public partial class Key
    {
        [Key]
        [Column("ID")]
        public string Id { get; set; } = null!;
        [Column("VERSION")]
        [Precision(10)]
        public int Version { get; set; }
        [Column("CREATED")]
        [Precision(7)]
        public DateTime Created { get; set; }
        [Column("USE")]
        public string? Use { get; set; }
        [Column("ALGORITHM")]
        [StringLength(100)]
        public string Algorithm { get; set; } = null!;
        [Column("IS_X509_CERTIFICATE")]
        [Precision(1)]
        public bool IsX509Certificate { get; set; }
        [Column("DATA_PROTECTED")]
        [Precision(1)]
        public bool DataProtected { get; set; }
        [Column("DATA")]
        public string Data { get; set; } = null!;
    }
}
