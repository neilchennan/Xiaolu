﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Xiaolu.Models.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class XiaoluEntities : DbContext
    {
        public XiaoluEntities()
            : base("name=XiaoluEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<User> xiaolu_user { get; set; }
        public virtual DbSet<History> xiaolu_history { get; set; }
        public virtual DbSet<UserAccessToken> xiaolu_user_access_token { get; set; }
    }
}
