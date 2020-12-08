﻿// <auto-generated />
using System;
using GarbageCan.XP.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GarbageCan.Migrations
{
    [DbContext(typeof(XpContext))]
    partial class XPContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("GarbageCan.XP.Data.Entities.EntityActiveBooster", b =>
                {
                    b.Property<int>("id")
                        .HasColumnType("int");

                    b.Property<DateTime>("expiration_date")
                        .HasColumnType("datetime");

                    b.Property<float>("multipler")
                        .HasColumnType("float");

                    b.HasKey("id");

                    b.ToTable("xp_active_boosters");
                });

            modelBuilder.Entity("GarbageCan.XP.Data.Entities.EntityAvailableSlot", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<ulong>("channel_id")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("id");

                    b.ToTable("xp_available_slots");
                });

            modelBuilder.Entity("GarbageCan.XP.Data.Entities.EntityExcludedChannel", b =>
                {
                    b.Property<ulong>("channel_id")
                        .HasColumnType("bigint unsigned");

                    b.ToTable("xp_excluded_channels");
                });

            modelBuilder.Entity("GarbageCan.XP.Data.Entities.EntityQueuedBooster", b =>
                {
                    b.Property<int>("position")
                        .HasColumnType("int");

                    b.Property<long>("duration_in_seconds")
                        .HasColumnType("bigint");

                    b.Property<float>("multiplier")
                        .HasColumnType("float");

                    b.HasKey("position");

                    b.ToTable("xp_queued_boosters");
                });

            modelBuilder.Entity("GarbageCan.XP.Data.Entities.EntityUser", b =>
                {
                    b.Property<ulong>("id")
                        .HasColumnType("bigint unsigned");

                    b.Property<int>("lvl")
                        .HasColumnType("int");

                    b.Property<double>("xp")
                        .HasColumnType("double");

                    b.HasKey("id");

                    b.ToTable("xp_users");
                });

            modelBuilder.Entity("GarbageCan.XP.Data.Entities.EntityUserBooster", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<long>("duration_in_seconds")
                        .HasColumnType("bigint");

                    b.Property<float>("multiplier")
                        .HasColumnType("float");

                    b.Property<ulong>("user_id")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("id");

                    b.ToTable("xp_user_boosters");
                });

            modelBuilder.Entity("GarbageCan.XP.Data.Entities.EntityActiveBooster", b =>
                {
                    b.HasOne("GarbageCan.XP.Data.Entities.EntityAvailableSlot", "slot")
                        .WithMany()
                        .HasForeignKey("id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("slot");
                });
#pragma warning restore 612, 618
        }
    }
}
