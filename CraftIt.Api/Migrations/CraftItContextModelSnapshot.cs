﻿// <auto-generated />
using System;
using CraftIt.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CraftIt.Api.Migrations
{
    [DbContext(typeof(CraftItContext))]
    partial class CraftItContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<byte[]>("ProductImage");

                    b.Property<string>("Requirements");

                    b.Property<string>("TimeEstimate");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("CraftIt.Api.Models.Instruction", b =>
                {
                    b.HasOne("CraftIt.Api.Models.Product")
                        .WithMany("Instructions")
                        .HasForeignKey("ProductId");
                });
#pragma warning restore 612, 618
        }
    }
}
