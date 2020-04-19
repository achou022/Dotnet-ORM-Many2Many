﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeddingPlanner.Models;

namespace WeddingPlanner.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("WeddingPlanner.Models.RSVP", b =>
                {
                    b.Property<int>("RSVPId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AttendeeId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<int>("WeddingId");

                    b.HasKey("RSVPId");

                    b.HasIndex("AttendeeId");

                    b.HasIndex("WeddingId");

                    b.ToTable("AllRSVPs");
                });

            modelBuilder.Entity("WeddingPlanner.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("UserId");

                    b.ToTable("AllUsers");
                });

            modelBuilder.Entity("WeddingPlanner.Models.Wedding", b =>
                {
                    b.Property<int>("WeddingId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bride")
                        .IsRequired();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("CreatorId");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Groom")
                        .IsRequired();

                    b.Property<string>("Location")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("WeddingId");

                    b.HasIndex("CreatorId");

                    b.ToTable("AllWeddings");
                });

            modelBuilder.Entity("WeddingPlanner.Models.RSVP", b =>
                {
                    b.HasOne("WeddingPlanner.Models.User", "Attendee")
                        .WithMany("RSVPs")
                        .HasForeignKey("AttendeeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WeddingPlanner.Models.Wedding", "Wedding")
                        .WithMany("RSVPs")
                        .HasForeignKey("WeddingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WeddingPlanner.Models.Wedding", b =>
                {
                    b.HasOne("WeddingPlanner.Models.User", "Creator")
                        .WithMany("Weddings")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
