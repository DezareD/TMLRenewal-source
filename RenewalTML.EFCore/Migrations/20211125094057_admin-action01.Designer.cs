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
    [Migration("20211125094057_admin-action01")]
    partial class adminaction01
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("_Abp_DatabaseProvider", EfCoreDatabaseProvider.MySql)
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.7");

            modelBuilder.Entity("RenewalTML.Data.Model.AdminAction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AdminId")
                        .HasColumnType("int");

                    b.Property<string>("Date")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("HtmlText")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Icon")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("AdminActions");
                });

            modelBuilder.Entity("RenewalTML.Data.Model.Award", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AwardType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Icon")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("ProgressFinal")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.Property<string>("requereName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Awards");
                });

            modelBuilder.Entity("RenewalTML.Data.Model.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Balance")
                        .HasColumnType("int");

                    b.Property<int>("CurrencyExp")
                        .HasColumnType("int");

                    b.Property<int>("GeneralExp")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PasswordSalt")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("ScreenName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("UserAvatar_cropp")
                        .HasColumnType("int");

                    b.Property<int>("UserAvatar_cropp64x64")
                        .HasColumnType("int");

                    b.Property<int>("UserAvatar_main")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("VkId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("RenewalTML.Data.Model.ClientAward", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AwardId")
                        .HasColumnType("int");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<int>("Progress")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ClientAwards");
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

                    b.Property<int>("hourseToDelete")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("RenewalTML.Data.Model.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ClientOwnerId")
                        .HasColumnType("int");

                    b.Property<string>("Date")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Information")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("LogoType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Text")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("isViewed")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Notifications");
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

                    b.Property<bool>("isBlockedEconomic")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("isHaveAccesToAdminPanel")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("isHaveAccesToEditRoles")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("isHaveAccesToPremiumEditor")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("isHaveAccesToUltimateEditor")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("isHaveAccessToSite")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("isHaveToAdministrateUserAccount")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("isHaveToModerateTransactions")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("isHaveToModerateUserAccount")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("isHaveToViewUserList")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("RenewalTML.Data.Model.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Date")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Information")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("OutEntityId")
                        .HasColumnType("int");

                    b.Property<int>("ToEntityId")
                        .HasColumnType("int");

                    b.Property<string>("TransactionType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
