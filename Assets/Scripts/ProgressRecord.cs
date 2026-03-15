using System;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace splash_guardians
{
    [Table("progress")]
    public class ProgressRecord : BaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }

        [Column("user_id")]
        public string UserId { get; set; }

        [Column("level_key")]
        public string LevelKey { get; set; }

        [Column("completed")]
        public bool Completed { get; set; }

        [Column("score")]
        public int Score { get; set; }

        [Column("completed_at")]
        public DateTime? CompletedAt { get; set; }
    }
}