using System;
using System.ComponentModel.DataAnnotations.Schema;
using GarbageCan.Moderation;
using Microsoft.EntityFrameworkCore;

namespace GarbageCan.Data.Entities.Moderation
{
    [Table("moderation_warnings")]
    [Keyless]
    public class EntityActionLog
    {
        public ulong uId { get; set; }
        [Column(TypeName = "datetime")] public DateTime issuedDate { get; set; }
        public PunishmentLevel punishmentLevel { get; set; }
        public string comments { get; set; }
    }
}