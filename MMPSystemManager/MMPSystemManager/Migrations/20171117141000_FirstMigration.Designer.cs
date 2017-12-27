﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using MMPSystemManager.DBContext;
using System;

namespace MMPSystemManager.Migrations
{
    [DbContext(typeof(MMPContext))]
    [Migration("20171117141000_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("MMPSystemManager.Module.SystemUser", b =>
                {
                    b.Property<string>("SystemUserID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Passwd");

                    b.Property<string>("SystemUserName");

                    b.HasKey("SystemUserID");

                    b.ToTable("SystemUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
