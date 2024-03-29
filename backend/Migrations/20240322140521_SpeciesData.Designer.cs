﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WhaleSpotting;

#nullable disable

namespace WhaleSpotting.Migrations
{
    [DbContext(typeof(WhaleSpottingContext))]
    [Migration("20240322140521_SpeciesData")]
    partial class SpeciesData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("WhaleSpotting.Models.Data.Achievement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("BadgeImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Achievements");
                });

            modelBuilder.Entity("WhaleSpotting.Models.Data.BodyOfWater", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("BodiesOfWater");
                });

            modelBuilder.Entity("WhaleSpotting.Models.Data.Reaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("SightingId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SightingId");

                    b.HasIndex("UserId");

                    b.ToTable("Reactions");
                });

            modelBuilder.Entity("WhaleSpotting.Models.Data.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        });
                });

            modelBuilder.Entity("WhaleSpotting.Models.Data.Sighting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BodyOfWaterId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreationTimestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Latitude")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("SightingTimestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("SpeciesId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int?>("VerificationEventId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BodyOfWaterId");

                    b.HasIndex("SpeciesId");

                    b.HasIndex("UserId");

                    b.HasIndex("VerificationEventId");

                    b.ToTable("Sightings");
                });

            modelBuilder.Entity("WhaleSpotting.Models.Data.Species", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ExampleImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("WikiLink")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Species");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ExampleImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/62/Berardius_bairdii.jpg/1599px-Berardius_bairdii.jpg?20151221184110",
                            Name = "Beaked whale",
                            WikiLink = "https://en.wikipedia.org/wiki/Beaked_whale"
                        },
                        new
                        {
                            Id = 2,
                            ExampleImageUrl = "https://upload.wikimedia.org/wikipedia/commons/e/e8/Oceanogr%C3%A0fic_29102004.jpg",
                            Name = "Beluga whale",
                            WikiLink = "https://en.wikipedia.org/wiki/Beluga_whale"
                        },
                        new
                        {
                            Id = 3,
                            ExampleImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1c/Anim1754_-_Flickr_-_NOAA_Photo_Library.jpg/2560px-Anim1754_-_Flickr_-_NOAA_Photo_Library.jpg",
                            Name = "Blue whale",
                            WikiLink = "https://en.wikipedia.org/wiki/Blue_whale"
                        },
                        new
                        {
                            Id = 4,
                            ExampleImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Bowhead_Whale_NOAA.jpg/2560px-Bowhead_Whale_NOAA.jpg",
                            Name = "Bowhead whale",
                            WikiLink = "https://en.wikipedia.org/wiki/Bowhead_whale"
                        },
                        new
                        {
                            Id = 5,
                            ExampleImageUrl = "https://upload.wikimedia.org/wikipedia/commons/3/35/Balaenoptera_brydei.jpg",
                            Name = "Bryde's whale",
                            WikiLink = "https://en.wikipedia.org/wiki/Bryde%27s_whale"
                        },
                        new
                        {
                            Id = 6,
                            ExampleImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/ce/Finhval_%281%29.jpg/2560px-Finhval_%281%29.jpg",
                            Name = "Fin whale",
                            WikiLink = "https://en.wikipedia.org/wiki/Fin_whale"
                        },
                        new
                        {
                            Id = 7,
                            ExampleImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/00/Ballena_gris_adulta_con_su_ballenato.jpg/2560px-Ballena_gris_adulta_con_su_ballenato.jpg",
                            Name = "Gray whale",
                            WikiLink = "https://en.wikipedia.org/wiki/Gray_whale"
                        },
                        new
                        {
                            Id = 8,
                            ExampleImageUrl = "https://upload.wikimedia.org/wikipedia/commons/6/61/Humpback_Whale_underwater_shot.jpg",
                            Name = "Humpback whale",
                            WikiLink = "https://en.wikipedia.org/wiki/Humpback_whale"
                        },
                        new
                        {
                            Id = 9,
                            ExampleImageUrl = "https://upload.wikimedia.org/wikipedia/commons/3/37/Killerwhales_jumping.jpg",
                            Name = "Killer whale",
                            WikiLink = "https://en.wikipedia.org/wiki/Orca"
                        },
                        new
                        {
                            Id = 10,
                            ExampleImageUrl = "https://upload.wikimedia.org/wikipedia/commons/d/d9/Minke_Whale_%28NOAA%29.jpg",
                            Name = "Minke whale",
                            WikiLink = "https://en.wikipedia.org/wiki/Minke_whale"
                        },
                        new
                        {
                            Id = 11,
                            ExampleImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/bc/%D0%9D%D0%B0%D1%80%D0%B2%D0%B0%D0%BB_%D0%B2_%D1%80%D0%BE%D1%81%D1%81%D0%B8%D0%B9%D1%81%D0%BA%D0%BE%D0%B9_%D0%90%D1%80%D0%BA%D1%82%D0%B8%D0%BA%D0%B5.jpg/1280px-%D0%9D%D0%B0%D1%80%D0%B2%D0%B0%D0%BB_%D0%B2_%D1%80%D0%BE%D1%81%D1%81%D0%B8%D0%B9%D1%81%D0%BA%D0%BE%D0%B9_%D0%90%D1%80%D0%BA%D1%82%D0%B8%D0%BA%D0%B5.jpg",
                            Name = "Narwhal",
                            WikiLink = "https://en.wikipedia.org/wiki/Narwhal"
                        },
                        new
                        {
                            Id = 12,
                            ExampleImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e2/Pilot_whale.jpg/1920px-Pilot_whale.jpg",
                            Name = "Pilot whale",
                            WikiLink = "https://en.wikipedia.org/wiki/Pilot_whale"
                        },
                        new
                        {
                            Id = 13,
                            ExampleImageUrl = "https://upload.wikimedia.org/wikipedia/commons/e/e2/Southern_right_whale.jpg",
                            Name = "Right whale",
                            WikiLink = "https://en.wikipedia.org/wiki/Right_whale"
                        },
                        new
                        {
                            Id = 14,
                            ExampleImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e3/Sei_whale_mother_and_calf_Christin_Khan_NOAA.jpg/1280px-Sei_whale_mother_and_calf_Christin_Khan_NOAA.jpg",
                            Name = "Sei whale",
                            WikiLink = "https://en.wikipedia.org/wiki/Sei_whale"
                        },
                        new
                        {
                            Id = 15,
                            ExampleImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b1/Mother_and_baby_sperm_whale.jpg/1920px-Mother_and_baby_sperm_whale.jpg",
                            Name = "Sperm whale",
                            WikiLink = "https://en.wikipedia.org/wiki/Sperm_whale"
                        },
                        new
                        {
                            Id = 16,
                            ExampleImageUrl = "https://cdn1.vectorstock.com/i/thumb-large/32/75/cartoon-curious-whale-and-speech-bubble-sticker-vector-26423275.jpg",
                            Name = "Other/unknown",
                            WikiLink = "https://en.wikipedia.org/wiki/Whale"
                        });
                });

            modelBuilder.Entity("WhaleSpotting.Models.Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<int>("Experience")
                        .HasColumnType("integer");

                    b.Property<decimal?>("FavoriteLocationLatitude")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("FavoriteLocationLongitude")
                        .HasColumnType("numeric");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("ProfileImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("WhaleSpotting.Models.Data.VerificationEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AdminId")
                        .HasColumnType("integer");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<int>("SightingId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.HasIndex("SightingId");

                    b.ToTable("VerificationEvents");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("WhaleSpotting.Models.Data.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("WhaleSpotting.Models.Data.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("WhaleSpotting.Models.Data.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("WhaleSpotting.Models.Data.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WhaleSpotting.Models.Data.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("WhaleSpotting.Models.Data.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WhaleSpotting.Models.Data.Achievement", b =>
                {
                    b.HasOne("WhaleSpotting.Models.Data.User", null)
                        .WithMany("Achievements")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("WhaleSpotting.Models.Data.Reaction", b =>
                {
                    b.HasOne("WhaleSpotting.Models.Data.Sighting", "Sighting")
                        .WithMany("Reactions")
                        .HasForeignKey("SightingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WhaleSpotting.Models.Data.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sighting");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WhaleSpotting.Models.Data.Sighting", b =>
                {
                    b.HasOne("WhaleSpotting.Models.Data.BodyOfWater", "BodyOfWater")
                        .WithMany()
                        .HasForeignKey("BodyOfWaterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WhaleSpotting.Models.Data.Species", "Species")
                        .WithMany()
                        .HasForeignKey("SpeciesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WhaleSpotting.Models.Data.User", "User")
                        .WithMany("Sightings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WhaleSpotting.Models.Data.VerificationEvent", "VerificationEvent")
                        .WithMany()
                        .HasForeignKey("VerificationEventId");

                    b.Navigation("BodyOfWater");

                    b.Navigation("Species");

                    b.Navigation("User");

                    b.Navigation("VerificationEvent");
                });

            modelBuilder.Entity("WhaleSpotting.Models.Data.VerificationEvent", b =>
                {
                    b.HasOne("WhaleSpotting.Models.Data.User", "Admin")
                        .WithMany()
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WhaleSpotting.Models.Data.Sighting", "Sighting")
                        .WithMany()
                        .HasForeignKey("SightingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admin");

                    b.Navigation("Sighting");
                });

            modelBuilder.Entity("WhaleSpotting.Models.Data.Sighting", b =>
                {
                    b.Navigation("Reactions");
                });

            modelBuilder.Entity("WhaleSpotting.Models.Data.User", b =>
                {
                    b.Navigation("Achievements");

                    b.Navigation("Sightings");
                });
#pragma warning restore 612, 618
        }
    }
}
