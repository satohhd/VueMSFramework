// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VueMSFramework.Models;
using VueMSFramework.Models.Auth;
using VueMSFramework.Models.Example;
using VueMSFramework.Models.Itsukara;
using VueMSFramework.Models.Keicho;
using VueMSFramework.Models.kintai;
using VueMSFramework.Models.Kintai;

namespace VueMSFramework.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<MailTuser>().HasKey(c => new { c.MailId, c.TuserId });
            modelBuilder.Entity<GroupTuser>().HasKey(c => new { c.GroupId, c.TuserId });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<Itsukara> Itsukaras { get; set; }
        public DbSet<ItsukaraDel> ItsukaraDels { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Column> Columns { get; set; }
        public DbSet<Keicho> Keichos { get; set; }
        public DbSet<KeichoType> KeichoTypes { get; set; }


        public DbSet<Content> Contents { get; set; }
        public DbSet<Shain> Shains { get; set; }
        public DbSet<Kintai> Kintais { get; set; }

        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamCategory> ExamCategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Account> Accounts { get; set; }


        public DbSet<Menu> Menus { get; set; }
        public DbSet<Mail> Mails { get; set; }
        public DbSet<MailTuser> MailTusers { get; set; }
        public DbSet<Tuser> Tusers { get; set; }
        public DbSet<Option> Options { get; set; }

        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupTuser> GroupTusers { get; set; }

        public DbSet<Example> Examples { get; set; }

    }
}
