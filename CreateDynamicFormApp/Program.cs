using CreateDynamicFormApp.Repository.FormElement;
using CreateDynamicFormApp.Repository.DbAsJson;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

    // services.AddMvc()
    //         .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
builder.Services.AddScoped<IFormElementRepository,FormElementRepository>();
builder.Services.AddScoped<IDbJsonRepository,DbJsonRepository>();

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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
