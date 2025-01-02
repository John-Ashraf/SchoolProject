﻿using AutoMapper;

namespace School.Core.Mapping.User
{
    public partial class UserProfile : Profile
    {
        public UserProfile()
        {
            AddUserMapping();
            GetUserPaginationMapping();
            GetUserByIdMapping();
            UpdateUserMapping();
        }
    }
}
