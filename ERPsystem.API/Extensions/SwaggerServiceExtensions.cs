//using Microsoft.OpenApi;

//namespace ERPSystem.API.Extensions
//{
//    public static class SwaggerServiceExtensions
//    {
//        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
//        {
//            // إضافة خدمات Swagger
//            services.AddEndpointsApiExplorer();
//            services.AddSwaggerGen(c =>
//            {
//                c.SwaggerDoc("v1", new OpenApiInfo
//                {
//                    Title = "ERP System API",
//                    Version = "v1",
//                    Description = "ERP System"
//                });
//            });

//            return services;
//        }

//        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
//        {
//            // إعداد Swagger في الـ Middleware
//            app.UseSwagger();
//            app.UseSwaggerUI(c =>
//            {
//                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ERP System API V1");
//            });

//            return app;
//        }
//    }
//}