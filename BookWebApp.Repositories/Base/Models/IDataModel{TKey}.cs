namespace BookWebApp.Repositories.Base.Models
{
    public interface IDataModel<TKey>
    {
        TKey Id { get; set; }
    }
}
