﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace cms.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CMSEntities : DbContext
    {
        public CMSEntities()
            : base("name=CMSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<editor> editors { get; set; }
        public DbSet<menu> menus { get; set; }
        public DbSet<module> modules { get; set; }
        public DbSet<news_item> news_item { get; set; }
        public DbSet<news_item_image> news_item_image { get; set; }
        public DbSet<news_item_menu> news_item_menu { get; set; }
        public DbSet<page> pages { get; set; }
        public DbSet<pages_modules_pos> pages_modules_pos { get; set; }
    }
}
