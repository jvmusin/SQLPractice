using System;
using System.Data.Linq.Mapping;

namespace Tests
{
    [Table(Name = "Users")]
    public class UserEntity
    {
        [Column(Name = "UserId", IsPrimaryKey = true, DbType = "uniqueidentifier")]
        public Guid UserId { get; set; }
        [Column(Name = "Login", DbType = "nvarchar(256)", CanBeNull = false)]
        public string Login { get; set; }
        [Column(Name = "PasswordHash", DbType = "nvarchar(256)", CanBeNull = false)]
        public string PasswordHash { get; set; }
    }
}