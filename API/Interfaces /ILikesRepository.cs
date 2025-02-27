using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API;

public interface ILikesRepository
{
    Task<UserLike?> GetUserLike(int sourceUserId, int targetUserID);
    Task<PagedList<MemberDto>> GetUserLikes(LikeParams likeParams);
    Task<IEnumerable<int>> GetCurrentUserLikeIds(int currentUserId);
    void DeleteLike(UserLike like);
    void AddLike(UserLike like);
    //Removed afer added Unit of work
    //Task<bool> SaveChanges();

}