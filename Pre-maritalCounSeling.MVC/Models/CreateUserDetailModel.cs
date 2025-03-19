using Pre_maritalCounSeling.DAL.Entities;

namespace Pre_maritalCounSeling.MVC.Models
{
    public class CreateUserDetailModel
    {
        public long Id { get; set; }

        public string Summary { get; set; }

        public string Name { get; set; }

        public string? ReferedLink { get; set; }

        public IFormFile AttachedDocument { get; set; }

        public string? Image { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public long CategoryId { get; set; }

        public long UserId { get; set; }

    }
}
