using System.Linq;

namespace HG100.Models
{
    public partial class Plant
    {
        public bool isFavorite(string userId)
        {
            return UserFavorited.Any(u => u.Id == userId);
        }
        public bool AddedtoGarden(string userId)
        {
            return UserSelected.Any(u => u.Id == userId);
        }
    }
}