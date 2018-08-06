﻿// <auto-generated />
using Infrastructure.DataProvider;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.DataProvider.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20180805185923_AddNestedItems")]
    partial class AddNestedItems
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Infrastructure.DataProvider.AttachmentDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted");

                    b.Property<string>("Path");

                    b.HasKey("Id");

                    b.ToTable("Attachment");

                    b.HasData(
                        new { Id = 1, Deleted = false, Path = "some/path" },
                        new { Id = 2, Deleted = false, Path = "some/another/path" },
                        new { Id = 3, Deleted = true, Path = "some/either/path" }
                    );
                });

            modelBuilder.Entity("Infrastructure.DataProvider.AttachmentLinkDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AttachmentId");

                    b.Property<int>("DocumentId");

                    b.HasKey("Id");

                    b.HasIndex("AttachmentId");

                    b.HasIndex("DocumentId");

                    b.ToTable("AttachmentLink");

                    b.HasData(
                        new { Id = 1, AttachmentId = 1, DocumentId = 1 },
                        new { Id = 2, AttachmentId = 2, DocumentId = 1 },
                        new { Id = 3, AttachmentId = 3, DocumentId = 2 },
                        new { Id = 4, AttachmentId = 1, DocumentId = 2 },
                        new { Id = 5, AttachmentId = 2, DocumentId = 3 }
                    );
                });

            modelBuilder.Entity("Infrastructure.DataProvider.DocumentDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted");

                    b.Property<int>("DocumentType");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Document");

                    b.HasData(
                        new { Id = 1, Deleted = false, DocumentType = 0, Name = "Первый документ первого типа" },
                        new { Id = 2, Deleted = true, DocumentType = 0, Name = "Второй удаленный документ первого типа" },
                        new { Id = 3, Deleted = false, DocumentType = 0, Name = "Третий документ первого типа" },
                        new { Id = 4, Deleted = false, DocumentType = 0, Name = "Четвертый документ первого типа" }
                    );
                });

            modelBuilder.Entity("Infrastructure.DataProvider.NestedItemDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted");

                    b.Property<string>("NestedItemName");

                    b.Property<int>("OtherDocumentItemId");

                    b.HasKey("Id");

                    b.HasIndex("OtherDocumentItemId");

                    b.ToTable("NestedItem");

                    b.HasData(
                        new { Id = 1, Deleted = false, NestedItemName = "nested item name 1", OtherDocumentItemId = 1 },
                        new { Id = 2, Deleted = false, NestedItemName = "nested item name 2", OtherDocumentItemId = 1 },
                        new { Id = 3, Deleted = false, NestedItemName = "nested item name 3", OtherDocumentItemId = 2 },
                        new { Id = 4, Deleted = false, NestedItemName = "nested item name 4", OtherDocumentItemId = 2 },
                        new { Id = 5, Deleted = false, NestedItemName = "nested item name 5", OtherDocumentItemId = 3 },
                        new { Id = 6, Deleted = false, NestedItemName = "nested item name 6", OtherDocumentItemId = 3 },
                        new { Id = 7, Deleted = false, NestedItemName = "nested item name 7", OtherDocumentItemId = 4 },
                        new { Id = 8, Deleted = false, NestedItemName = "nested item name 8", OtherDocumentItemId = 4 },
                        new { Id = 9, Deleted = false, NestedItemName = "nested item name 9", OtherDocumentItemId = 8 }
                    );
                });

            modelBuilder.Entity("Infrastructure.DataProvider.OneMoreNestedItemDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted");

                    b.Property<int>("NestedItemId");

                    b.Property<string>("OneMoreNestedItemName");

                    b.HasKey("Id");

                    b.HasIndex("NestedItemId");

                    b.ToTable("OneMoreNestedItem");

                    b.HasData(
                        new { Id = 1, Deleted = false, NestedItemId = 1, OneMoreNestedItemName = "one more nested item name 1" },
                        new { Id = 2, Deleted = false, NestedItemId = 1, OneMoreNestedItemName = "one more nested item name 2" },
                        new { Id = 3, Deleted = false, NestedItemId = 2, OneMoreNestedItemName = "one more nested item name 3" },
                        new { Id = 4, Deleted = false, NestedItemId = 2, OneMoreNestedItemName = "one more nested item name 4" },
                        new { Id = 5, Deleted = false, NestedItemId = 3, OneMoreNestedItemName = "one more nested item name 5" },
                        new { Id = 6, Deleted = false, NestedItemId = 3, OneMoreNestedItemName = "one more nested item name 6" },
                        new { Id = 7, Deleted = false, NestedItemId = 4, OneMoreNestedItemName = "one more nested item name 7" },
                        new { Id = 8, Deleted = false, NestedItemId = 4, OneMoreNestedItemName = "one more nested item name 8" },
                        new { Id = 9, Deleted = false, NestedItemId = 5, OneMoreNestedItemName = "one more nested item name 9" },
                        new { Id = 10, Deleted = false, NestedItemId = 5, OneMoreNestedItemName = "one more nested item name 10" },
                        new { Id = 11, Deleted = false, NestedItemId = 6, OneMoreNestedItemName = "one more nested item name 11" },
                        new { Id = 12, Deleted = false, NestedItemId = 6, OneMoreNestedItemName = "one more nested item name 12" },
                        new { Id = 13, Deleted = false, NestedItemId = 7, OneMoreNestedItemName = "one more nested item name 13" },
                        new { Id = 14, Deleted = false, NestedItemId = 7, OneMoreNestedItemName = "one more nested item name 14" },
                        new { Id = 15, Deleted = false, NestedItemId = 8, OneMoreNestedItemName = "one more nested item name 15" },
                        new { Id = 16, Deleted = false, NestedItemId = 8, OneMoreNestedItemName = "one more nested item name 16" }
                    );
                });

            modelBuilder.Entity("Infrastructure.DataProvider.OtherDocumentDto", b =>
                {
                    b.Property<int>("Id");

                    b.Property<bool>("Deleted");

                    b.Property<string>("TestName");

                    b.HasKey("Id");

                    b.ToTable("OtherDocument");

                    b.HasData(
                        new { Id = 1, Deleted = false, TestName = "Первый документ первого типа" },
                        new { Id = 2, Deleted = true, TestName = "Второй документ первого типа" },
                        new { Id = 3, Deleted = false, TestName = "Третий удаленный документ первого типа" },
                        new { Id = 4, Deleted = false, TestName = "Четвертый документ первого типа" }
                    );
                });

            modelBuilder.Entity("Infrastructure.DataProvider.OtherDocumentItemDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted");

                    b.Property<string>("Name");

                    b.Property<int>("OtherDocumentId");

                    b.HasKey("Id");

                    b.HasIndex("OtherDocumentId");

                    b.ToTable("OtherDocumentItem");

                    b.HasData(
                        new { Id = 1, Deleted = false, Name = "item 1 1", OtherDocumentId = 1 },
                        new { Id = 2, Deleted = true, Name = "item 1 2", OtherDocumentId = 1 },
                        new { Id = 3, Deleted = false, Name = "item 2 1", OtherDocumentId = 2 },
                        new { Id = 4, Deleted = true, Name = "item 2 2", OtherDocumentId = 2 },
                        new { Id = 5, Deleted = false, Name = "item 3 1", OtherDocumentId = 3 },
                        new { Id = 6, Deleted = true, Name = "item 3 2", OtherDocumentId = 3 },
                        new { Id = 7, Deleted = false, Name = "item 4 1", OtherDocumentId = 4 },
                        new { Id = 8, Deleted = true, Name = "item 4 2", OtherDocumentId = 4 }
                    );
                });

            modelBuilder.Entity("Infrastructure.DataProvider.OtherDocumentPaymentDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted");

                    b.Property<int>("OtherDocumentId");

                    b.Property<string>("Total");

                    b.HasKey("Id");

                    b.HasIndex("OtherDocumentId");

                    b.ToTable("OtherDocumentPayment");

                    b.HasData(
                        new { Id = 1, Deleted = false, OtherDocumentId = 1, Total = "150" },
                        new { Id = 2, Deleted = true, OtherDocumentId = 1, Total = "25" },
                        new { Id = 3, Deleted = false, OtherDocumentId = 2, Total = "450" },
                        new { Id = 4, Deleted = true, OtherDocumentId = 2, Total = "132" },
                        new { Id = 5, Deleted = false, OtherDocumentId = 3, Total = "444" },
                        new { Id = 6, Deleted = true, OtherDocumentId = 3, Total = "521" },
                        new { Id = 7, Deleted = false, OtherDocumentId = 4, Total = "421" },
                        new { Id = 8, Deleted = true, OtherDocumentId = 4, Total = "4444" }
                    );
                });

            modelBuilder.Entity("Infrastructure.DataProvider.AttachmentLinkDto", b =>
                {
                    b.HasOne("Infrastructure.DataProvider.AttachmentDto", "AttachmentDto")
                        .WithMany("AttachmentLinkDtos")
                        .HasForeignKey("AttachmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Infrastructure.DataProvider.DocumentDto", "DocumentDto")
                        .WithMany("AttachmentLinkDtos")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Infrastructure.DataProvider.NestedItemDto", b =>
                {
                    b.HasOne("Infrastructure.DataProvider.OtherDocumentItemDto", "OtherDocumentItemDto")
                        .WithMany("NestedItemDtos")
                        .HasForeignKey("OtherDocumentItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Infrastructure.DataProvider.OneMoreNestedItemDto", b =>
                {
                    b.HasOne("Infrastructure.DataProvider.NestedItemDto", "NestedItemDto")
                        .WithMany("OneMoreNestedItemDtos")
                        .HasForeignKey("NestedItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Infrastructure.DataProvider.OtherDocumentDto", b =>
                {
                    b.HasOne("Infrastructure.DataProvider.DocumentDto", "DocumentDto")
                        .WithOne("OtherDocumentDto")
                        .HasForeignKey("Infrastructure.DataProvider.OtherDocumentDto", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Infrastructure.DataProvider.OtherDocumentItemDto", b =>
                {
                    b.HasOne("Infrastructure.DataProvider.OtherDocumentDto", "OtherDocumentDto")
                        .WithMany("OtherDocumentItemDtos")
                        .HasForeignKey("OtherDocumentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Infrastructure.DataProvider.OtherDocumentPaymentDto", b =>
                {
                    b.HasOne("Infrastructure.DataProvider.OtherDocumentDto", "OtherDocumentDto")
                        .WithMany("OtherDocumentPaymentDtos")
                        .HasForeignKey("OtherDocumentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}