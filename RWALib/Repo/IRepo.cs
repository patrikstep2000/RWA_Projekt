using RWALib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RWALib.Repo
{
    public interface IRepo
    {
        IList<Apartment> LoadApartments();
        Apartment GetApartmentById(int selectedId);
        IList<Apartment> LoadApartmentsByTagID(int id);
        void SaveApartment(Apartment a);
        void AddApartment(Apartment a);
        User AuthUser(string email, string password);
        User AuthUserWithoutHash(string email, string password);
        IList<User> LoadUsers();
        void AddUser(User u);
        void SaveUser(User u);
        void DeleteUser(int id);

        IList<Owner> GetOwners();
        IList<Status> GetStatus();
        IList<City> GetCities();
        IList<Tag> LoadTags();
        IList<Tag> LoadTagsForApartment(int id);
        IList<TagType> LoadTagTypes();
        void DeleteApartment(int id);
        void DeleteTag(int tagID);
        void AddTag(Tag t);
        void AddTaggedApartment(string aName, string tName);
        void DeleteTaggedApartment(string text, string name);
        IList<Apartment> LoadApartmentsByCityAndStatus(string status, string city);
        void AddReservationForExistingUser(int userId, int apartmentId, string details);
        void AddReservationForNonExistingUser(int apartmentId, string details, string username, string email, string phone);
    }
}
