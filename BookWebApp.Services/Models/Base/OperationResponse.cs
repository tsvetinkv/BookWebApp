using BookWebApp.Repositories.Base.Models;
using BookWebApp.Utilities.Mapper;

namespace BookWebApp.Services.Models.Base
{
    public class OperationResponse : IMapFrom<SaveResult>
    {
        public bool IsSuccessful { get; set; }

        public string ErrorMessage { get; set; }
    }
}
