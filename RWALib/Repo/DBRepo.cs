﻿using Microsoft.ApplicationBlocks.Data;
using RWALib.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.IO;

namespace RWALib.Repo
{
    public class DBRepo : IRepo
    {
        private static string APARTMENTS_CS = ConfigurationManager.ConnectionStrings["apartments"].ConnectionString;

        public void AddUser(User u)
        {
            SqlHelper.ExecuteNonQuery(APARTMENTS_CS, nameof(AddUser), u.Email, u.UserName, Cryptography.HashPassword(u.PasswordHash), u.PhoneNumber, u.Address);
        }
        public IList<User> LoadUsers()
        {
            IList<User> users = new List<User>();

            var tblUsers = SqlHelper.ExecuteDataset(APARTMENTS_CS, nameof(LoadUsers)).Tables[0];
            foreach (DataRow row in tblUsers.Rows)
            {
                users.Add(
                    new User
                    {
                        Id = (int)row[nameof(User.Id)],
                        Email = row[nameof(User.Email)].ToString(),
                        UserName = row[nameof(User.UserName)].ToString(),
                        PasswordHash = row[nameof(User.PasswordHash)].ToString(),
                        CreatedAt =  row[nameof(User.CreatedAt)].ToString(),
                        DeletedAt = row[nameof(User.DeletedAt)].ToString(),
                        PhoneNumber = row[nameof(User.PhoneNumber)].ToString(),
                        Address = row[nameof(User.Address)].ToString()
                    }
                );
            }

            return users;
        }
        public User AuthUser(string email, string password)
        {
            var tblAuth = SqlHelper.ExecuteDataset(APARTMENTS_CS, nameof(AuthUser), email, Cryptography.HashPassword(password)).Tables[0];
            if (tblAuth.Rows.Count == 0) return null;

            DataRow row = tblAuth.Rows[0];
            return new User()
            {
                Id = (int)row[nameof(User.Id)],
                Email = row[nameof(User.Email)].ToString(),
                UserName = row[nameof(User.UserName)].ToString(),
                PasswordHash = row[nameof(User.PasswordHash)].ToString(),
                CreatedAt = row[nameof(User.CreatedAt)].ToString(),
                DeletedAt = row[nameof(User.DeletedAt)].ToString(),
                PhoneNumber = row[nameof(User.PhoneNumber)].ToString(),
                Address = row[nameof(User.Address)].ToString()
            };
        }
        public void DeleteUser(int id)
        {
            SqlHelper.ExecuteDataset(APARTMENTS_CS, nameof(DeleteUser), id);
        }
        public User AuthUserWithoutHash(string email, string password)
        {
            var tblAuth = SqlHelper.ExecuteDataset(APARTMENTS_CS, nameof(AuthUser), email, password).Tables[0];
            if (tblAuth.Rows.Count == 0) return null;

            DataRow row = tblAuth.Rows[0];
            return new User()
            {
                Id = (int)row[nameof(User.Id)],
                Email = row[nameof(User.Email)].ToString(),
                UserName = row[nameof(User.UserName)].ToString(),
                PasswordHash = row[nameof(User.PasswordHash)].ToString(),
                CreatedAt = row[nameof(User.CreatedAt)].ToString(),
                DeletedAt = row[nameof(User.DeletedAt)].ToString(),
                PhoneNumber = row[nameof(User.PhoneNumber)].ToString(),
                Address = row[nameof(User.Address)].ToString()
            };
        }

        public void SaveUser(User u)
        {
            SqlHelper.ExecuteNonQuery(APARTMENTS_CS, nameof(SaveUser), u.Id, u.Email, u.UserName, u.PasswordHash, u.PhoneNumber, u.Address);
        }

        public Apartment GetApartmentById(int selectedId)
        {
            var tblApartments = SqlHelper.ExecuteDataset(APARTMENTS_CS, nameof(GetApartmentById), selectedId).Tables[0];
            var row = tblApartments.Rows[0];
            if (tblApartments != null)
            {
                return new Apartment
                {
                    Id = (int)row[nameof(Apartment.Id)],
                    Address = row[nameof(Apartment.Address)].ToString(),
                    Name = row[nameof(Apartment.Name)].ToString(),
                    NameEng = row[nameof(Apartment.NameEng)].ToString(),
                    Price = (decimal)row[nameof(Apartment.Price)],
                    MaxAdults = (int)row[nameof(Apartment.MaxAdults)],
                    MaxChildren = (int)row[nameof(Apartment.MaxChildren)],
                    TotalRooms = (int)row[nameof(Apartment.TotalRooms)],
                    BeachDistance = (int)row[nameof(Apartment.BeachDistance)],
                    Owner = row["OwnerName"].ToString(),
                    Status = row["StatusName"].ToString(),
                    City = row["CityName"].ToString()
                };
            }
            else return null;
        }

