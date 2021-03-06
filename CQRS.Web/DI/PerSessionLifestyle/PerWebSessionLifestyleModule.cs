using System;
using System.Web;
using System.Web.SessionState;
using Castle.MicroKernel.Lifestyle;
using Castle.MicroKernel.Lifestyle.Scoped;

namespace CQRS.Web
{
    public class PerWebSessionLifestyleModule : IHttpModule
    {
        private const string Key = "castle.per-web-session-lifestyle-cache";

        public void Init(HttpApplication context)
        {
            var sessionState = ((SessionStateModule)context.Modules["Session"]);
            sessionState.End += SessionEnd;
        }

        private static void SessionEnd(object sender, EventArgs e)
        {
            var app = (HttpApplication)sender;

            var scope = GetScope(app.Context.Session, false);

            if (scope != null)
            {
                scope.Dispose();
            }
        }

        internal static ILifetimeScope GetScope()
        {
            var current = HttpContext.Current;

            if (current == null)
            {
                throw new InvalidOperationException("HttpContext.Current is null. PerWebSessionLifestyle can only be used in ASP.Net");
            }

            return GetScope(current.Session, true);
        }

        internal static ILifetimeScope YieldScope()
        {
            var context = HttpContext.Current;

            if (context == null)
            {
                return null;
            }

            var scope = GetScope(context.Session, true);

            if (scope != null)
            {
                context.Session.Remove(Key);
            }

            return scope;
        }

        private static ILifetimeScope GetScope(HttpSessionState session, bool createIfNotPresent)
        {
            var lifetimeScope = (ILifetimeScope)session[Key];

            if (lifetimeScope == null && createIfNotPresent)
            {
                lifetimeScope = new DefaultLifetimeScope(new ScopeCache(), null);
                session[Key] = lifetimeScope;
                return lifetimeScope;
            }

            return lifetimeScope;
        }

        public void Dispose()
        {
        }
    }
}