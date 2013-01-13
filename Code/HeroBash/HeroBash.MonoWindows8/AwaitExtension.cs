using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace TMGame2
{
    public static class AwaitExtension
    {
        public static TResult Await<TResult>(this IAsyncOperation<TResult> operation)
        {
            return operation.AsTask().Result;
        }
    }
}
