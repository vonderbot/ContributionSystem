﻿// <auto-generated />
using ContributionSystem.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ContributionSystem.DataAccess.Migrations
{
    [DbContext(typeof(ContributionDbContext))]
    partial class ContributionDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ContributionSystem.Entities.Entities.Contribution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Percent")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("StartValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Term")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Contribution");
                });
#pragma warning restore 612, 618
        }
    }
}
