using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class UseCaseResult
    {
        public bool IsSuccess { get; }
        public string Message { get; }
        public IEnumerable<string> Errors { get; }

        private UseCaseResult(bool success, IEnumerable<string> errors, string message = "")
        {
            IsSuccess = success;
            Errors = errors ?? Enumerable.Empty<string>();
            Message = message;
        }

        public static UseCaseResult Success(string message = "")
        {
            return new UseCaseResult(true, null, message);
        }

        public static UseCaseResult Failure(string error)
        {
            return new UseCaseResult(false, new List<string> { error });
        }

        public static UseCaseResult Failure(IEnumerable<string> errors)
        {
            return new UseCaseResult(false, errors);
        }
    }
}
