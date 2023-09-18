using WebAppClientes.Services;

namespace WebAppClientes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession();

            builder.Services.AddRazorPages();

            builder.Services.AddHttpClient<ClienteApiService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7294/");
            });

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "accountRoute",
                    pattern: "{controller=Account}/{action=Login}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "clienteRoute",
                    pattern: "{controller=Cliente}/{action=ListarClientes}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "clienteAdicionarRoute",
                    pattern: "{controller=Cliente}/{action=AdicionarCliente}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "clienteDetalhesRoute",
                    pattern: "{controller=Cliente}/{action=Detalhes}/{id?}"
                );

                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller=Home}/{action=Index}/{id?}"
                //);

                endpoints.MapRazorPages();
            });

            app.Run();
        }
    }
}