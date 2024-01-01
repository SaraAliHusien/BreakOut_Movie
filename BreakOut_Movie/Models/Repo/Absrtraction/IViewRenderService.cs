namespace BreakOut_Movie.Models.Repo.Absrtraction
{
    public interface IViewRenderService
    {
        Task<string> RenderToStringAsync(string viewName, object model);
    }

}