        public IList<Apartment> LoadApartmentsByTagID(int id)
        {
            IList<Apartment> apartments = new List<Apartment>();

            var tblApartments = SqlHelper.ExecuteDataset(APARTMENTS_CS, nameof(LoadApartmentsByTagID), id).Tables[0];
            foreach (DataRow row in tblApartments.Rows)
            {
                apartments.Add(
                    new Apartment
                    {
                        Id = (int)row[nameof(Apartment.Id)],
                        Address = row[nameof(Apartment.Address)].ToString(),
                        Name = row[nameof(Apartment.Name)].ToString(),
                        NameEng = row[nameof(Apartment.NameEng)].ToString(),
                        Price = (decimal)row[nameof(Apartment.Price)],
                        MaxAdults = (int)row[nameof(Apartment.MaxAdults)],
                        MaxChildren = (int)row[nameof(Apartment.MaxChildren)],
                        TotalRooms = (int)row[nameof(Apartment.TotalRooms)],
                        BeachDistance = (int)row[nameof(Apartment.BeachDistance)],
                        Owner = row["OwnerName"].ToString(),
                        Status = row["StatusName"].ToString(),
                        City = row["CityName"].ToString()
                    }
                );
            }

            return apartments;
        }
        public IList<Apartment> LoadApartments()
        {
            IList<Apartment> apartments = new List<Apartment>();

            var tblApartments = SqlHelper.ExecuteDataset(APARTMENTS_CS, nameof(LoadApartments)).Tables[0];

            foreach (DataRow row in tblApartments.Rows)
            {
                apartments.Add(new Apartment
                {
                    Id = (int)row[nameof(Apartment.Id)],
                    Address = row[nameof(Apartment.Address)].ToString(),
                    Name = row[nameof(Apartment.Name)].ToString(),
                    NameEng = row[nameof(Apartment.NameEng)].ToString(),
                    CreatedAt = row[nameof(Apartment.CreatedAt)].ToString(),
                    DeletedAt = row[nameof(Apartment.DeletedAt)].ToString(),
                    Price = (decimal)row[nameof(Apartment.Price)],
                    MaxAdults = (int)row[nameof(Apartment.MaxAdults)],
                    MaxChildren = (int)row[nameof(Apartment.MaxChildren)],
                    TotalRooms = (int)row[nameof(Apartment.TotalRooms)],
                    BeachDistance = (int)row[nameof(Apartment.BeachDistance)],
                    Owner = row["OwnerName"].ToString(),
                    Status = row["StatusName"].ToString(),
                    City = row["CityName"].ToString()
                });
            }

            return apartments;
        }
        public void SaveApartment(Apartment a)
        {
            SqlHelper.ExecuteNonQuery(APARTMENTS_CS, nameof(SaveApartment), a.Id, a.Name, a.NameEng, a.City, a.Owner, a.Status, a.MaxAdults, a.MaxChildren, a.Price, a.BeachDistance, a.TotalRooms, a.Address);
        }
        public void DeleteApartment(int id)
        {
            SqlHelper.ExecuteNonQuery(APARTMENTS_CS, nameof(DeleteApartment), id);
        }
        public void AddApartment(Apartment a)
        {
            SqlHelper.ExecuteNonQuery(APARTMENTS_CS, nameof(AddApartment), a.Name, a.NameEng, a.City, a.Owner, a.Status, a.MaxAdults, a.MaxChildren, a.Price, a.BeachDistance, a.TotalRooms, a.Address);
        }

        public IList<City> GetCities()
        {
            IList<City> cities = new List<City>();
            var tblApartments = SqlHelper.ExecuteDataset(APARTMENTS_CS, nameof(GetCities)).Tables[0];

            foreach (DataRow row in tblApartments.Rows)
            {
                cities.Add(new City
                {
                    Id = (int)row[nameof(City.Id)],
                    Name = row[nameof(City.Name)].ToString()
                });
            }

            return cities;
        }

        public IList<Owner> GetOwners()
        {
            IList<Owner> owners = new List<Owner>();
            var tblApartments = SqlHelper.ExecuteDataset(APARTMENTS_CS, nameof(GetOwners)).Tables[0];

            foreach (DataRow row in tblApartments.Rows)
            {
                owners.Add(new Owner
                {
                    Id = (int)row[nameof(Owner.Id)],
                    Name = row[nameof(Owner.Name)].ToString()
                });
            }

            return owners;
        }

        public IList<Status> GetStatus()
        {
            IList<Status> statuses = new List<Status>();
            var tblApartments = SqlHelper.ExecuteDataset(APARTMENTS_CS, nameof(GetStatus)).Tables[0];

            foreach (DataRow row in tblApartments.Rows)
            {
                statuses.Add(new Status
                {
                    Id = (int)row[nameof(Status.Id)],
                    Name = row[nameof(Status.Name)].ToString(),
                    NameEng = row[nameof(Status.NameEng)].ToString()
                });
            }

            return statuses;
        }

