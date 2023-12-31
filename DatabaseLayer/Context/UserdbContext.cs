﻿using System;
using System.Collections.Generic;
using DB.TableModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DBLayer.Context
{
    public partial class UserdbContext : DbContext
    {
        private ILogger _logger { get; set; }

        public UserdbContext(ILoggerProvider loggerProvider)
        {
            _logger = loggerProvider.CreateLogger("dbUserLogger");
        }

        public UserdbContext(DbContextOptions<UserdbContext> options)
            : base(options)
        {
        }
        
        public DbSet<User> Users {get;set;}
        public DbSet<PageAccessRight> AccessRights {get;set;} 

        public DbSet<ParamAccessRight> ParamAccessRights {get;set;} 

        public DbSet<PunktAccessRight> PunktAccessRight {get;set;}

        public DbSet<NullDataTable> NullDataTables {get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        { 
            optionsBuilder.UseNpgsql("User Id=admin;Password=fYho2G;Server=192.168.29.128;Port=5432;Database=userdb;");
            Action<string> a = str =>
            {
                _logger.LogTrace(str);
                //Console.WriteLine(str);
            };
            optionsBuilder.LogTo(a);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasMany(u => u.right_list)
                .WithOne(r => r.User)
                .HasForeignKey(k => k.UserId);


            modelBuilder
                .Entity<User>()
                .HasMany(u => u.param_list)
                .WithOne(r => r.User)
                .HasForeignKey(k => k.UserId);

            modelBuilder
                .Entity<User>()
                .HasMany(u => u.punkt_list)
                .WithOne(r => r.User)
                .HasForeignKey(k => k.UserId);

            modelBuilder
                .Entity<User>()
                .HasMany(u => u.punkt_list)
                .WithOne(r => r.User)
                .HasForeignKey(k => k.UserId);

            modelBuilder.Entity<PageAccessRight>().ToTable("PageAccessRight"); 
            modelBuilder.Entity<ParamAccessRight>().ToTable("ParamAccessRight"); 


            OnModelCreatingPartial(modelBuilder);

            
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}