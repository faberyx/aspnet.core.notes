using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using notes_manager.Models.Context;

namespace notes_manager.Migrations
{
    [DbContext(typeof(SQLiteContext))]
    partial class SQLiteContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("notes_manager.Models.Entities.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("Id");
                });
        }
    }
}
