using KVH.Controllers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();  // IHttpClientFactory ekleniyor

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<HeartDiseaseController>();

// builder.Services.AddSession();//oturum ekleme
builder.Services.AddHttpContextAccessor();


//burayý proje genelinde authorization yapmak için yazdýk
builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddMvc();
builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
     .AddCookie(x =>
     {
         x.LoginPath = "/Login/SignIn";
     }
    );


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
//app.UseSession();//oturum için
app.UseRouting();

app.UseAuthorization();

/*app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
*/

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Clinic}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "giriþyap",
    pattern: "/giriþyap/{id?}",
    defaults: new { controller = "Login", action = "SignIn" });

app.MapControllerRoute(
    name: "anasayfa",
    pattern: "/anasayfa/{id?}",
    defaults: new { controller = "Home", action = "Index" });


app.MapControllerRoute(
    name: "randevu",
    pattern: "/randevular/{id?}",
    defaults: new { controller = "Randevu", action = "NewAppointment" });

app.MapControllerRoute(
    name: "kaydol",
    pattern: "/kaydol/{id?}",
    defaults: new { controller = "Login", action = "SignUp" });


app.MapControllerRoute(
    name: "yenirandevu",
    pattern: "/yenirandevu/{id?}",
    defaults: new { controller = "Randevu", action = "NewAppointment" });


app.MapControllerRoute(
    name: "aktifrandevu",
    pattern: "/aktifrandevu/{id?}",
    defaults: new { controller = "Randevu", action = "MyCurrentAppointment" });


app.MapControllerRoute(
    name: "geçmiþrandevu",
    pattern: "/geçmiþrandevu/{id?}",
    defaults: new { controller = "Randevu", action = "MyOldAppointment" });


app.MapControllerRoute(
    name: "predict",
    pattern: "/predict/{id?}",
    defaults: new { controller = "Heart", action = "Predict" });


app.MapControllerRoute(
    name: "predict",
    pattern: "/predict/{id?}",
    defaults: new { controller = "Risk", action = "Index" });

app.MapControllerRoute(
    name: "predict1",
    pattern: "/predict1/{id?}",
    defaults: new { controller = "RiskPrediction", action = "Index" });



app.Run();
