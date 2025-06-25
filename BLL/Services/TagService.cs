// TagService.cs
using AutoMapper;
using BLL.DTOs;
using DAL;
using DAL.EF;
using System;
using System.Collections.Generic;

namespace BLL.Services
{
    public class TagService
    {
        private static readonly IMapper mapper;

        static TagService()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Tag, TagDTO>().ReverseMap();
            });
            mapper = config.CreateMapper();
        }

        public static TagDTO Create(TagDTO tagDto)
        {
            var tag = mapper.Map<Tag>(tagDto);
            var createdTag = DataAccess.TagData().Create(tag);
            return mapper.Map<TagDTO>(createdTag);
        }

        public static List<TagDTO> Get()
        {
            var tags = DataAccess.TagData().Get();
            return mapper.Map<List<TagDTO>>(tags);
        }

        public static TagDTO Get(int id)
        {
            var tag = DataAccess.TagData().Get(id);
            return tag != null ? mapper.Map<TagDTO>(tag) : null;
        }

        public static void Delete(int id)
        {
             DataAccess.TagData().Delete(id);
        }
    }
}