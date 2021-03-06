﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Webox.DAL.Database;

namespace Webox.DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Webox.DAL.Entities.Comparison", b =>
                {
                    b.Property<string>("ComparisonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccountId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LaptopId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ComparisonId");

                    b.HasIndex("AccountId");

                    b.HasIndex("LaptopId");

                    b.ToTable("Comparisons");
                });

            modelBuilder.Entity("Webox.DAL.Entities.Deliverer", b =>
                {
                    b.Property<string>("DelivererId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MainOfficeAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("DelivererId");

                    b.HasIndex("CompanyName")
                        .IsUnique();

                    b.HasIndex("MainOfficeAddress")
                        .IsUnique();

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.ToTable("Deliverers");
                });

            modelBuilder.Entity("Webox.DAL.Entities.Laptop", b =>
                {
                    b.Property<string>("LaptopId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("GraphicsCard")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModelImagePath")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("https://firebasestorage.googleapis.com/v0/b/webox-63a97.appspot.com/o/laptops%2Fdefault_laptop_img.png?alt=media&token=74eea36a-2376-4e55-824f-f67a5a4aa753");

                    b.Property<string>("ModelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("OS")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("Processor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RAMCapacity")
                        .HasColumnType("int");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.Property<int>("SSDCapacity")
                        .HasColumnType("int");

                    b.Property<float>("ScreenSize")
                        .HasColumnType("real");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.HasKey("LaptopId");

                    b.HasIndex("ModelName")
                        .IsUnique();

                    b.ToTable("Laptops");
                });

            modelBuilder.Entity("Webox.DAL.Entities.Order", b =>
                {
                    b.Property<string>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccountId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DeliveryAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DeliveryDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PlacementDateTime")
                        .HasColumnType("datetime2");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("OrderId");

                    b.HasIndex("AccountId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Webox.DAL.Entities.OrderItem", b =>
                {
                    b.Property<string>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("LaptopId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("OrderId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("OrderItemId");

                    b.HasIndex("LaptopId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Webox.DAL.Entities.Preference", b =>
                {
                    b.Property<string>("PreferenceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccountId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LaptopId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("PreferenceId");

                    b.HasIndex("AccountId");

                    b.HasIndex("LaptopId");

                    b.ToTable("Preferences");
                });

            modelBuilder.Entity("Webox.DAL.Entities.Review", b =>
                {
                    b.Property<string>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccountId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LaptopId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("PublishDateTime")
                        .HasColumnType("datetime2");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.Property<string>("ReviewText")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReviewId");

                    b.HasIndex("AccountId");

                    b.HasIndex("LaptopId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Webox.DAL.Entities.StorageLot", b =>
                {
                    b.Property<string>("StorageLotId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DelivererId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LaptopId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("LaptopsAmount")
                        .HasColumnType("int");

                    b.Property<float>("LaptopsCostPerUnit")
                        .HasColumnType("real");

                    b.Property<DateTime>("SupplyDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("WarehouseAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StorageLotId");

                    b.HasIndex("DelivererId");

                    b.HasIndex("LaptopId");

                    b.ToTable("StorageLots");
                });

            modelBuilder.Entity("Webox.DAL.Entities.UserAccount", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ProfileImagePath")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("https://firebasestorage.googleapis.com/v0/b/webox-63a97.appspot.com/o/profiles%2Fdefault_profile_img.png?alt=media&token=63549611-0fed-40b6-abde-603778cc63f0");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Webox.DAL.Entities.UserAccount", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Webox.DAL.Entities.UserAccount", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Webox.DAL.Entities.UserAccount", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Webox.DAL.Entities.UserAccount", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Webox.DAL.Entities.Comparison", b =>
                {
                    b.HasOne("Webox.DAL.Entities.UserAccount", "Account")
                        .WithMany("Comparisons")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Webox.DAL.Entities.Laptop", "Laptop")
                        .WithMany("Comparisons")
                        .HasForeignKey("LaptopId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Account");

                    b.Navigation("Laptop");
                });

            modelBuilder.Entity("Webox.DAL.Entities.Order", b =>
                {
                    b.HasOne("Webox.DAL.Entities.UserAccount", "Account")
                        .WithMany("Orders")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Webox.DAL.Entities.OrderItem", b =>
                {
                    b.HasOne("Webox.DAL.Entities.Laptop", "Laptop")
                        .WithMany("OrderItems")
                        .HasForeignKey("LaptopId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Webox.DAL.Entities.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Laptop");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Webox.DAL.Entities.Preference", b =>
                {
                    b.HasOne("Webox.DAL.Entities.UserAccount", "Account")
                        .WithMany("Preferences")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Webox.DAL.Entities.Laptop", "Laptop")
                        .WithMany("Preferences")
                        .HasForeignKey("LaptopId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Account");

                    b.Navigation("Laptop");
                });

            modelBuilder.Entity("Webox.DAL.Entities.Review", b =>
                {
                    b.HasOne("Webox.DAL.Entities.UserAccount", "Account")
                        .WithMany("Reviews")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Webox.DAL.Entities.Laptop", "Laptop")
                        .WithMany("Reviews")
                        .HasForeignKey("LaptopId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Account");

                    b.Navigation("Laptop");
                });

            modelBuilder.Entity("Webox.DAL.Entities.StorageLot", b =>
                {
                    b.HasOne("Webox.DAL.Entities.Deliverer", "Deliverer")
                        .WithMany("StorageLots")
                        .HasForeignKey("DelivererId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Webox.DAL.Entities.Laptop", "Laptop")
                        .WithMany("StorageLots")
                        .HasForeignKey("LaptopId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Deliverer");

                    b.Navigation("Laptop");
                });

            modelBuilder.Entity("Webox.DAL.Entities.Deliverer", b =>
                {
                    b.Navigation("StorageLots");
                });

            modelBuilder.Entity("Webox.DAL.Entities.Laptop", b =>
                {
                    b.Navigation("Comparisons");

                    b.Navigation("OrderItems");

                    b.Navigation("Preferences");

                    b.Navigation("Reviews");

                    b.Navigation("StorageLots");
                });

            modelBuilder.Entity("Webox.DAL.Entities.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("Webox.DAL.Entities.UserAccount", b =>
                {
                    b.Navigation("Comparisons");

                    b.Navigation("Orders");

                    b.Navigation("Preferences");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
