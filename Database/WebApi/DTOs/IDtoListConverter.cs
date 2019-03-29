using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace WebApi.DTOs
{
    public interface IDtoListConverter<T,Y>
    {
        List<T> ToDtoList(List<Y> fromList);
    }
}