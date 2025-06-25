using AutoMapper;
using BLL.DTOs;
using DAL;
using DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    public class FileService
    {
        private static readonly IMapper mapper;

        static FileService()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<File_Info, FileDTO>()
                   .ForMember(dest => dest.Tag, opt => opt.MapFrom(src => src.Tag))
                   .ForMember(dest => dest.User, opt => opt.Ignore()) 
                   .ReverseMap();

                cfg.CreateMap<Tag, TagDTO>()
                   .ForMember(dest => dest.Files, opt => opt.Ignore())
                   .ReverseMap();
            });
            mapper = config.CreateMapper();
        }

        public static FileDTO Create(FileDTO fileDto)
        {
            var file = mapper.Map<File_Info>(fileDto);

            // Clear navigation to avoid EF error
            file.Tag = null;
            file.User = null;
            file.UploadedAt = DateTime.Now;

            var createdFile = DataAccess.FileManage().Create(file);

            // Re-fetch with Tag & User loaded
            var fullFile = DataAccess.FileManage().Get(createdFile.Id);
            return mapper.Map<FileDTO>(fullFile);
        }

        public static List<FileDTO> Get()
        {
            var files = DataAccess.FileManage().Get();
            return mapper.Map<List<FileDTO>>(files);
        }

        public static FileDTO Get(int id)
        {
            var file = DataAccess.FileManage().Get(id);
            return file != null ? mapper.Map<FileDTO>(file) : null;
        }

        public static List<FileDTO> GetByUser(int userId)
        {
            var files = DataAccess.FileManage().Get().Where(f => f.User_Id == userId).ToList();
            return mapper.Map<List<FileDTO>>(files);
        }

        public static List<FileDTO> GetByTag(int tagId)
        {
            var files = DataAccess.FileManage().Get().Where(f => f.Tag_Id == tagId).ToList();
            return mapper.Map<List<FileDTO>>(files);
        }

        public static FileDTO Update(FileDTO fileDto)
        {
            var file = mapper.Map<File_Info>(fileDto);
            var updated = DataAccess.FileManage().Update(file);
            return mapper.Map<FileDTO>(updated);
        }

        public static bool Delete(int id)
        {
            var existing = DataAccess.FileManage().Get(id);
            if (existing != null)
            {
                DataAccess.FileManage().Delete(id);
                return true;
            }
            return false;
        }
    }
}
