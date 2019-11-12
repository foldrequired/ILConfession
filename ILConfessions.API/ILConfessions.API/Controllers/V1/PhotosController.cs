using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ILConfessions.API.Contracts.V1.Requests;
using ILConfessions.API.Helpers;
using ILConfessions.API.MagicStringHandlers.V1;
using ILConfessions.API.Models;
using ILConfessions.API.Repositories.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ILConfessions.API.Controllers.V1
{
    [Authorize]
    [Produces("application/json")]
    public class PhotosController : Controller
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;
        private readonly IUriRepository _uriRepository;

        public PhotosController(IUserRepository repo, IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig, IUriRepository uriRepository)
        {
            _repo = repo;
            _mapper = mapper;
            _cloudinaryConfig = cloudinaryConfig;
            _uriRepository =  uriRepository;

            //Cloudinary
            Account acc = new Account
            (
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        [HttpGet(ApiRoutes.Photos.Get)]
        public async Task<IActionResult> GetPhoto([FromRoute]int photoId)
        {
            var photoFromRepository = await _repo.GetPhoto(photoId);

            var photo = _mapper.Map<GetPhotoRequest>(photoFromRepository);
            
            return Ok(photo);
        }

        [HttpPost(ApiRoutes.Photos.Create)]
        public async Task<IActionResult> AddPhoto([FromRoute]string userId, [FromForm]CreatePhotoRequest photoDto)
        {
            if (userId != User.FindFirst("Id").Value)
            {
                return Unauthorized();
            }

            var userFromRepo = await _repo.GetUser(userId);

            var file = photoDto.File;

            var upload = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation()
                            .Width(500).Height(500).Crop("fill")
                            .Gravity("face")
                    };

                    upload = _cloudinary.Upload(uploadParams);
                }
            }

            photoDto.Url = upload.Uri.ToString();
            photoDto.PublicId = upload.PublicId;

            var photo = _mapper.Map<Photo>(photoDto);

            if (!userFromRepo.Photos.Any(p => p.IsMain))
            {
                photo.IsMain = true;
            }

            userFromRepo.Photos.Add(photo);

            if (await _repo.SaveAll())
            {
                var photoResponse = _mapper.Map<GetPhotoRequest>(photo);

                var locationUri = _uriRepository.GetPhotoUri(userFromRepo.Id, photo.Id);

                return Created(locationUri, photoResponse);
            }

            return BadRequest("There was an error uploading a new photo, please try again");
        } 


    }
}