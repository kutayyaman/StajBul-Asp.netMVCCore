using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StajBul.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StajBul.Data.Concrete.EfCore
{
    public static class SeedData
    {
        public static void Seed(StajBulContext context)
        {
            
            //context.Database.Migrate();

            if (!context.Users.Any())
            {
                context.Users.AddRange(
                        new User() { Age = 22, Mail = "yamankutay1@gmail.com", RegistrationDate = new DateTime(), UserName = "kutayyaman", UserSurname = "yaman", UserType = UserType.USER },
                        new User() { Age = 23, Mail = "badoonline@gmail.com", RegistrationDate = new DateTime(), UserName = "badoonline", UserSurname = "gungor", UserType = UserType.USER },
                        new User() { Age = 22, Mail = "siresat@gmail.com", RegistrationDate = new DateTime(), UserName = "siresat", UserSurname = "genc", UserType = UserType.USER },
                        new User() { Age = 22, Mail = "batugokalp@gmail.com", RegistrationDate = new DateTime(), UserName = "batugokalp", UserSurname = "gokalp", UserType = UserType.USER }
                    );
                context.SaveChanges();
            }

            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                        new Category() { CategoryName = "Yazilim"},
                        new Category() { CategoryName = "Saglik" },
                        new Category() { CategoryName = "Insaat" },
                        new Category() { CategoryName = "Ekonomi" }
                    );
                context.SaveChanges();
            }

            if (!context.Cities.Any())
            {
                context.Cities.AddRange(
                        new City() { CityName = "Istanbul"},
                        new City() { CityName = "Ankara" },
                        new City() { CityName = "Bursa" },
                        new City() { CityName = "Izmir" }
                    );
                context.SaveChanges();
            }

            if (!context.Addresses.Any())
            {
                context.Addresses.AddRange(
                        new Address() { AddressLine1 = "bla bla cad. bla bla sok.", AddressLine2 = "adresline 2 bos birakilabilsin bu ilerde", District = "ilce bilgisi buraya", PostalCode = "34044", Phone = "5380152255", AddressName = "Evim", CityId = 1, UserId = 1 },
                        new Address() { AddressLine1 = "gop meydan bla blaa", AddressLine2 = "adresline 2 bos birakilabilsin bu ilerde", District = "ilce bilgisi buraya", PostalCode = "34044", Phone = "5380152255", AddressName = "Evim", CityId = 2, UserId = 2 },
                        new Address() { AddressLine1 = "karagumruk fatih caddesi falan filan", AddressLine2 = "adresline 2 bos birakilabilsin bu ilerde", District = "ilce bilgisi buraya", PostalCode = "34044", Phone = "5380152255", AddressName = "Evim", CityId = 3, UserId = 3 },
                        new Address() { AddressLine1 = "bayrampasa forum istanbul yani ", AddressLine2 = "adresline 2 bos birakilabilsin bu ilerde", District = "ilce bilgisi buraya", PostalCode = "34044", Phone = "5380152255", AddressName = "Evim", CityId = 3, UserId = 4 }
                    );
                context.SaveChanges();
            }

            if (!context.Announcements.Any())
            {
                context.Announcements.AddRange(
                        new InternshipAnnouncement() { AddressId = 1, CategoryId = 1, UserId = 1, AnnouncementStatus = AnnouncementStatus.ACTIVE, AnnouncementType = AnnouncementType.COMPANY, CreationDate = new DateTime(), Explanation = "Ofisimizde full time calisacak java developer stajyrine ihtiyacimiz var cvnizi yamankutay1@gmail.com mailine gonderebilirsiniz.", Mail = "yamankutay1@gmail.com", Name = "Java Developer Stajyeri", Title = "Yazilim Stajyeri BASLIGI" },
                        new InternshipAnnouncement() { AddressId = 2, CategoryId = 2, UserId = 2, AnnouncementStatus = AnnouncementStatus.ACTIVE, AnnouncementType = AnnouncementType.COMPANY, CreationDate = new DateTime(), Explanation = "Bilgsayar muhendisligi donanim stajyeri ariyoruz cvnizi ..... 'ya gonderebilirsiniz", Title = "Donanim Stajyeri BASLIGI" },
                        new InternshipAnnouncement() { AddressId = 3, CategoryId = 3, UserId = 3, AnnouncementStatus = AnnouncementStatus.ACTIVE, AnnouncementType = AnnouncementType.STAJYER, CreationDate = new DateTime(), Explanation = "Staj Yapmak Icin Bir Yazilim Ofisi Ariyorum.", Mail = "yamankutay1@gmail.com", Name = "Staj Yeri Ariyorum", Title = "Yer Ariyorum Basligi" }

                    );
                context.SaveChanges();
            }
        }
    }
}
