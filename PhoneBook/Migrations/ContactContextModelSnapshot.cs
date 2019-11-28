﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PhoneBook.Models;

namespace PhoneBook.Migrations
{
    [DbContext(typeof(ContactContext))]
    partial class ContactContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PhoneBook.Models.Contact", b =>
                {
                    b.Property<int>("ContactId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cell_phone")
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("First_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Home_phone")
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("IRD")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 64)))
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Last_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Middle_initial")
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Office_ext")
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("active")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 1)))
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("ContactId");

                    b.ToTable("Contacts");
                });
#pragma warning restore 612, 618
        }
    }
}
