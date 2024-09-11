using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Interfaces
{
    public interface IConverter<T, U>
    {
        T Convert(U model);
        U Convert(T model);
    }
}
