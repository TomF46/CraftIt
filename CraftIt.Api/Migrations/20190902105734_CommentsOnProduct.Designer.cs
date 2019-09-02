﻿// <auto-generated />
using System;
using CraftIt.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CraftIt.Api.Migrations
{
    [DbContext(typeof(CraftItContext))]
    [Migration("20190902105734_CommentsOnProduct")]
    partial class CommentsOnProduct
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CraftIt.Api.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Message");

                    b.Property<int?>("ProductId");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("CraftIt.Api.Models.Instruction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<byte[]>("Image");

                    b.Property<int>("Ordinal");

                    b.Property<int?>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Instructions");
                });

            modelBuilder.Entity("CraftIt.Api.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AddedById");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<byte[]>("ProductImage");

                    b.Property<string>("Requirements");

                    b.Property<string>("TimeEstimate");

                    b.HasKey("Id");

                    b.HasIndex("AddedById");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("CraftIt.Api.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CraftIt.Api.Models.Comment", b =>
                {
                    b.HasOne("CraftIt.Api.Models.Product", "Product")
                        .WithMany("Comments")
                        .HasForeignKey("ProductId");

                    b.HasOne("CraftIt.Api.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("CraftIt.Api.Models.Instruction", b =>
                {
                    b.HasOne("CraftIt.Api.Models.Product")
                        .WithMany("Instructions")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("CraftIt.Api.Models.Product", b =>
                {
                    b.HasOne("CraftIt.Api.Models.User", "AddedBy")
                        .WithMany()
                        .HasForeignKey("AddedById");
                });
#pragma warning restore 612, 618
        }
    }
}
