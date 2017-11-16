using Sitecore.Mvc.Pipelines.Response.RenderRendering;
using Sitecore.Mvc.Presentation;

namespace Sitecore.Support.Mvc.Pipelines.Response.RenderRendering
{
  public class SetCacheability : Sitecore.Mvc.Pipelines.Response.RenderRendering.SetCacheability
  {
    protected virtual string ClearOnIndexUpdateCacheKey
    {
      get
      {
        return "ClearOnIndexUpdate";
      }
    }

    protected override bool IsCacheable(Rendering rendering, RenderRenderingArgs args)
    {
      if (rendering.RenderingItem != null && rendering.RenderingItem.Caching != null)
      {
        rendering.Caching.Cacheable = rendering.RenderingItem.Caching.Cacheable;
      }
      bool flag = rendering.Caching.Cacheable && this.DoesContextAllowCaching(args);
      if (flag)
      {
        this.AddCachingSettings(rendering);
      }
      return flag;
    }

    protected virtual void AddCachingSettings(Rendering rendering)
    {
      rendering.Caching.VaryByData = rendering.RenderingItem.Caching.VaryByData;
      rendering.Caching.VaryByDevice = rendering.RenderingItem.Caching.VaryByDevice;
      rendering.Caching.VaryByLogin = rendering.RenderingItem.Caching.VaryByLogin;
      rendering.Caching.VaryByParameters = rendering.RenderingItem.Caching.VaryByParm;
      rendering.Caching.VaryByQueryString = rendering.RenderingItem.Caching.VaryByQueryString;
      rendering.Caching.VaryByUser = rendering.RenderingItem.Caching.VaryByUser;
      rendering[this.ClearOnIndexUpdateCacheKey] = (rendering.RenderingItem.Caching.ClearOnIndexUpdate ? "1" : string.Empty);
    }
  }
}