        public IList<Tag> LoadTags()
        {
            IList<Tag> tags = new List<Tag>();

            var tblTags = SqlHelper.ExecuteDataset(APARTMENTS_CS, nameof(LoadTags)).Tables[0];
            foreach (DataRow row in tblTags.Rows)
            {
                Tag tag = new Tag
                {
                    Id = (int)row[nameof(Tag.Id)],
                    Name = row[nameof(Tag.Name)].ToString(),
                    NameEng = row[nameof(Tag.NameEng)].ToString(),
                    CreatedAt = row[nameof(Tag.CreatedAt)].ToString(),
                    Type = row["Type"].ToString()
                };
                tag.NumberOfApartments = LoadApartmentsByTagID(tag.Id).Count;
                tags.Add(tag);
            }

            return tags;
        }

        public IList<Tag> LoadTagsForApartment(int id)
        {
            IList<Tag> tags = new List<Tag>();

            var tblTags = SqlHelper.ExecuteDataset(APARTMENTS_CS, nameof(LoadTagsForApartment), id).Tables[0];
            foreach (DataRow row in tblTags.Rows)
            {
                Tag tag = new Tag
                {
                    Id = (int)row[nameof(Tag.Id)],
                    Name = row[nameof(Tag.Name)].ToString(),
                    NameEng = row[nameof(Tag.NameEng)].ToString(),
                    CreatedAt = row[nameof(Tag.CreatedAt)].ToString(),
                    Type = row["Type"].ToString()
                };
                tag.NumberOfApartments = LoadApartmentsByTagID(tag.Id).Count;
                tags.Add(tag);
            }

            return tags;
        }

        public void DeleteTag(int tagID)
        {
            SqlHelper.ExecuteNonQuery(APARTMENTS_CS, nameof(DeleteTag), tagID);
        }

        public void AddTag(Tag t)
        {
            SqlHelper.ExecuteNonQuery(APARTMENTS_CS, nameof(AddTag), t.Name, t.NameEng, t.Type);
        }

        public IList<TagType> LoadTagTypes()
        {
            IList<TagType> tt = new List<TagType>();

            var tblTagTypes = SqlHelper.ExecuteDataset(APARTMENTS_CS, nameof(LoadTagTypes)).Tables[0];
            foreach (DataRow row in tblTagTypes.Rows)
            {
                TagType type = new TagType
                {
                    Id = (int)row[nameof(Tag.Id)],
                    Name = row[nameof(Tag.Name)].ToString(),
                    NameEng = row[nameof(Tag.NameEng)].ToString()
                };
                tt.Add(type);
            }

            return tt;
        }

        public void AddTaggedApartment(string aName, string tName)
        {
            SqlHelper.ExecuteNonQuery(APARTMENTS_CS, nameof(AddTaggedApartment), aName, tName);
        }

        public void DeleteTaggedApartment(string aName, string tName)
        {
            SqlHelper.ExecuteNonQuery(APARTMENTS_CS, nameof(DeleteTaggedApartment), aName, tName);
        }

        public IList<Apartment> LoadApartmentsByCityAndStatus(string status, string city)
        {
            IList<Apartment> apartments = new List<Apartment>();

            var tblApartments = SqlHelper.ExecuteDataset(APARTMENTS_CS, nameof(LoadApartmentsByCityAndStatus), status, city).Tables[0];
            foreach (DataRow row in tblApartments.Rows)
            {
                apartments.Add(
                    new Apartment
                    {
                        Id = (int)row[nameof(Apartment.Id)],
                        Address = row[nameof(Apartment.Address)].ToString(),
                        Name = row[nameof(Apartment.Name)].ToString(),
                        NameEng = row[nameof(Apartment.NameEng)].ToString(),
                        Price = (decimal)row[nameof(Apartment.Price)],
                        MaxAdults = (int)row[nameof(Apartment.MaxAdults)],
                        MaxChildren = (int)row[nameof(Apartment.MaxChildren)],
                        TotalRooms = (int)row[nameof(Apartment.TotalRooms)],
                        BeachDistance = (int)row[nameof(Apartment.BeachDistance)],
                        Owner = row["OwnerName"].ToString(),
                        Status = row["StatusName"].ToString(),
                        City = row["CityName"].ToString()
                    }
                );
            }

            return apartments;
        }

        public void AddReservationForNonExistingUser(int apartmentId, string details, string username, string email, string phone)
        {
            SqlHelper.ExecuteNonQuery(APARTMENTS_CS, nameof(AddReservationForNonExistingUser), apartmentId, details, username, email, phone);
        }

        public void AddReservationForExistingUser(int userId, int apartmentId, string details)
        {
            SqlHelper.ExecuteNonQuery(APARTMENTS_CS, nameof(AddReservationForExistingUser), userId, apartmentId, details);
        }
    }
}
