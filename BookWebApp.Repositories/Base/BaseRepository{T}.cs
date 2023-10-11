using Dapper;
using BookWebApp.Repositories.Base.Contracts;
using BookWebApp.Repositories.Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookWebApp.Repositories.Base
{
    public class BaseRepository<T> : BaseRepository, IBaseRepository<T> where T : IDataModel<int>
    {
        public BaseRepository(IDbEngine dbEngine) : base(dbEngine)
        {
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var connection = this.DbEngine.Connection;
            var models = await connection.GetListAsync<T>(null, transaction: this.DbEngine.Transaction);
            return models;
        }

        public async Task<T> GetAsync(int id)
        {
            var connection = this.DbEngine.Connection;
            var models = await connection.GetAsync<T>(null, this.DbEngine.Transaction);
            return models;
        }

        public async Task<SaveResult> InsertAsync(T model)
        {
            if (model == null)
            {
                return new SaveResult { IsSuccessful = false, StackTrace = $"Model {typeof(T).Name} is null!" };
            }

            var connection = this.DbEngine.Connection;
            var result = new SaveResult();

            try
            {
                var id = await connection.InsertAsync<T>(model, this.DbEngine.Transaction);
                if (typeof(T).GetInterfaces().Contains(typeof(IDataModel<int>)))
                {
                    (model as IDataModel<int>).Id = id.Value;
                }

                result.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.StackTrace = ex.ToString();
                result.ErrorMessage = "Неуспешно създаване";
            }

            return result;
        }

        public async Task<SaveResult> UpdateAsync(T model)
        {
            if (model == null)
            {
                return new SaveResult { IsSuccessful = false, StackTrace = "Model {typeof(T).Name} is null!" };
            }

            var connection = this.DbEngine.Connection;
            var result = new SaveResult();

            try
            {
                await connection.UpdateAsync(model, this.DbEngine.Transaction);
                result.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.StackTrace = ex.ToString();
                result.ErrorMessage = "Неуспешно обновяване";
            }

            return result;
        }

        public async Task<SaveResult> DeleteAsync(int id)
        {
            var connection = this.DbEngine.Connection;
            var result = new SaveResult();

            try
            {
                await connection.DeleteAsync<T>(id, this.DbEngine.Transaction);
                result.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.StackTrace = ex.ToString();
                result.ErrorMessage = "Неуспешно изтриване.";
            }

            return result;
        }
    }
}
