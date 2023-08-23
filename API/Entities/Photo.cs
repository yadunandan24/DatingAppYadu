using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Photos")] //to create a table named photos based on this class, also dont need to create a DbSet as this attribute will create table auto
                      //and also make relation between this table and User table
    public class Photo
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public bool IsMain { get; set; }

        public string PublicId { get; set; }

        public int AppUserId { get; set; }  //added these two prop to establish rel between users and photos with non nullable foreign key in photos table
                                            //meaning photos have to be of some users, no photos can be added if there are no users
                                            //also cascading delete is now on means if a user is deleted all photos are also deleted
        public AppUser AppUser { get; set; }
    }
}