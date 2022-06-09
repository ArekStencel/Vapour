using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vapour.Model.Dto
{
    public class GameCommentDto
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string IsFollowing { get; set; }
        public string Text { get; set; }
        public string Date { get; set; }
    }
    public class GameDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string ReleaseDate { get; set; }
    }
}
