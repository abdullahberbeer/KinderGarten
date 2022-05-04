using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ServerApp.Data;
using ServerApp.Models;
using ServerApp.Repo;

namespace ServerApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<KinderGartenContext>(x=>x.UseSqlite("Data Source=kindergarten.db"));
            services.AddIdentity<User,Role>().AddEntityFrameworkStores<KinderGartenContext>();
            services.Configure<IdentityOptions>(options=>{
                  options.Password.RequireDigit= true; // sayısal değer
                options.Password.RequireUppercase=true;//büyük harf
                options.Password.RequireNonAlphanumeric=true;//-+/., harf
                options.Password.RequiredLength=6;//en az 6 karakter

                options.Lockout.DefaultLockoutTimeSpan =TimeSpan.FromMinutes(5);//5 dk hesap kilitlenmesi
                options.Lockout.MaxFailedAccessAttempts=5;//5denemeden sonra
                options.Lockout.AllowedForNewUsers=true;//hesabı yeni oluşturanın kitlenip kitlenmeyeceği

                // options.User.AllowedUserNameCharacters="abcd1234";//parola içinde girebileceğimiz harf ve sayılar
                options.User.RequireUniqueEmail=true; //her bir kayıt farklı email olacak

            });
            services.AddScoped<IStudentRepository,StudentRepository>();
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers().AddNewtonsoftJson(options=>{
                options.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddCors(options=>{
                options.AddPolicy(
                    name:"MyPolicy",
                    builder=>{
                        builder.WithOrigins("http://localhost:4200").
                      AllowAnyMethod().AllowAnyHeader();
                    }
                );
            });
            services.AddAuthentication(opt=>{
                opt.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt=>{
                opt.RequireHttpsMetadata=false;
                opt.SaveToken=true;
                opt.TokenValidationParameters= new TokenValidationParameters{
                    ValidateIssuerSigningKey=true,
                    IssuerSigningKey=new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Secret").Value)),
                    ValidateIssuer=false,
                    ValidateAudience=false
                };
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ServerApp", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ServerApp v1"));
            }else{
                app.UseExceptionHandler(appError=>{
                    appError.Run(async context=>{
                        context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType="application/json";
                        var exception = context.Features.Get<IExceptionHandlerFeature>();
                        if(exception!=null){
                            await context.Response.WriteAsync(new ErrorDetail(){
                                StatusCode=context.Response.StatusCode,
                                Message=exception.Error.Message
                            }.ToString());
                        }
                    });
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("MyPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
