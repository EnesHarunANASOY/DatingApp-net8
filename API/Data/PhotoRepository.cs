
using API.DTOs;
using API.Entities;
using API.Interfaces_;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class PhotoRepository(DataContext context) : IPhotoRepository
{
    public async Task<Photo?> GetPhotoById(int photoId)
    {
        return await context.Photos.IgnoreQueryFilters().SingleOrDefaultAsync(x=>x.Id==photoId);
    }

    public async Task<IEnumerable<PhotoForApprovalDto>> GetUnapprovedPhotos()
    {
        return await context.Photos.IgnoreQueryFilters()
        .Where(x=>x.IsApproved==false)
        .Select(u=> new PhotoForApprovalDto{
             Id=u.Id,
              Url =u.Url,
             Username= u.AppUser.UserName,
             IsApproved=u.IsApproved     
        }).ToListAsync();
    }

    public void RemovePhoto(Photo photo)
    {
        context.Photos.Remove(photo);
    }
}
