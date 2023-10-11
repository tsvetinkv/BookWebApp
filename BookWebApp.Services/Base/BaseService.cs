using AutoMapper;
using BookWebApp.Services.Base.Contracts;

namespace BookWebApp.Services.Base
{
    public abstract class BaseService
    {
        protected readonly IMapper Mapper;
        protected readonly IUserData UserData;

        public BaseService(IUserData userData)
        {
            this.UserData = userData;
        }

        public BaseService(IMapper mapper, IUserData userData) 
            : this(userData)
        {
            this.Mapper = mapper;
        }
    }
}
