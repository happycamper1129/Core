﻿using System.Security.Claims;
using AspNetCoreSpa.Application.Abstractions;
using Microsoft.AspNetCore.Http;

namespace AspNetCoreSpa.Web.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            IsAuthenticated = UserId != null;
        }

        public string UserId { get; }

        public bool IsAuthenticated { get; }
    }

}
