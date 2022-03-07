﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RenewalTML.EFCore;
using Volo.Abp.EntityFrameworkCore;

namespace RenewalTML.EFCore.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20210816150254_cropper")]
    partial class cropper
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("_Abp_DatabaseProvider", EfCoreDatabaseProvider.MySql)
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.7");

            modelBuilder.Entity("RenewalTML.Data.Model.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Balance")
                        .HasColumnType("int");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PasswordSalt")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("ScreenName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("UserAvatar_big")
                        .HasColumnType("int");

                    b.Property<int>("UserAvatar_medium")
                        .HasColumnType("int");

                    b.Property<int>("UserAvatar_small")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("VkId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("RenewalTML.Data.Model.CroppedImageFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ImageId")
                        .HasColumnType("int");

                    b.Property<double>("height")
                        .HasColumnType("double");

                    b.Property<double>("width")
                        .HasColumnType("double");

                    b.Property<double>("x")
                        .HasColumnType("double");

                    b.Property<double>("y")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("CroppedImageFiles");
                });

            modelBuilder.Entity("RenewalTML.Data.Model.File", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Extension")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Path")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("SitePath")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<double>("Size")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("RenewalTML.Data.Model.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("RequereName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("RoleName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("RoleStyle")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("isHaveAccesToAdminPanel")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("isHaveAccesToEditRoles")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("isHaveAccessToSite")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
