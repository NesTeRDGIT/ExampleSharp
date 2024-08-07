﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using zms.Persistence.SqlServer.EF.Articles.Context;

#nullable disable

namespace zms.Persistence.SqlServer.EF.Articles.Migrations
{
    [DbContext(typeof(ArticlesContext))]
    [Migration("20240611053226_InitCreate")]
    partial class InitCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.HasSequence("ArticleHiLo");

            modelBuilder.HasSequence("AttachmentHiLo");

            modelBuilder.Entity("zms.Support.Articles.Domain.OfArticle.Article", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasComment("Первичный ключ");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasComment("Дата создания");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.ToTable("Article", null, t =>
                        {
                            t.HasComment("Таблица статей");
                        });
                });

            modelBuilder.Entity("zms.Support.Articles.Domain.OfAttachment.Attachment", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasComment("Первичный ключ");

                    b.Property<long?>("ArticleId")
                        .HasColumnType("bigint")
                        .HasComment("Внешний ключ");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasComment("Дата создания");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<long>("StorageId")
                        .HasColumnType("bigint")
                        .HasComment("Идентификатор в хранилище");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.ToTable("Attachment", null, t =>
                        {
                            t.HasComment("Таблица вложений");
                        });
                });

            modelBuilder.Entity("zms.Support.Articles.Domain.OfArticle.Article", b =>
                {
                    b.OwnsOne("zms.Support.Articles.Domain.OfArticle.Content", "Content", b1 =>
                        {
                            b1.Property<long>("ArticleId")
                                .HasColumnType("bigint");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasComment("Содержимое");

                            b1.HasKey("ArticleId");

                            b1.ToTable("Article");

                            b1.WithOwner()
                                .HasForeignKey("ArticleId");
                        });

                    b.OwnsOne("zms.Support.Articles.Domain.OfArticle.Title", "Title", b1 =>
                        {
                            b1.Property<long>("ArticleId")
                                .HasColumnType("bigint");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasComment("Заголовок");

                            b1.HasKey("ArticleId");

                            b1.ToTable("Article");

                            b1.WithOwner()
                                .HasForeignKey("ArticleId");
                        });

                    b.Navigation("Content")
                        .IsRequired();

                    b.Navigation("Title")
                        .IsRequired();
                });

            modelBuilder.Entity("zms.Support.Articles.Domain.OfAttachment.Attachment", b =>
                {
                    b.HasOne("zms.Support.Articles.Domain.OfArticle.Article", null)
                        .WithMany()
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.OwnsOne("zms.Support.Articles.Domain.OfAttachment.AttachmentType", "Type", b1 =>
                        {
                            b1.Property<long>("AttachmentId")
                                .HasColumnType("bigint");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasComment("Наименование типа вложения");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasComment("Код типа вложения");

                            b1.HasKey("AttachmentId");

                            b1.ToTable("Attachment");

                            b1.WithOwner()
                                .HasForeignKey("AttachmentId");
                        });

                    b.OwnsOne("zms.Support.Articles.Domain.OfAttachment.Name", "Name", b1 =>
                        {
                            b1.Property<long>("AttachmentId")
                                .HasColumnType("bigint");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasComment("Наименование");

                            b1.HasKey("AttachmentId");

                            b1.ToTable("Attachment");

                            b1.WithOwner()
                                .HasForeignKey("AttachmentId");
                        });

                    b.Navigation("Name")
                        .IsRequired();

                    b.Navigation("Type")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
