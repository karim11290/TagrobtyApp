using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QuizbeePlus.Startup))]
namespace QuizbeePlus
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
