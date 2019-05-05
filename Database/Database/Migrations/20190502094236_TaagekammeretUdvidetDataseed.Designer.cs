﻿// <auto-generated />
using System;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Database.Migrations
{
    [DbContext(typeof(BarOMeterContext))]
    [Migration("20190502094236_TaagekammeretUdvidetDataseed")]
    partial class TaagekammeretUdvidetDataseed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Database.Bar", b =>
                {
                    b.Property<string>("BarName")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(150);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int>("AgeLimit");

                    b.Property<double>("AvgRating");

                    b.Property<int>("CVR");

                    b.Property<string>("Educations")
                        .HasMaxLength(255);

                    b.Property<string>("Email")
                        .HasMaxLength(150);

                    b.Property<string>("Image");

                    b.Property<string>("LongDescription")
                        .HasMaxLength(2500);

                    b.Property<int>("PhoneNumber");

                    b.Property<string>("ShortDescription")
                        .HasMaxLength(500);

                    b.HasKey("BarName");

                    b.HasIndex("CVR")
                        .IsUnique();

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("Bar");

                    b.HasData(
                        new
                        {
                            BarName = "Katrines Kælder",
                            Address = "5125 Edison, Finlandsgade 22, 8200 Aarhus",
                            AgeLimit = 18,
                            AvgRating = 5.0,
                            CVR = 33985703,
                            Educations = "IKT,EE,E,ST",
                            Email = "katrineskaelder@outlook.dk",
                            Image = "https://scontent-dus1-1.xx.fbcdn.net/v/t1.0-9/13166_441233562611984_1450333570_n.png?_nc_cat=105&_nc_ht=scontent-dus1-1.xx&oh=3a0e9139a633dd8d9131afd229eab1da&oe=5D2B6EDD",
                            LongDescription = "Der er mange øl",
                            PhoneNumber = 12345678,
                            ShortDescription = "Der er øl"
                        },
                        new
                        {
                            BarName = "Medicinsk Fredagsbar - Umbilicus",
                            Address = "Medicinerhuset, Bygning 1161, Ole Worms Allé 4, 8000 Aarhus",
                            AgeLimit = 18,
                            AvgRating = 3.0,
                            CVR = 29129932,
                            Educations = "Medicin",
                            Email = "bestyrelsen@umbi.dk",
                            Image = "https://scontent-dus1-1.xx.fbcdn.net/v/t1.0-9/43698279_2427965823910886_4605085834809442304_n.png?_nc_cat=111&_nc_ht=scontent-dus1-1.xx&oh=e3bc006d52005d545011cc52b1f8a7d8&oe=5D76ACBE",
                            LongDescription = "Der er alt for mange mennesker og alt for få øl",
                            PhoneNumber = 51927090,
                            ShortDescription = "Der er varme øl"
                        },
                        new
                        {
                            BarName = "Tågekammeret",
                            Address = "Ny Munkegade 118, 8000 Aarhus",
                            AgeLimit = 18,
                            AvgRating = 4.0,
                            CVR = 34126399,
                            Educations = "Fysik, Datalogi, IT Bachelor, Matematik-Økonomi, Nanoteknologi",
                            Email = "BEST@TAAGEKAMMERET.dk",
                            Image = "https://taagekammeret.dk/static/TKlogo.jpg",
                            LongDescription = "Du ender i et sort hul, hvis du bliver ved med at drikke her",
                            PhoneNumber = 87154052,
                            ShortDescription = "Husk Tågelygter!"
                        });
                });

            modelBuilder.Entity("Database.BarEvent", b =>
                {
                    b.Property<string>("BarName")
                        .HasMaxLength(150);

                    b.Property<string>("EventName")
                        .HasMaxLength(75);

                    b.Property<DateTime>("Date");

                    b.Property<string>("Image");

                    b.HasKey("BarName", "EventName");

                    b.ToTable("BarEvents");

                    b.HasData(
                        new
                        {
                            BarName = "Katrines Kælder",
                            EventName = "Tobias tager Level Up",
                            Date = new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Image = "https://vignette.wikia.nocookie.net/my-hero-academia-fanon/images/0/0a/Level_Up.png/revision/latest?cb=20180722000746"
                        },
                        new
                        {
                            BarName = "Katrines Kælder",
                            EventName = "Bingo Bar",
                            Date = new DateTime(2019, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Image = "https://letsbingo.dk/wp-content/uploads/2016/01/Bingo-graphic21.jpg"
                        },
                        new
                        {
                            BarName = "Medicinsk Fredagsbar - Umbilicus",
                            EventName = "Andreas på tur!",
                            Date = new DateTime(2019, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Image = "https://media1.s-nbcnews.com/i/newscms/2016_48/1811466/161128-drinking-alcohol-jpo-108p_52ad934c90bc61c93c2242c4349f5e55.jpg"
                        },
                        new
                        {
                            BarName = "Medicinsk Fredagsbar - Umbilicus",
                            EventName = "Ipod Battle Bar",
                            Date = new DateTime(2019, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Image = "https://ecdn.evensi.com/e301908712?cph=gAAAAABcvXMS1wYy7ZyTjIpjScMOwT9aERAm7bQrt8iS-5I7umkCbr2B_3zz_OTxoGxamKgI6Su1nCvudV9KA74kF3CljhF-AJEPUhQt7K-NfWz1IbgakBxjcqUGS98Pw-gQ8RLV39CMKlB13oChr1i6Ai8xjvDMOfQ-aJr-3abxG37hRxzubXfSky6WWGnYlQDbioz7SjJXMWE3eT1mPcBhMZox_B0gQLJw7cR802diHabhG3iV17hgvr0-zOUE-DlWTjFEuK_TuZ1yFd6kuC3BWwBswtOM58r_prd6HgX2ETPXJ9iFge65DKMKUsXhVCrwB5GjRBXU0NXP9MccSFwpN2rgrXAXykspY9N_FFUMbaJOZGlhllsH89N4QKdv4oba32VDCPa9"
                        },
                        new
                        {
                            BarName = "Tågekammeret",
                            EventName = "Bodycrashing",
                            Date = new DateTime(2019, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Image = "https://taagekammeret.dk/media/__sized__/2016/bodycrashing/15271383_10210607041708452_1374044913_o-crop-c0-5__0-5-253x253-70.jpg"
                        });
                });

            modelBuilder.Entity("Database.BarRepresentative", b =>
                {
                    b.Property<string>("Username")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<string>("BarName")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.HasKey("Username");

                    b.HasIndex("BarName");

                    b.ToTable("BarRepresentatives");

                    b.HasData(
                        new
                        {
                            Username = "Legend27",
                            BarName = "Katrines Kælder",
                            Name = "Ole Ølmave"
                        },
                        new
                        {
                            Username = "Kratluskeren",
                            BarName = "Medicinsk Fredagsbar - Umbilicus",
                            Name = "Tørstige Torsten"
                        },
                        new
                        {
                            Username = "Humleridderen",
                            BarName = "Tågekammeret",
                            Name = "Kenny Kernel Space"
                        });
                });

            modelBuilder.Entity("Database.Coupon", b =>
                {
                    b.Property<string>("CouponID")
                        .HasMaxLength(50);

                    b.Property<string>("BarName")
                        .HasMaxLength(150);

                    b.Property<DateTime>("ExpirationDate");

                    b.HasKey("CouponID", "BarName");

                    b.HasIndex("BarName");

                    b.ToTable("Coupons");

                    b.HasData(
                        new
                        {
                            CouponID = "123ØL",
                            BarName = "Katrines Kælder",
                            ExpirationDate = new DateTime(2019, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            CouponID = "VarmØlNuTak",
                            BarName = "Medicinsk Fredagsbar - Umbilicus",
                            ExpirationDate = new DateTime(2019, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            CouponID = "20MemLeak",
                            BarName = "Tågekammeret",
                            ExpirationDate = new DateTime(2019, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Database.Customer", b =>
                {
                    b.Property<string>("Username")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<string>("FavoriteBar")
                        .HasMaxLength(150);

                    b.Property<string>("FavoriteDrink")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.HasKey("Username");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Username = "Bodega Bent",
                            DateOfBirth = new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "JegElskerØl@Yahoo.com",
                            FavoriteBar = "Katrines Kælder",
                            FavoriteDrink = "Fadøl",
                            Name = "Bent"
                        },
                        new
                        {
                            Username = "Dehydrerede Dennis",
                            DateOfBirth = new DateTime(1990, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "JegErTørstig@gmail.com",
                            FavoriteBar = "Medicinsk Fredagsbar - Umbilicus",
                            FavoriteDrink = "Vodka Redbull",
                            Name = "Dennis"
                        },
                        new
                        {
                            Username = "Koffein Karsten",
                            DateOfBirth = new DateTime(1990, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "KaffeTrolden@gmail.com",
                            FavoriteBar = "Tågekammeret",
                            FavoriteDrink = "Kaffe",
                            Name = "Karsten"
                        });
                });

            modelBuilder.Entity("Database.Drink", b =>
                {
                    b.Property<string>("BarName")
                        .HasMaxLength(150);

                    b.Property<string>("DrinksName")
                        .HasMaxLength(50);

                    b.Property<string>("Image");

                    b.Property<double>("Price");

                    b.HasKey("BarName", "DrinksName");

                    b.ToTable("Drinks");

                    b.HasData(
                        new
                        {
                            BarName = "Katrines Kælder",
                            DrinksName = "Spejlæg",
                            Price = 50.0
                        },
                        new
                        {
                            BarName = "Katrines Kælder",
                            DrinksName = "Flaskeøl",
                            Image = "https://www.calle.dk/SL/PI/705/128/8c021bde-d649-4515-8c92-0effa962bafe.jpg",
                            Price = 10.0
                        },
                        new
                        {
                            BarName = "Katrines Kælder",
                            DrinksName = "Fadøl",
                            Image = "https://r2brewery.dk/wp-content/uploads/2017/11/pilsner.png",
                            Price = 20.0
                        },
                        new
                        {
                            BarName = "Medicinsk Fredagsbar - Umbilicus",
                            DrinksName = "Ceres Top",
                            Image = "https://www.calle.dk/SL/PI/705/128/8c021bde-d649-4515-8c92-0effa962bafe.jpg",
                            Price = 10.0
                        },
                        new
                        {
                            BarName = "Medicinsk Fredagsbar - Umbilicus",
                            DrinksName = "Vodka Redbull",
                            Image = "https://www.drinkdelivery.it/wp-content/uploads/2015/05/vodka-absolute-redbull.jpg",
                            Price = 20.0
                        },
                        new
                        {
                            BarName = "Medicinsk Fredagsbar - Umbilicus",
                            DrinksName = "Hospitalssprit",
                            Image = "https://www.fotoagent.dk/single_picture/11981/138/mega/201096_1.jpg",
                            Price = 10.0
                        },
                        new
                        {
                            BarName = "Tågekammeret",
                            DrinksName = "Radioaktivt Affald",
                            Image = "https://videnskab.dk/sites/default/files/styles/columns_12_12_desktop/public/article_media/atomaffald.jpg?itok=LXcUsHe-&timestamp=1464219173",
                            Price = 20.0
                        },
                        new
                        {
                            BarName = "Tågekammeret",
                            DrinksName = "Fadøl",
                            Image = "https://r2brewery.dk/wp-content/uploads/2017/11/pilsner.png",
                            Price = 10.0
                        },
                        new
                        {
                            BarName = "Tågekammeret",
                            DrinksName = "Stroh Rom",
                            Image = "https://www.fotoagent.dk/single_picture/10620/138/mega/stroh_80(2).jpg",
                            Price = 30.0
                        });
                });

            modelBuilder.Entity("Database.Review", b =>
                {
                    b.Property<string>("BarName")
                        .HasMaxLength(150);

                    b.Property<string>("Username")
                        .HasMaxLength(50);

                    b.Property<int>("BarPressure");

                    b.HasKey("BarName", "Username");

                    b.HasIndex("Username");

                    b.ToTable("Reviews");

                    b.HasData(
                        new
                        {
                            BarName = "Katrines Kælder",
                            Username = "Bodega Bent",
                            BarPressure = 5
                        },
                        new
                        {
                            BarName = "Medicinsk Fredagsbar - Umbilicus",
                            Username = "Bodega Bent",
                            BarPressure = 3
                        },
                        new
                        {
                            BarName = "Katrines Kælder",
                            Username = "Dehydrerede Dennis",
                            BarPressure = 5
                        },
                        new
                        {
                            BarName = "Medicinsk Fredagsbar - Umbilicus",
                            Username = "Dehydrerede Dennis",
                            BarPressure = 3
                        });
                });

            modelBuilder.Entity("Database.BarEvent", b =>
                {
                    b.HasOne("Database.Bar", "Bar")
                        .WithMany("BarEvents")
                        .HasForeignKey("BarName")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Database.BarRepresentative", b =>
                {
                    b.HasOne("Database.Bar", "Bar")
                        .WithMany("BarRepresentatives")
                        .HasForeignKey("BarName")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Database.Coupon", b =>
                {
                    b.HasOne("Database.Bar", "Bar")
                        .WithMany("Coupons")
                        .HasForeignKey("BarName")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Database.Drink", b =>
                {
                    b.HasOne("Database.Bar", "Bar")
                        .WithMany("Drinks")
                        .HasForeignKey("BarName")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Database.Review", b =>
                {
                    b.HasOne("Database.Bar", "Bar")
                        .WithMany("Reviews")
                        .HasForeignKey("BarName")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Database.Customer", "Customer")
                        .WithMany("Reviews")
                        .HasForeignKey("Username")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}