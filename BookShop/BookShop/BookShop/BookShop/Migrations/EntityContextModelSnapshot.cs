﻿// <auto-generated />
using System;
using MusicEvents.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MusicEvents.Migrations
{
    [DbContext(typeof(EntityContext))]
    partial class EntityContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MusicEvents.Data.Music", Music =>
                {
                    Music.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    Music.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    Music.Property<string>("ImageBase64")
                        .HasColumnType("nvarchar(max)");

                    Music.Property<bool>("IsPublic")
                        .HasColumnType("bit");

                    Music.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    Music.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    Music.HasKey("Id");

                    Music.HasIndex("UserId");

                    Music.ToTable("Events");
                });

            modelBuilder.Entity("MusicEvents.Data.User", Music =>
                {
                    Music.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    Music.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    Music.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    Music.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    Music.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    Music.HasKey("Id");

                    Music.ToTable("Users");
                });

            modelBuilder.Entity("MusicEvents.Data.Music", Music =>
                {
                    Music.HasOne("MusicEvents.Data.User", "User")
                        .WithMany("Events")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    Music.Navigation("User");
                });

            modelBuilder.Entity("MusicEvents.Data.User", Music =>
                {
                    Music.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
