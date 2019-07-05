﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ferrilata_devilline.Repositories;

namespace ferrilata_devilline.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20190703210041_EmptyDb9")]
    partial class EmptyDb9
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("ferrilata_devilline.Models.AuxPitch", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BadgeName");

                    b.Property<long>("OldLVL");

                    b.Property<string>("PitchMessage");

                    b.Property<long>("PitchedLVL");

                    b.HasKey("Id");

                    b.ToTable("PitchTable");
                });
#pragma warning restore 612, 618
        }
    }
}
