﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ferrilata_devilline.Repositories;

namespace ferrilata_devilline.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20190725144240_Initial1")]
    partial class Initial1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("ferrilata_devilline.Models.DAOs.Badge", b =>
                {
                    b.Property<long>("BadgeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Tag");

                    b.Property<double>("Version");

                    b.HasKey("BadgeId");

                    b.ToTable("Badges");
                });

            modelBuilder.Entity("ferrilata_devilline.Models.DAOs.Level", b =>
                {
                    b.Property<long>("LevelId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int>("LevelNumber");

                    b.Property<string>("Weight");

                    b.Property<long?>("f");

                    b.HasKey("LevelId");

                    b.HasIndex("f");

                    b.ToTable("Levels");
                });

            modelBuilder.Entity("ferrilata_devilline.Models.DAOs.Pitch", b =>
                {
                    b.Property<long>("PitchId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("Created");

                    b.Property<long?>("LevelId");

                    b.Property<string>("PitchedLevel");

                    b.Property<string>("PitchedMessage");

                    b.Property<string>("Result");

                    b.Property<string>("Status");

                    b.Property<long?>("UserId");

                    b.HasKey("PitchId");

                    b.HasIndex("LevelId");

                    b.HasIndex("UserId");

                    b.ToTable("Pitches");
                });

            modelBuilder.Entity("ferrilata_devilline.Models.DAOs.Review", b =>
                {
                    b.Property<long>("ReviewId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Message");

                    b.Property<long?>("PitchId");

                    b.Property<string>("Result");

                    b.Property<long?>("UserId");

                    b.HasKey("ReviewId");

                    b.HasIndex("PitchId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("ferrilata_devilline.Models.DAOs.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Role");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ferrilata_devilline.Models.DAOs.UserLevel", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<long>("LevelId");

                    b.HasKey("UserId", "LevelId");

                    b.HasIndex("LevelId");

                    b.ToTable("UserLevels");
                });

            modelBuilder.Entity("ferrilata_devilline.Models.DAOs.Level", b =>
                {
                    b.HasOne("ferrilata_devilline.Models.DAOs.Badge", "Badge")
                        .WithMany("Levels")
                        .HasForeignKey("f");
                });

            modelBuilder.Entity("ferrilata_devilline.Models.DAOs.Pitch", b =>
                {
                    b.HasOne("ferrilata_devilline.Models.DAOs.Level", "Level")
                        .WithMany("Pitches")
                        .HasForeignKey("LevelId");

                    b.HasOne("ferrilata_devilline.Models.DAOs.User", "User")
                        .WithMany("Pitches")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ferrilata_devilline.Models.DAOs.Review", b =>
                {
                    b.HasOne("ferrilata_devilline.Models.DAOs.Pitch", "Pitch")
                        .WithMany("Reviews")
                        .HasForeignKey("PitchId");

                    b.HasOne("ferrilata_devilline.Models.DAOs.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ferrilata_devilline.Models.DAOs.UserLevel", b =>
                {
                    b.HasOne("ferrilata_devilline.Models.DAOs.Level", "Level")
                        .WithMany("UserLevels")
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ferrilata_devilline.Models.DAOs.User", "User")
                        .WithMany("UserLevels")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
