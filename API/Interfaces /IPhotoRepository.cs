
using API.DTOs;
using API.Entities;

namespace API.Interfaces_;

public interface IPhotoRepository
{

    Task<IEnumerable<PhotoForApprovalDto>> GetUnapprovedPhotos();
    Task<Photo?> GetPhotoById(int photoId);
    void RemovePhoto(Photo photo);
}   
