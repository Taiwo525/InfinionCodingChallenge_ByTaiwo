using InfinionProduct_Application.Interfaces;
using InfinionProduct_Application.Services;
using InfinionProduct_Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfinionProduct_Application.Extensions
{
    public static class CollectionExtention
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        }
    }
}
