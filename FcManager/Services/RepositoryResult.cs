using System.Collections;

namespace FcManager.Services
{
    public class RepositoryResult<T>
    {
        public RepositoryResult(bool succeeded, T model, string[] errors)
        {
            this.Succeeded = succeeded;
            this.Model = model;
            this.Errors = errors;
        }
        public T Model { get; private set; }
        public bool Succeeded { get; private set; }
        public string[] Errors { get; private set; }
    }
}